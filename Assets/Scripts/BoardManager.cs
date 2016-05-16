using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour 
{
	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;

		public Count (int min, int max)
		{
			maximum = max;
			minimum = min;
		}
	}

	public int collums = 8;
	public int rows = 8;
	public Count wallCount = new Count (5, 9);
	public Count foodCount = new Count (1, 5);
	public GameObject exit;
	public GameObject[] floorTiles;
	public GameObject[] foodTiles;
	public GameObject[] enemyTiles;
	public GameObject[] wallTiles;
	public GameObject[] outerWallTiles;

	private Transform boardHolder;
	private List <Vector3> gridPositions = new List<Vector3>();

	void InitialiseList() //Creates list of all possible positions (for random object generation)
	{
		gridPositions.Clear ();

		for(int x=1; x<collums-1 ;x++)
		{
			for (int y = 1; y < rows-1; y++) 
			{
				gridPositions.Add (new Vector3 (x, y, 0f));
			}
		}
	}

	void BoardSetup() //Places outer walls and floors tiles on boards
	{
		boardHolder = new GameObject ("Board").transform;

		for (int x = -1; x < collums + 1; x++) 
		{
			for (int y = -1; y < rows + 1; y++) 
			{
				GameObject toInstantiate = floorTiles [Random.Range (0, floorTiles.Length)];
				if (x == -1 || x == collums || y == -1 || y == rows) 
				{
					toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
				}

				GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent (boardHolder);

			}
		}
	}

	Vector3 RandomPosition () //return a random position from the available positions list
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions [randomIndex];
		gridPositions.RemoveAt (randomIndex); //remove position from list to avoid using it again
		return randomPosition;
	}

	void LayoutObjectAtRandom(GameObject[] tileArray, int min, int max) //Places object of the array randomly on board
	{
		int objectCount = Random.Range (min, max + 1); //decides randomly how many object will be generated
		for (int i = 0; i < objectCount; i++) 
		{
			Vector3 randomPosition = RandomPosition (); //get random available position
			GameObject tileChoice = tileArray [Random.Range (0, tileArray.Length)];  //pick an object from list of objects
			GameObject instance = Instantiate (tileChoice, randomPosition, Quaternion.identity) as GameObject; //instantiate that random object at a random position
			instance.transform.SetParent (boardHolder);
			
		}
	}

	public void SetupScene (int level)
	{
		//Create board floor and bounderys
		BoardSetup (); 
		InitialiseList ();
		//Create random objects on board
		LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum);
		LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);
		int enemyCount = (int)Mathf.Log (level, 2f);
		LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
		//Place exit (allways same place)
		Instantiate (exit, new Vector3 (collums - 1, rows - 1, 0f), Quaternion.identity);
	}

}
