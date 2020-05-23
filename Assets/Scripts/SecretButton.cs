using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretButton : MonoBehaviour
{
	[SerializeField] GameObject SecretRock;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Destroy(SecretRock);
	}
}
