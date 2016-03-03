﻿using UnityEngine;
using System.Collections;

public enum PlayerState {Idle,Running,Jumping,WallGrabLeft,WallGrabRight};

public class CharacterController : MonoBehaviour {

	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool jumpLeft = false;
	[HideInInspector] public bool jumpRight = false;
	public PlayerState m_PlayerState;
    
	public string name;
    public int m_PlayerNumber;
	public float moveForce = 100;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	public Transform leftGrabCheck;
	public Transform rightGrabCheck;
	public float shuntForce = 2000.0f;
	public bool CanDoubleJump = false;
	public bool isTimePoweredup = false;
	private float timeSlowScaler= 1.0f;
	public float height;
	
	public bool grounded = false;
	public bool grabbingLeft = false;
	public bool grabbingRight = false;

    private bool groundedPrev = false;
    private bool grabbingLeftPrev = false;
    private bool grabbingRightPrev = false;

    //AUDIO
    [HideInInspector] public AudioSource m_AudioSource;
    public AudioClip m_AudioClipLand;
    public AudioClip m_AudioClipShoot;
    public AudioClip m_AudioClipDeath;
    public AudioClip m_AudioClipJump;

    //EFFECTS
    public ParticleSystem m_ParticleSystemLand;
    public ParticleSystem m_ParticleSystemWallLeft;
    public ParticleSystem m_ParticleSystemWallLeftJump;
    public ParticleSystem m_ParticleSystemWallRight;
    public ParticleSystem m_ParticleSystemWallRightJump;

	public Animator anim;
	public GameObject Model;
	private float maxSpeed = 100f;
    private float maxControlSpeed = 5.0f;
	private float fixeddeltatime;

	private Rigidbody rigidBody;
	private bool m_DoubleJump = false;
	private bool m_DoubleJumpAvailable = true;
	// Use this for initialization
    void Awake()
    {
        
        
    }
    
    void Start()
    {
        switch (m_PlayerNumber)
        {
            case 1:
                name = "Player1";
                GetComponent<MeshColour>().ColourizeMesh(MeshColour.PlayerColour.Red);
                break;
            case 2:
                name = "Player2";
                GetComponent<MeshColour>().ColourizeMesh(MeshColour.PlayerColour.Blue);
                break;
            case 3:
                name = "Player3";
                GetComponent<MeshColour>().ColourizeMesh(MeshColour.PlayerColour.Green);
                break;
            case 4:
                name = "Player4";
                GetComponent<MeshColour>().ColourizeMesh(MeshColour.PlayerColour.Yellow);
                break;
            default:
                name = "InvalidPlayerNumber";
                break;
        }
	


        anim = Model.GetComponent<Animator>();
        m_AudioSource = gameObject.AddComponent<AudioSource>();
        

        rigidBody = GetComponent<Rigidbody>();
        height = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        m_PlayerState = PlayerState.Idle;

        

        
		rigidBody = GetComponent<Rigidbody>();
		height = GetComponent<CapsuleCollider> ().height *transform.localScale.y;
		m_PlayerState = PlayerState.Idle;

		fixeddeltatime = Time.fixedDeltaTime;


    }


	
	// Update is called once per frame
	void Update () 
	{
		gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
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
        //PARTICLES////////////////////////////////////////////////////
        ////////////////////////////////////////////////////PARTICLES//
        if (grounded != groundedPrev)
        {
            if (grounded == true)
            {
                m_AudioSource.PlayOneShot(m_AudioClipLand);
                m_ParticleSystemLand.Emit(Mathf.Abs((int)rigidBody.velocity.y));
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

        if (grabbingLeft)
        {
            m_ParticleSystemWallLeft.enableEmission = true;
        }
        else
        {
            m_ParticleSystemWallLeft.enableEmission = false;
        }
        if (grabbingRight)
        {
            m_ParticleSystemWallRight.enableEmission = true;
        }
        else
        {
            m_ParticleSystemWallRight.enableEmission = false;
        }

		gameObject.layer = LayerMask.NameToLayer("Ground");
	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxis (name + "LeftStickX");

		//anim.SetFloat("Speed", Mathf.Abs(h));
		if (!grabbingLeft && !grabbingRight) 
        {
			if (h * rigidBody.velocity.x < maxControlSpeed/(timeSlowScaler))
				rigidBody.AddForce (Vector2.right * h * moveForce/(timeSlowScaler*Time.fixedDeltaTime/fixeddeltatime));	// DM: term under move force is 1 when no slow time in play
																														// but adjusts appropriately when it is(effectively divides by slow 
																														// ratio twice as acceleration goes with the square of time and linearly with force
		
			if (Mathf.Abs (rigidBody.velocity.x) > maxSpeed/(timeSlowScaler))
				rigidBody.velocity = new Vector2 (Mathf.Sign (rigidBody.velocity.x) * maxSpeed/(timeSlowScaler*Time.fixedDeltaTime/fixeddeltatime), rigidBody.velocity.y);

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
			rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
		} else if (h < 0 && facingRight) {
			Flip ();
			rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
		}


        //SOUND/////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////SOUND//
        //PARTICLES/////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////PARTICLES//
        if (jump || jumpLeft || jumpRight || (m_DoubleJump && CanDoubleJump))
        {
            m_AudioSource.PlayOneShot(m_AudioClipJump);
            //Debug.Log("jumpSound");
            if (jump)
            {
                m_ParticleSystemLand.Emit(10);
            }

            if (m_DoubleJump && CanDoubleJump)
            {
                m_ParticleSystemLand.Emit(5);
            }

            if (jumpLeft)
            {
                m_ParticleSystemWallRightJump.Emit(10);
            }

            if (jumpRight)
            {
                m_ParticleSystemWallLeftJump.Emit(10);
            }
        }


		if (jump) {
			rigidBody.AddForce (new Vector2 (0f, jumpForce)/(timeSlowScaler*Time.fixedDeltaTime/fixeddeltatime));
			jump = false;
		} else if (jumpLeft) {
			//anim.SetTrigger("Jump");
			rigidBody.AddForce (new Vector2 (-200f, jumpForce)/(timeSlowScaler*Time.fixedDeltaTime/fixeddeltatime));
			gameObject.GetComponent<Player> ().lowerDrag ();
			jumpLeft = false;
		} else if (jumpRight) {
			//anim.SetTrigger("Jump");
			rigidBody.AddForce (new Vector2 (200f, jumpForce)/(timeSlowScaler*Time.fixedDeltaTime/fixeddeltatime));
			gameObject.GetComponent<Player> ().lowerDrag ();
			jumpRight = false;
		} else if (m_DoubleJump && CanDoubleJump) {
			rigidBody.velocity = new Vector3(rigidBody.velocity.x,0,0);
			rigidBody.AddForce (new Vector2 (0f, jumpForce/(timeSlowScaler*Time.fixedDeltaTime/fixeddeltatime)));
			m_DoubleJump = false;
		}

		if (isTimePoweredup) {
			rigidBody.AddForce (new Vector2 (0f, Physics.gravity.y / (timeSlowScaler*timeSlowScaler)));
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

	public void EnableTimeSlow(float time)
	{
		StartCoroutine(TimeSlow(time));
	}

	IEnumerator TimeSlow(float time)
	{
		timeSlowScaler = 0.5f; 											//DM: sets time slow ratio
		Time.timeScale = timeSlowScaler;								//sets time scale
		Time.fixedDeltaTime = Time.fixedDeltaTime * timeSlowScaler;		// adjusts physics frame rate to account for change in time scale(timeScale dictates how quickly game time passes 
																		// and fixedDeltaTime is calculated in game time for some reason
		isTimePoweredup = true;											// boolean to alter gravity behaviour up above probably not necessary if you give gravity same treatment as jump/move
		rigidBody.useGravity = false;									// see gravity treatment above

		yield return new WaitForSeconds (time);

		Time.timeScale = 1.0f;											// reverses all previous effects once powerup timer ends
		Time.fixedDeltaTime = Time.fixedDeltaTime /	 timeSlowScaler;
		timeSlowScaler = 1.0f;
		isTimePoweredup = false;
		rigidBody.useGravity = true;

	}
}

