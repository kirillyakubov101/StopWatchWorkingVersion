using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyRock : MonoBehaviour
{
	public void StartMovingTheRock() //make the rock drop and start moving
	{
		GetComponent<Rigidbody2D>().simulated = true;
	}

}
