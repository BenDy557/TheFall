using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool jumpLeft = false;
	[HideInInspector] public bool jumpRight = false;
	public string name;
	public float moveForce = 100;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	public Transform leftGrabCheck;
	public Transform rightGrabCheck;
	public float shuntForce = 2000.0f;

	public float height;
	
	public bool grounded = false;
	public bool grabbingLeft = false;
	public bool grabbingRight = false;
	//private Animator anim;
	private Rigidbody rigidBody;
	private bool m_DoubleJump = false;
	private bool m_DoubleJumpAvailable = true;
	// Use this for initialization
	void Awake () 
	{
		//anim = GetComponent<Animator>();
		rigidBody = GetComponent<Rigidbody>();
		height = GetComponent<CapsuleCollider> ().height *transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		grounded = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		Vector3 grabPos = leftGrabCheck.position;
		grabPos.y += (height / 2)- 0.1f;
		grabbingLeft = Physics.Linecast (transform.position, grabPos, 1 << LayerMask.NameToLayer ("Ground"));

		if (!grabbingLeft) {
			grabPos = leftGrabCheck.position;
			grabPos.y -= (height / 2) - 0.1f;
			grabbingLeft = Physics.Linecast (transform.position, grabPos, 1 << LayerMask.NameToLayer ("Ground"));
		}

		grabPos = rightGrabCheck.position;
		grabPos.y += (height / 2)- 0.1f;
		grabbingRight = Physics.Linecast (transform.position, grabPos, 1 << LayerMask.NameToLayer ("Ground"));

		if (!grabbingRight) {
			grabPos = rightGrabCheck.position;
			grabPos.y -= (height / 2)- 0.1f;
			grabbingRight = Physics.Linecast (transform.position, grabPos, 1 << LayerMask.NameToLayer ("Ground"));
			//Debug.Log(grabPos.y);
		}
		//stop players getting stuck at "steps"
		if((grounded&&grabbingLeft || grounded&&grabbingRight)) {
			grounded = true; grabbingLeft = false; grabbingRight = false;
		}


		if (Input.GetButtonDown (name+"Jump") && grounded) {
			jump = true;
		}
		else if (Input.GetButtonDown (name+"Jump") && grabbingLeft) {
			jumpRight= true;
		}
		else if (Input.GetButtonDown (name+"Jump") && grabbingRight) {
			jumpLeft = true;
		}
		else if (Input.GetButtonDown (name+"Jump") && m_DoubleJumpAvailable)
		{
			m_DoubleJump = true;
			m_DoubleJumpAvailable = false;
		}

		if ((grounded || grabbingLeft || grabbingRight) && !m_DoubleJumpAvailable) {
			m_DoubleJumpAvailable = true;
		}

	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxis(name+"LeftStickX");
		
		//anim.SetFloat("Speed", Mathf.Abs(h));
		if (!grabbingLeft && !grabbingRight) {
			if (h * rigidBody.velocity.x < maxSpeed)
				rigidBody.AddForce (Vector2.right * h * moveForce);
		
			if (Mathf.Abs (rigidBody.velocity.x) > maxSpeed)
				rigidBody.velocity = new Vector2 (Mathf.Sign (rigidBody.velocity.x) * maxSpeed, rigidBody.velocity.y);
		}

		/*if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();*/
		
		if (jump) {
			//anim.SetTrigger("Jump");
			rigidBody.AddForce (new Vector2 (0f, jumpForce));
			jump = false;
		} else if (jumpLeft) {
			//anim.SetTrigger("Jump");
			rigidBody.AddForce (new Vector2 (-200f, jumpForce));
			gameObject.GetComponent<Player> ().LowerDrag ();
			jumpLeft = false;
		} else if (jumpRight) {
			//anim.SetTrigger("Jump");
			rigidBody.AddForce (new Vector2 (200f, jumpForce));
			gameObject.GetComponent<Player> ().LowerDrag ();
			jumpRight = false;
		} else if (m_DoubleJump) {
			rigidBody.velocity = new Vector3(rigidBody.velocity.x,0,0);
			rigidBody.AddForce (new Vector2 (0f, jumpForce));
			m_DoubleJump = false;
		}
	}
	
	/*
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}*/

	 public void Shunt(float dir)
	{
		rigidBody.AddForce(new Vector2(Mathf.Sign(-dir)*shuntForce,shuntForce));
	}
}

