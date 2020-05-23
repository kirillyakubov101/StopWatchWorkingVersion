using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeWatch : MonoBehaviour
{

	GameSession gameSession;
	Messages messages;
	
	private void Awake()
	{
		gameSession = FindObjectOfType<GameSession>();
		messages = FindObjectOfType<Messages>();
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		gameSession.SetWatch(true);
		messages.updateText("PRESS LEFT-SHIFT TO SLOW DOWN TIME");
		Destroy(gameObject);
	}
}
