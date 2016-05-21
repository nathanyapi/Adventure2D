using UnityEngine;
using System.Collections;

public class Projectile_Linear_mvt : MonoBehaviour {

	public Transform myPosition;
	public float linSpeed = 5f;
	public float maxDistance= 100f;
	public float myDistance=5.0f;
	// Use this for initialization

	void Awake()
	{
		myPosition = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		myPosition.Translate (Vector3.up * Time.deltaTime * linSpeed);

		myDistance += Time.deltaTime * linSpeed;

		if (myDistance > maxDistance)
		{
			Destroy (gameObject);
		}
	}
}
