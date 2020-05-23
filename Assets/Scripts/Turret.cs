using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	[SerializeField] GameObject LaserPrefab;
	[SerializeField] GameObject Gun;
	[SerializeField] float startTimeBtwSpawns;
	[Header("Directions Of Shoot")]
	[SerializeField] bool isShootingLeft = false;
	[SerializeField] bool isShootingRight = false;
	[SerializeField] bool isShootingDown = false;
	[SerializeField] bool isShootingUp = false;

	Vector2 DirectionOfShoot;
	float timeBetweenSpawns;
	float RotationShot = 0f;


	private void Start()
	{
		DetermineDirectionOfShoot();
	}

	// Update is called once per frame
	void Update()
    {
		if (!TimeWizard.isTimeStopped) //if the time is stopped, stop shooting
		{
			Shoot(RotationShot);
		}
		
	}


	private void Shoot(float RotationShot) //shoot the lasers with the rotation of the direction
	{
		if (timeBetweenSpawns <= 0)
		{
			var gameobj = Instantiate(LaserPrefab, Gun.transform.position, Quaternion.identity);
			gameobj.transform.parent = gameObject.transform;
			gameobj.transform.transform.Rotate(0f, 0f, RotationShot);
			timeBetweenSpawns = startTimeBtwSpawns;
			Destroy(gameobj, 5f); // delete, make it destroy on hit
		}
		else
		{
			timeBetweenSpawns -= Time.fixedDeltaTime;
		}
	}

	private void DetermineDirectionOfShoot() //shoot in the direction of the 'true' boolean
	{

		if (isShootingLeft)
		{
			SetDirection(new Vector2(-1f, 0f));
			RotationShot = 90f;
		}

		else if (isShootingUp)
		{
			SetDirection(new Vector2(0f, 1f));
			RotationShot = 0f;
		}

		else if (isShootingRight)
		{
			SetDirection(new Vector2(1f, 0f));
			RotationShot = 90f;
		}
		else if(isShootingDown)
		{
			SetDirection(new Vector2(0f, -1f));
			RotationShot = 0f;
		}
	}

	private void SetDirection(Vector2 direction)  //SET
	{
		DirectionOfShoot = direction;
	}

	public Vector2 GetDirectionOfShoot()  //GET
	{
		return DirectionOfShoot;
	}
}
