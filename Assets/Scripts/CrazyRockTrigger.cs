using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyRockTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		FindObjectOfType<CrazyRock>().StartMovingTheRock();
		FindObjectOfType<CrazyRock>().GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		Destroy(gameObject);
	}
}
