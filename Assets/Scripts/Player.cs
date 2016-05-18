using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	public int wallDamage =1;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 20;
	public float restartLevelDelay = 1f;
	public Text foodText;

	public AudioClip moveSound1;
	public AudioClip moveSound2;
	public AudioClip eatSound1;
	public AudioClip eatSound2;
	public AudioClip drinkSound1;
	public AudioClip drinkSound2;
	public AudioClip gameOverSound;

	private int food;

	private Vector2 touchOrigine = -Vector2.one;


	// Use this for initialization
	//Different implementation in player then in moving object
	void Start () 
	{

		//GameManager stores info as change levels
		food = GameManager.instance.playerFoodPoints;
		foodText = GameObject.Find ("FoodText").GetComponent<Text> ();
		foodText.text = "Food : " + food;
		//Call the MovingObject start fonction
		//base.Start ();

	}

	//called automatically when object is disabled
	private void OnDisable()
	{
		GameManager.instance.playerFoodPoints = food;
	}
		

//	// Update is called once per frame
//	void Update () 
//	{
//		//turn based game (to be modified :P)
//		if (!GameManager.instance.playerTurn)
//		{
//			return;
//		}
//
//		int horizontal = 0;
//		int vertical = 0;
//
//		#if UNITY_STANDALONE || UNITY_WEBPLATER
//
//
//
//
//		//Keyboard or controller input
//		horizontal = (int)Input.GetAxisRaw ("Horizontal");
//		vertical = (int)Input.GetAxisRaw ("Vertical");
//
//		
//		if (horizontal != 0) {
//			vertical = 0;
//		}
//
//		#else
//		if(Input.touchCount >0)
//		{
//			Touch myTouch = Input.touches[0];
//
//			if(myTouch.phase == TouchPhase.Began)
//			{
//				touchOrigine = myTouch.position;
//			}
//			else if(myTouch.phase == TouchPhase.Ended && touchOrigine.x >= 0)
//			{
//				Vector2 touchEnd = myTouch.position;
//				float x = touchEnd.x - touchOrigine.x;
//				float y = touchEnd.y - touchOrigine.y;
//				touchOrigine.x =-1;
//
//				if(Mathf.Abs(x) > Mathf.Abs(y))
//					horizontal = x>0 ? 1 : -1;
//				else
//					vertical = y>0 ? 1: -1;
//			}
//		}
//
//		#endif
//
//		if (horizontal != 0 || vertical != 0) 
//		{
//			AttemptMove<Wall> (horizontal, vertical);
//		}
//	}

//	protected override void AttemptMove <T> ( int xDir, int yDir)
//	{
//		food--;
//		foodText.text = "Food : " + food;
//		base.AttemptMove<T> (xDir, yDir);
//
//		RaycastHit2D hit;
//		if(Move(xDir,yDir, out hit))
//		{
//			SoundManager.instance.RandomizeSfx (moveSound1, moveSound2);
//		}
//
//		CheckIfGameOver ();
//
//		GameManager.instance.playerTurn = false;
//	}
//
//	//what to do in case of collision with blocking object
//	protected override void OnCantMove <T> (T component)
//	{
//		Wall hitWall = component as Wall;
//		hitWall.DamageWall (wallDamage);
//		animator.SetTrigger ("PlayerChop");
//
//	}
//
//	//when player touches Exit
//	private void Restart()
//	{
//		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
//		//Application.LoadLevel (Application.loadedLevel);
//	}
//
//	//On ennemy Hit 
//	public void LoseFood (int loss)
//	{
//		animator.SetTrigger ("PlayerHit");
//		food -= loss;
//		foodText.text = "-" + loss + " Food : " + food;
//		CheckIfGameOver ();
//	}

	//object collision manager
	private void OnTriggerEnter2D (Collider2D other)
	{
//		if (other.tag == "Exit") 
//		{
//			Invoke ("Restart", restartLevelDelay);
//			enabled = false;
//		}
//		else if (other.tag == "Food")
//		{
//			food += pointsPerFood;
//			foodText.text = "+" + pointsPerFood + " Food : " + food;
//			SoundManager.instance.RandomizeSfx (eatSound1, eatSound2);
//			other.gameObject.SetActive(false);
//		}
//		else if (other.tag == "Soda")
//		{
//			food += pointsPerSoda;
//			foodText.text = "+" + pointsPerSoda + " Food : " + food;
//			SoundManager.instance.RandomizeSfx (drinkSound1, drinkSound2);
//			other.gameObject.SetActive(false);
//		}
	}

	private void CheckIfGameOver()
	{
		if (food <= 0) 
		{
			SoundManager.instance.RandomizeSfx (gameOverSound);
			SoundManager.instance.musicSource.Stop ();
			GameManager.instance.GameOver ();
		}
	}
}
