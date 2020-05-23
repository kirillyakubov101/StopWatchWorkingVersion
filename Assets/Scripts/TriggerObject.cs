using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		var Obstacle = collision.gameObject;
		var ObstacleRigidBoy = Obstacle.GetComponent<Rigidbody2D>();
		ObstacleRigidBoy.velocity = -ObstacleRigidBoy.velocity;
	}
}
