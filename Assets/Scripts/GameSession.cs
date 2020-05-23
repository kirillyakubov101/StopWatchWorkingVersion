using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
	[SerializeField] TMPro.TMP_Text Ammo;
	[SerializeField] int AmmoCount = 20;
	[SerializeField] Image bulletImage;
	[SerializeField] BoxCollider2D FirstCheckpoint;
	[SerializeField] BoxCollider2D SecondCheckpoint;

	[Header("Public Elements")]
	public GameObject LoseMenu;
	public GameObject MainMenu;
	public Slider VolumeSlider;

	//music player
	private AudioSource audioSource;
	private MusicPlayer musicPlayer;

	private bool hasWatch = false; //TODO MAKE STATIC

	private void Awake()
	{
		int amountOfGameSessions = FindObjectsOfType<GameSession>().Length;

		if(amountOfGameSessions > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
		Initialize();
	}

    // Update is called once per frame
    void Update()
    {
		CheckChecpoint();
		UpdateAmmoColor();

	}

	public void ReduceAmmo()
	{
		AmmoCount--;
		Ammo.text = AmmoCount.ToString();
	}

	public int GetAmmoCount()
	{
		return AmmoCount;
	}

	public void IncreaceAmmo(int amount)
	{
		AmmoCount += amount;
		Ammo.text = AmmoCount.ToString();
	}

	//it's only for the first level
	public void SetWatch(bool status)
	{
		hasWatch = status;
	}
	//it's only for the first level
	public bool GetWatchStatus()
	{
		return hasWatch;
	}

	public void ShowLoseScreen()
	{
		Time.timeScale = 0;
		LoseMenu.SetActive(true);
	}

	public void ShowMainMenuScreen()
	{
		Time.timeScale = 0;
		MainMenu.SetActive(true);
	}

	public void CancelMainMenuScreen()
	{
		PlayerPrefsController.SetMasterVolume(VolumeSlider.value);
		MainMenu.SetActive(false);
		Time.timeScale = 1;
	}


	private void Initialize()
	{
		musicPlayer = FindObjectOfType<MusicPlayer>();
		audioSource = musicPlayer.GetComponent<AudioSource>();
		audioSource.Stop();
		audioSource.PlayOneShot(musicPlayer.Clips[1]);
		Time.timeScale = 1;
		Ammo.text = AmmoCount.ToString();
		LoseMenu.SetActive(false);
		MainMenu.SetActive(false);
	}

	private void UpdateAmmoColor()
	{
		if (GetAmmoCount() <= 0)
		{
			bulletImage.color = Color.red;
		}
		else
		{
			bulletImage.color = Color.white;
		}
	}

	private void CheckChecpoint()
	{
		if (FirstCheckpoint.IsTouchingLayers(LayerMask.GetMask("Player")))
		{
			hasWatch = true;
			Player.Checkpoint = FirstCheckpoint.transform.position;
		}
		if (SecondCheckpoint.IsTouchingLayers(LayerMask.GetMask("Player")))
		{
			hasWatch = true;
			Player.Checkpoint = SecondCheckpoint.transform.position;
		}
	}
}


