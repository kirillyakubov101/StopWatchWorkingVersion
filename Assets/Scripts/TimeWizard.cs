using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWizard : MonoBehaviour
{
	[SerializeField] float AllRobotSpeed = 8f;  //AI Speed
	[SerializeField] float EnemyBulletSpeed = 10f; //Bullet speed

	public static bool isTimeStopped = false;

	ObstaclesMovement[] Obstacles;

	private void Start()
	{
		Obstacles = FindObjectsOfType<ObstaclesMovement>();
	}


	public float GetRobotSpeed()
	{
		return AllRobotSpeed;
	}

	public float GetEnemyBulletSpeed()
	{
		return EnemyBulletSpeed;
	}

	public void StopTime()
	{
		//Stop Time
		AllRobotSpeed = 0.5f;
		EnemyBulletSpeed = 0.1f;
		isTimeStopped = true;

		var allRobots = FindObjectsOfType<Robot>();
		StopTimeRobots(allRobots);
		StopTimeObstacles();
	}

	private static void StopTimeRobots(Robot[] allRobots)
	{
		foreach (var child in allRobots)
		{
			child.GetComponent<Animator>().speed = 0.2f;
		}
	}

	private void StopTimeObstacles() //slows time for all the moving obstacles
	{
		foreach (var child in Obstacles)
		{
			ObstaclesMovement.isTimeStopped = true;
			child.SaveMovementVector();
			child.SetStopVelocity();
		}
	}

	public void ContinueTime()
	{
		//Back to normal
		AllRobotSpeed = 8f;
		EnemyBulletSpeed = 10f;
		isTimeStopped = false;

		var allRobots = FindObjectsOfType<Robot>();
		ReleaseRobots(allRobots);
		ReleaseObstacles();
	}

	private static void ReleaseRobots(Robot[] allRobots)
	{
		foreach (var child in allRobots)
		{
			child.GetComponent<Animator>().speed = 1f;
		}
	} 

	private void ReleaseObstacles() //resets time for obstacles
	{
		foreach (var child in Obstacles)
		{
			ObstaclesMovement.isTimeStopped = false;
			child.ResetVelocity();
		}
	}
}
