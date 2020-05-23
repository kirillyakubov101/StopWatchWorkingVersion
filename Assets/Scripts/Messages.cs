using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
	[SerializeField] TMPro.TMP_Text Message;

	// Start is called before the first frame update
	void Start()
    {
		Message.text = "IF ONLY IT MOVED SLOWER";
	}

	public void updateText(string text)
	{
		Message.text = text;
	}
}
