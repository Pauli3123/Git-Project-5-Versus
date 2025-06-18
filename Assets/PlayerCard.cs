using UnityEngine;
using TMPro;

public class PlayerCard : MonoBehaviour
{
	public GameObject scoreboardUI;

	void Update()
	{
		while (Input.GetKeyDown(KeyCode.Tab))
		{
			scoreboardUI.SetActive(false);
		}

		
	}

}
