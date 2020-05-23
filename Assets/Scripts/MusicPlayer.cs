using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{ 
	AudioSource audioSource;
	public AudioClip[] Clips;

    // Start is called before the first frame update
    void Awake()
    {
		int amountOfObjects = FindObjectsOfType<MusicPlayer>().Length;
		if(amountOfObjects > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}

		
	}

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.PlayOneShot(Clips[0]);
	}

	public void SetMusicVolume(float volume)
	{
		audioSource.volume = volume;
	}
}
