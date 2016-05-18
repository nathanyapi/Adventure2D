using UnityEngine;
using System.Collections;

public class PlayerMouvement : MonoBehaviour {


	private Animator animator;
	private Rigidbody2D rb2D;

	public float baseSpeed = 7f;
	private float modifiedSpeed;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();

		//mouvement related
		modifiedSpeed = baseSpeed + (baseSpeed / 2);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		#if UNITY_STANDALONE || UNITY_WEBPLATER
		float input_x = Input.GetAxisRaw ("Horizontal");
		float input_y = Input.GetAxisRaw ("Vertical");
		#else
		#endif

		bool isWalking = (Mathf.Abs (input_x) + Mathf.Abs (input_y)) > 0 ;

		animator.SetBool ("isWalking", isWalking);

		if (isWalking) 
		{
			animator.SetFloat ("x", input_x);
			animator.SetFloat ("y", input_y);
		}

		// Move senteces
		rb2D.velocity = new Vector2(Mathf.Lerp(0, input_x * modifiedSpeed, 0.8f),
			Mathf.Lerp(0, input_y * modifiedSpeed, 0.8f));
	}
		
}
