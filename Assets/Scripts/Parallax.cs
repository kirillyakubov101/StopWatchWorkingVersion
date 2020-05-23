using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	[SerializeField] float parallaxEffectMultiplier = 0.5f;

	private Transform CameraTransform;
	private Vector3 LastCameraPosition;
	private float textureUnitSizeX;

    // Start is called before the first frame update
    void Start()
    {
		CameraTransform = Camera.main.transform;
		LastCameraPosition = CameraTransform.position;
		Sprite sprite = GetComponent<SpriteRenderer>().sprite;
		Texture2D texture = sprite.texture;
		textureUnitSizeX = texture.width / sprite.pixelsPerUnit;

	}

    // Update is called once per frame
    void LateUpdate()
    {
		Vector3 deltaMovement = CameraTransform.position - LastCameraPosition;
		transform.position += deltaMovement * parallaxEffectMultiplier;
		LastCameraPosition = CameraTransform.position; //reset position

		/*if(Mathf.Abs(CameraTransform.position.x - transform.position.x) >= textureUnitSizeX)  //Infinite level
		{
			float offsetPositionX = (CameraTransform.position.x - transform.position.x) % textureUnitSizeX;
			transform.position = new Vector3(CameraTransform.position.x + offsetPositionX, transform.position.y);
		}*/

	}
}
