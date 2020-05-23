using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
	Slider slider;
	Player player;
	[SerializeField] float chargeRateUp = 0.02f;
	[SerializeField] float chargeRateDown = 0.08f;
	bool isPaused = false;

	// Start is called before the first frame update
	void Start()
	{
		player = FindObjectOfType<Player>();
		slider = GetComponent<Slider>();

	}

	// Update is called once per frame
	void Update()
	{
		RechargeTime();
	}

	void RechargeTime() //Gain energy in the slider
	{
		if (!isPaused)
		{
			slider.value += chargeRateUp;
		}
		else
		{
			if (OutOfEnergy())
			{
				player.HandleOutOfTimeEnergy();
				FindObjectOfType<TimeWizard>().ContinueTime();
			}
			slider.value -= chargeRateDown;
		}

	}

	public bool IsPaused(bool n) //When Player hits "SHIFT"
	{
		isPaused = n;
		return isPaused;
	}

	public bool OutOfEnergy() //When the Slider is 0
	{
		if(slider.value <= 0)
		{
			return true;
			
		}
		else
		{
			return false;
		}
	}

	public bool FullEnergy() //when the slider is FULL
	{
		if (slider.value == 1)
		{
			return true;

		}
		else
		{
			return false;
		}
	}

}
