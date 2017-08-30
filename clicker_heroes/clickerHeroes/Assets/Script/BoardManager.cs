﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	[Serializable]
	public class Count{
		public int minimum;
		public int maximum;

		public Count (int min, int max){
			minimum = min;
			maximum = max;
		}
	}

	public int columns = 8;
	public int rows = 8;
	public Count WallCount = new Count(5,9);
	public Count FoodCount = new Count(1,5);
	public GameObject exit;
	public GameObject[] floorTiles;
	public GameObject[] wallTiles;
	public GameObject[] enemyTiles;
	public GameObject[] outerWallTiles;
	public GameObject[] foodTiles;

	// Childing objects to this object to keep things clean
	private Transform boardHolder;
	private List <Vector3> gridPositions = new List<Vector3>();

	void InitializeList(){
		gridPositions.Clear ();

		int playerAreaX = columns-1;
		int playerAreaY = rows-1;

		for(int x = 1; x < playerAreaX; x++){
			for(int y = 1; y < playerAreaY; y++){
				gridPositions.Add(new Vector3(x,y,0f));
			}
		}
	}

	void BoardSetup(){
		boardHolder = new GameObject ("Board").transform;

		for (int x = -1; x < columns + 1; x++) {
			for (int y = -1; y < rows + 1; y++) {
				GameObject floorToInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];
				if(x == -1 || y == columns || y == -1 || x == rows)
					floorToInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

				GameObject instance = Instantiate (floorToInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent (boardHolder);
			}
		}
	}

	Vector3 RandomPosition()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions [randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPosition;
	}

	void LayoutObectAtRandom(GameObject[] tileArray, int min, int max) {
		int objectCount = Random.Range(min, max+1);
		for(int i = 0; i < objectCount; i++){
			Vector3 randomPosition = RandomPosition();
			GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
			Instantiate (tileChoice, randomPosition, Quaternion.identity);
		}
	}

	public void SetupScene(int level){
		BoardSetup ();
		InitializeList ();
		LayoutObectAtRandom (wallTiles, WallCount.minimum, WallCount.maximum);
		LayoutObectAtRandom (foodTiles, FoodCount.minimum, FoodCount.maximum);
		int enemyCount = (int)Mathf.Log (level, 2f);
		LayoutObectAtRandom (enemyTiles, enemyCount, enemyCount);
		Instantiate(exit, new Vector3(columns-1, rows-1, 0f), Quaternion.identity);
	}
}