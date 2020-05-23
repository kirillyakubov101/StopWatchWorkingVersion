using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
	[SerializeField] Health PlayerHealth;

	//Cached
	Slider slider;

    // Start is called before the first frame update
    void Start()
    {
		slider = GetComponent<Slider>();
	}

	public void UpdateHealthBar(float amount)
	{
		slider.value -= amount;
	}
}
