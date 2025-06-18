using UnityEngine;
using TMPro;

public class PlayerCard : MonoBehaviour
{
	public GameObject scoreboardUI;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			scoreboardUI.SetActive(true);
		}

		if (Input.GetKeyUp(KeyCode.Tab))
		{
			scoreboardUI.SetActive(false);
		}
	}

}
