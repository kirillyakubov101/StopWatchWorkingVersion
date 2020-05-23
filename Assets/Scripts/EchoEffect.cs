using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
	[SerializeField] GameObject EchoPrefab;
	[SerializeField] float timeBetweenSpawns;
	[SerializeField] float startTimeBtwSpawns;

	public void EchoMovement()
	{
		if (GetComponent<Rigidbody2D>().velocity.x != 0)//if speed is not 0
		{  
			

			if (timeBetweenSpawns <= 0)
			{
				var gameobj = Instantiate(EchoPrefab, transform.position, Quaternion.identity);
				bool PlayerHasHorizontalSpeed = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > Mathf.Epsilon;
				gameobj.transform.localScale = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x), 1f);

				timeBetweenSpawns = startTimeBtwSpawns;
				Destroy(gameobj, 0.1f);
			}
			else
			{
				timeBetweenSpawns -= Time.deltaTime;
			}
		}

		

	}
}
