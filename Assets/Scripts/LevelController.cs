using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	public void LoadStartGame()
	{
		Player.Checkpoint = new Vector2(-29.45f, -11.14f); //load the start of the game with the 1st origin position
		SceneManager.LoadScene(2);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void LoadMainMenu()
	{
		var gamesession = FindObjectOfType<GameSession>().gameObject;   //go back to main menu and reset game session
		Destroy(gamesession);
		Time.timeScale = 1f;
		SceneManager.LoadScene(1);
	}

	public void ResetGame()
	{
		var gamesession = FindObjectOfType<GameSession>().gameObject; //reset game session
		Destroy(gamesession);
		SceneManager.LoadScene(2);

	}

	public void LoadOptions()
	{
		SceneManager.LoadScene("Options");
	}

	public void LoadEndGame()
	{
		SceneManager.LoadScene("EndGame");
	}

	public void LoadCredits()
	{
		SceneManager.LoadScene("Credits");
	}

	public void LoadMainMenuWithoutGameSession()
	{
		SceneManager.LoadScene("Main Menu");
	}
	
}

