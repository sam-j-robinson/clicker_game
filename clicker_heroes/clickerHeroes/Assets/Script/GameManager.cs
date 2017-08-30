using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
	private int level = 3;                                  //Current level number, expressed in game as "Day 1".

	private int enemyLevel = 3;
	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance == this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject); 
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame (){
		boardScript.SetupScene(enemyLevel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
