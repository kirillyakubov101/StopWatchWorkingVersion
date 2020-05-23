using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
	[SerializeField] Slider volumeSlider;

	//called once on game start
	private void Start()
	{
		volumeSlider.value = PlayerPrefsController.GetMasterVolume();
	}

	// Update is called once per frame
	void Update()
    {
		MusicTuning();
    }

	private void MusicTuning()
	{
		var MusicPlayerController = FindObjectOfType<MusicPlayer>();

		if (!MusicPlayerController)
		{
			Debug.Log("no music player found");
		}
		else
		{
			MusicPlayerController.SetMusicVolume(volumeSlider.value);
		}
	}

	public void SaveAndQuit()
	{
		PlayerPrefsController.SetMasterVolume(volumeSlider.value);
		SceneManager.LoadScene("Main Menu");
	}
}
