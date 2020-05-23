using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
	[SerializeField] int Ammo = 10;

	GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
		gameSession = FindObjectOfType<GameSession>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//when we collect the box
		gameSession.IncreaceAmmo(Ammo);
		Destroy(gameObject);
	}
}
