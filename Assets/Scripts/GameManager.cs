using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float levelStartDelay = 2f;
	public float turnDelay = 0.1f;
	public static GameManager instance = null; //Create singleton 
	private BoardManager boardScript;
	public int playerFoodPoints = 100;
	[HideInInspector] public bool playerTurn = true;

	private Text levelText;
	private GameObject levelImage;
	private int level = 1;
	private List<Enemy> enemies;
	private bool enemiesMoving;
	private bool doingSetup;

	// Use this for initialization
	void Awake()
	{
		//Make sure there is only one instance of this script runing
		if (instance == null) 
		{
			instance = this;
		} 
		else if (instance != this) 
		{
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
		enemies = new List<Enemy> ();
		//boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	//Part of unity API and is called after a scene is loaded
	void OnLevelWasLoaded(int index)
	{
		level++;
		InitGame ();
	}

	void InitGame()
	{ 
		doingSetup = true;
		levelImage = GameObject.Find ("LevelImage");
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
		levelText.text = "Day " + level;
		levelImage.SetActive (true);
		//wait 2 sec before turning it off
		Invoke ("HideLevelImage", levelStartDelay);


		//On restart clear enemys from last level
		enemies.Clear ();
		//boardScript.SetupScene (level);
	}

	private void HideLevelImage()
	{
		levelImage.SetActive (false);
		doingSetup = false;
	}

	public void GameOver()
	{
		levelText.text = "After " + level + " days, you starved";
		levelImage.SetActive (true);
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//player turn or enemys allready moving
		if (playerTurn || enemiesMoving || doingSetup) 
		{
			return;
		}

		StartCoroutine (MoveEnemies ());
	}

	//Enemys register themselfs
	public void AddEnemyToList(Enemy script)
	{
		enemies.Add (script);
	}

	IEnumerator MoveEnemies()
	{
		enemiesMoving = true;
		yield return new WaitForSeconds (turnDelay);
		if (enemies.Count == 0) 
		{
			yield return new WaitForSeconds (turnDelay);
		}

		for (int i = 0; i < enemies.Count; i++) 
		{
			enemies [i].MoveEnemy ();
			yield return new WaitForSeconds (enemies [i].moveTime);
		}

		playerTurn = true;
		enemiesMoving = false;
	}
}
