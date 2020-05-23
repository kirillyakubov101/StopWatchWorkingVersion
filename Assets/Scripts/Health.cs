using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] float maxHealth = 100f;
	[SerializeField] float currentHealth =100f;

    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;

	}

	public float GetHealth()
	{
		return currentHealth;
	}

	public void DamageHealth(float amountOfDamage) //just for player
	{
		currentHealth -= amountOfDamage;
		FindObjectOfType<HealthSlider>().UpdateHealthBar(amountOfDamage);
	}

	public float GetMaxHealth()
	{
		return maxHealth; 
	}

	public void DamageEnemyHealth(float amountOfDamage) //for enemy
	{
		currentHealth -= amountOfDamage; ;
	}
}
