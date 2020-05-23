using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
	

	private void RemoveFade()
	{
		if (SceneManager.GetActiveScene().buildIndex != 0)
		{
			Destroy(gameObject);
		}
	
	}

	private void LoadNextScene()
	{
		SceneManager.LoadScene(1);
	}
}
