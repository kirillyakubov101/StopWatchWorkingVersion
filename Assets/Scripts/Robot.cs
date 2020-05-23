using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{

	//Cached
	Rigidbody2D myRigidBody2D;
	TimeWizard timeWizard;
	float movementSpeed;
	float moveDirection = -1f;
	Animator animator;
	Health health;
	CapsuleCollider2D body;
	BoxCollider2D feet;

	//state
	bool isAlive = true;

	// Start is called before the first frame update
	void Start()
    {
		feet = GetComponent<BoxCollider2D>();
		body = GetComponent<CapsuleCollider2D>();
		health = GetComponent<Health>();
		myRigidBody2D = GetComponent<Rigidbody2D>();
		timeWizard = FindObjectOfType<TimeWizard>();
		movementSpeed = timeWizard.GetRobotSpeed();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (!isAlive) { return; }
		Move();
		OnHit();


	}

	private void Move()
	{
		movementSpeed = timeWizard.GetRobotSpeed();
		myRigidBody2D.velocity = new Vector2(movementSpeed* moveDirection, 0f);
	}

	private void OnTriggerExit2D(Collider2D other) 
	{
		//My Method
		moveDirection = -moveDirection;
		transform.localScale = new Vector2(-transform.localScale.x, 1f);
	}

	private void OnHit()
	{
			if(health.GetHealth() <= 0f)
			{
				die();
			}
	}

	private void die()
	{
		Destroy(feet);
		Destroy(body);
		Destroy(myRigidBody2D);
		isAlive = false;
		animator.SetTrigger("Hit2");
		Destroy(gameObject, 0.7f);
	}

}
