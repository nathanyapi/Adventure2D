using UnityEngine;
using System.Collections;

public class PlayerMouvement : MonoBehaviour {


	private Animator animator;
	private Rigidbody2D rb2D;
	private Transform transform;
	public GameObject Attack_A;
	public GameObject Attack_B;
	float x;
	float y;

	public float baseSpeed = 7f;
	private float modifiedSpeed;

	// Use this for initialization
	void Start () 
	{
		transform = GetComponent<Transform> ();
		animator = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//mouvement related
		modifiedSpeed = baseSpeed + (baseSpeed / 2); //add agility modificator

		#if UNITY_STANDALONE || UNITY_WEBPLATER
		float input_x = Input.GetAxisRaw ("Horizontal");
		float input_y = Input.GetAxisRaw ("Vertical");
		#else
		#endif

		bool isWalking = (Mathf.Abs (input_x) + Mathf.Abs (input_y)) > 0 ;

		animator.SetBool ("isWalking", isWalking);

		if (isWalking) 
		{
			x = input_x;
			y = input_y;
			animator.SetFloat ("x", x);
			animator.SetFloat ("y", y);
		}

		// Move senteces
		rb2D.velocity = new Vector2(Mathf.Lerp(0, input_x * modifiedSpeed, 0.8f),
			Mathf.Lerp(0, input_y * modifiedSpeed, 0.8f));

		if (Input.GetButtonDown ("Fire1")) 
		{
			float angle = Mathf.Atan2 (y, x)* Mathf.Rad2Deg - 90;
			Quaternion rotation = Quaternion.AngleAxis (angle, Vector3.forward);

			Instantiate (Attack_A, transform.position , rotation);
		}
	}
		
}
