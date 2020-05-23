using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
	[SerializeField] float waitTime = 5f;
	Animator animator;

	// Start is called before the first frame update
	void Start()
    {
		animator = FindObjectOfType<Fade>().GetComponent<Animator>();
		StartCoroutine(StartGame());
	}

   IEnumerator StartGame()
	{
		yield return new WaitForSeconds(waitTime);
		animator.SetTrigger("FadeOut");
	}
}
