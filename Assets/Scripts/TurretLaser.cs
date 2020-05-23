using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : MonoBehaviour
{
	[SerializeField] float attackDamage = 20f; //Damage of the laser

	//cached
	Vector2 bulletSpeed;
	TimeWizard timeWizard;
	Turret turret;
	Rigidbody2D myRigidBody2D;

	private void Start()
	{
		myRigidBody2D = GetComponent<Rigidbody2D>();
		timeWizard = FindObjectOfType<TimeWizard>();
		turret = GetComponentInParent<Turret>();
		bulletSpeed =timeWizard.GetEnemyBulletSpeed() * turret.GetDirectionOfShoot();   //the speed of the laser = the main Time script * direction
	}

	private void Update()
	{
		bulletSpeed = timeWizard.GetEnemyBulletSpeed() * turret.GetDirectionOfShoot(); //keep it updated to keep track of the new input during time stop
		myRigidBody2D.velocity = bulletSpeed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var HitInfo = collision.gameObject; 
		if (HitInfo)
		{		
			if (HitInfo.GetComponent<Player>()) //if the laser hit the player
			{
				HitInfo.GetComponentInChildren<SpriteRenderer>().color = Color.red;
				HitInfo.GetComponent<Health>().DamageHealth(attackDamage);
				GetComponent<SpriteRenderer>().color = Color.clear;
				Destroy(gameObject,0.5f);
			}
			else
			{
				Destroy(gameObject); //if the laser hit anything else
			}
		}

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		var HitInfo = collision.gameObject; //Player
		if (HitInfo)
		{
			if (HitInfo.GetComponent<Player>())
			{
				HitInfo.GetComponentInChildren<SpriteRenderer>().color = Color.white;
			}
		}
	}
}
