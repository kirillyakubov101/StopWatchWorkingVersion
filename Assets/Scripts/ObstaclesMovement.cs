using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesMovement : MonoBehaviour
{
	[SerializeField] Vector2 MovementVector = new Vector2(0f, 5f);
	Rigidbody2D rigidbody2;
	Vector2 LastMovementVector;

	public static bool isTimeStopped = false;

    // Start is called before the first frame update
    void Start()
    {
		rigidbody2 = GetComponent<Rigidbody2D>();
		rigidbody2.velocity = MovementVector;
	}

	public void SetStopVelocity()
	{
		if (rigidbody2.velocity != Vector2.zero && isTimeStopped)
		{
			rigidbody2.velocity = GetSavedMovementVector().normalized;
		}

	}

	public void ResetVelocity()
	{
		rigidbody2.velocity = GetSavedMovementVector();
	}

	public void SaveMovementVector()
	{
		LastMovementVector = rigidbody2.velocity;
	}

	private Vector2 GetSavedMovementVector()
	{
		return LastMovementVector;
	}
}
