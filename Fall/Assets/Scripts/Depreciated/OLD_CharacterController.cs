using UnityEngine;
using System.Collections;

/*
public enum PlayerState {Idle,Running,Jumping,WallGrabLeft,WallGrabRight};

public class CharacterController : MonoBehaviour {

	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool jumpLeft = false;
	[HideInInspector] public bool jumpRight = false;
	public PlayerState m_PlayerState;
    
	public string name;
	public float moveForce = 100;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	public Transform leftGrabCheck;
	public Transform rightGrabCheck;
	public float shuntForce = 2000.0f;
	public bool CanDoubleJump = false;
	public float height;
	
	public bool grounded = false;
	public bool grabbingLeft = false;
	public bool grabbingRight = false;

    private bool groundedPrev = false;
    private bool grabbingLeftPrev = false;
    private bool grabbingRightPrev = false;

    [HideInInspector] public AudioSource m_AudioSource;
    public AudioClip m_AudioClipLand;
    public AudioClip m_AudioClipShoot;
    public AudioClip m_AudioClipDeath;
    public AudioClip m_AudioClipJump;


	public Animator anim;
	public GameObject Model;
	private float maxSpeed = 100f;
    private float maxControlSpeed = 5.0f;

	private Rigidbody rigidBody;
	private bool m_DoubleJump = false;
	private bool m_DoubleJumpAvailable = true;
	// Use this for initialization
	void Awake () 
	{
		anim = Model.GetComponent<Animator>();
        m_AudioSource = gameObject.AddComponent<AudioSource>();
        
		rigidBody = GetComponent<Rigidbody>();
		height = GetComponent<CapsuleCollider> ().height *transform.localScale.y;
		m_PlayerState = PlayerState.Idle;
	}
	
	// Update is called once per frame
	void Update () 
	{
        groundedPrev = grounded;
        grabbingLeftPrev = grabbingLeft;
		grabbingRightPrev = grabbingRight;
        
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

		//set jump values
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

		//reset double jump
		if ((grounded || grabbingLeft || grabbingRight) && !m_DoubleJumpAvailable) {
			m_DoubleJumpAvailable = true;
		}
	
		if (jump || jumpLeft || jumpRight) {
			TransitionState(PlayerState.Jumping);
		}
		if (grabbingLeft) {
			TransitionState (PlayerState.WallGrabLeft);
		} else if (grabbingRight) {
			TransitionState (PlayerState.WallGrabRight);
		}



        //SOUND////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////SOUND//
        if (grounded != groundedPrev)
        {
            if (grounded == true)
            {
                m_AudioSource.PlayOneShot(m_AudioClipLand);
            }
        }

        if (grabbingLeft != grabbingLeftPrev)
        {
            if (grabbingLeft == true)
            {
                m_AudioSource.PlayOneShot(m_AudioClipLand);
            }
        }

        if (grabbingRight != grabbingRightPrev)
        {
            if (grabbingRight == true)
            {
                m_AudioSource.PlayOneShot(m_AudioClipLand);
            }
        }
	
	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxis(name+"LeftStickX");
		
		//anim.SetFloat("Speed", Mathf.Abs(h));
		if (!grabbingLeft && !grabbingRight) 
        {
			if (h * rigidBody.velocity.x < maxControlSpeed)
				rigidBody.AddForce (Vector2.right * h * moveForce);
		
			if (Mathf.Abs (rigidBody.velocity.x) > maxSpeed)
				rigidBody.velocity = new Vector2 (Mathf.Sign (rigidBody.velocity.x) * maxSpeed, rigidBody.velocity.y);

			if(h!=0)
			{
				TransitionState(PlayerState.Running);
			}
			else{
				TransitionState(PlayerState.Idle);
			}
		}

		if (h > 0 && !facingRight) {
			Flip ();
		} else if (h < 0 && facingRight) {
			Flip ();
		}


        //SOUND/////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////SOUND//
        if (jump || jumpLeft || jumpRight || (m_DoubleJump && CanDoubleJump))
        {
            m_AudioSource.PlayOneShot(m_AudioClipJump);
            //Debug.Log("jumpSound");
        }


		if (jump) {
			//anim.SetTrigger("Jump");
			rigidBody.AddForce (new Vector2 (0f, jumpForce));
			jump = false;
		} else if (jumpLeft) {
			//anim.SetTrigger("Jump");
			rigidBody.AddForce (new Vector2 (-200f, jumpForce));
			gameObject.GetComponent<Player> ().lowerDrag ();
			jumpLeft = false;
		} else if (jumpRight) {
			//anim.SetTrigger("Jump");
			rigidBody.AddForce (new Vector2 (200f, jumpForce));
			gameObject.GetComponent<Player> ().lowerDrag ();
			jumpRight = false;
		} else if (m_DoubleJump && CanDoubleJump) {
			rigidBody.velocity = new Vector3(rigidBody.velocity.x,0,0);
			rigidBody.AddForce (new Vector2 (0f, jumpForce));
			m_DoubleJump = false;
		}

        
	}


	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = Model.transform.localScale;
		theScale.z *= -1;
		Model.transform.localScale = theScale;
	}

	void TransitionState(PlayerState state)
	{
		m_PlayerState = state;
		switch (state) {
		case PlayerState.Idle:
			anim.SetBool("Idle",true);
			anim.SetBool("Running",false);
			anim.SetBool("Sliding",false);
			break;
		case PlayerState.Jumping:
			anim.SetTrigger("Jump");
			anim.SetBool("Idle",false);
			anim.SetBool("Running",false);
			anim.SetBool("Sliding",false);
			break;
		case PlayerState.Running:
			if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Jumping"))
			{
				anim.SetBool("Idle",false);
				anim.SetBool("Running",true);
				anim.SetBool("Sliding",false);
			}
			break;
		case PlayerState.WallGrabLeft:
			anim.SetBool("Idle",false);
			anim.SetBool("Running",false);
			anim.SetBool("Sliding",true);

			if(facingRight)
			{
				Flip();
			}
			break;
		case PlayerState.WallGrabRight:
			anim.SetBool("Idle",false);
			anim.SetBool("Running",false);
			anim.SetBool("Sliding",true);

			if(!facingRight)
			{
				Flip();
			}
			break;
		}
	}

	 public void Shunt(float dir)
	{
		rigidBody.AddForce(new Vector2(Mathf.Sign(-dir)*shuntForce,shuntForce));
	}

	public void EnableDoubleJump(float time)
	{
		StartCoroutine(DoubleJumpEnable(time));
	}
	IEnumerator DoubleJumpEnable(float time)
	{
		CanDoubleJump = true;
		yield return new WaitForSeconds(time);
		CanDoubleJump = false;
	}
}

*/