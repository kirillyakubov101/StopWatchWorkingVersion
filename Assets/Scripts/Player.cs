using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	//Configs
	[Header("Movement")]
	[SerializeField] float moveSpeed = 5f;
	[SerializeField] float jumpSpeed = 5f;
	[Header("Gun")]
	[SerializeField] float LaserVelocity = 20f;
	[SerializeField] GameObject Gun;
	[SerializeField] GameObject Laser;
	[Header("Sounds")]
	[SerializeField] AudioClip ShootSound;


	//State
	bool isAlive = true;
	public static Vector2 Checkpoint = new Vector2(-29.45f, -11.14f); //This is the starting Coordsssss

	//Cached Components References
	GameSession gameSession;
	TimeSlider timeSliderSript;
	Health health;
	Rigidbody2D rigidbody2d;
	Animator animator;
	CapsuleCollider2D Feet;
	BoxCollider2D body;
	TimeWizard timeWizard;
	bool isEchoEnabled;



	

	// Start is called before the first frame update
	void Start()
    {
		transform.position = Checkpoint;                    //we need the origin location (will change on different checkpoints
		gameSession = FindObjectOfType<GameSession>();      //Game Session
		timeSliderSript = FindObjectOfType<TimeSlider>();   //Time slider
		rigidbody2d = GetComponent<Rigidbody2D>();          //RigidBody
		animator = GetComponent<Animator>();				//Animator
		Feet = GetComponent<CapsuleCollider2D>();			//bottom collision part
		body = GetComponent<BoxCollider2D>();				//top collision part		
		timeWizard = FindObjectOfType<TimeWizard>();		//the main TIME Script
		health = GetComponent<Health>();					//the Health
	}

	// Update is called once per frame
	void Update()
    {
		if (!isAlive) { return; }
			CharacterLandingAnimation();
			Run();
			FlipSprite();
			Shoot();
			Jump();
			PressTheStopWatch();
			Die();
	}

	private void Run()
	{
		float ControlThrow = Input.GetAxis("Horizontal") * moveSpeed;
		Vector2 PlayerVelocity = new Vector2(ControlThrow, rigidbody2d.velocity.y);
		rigidbody2d.velocity = PlayerVelocity;

		if (isEchoEnabled)
		{
			GetComponent<EchoEffect>().EchoMovement();
		}

		bool PlayerHasHorizontalSpeed = Mathf.Abs(rigidbody2d.velocity.x) > Mathf.Epsilon;

		if (!Feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			animator.SetBool("Running", false); //avoid running in the air
			return;
		}
		animator.SetBool("Running", PlayerHasHorizontalSpeed);
	}

	private void FlipSprite() //flip on different directions
	{
		bool PlayerHasHorizontalSpeed = Mathf.Abs(rigidbody2d.velocity.x) > Mathf.Epsilon;
		if (PlayerHasHorizontalSpeed)
		{
			transform.localScale = new Vector2(Mathf.Sign(rigidbody2d.velocity.x), 1f);
		}
	}

	private void Jump()
	{
		if (!Feet.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;} //if it's in the air, avoid jumping again
		
		if (Input.GetButtonDown("Jump"))
		{
			animator.SetBool("Landed", false);
			Vector2 JumpVelocity = new Vector2(0f, jumpSpeed);
			rigidbody2d.velocity += JumpVelocity;
			animator.SetTrigger("Jumping");
		}
	}

	private void Shoot() //triggers the animation of "player shoot"
	{
		if (Input.GetKeyDown(KeyCode.F) && gameSession.GetAmmoCount() > 0)
		{
			moveSpeed = 0f;
			
			AudioSource.PlayClipAtPoint(ShootSound,transform.position);
			animator.SetBool("Shooting", true);
		}
		if (Input.GetKeyUp(KeyCode.F))
		{
			moveSpeed = 5f;
			animator.SetBool("Shooting", false);
		}
	}
	
	private void ShootLaser() //function to shoot is called inside the animation of the "player shoot"
	{
		if(gameSession.GetAmmoCount() > 0)
		{
			gameSession.ReduceAmmo();
			float DirectionOfThePlayer = Mathf.Sign(transform.localScale.x);
			var LaserGameOject = Instantiate(Laser, Gun.transform.position, Quaternion.identity);
			//LaserGameOject.transform.Rotate(0f, 0f, 90f);
			LaserGameOject.GetComponent<Rigidbody2D>().velocity = new Vector2(LaserVelocity * DirectionOfThePlayer, 0f);
		}
		else
		{
			animator.SetBool("Shooting", false);
		}
		
	}

	void PressTheStopWatch() //Press the SHIFT to "stop time"
	{
		if (!gameSession.GetWatchStatus()) { return; }

		if (Input.GetKeyDown(KeyCode.LeftShift) && timeSliderSript.FullEnergy())
		{
			timeSliderSript.IsPaused(true); //trigger the slider
			isEchoEnabled = true; //start echo animation
			timeWizard.StopTime();  //Stop time function
			moveSpeed = 15f; //create new move speed
		}

		if (timeSliderSript.OutOfEnergy()) //when energy is 0
		{
			HandleOutOfTimeEnergy();
		}
	}

	void Die() //Death conditions
	{
		if (Feet.IsTouchingLayers(LayerMask.GetMask("Hazard")) || body.IsTouchingLayers(LayerMask.GetMask("Hazard")) || body.IsTouchingLayers(LayerMask.GetMask("Enemy")))
		{
			StartCoroutine(ProcessDeath());
		}

		if (health.GetHealth() <= 0)
		{
			StartCoroutine(ProcessDeath());
		}
		
	}

	void CharacterLandingAnimation() //make sure the jump animation is stopped upon landing (still needs some tweaks)
	{
		if (Feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			animator.SetBool("Landed", true);
		}
		else if (!Feet.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			animator.SetBool("Landed", false);
		}
			
	}

	IEnumerator ProcessDeath() //Upon Death
	{
		timeWizard.ContinueTime();  
		Destroy(rigidbody2d);
		isAlive = false;
		animator.SetBool("Shooting", false);
		animator.SetTrigger("IsDead");
		var AllColliders = GetComponents<Collider2D>();
		foreach (var collider in AllColliders)
		{
			collider.enabled = false;
		}

		yield return new WaitForSeconds(1.5f);
		gameSession.ShowLoseScreen();


	}

	public void HandleOutOfTimeEnergy() //when energy reaches 0
	{
		
		timeSliderSript.IsPaused(false);  
		isEchoEnabled = false;  //Cancel echo animation
		timeWizard.ContinueTime();
		moveSpeed = 5f;
	}
}

