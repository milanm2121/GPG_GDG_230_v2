using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_deck_int_id : MonoBehaviour
{
	public List<int> deck = new List<int>();

	// Start is called before the first frame update
	void Start()
	{
		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	public void loadDeck()
	{
		if (SceneManager.GetActiveScene().name == "PVPTableScene")
		{
			
		}
	}
}
