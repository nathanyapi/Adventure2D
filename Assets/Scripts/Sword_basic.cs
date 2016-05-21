using UnityEngine;
using System.Collections;

public class Sword_basic : MonoBehaviour 
{
	//public Transform myTarget;
	public Transform myPosition;
	//public int myDmgAmount;
	public float rotSpeed=180f;

	void Awake()
	{
		myPosition = GetComponent<Transform> ();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		myPosition.Rotate (0,0, rotSpeed * Time.deltaTime);
		//myPosition.LookAt (myTarget);
	}

//	void OnTriggerEnter(Collider other)
//	{
//		if (other.gameObject.tag == "Enemy"|| other.gameObject.tag == "AirEnemy") 
//		{
//			//Deal damage
//			if(touchCount<1)
//			{
//				touchCount++;
//				other.gameObject.GetComponent<Unit> ().TakeDmg (myDmgAmount);
//				//explode or return to pool
//				Destroy (this.gameObject);
//			}
//		}
//
//	}
}

