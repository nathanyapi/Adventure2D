using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour {

	public float moveTime = 0.1f;
	public LayerMask blockingLayer;

	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2D;
	private float inverseMoveTime;
	// Use this for initialization
	protected virtual void Start () 
	{
		boxCollider = GetComponent<BoxCollider2D> ();
		rb2D = GetComponent<Rigidbody2D> ();
		inverseMoveTime = 1f / moveTime;
	}

	protected bool Move( int xDir, int yDir, out RaycastHit2D hit)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		//Disable box collider so you cant hit yourself
		boxCollider.enabled = false;
		hit = Physics2D.Linecast (start, end, blockingLayer);
		//Reeabled after rayCast
		boxCollider.enabled = true;

		if (hit.transform == null) 
		{
			StartCoroutine (SmoothMovement (end));
			return true;
		}

		return false;
	}

	//Coroutine founction to move the objects smoothly
	protected IEnumerator SmoothMovement (Vector3 end)
	{
		float sqrRamainingDistance = (transform.position - end).sqrMagnitude;
		while (sqrRamainingDistance > float.Epsilon) 
		{
			Vector3 newPosition = Vector3.MoveTowards (rb2D.position, end, inverseMoveTime * Time.deltaTime);
			rb2D.MovePosition (newPosition);
			sqrRamainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}
		
	//Test to see if the future position is accessible depending on the component touched
	protected virtual void AttemptMove<T> (int xDir,int yDir)
		where T : Component//component use to specifie what unit will interact with if blocked
	{
		RaycastHit2D hit;
		bool canMove = Move(xDir, yDir, out hit);

		if(hit.transform == null)
			return;

		T hitComponent = hit.transform.GetComponent<T>();

		if(!canMove && hitComponent != null)
			OnCantMove(hitComponent);
			
	}

	//this founction will be rewritten by object who inherent (abstract)
	protected abstract void OnCantMove <T> (T Component)
		where T : Component;
}
