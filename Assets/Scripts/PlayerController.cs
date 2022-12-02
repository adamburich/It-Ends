using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	public int difficulty = 1;
	public float jumpHeight = 325f;
	public bool allowAirControl = true;
	public Animator animator;
	public BoxCollider2D footCollider;
    private string currentState;
	public GameObject player;

    public LayerMask diags;
	public LayerMask flooring;
	public LayerMask walls;
	public Transform groundCheck;
	public Transform roofCheck;
	public Transform wallBehindCheck;
	public Transform wallAheadCheck;
	private Rigidbody2D rigidBody;
	public UnityEvent OnLandEvent;
	private bool walledBehindHard;
	public AudioSource jumpSound;
	private bool slipping;
	[SerializeField] private AudioClip[] m_FootstepSounds;
	public AudioSource m_AudioSource;

	public Vector3 playerVelocity = Vector3.zero;

	private float airControlDampener = 1.0f;
	[SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;

	private bool grounded;
	private bool walledBehind;
	private bool walledAhead;
	float behindRadius = .25f;
	float slideAnimatorRadius = .25f;
	const float rightRadius = .25f;
	const float leftRadius = .25f;
	const float groundRadius = .2f;
	const float roofRadius = .2f; 
	private bool goingRight = true;  // player moving right or left?
	float movemnt;
	[SerializeField] private float m_StepInterval;
	Vector3 hitNormal;
	private Vector2 slipOutNorm = new Vector2(0, 0);

	private float m_StepCycle;
	private float m_NextStep;
	private Collider2D lastJumpSource;
	private Collider2D thisJumpSource;

	public bool isGoingRight()
    {
		return goingRight;
    }
	public float getVerticalSpeed()
    {
		return rigidBody.velocity.y;
    }

	public int getDifficulty()
    {
		return difficulty;
    }

	public void setDifficulty(int i)
	{
		if(i == 1)
        {
			behindRadius = .25f;
			jumpHeight = 325f;
        }
		else if(i == 0)
        {
			behindRadius = .69f;
			jumpHeight = 333;
        }
		difficulty = i;
	}

	public void Move(float movement, bool jump)
	{
		if (!GameEnd.isEnding())
		{
			//Debug.Log("Grounded " + grounded);
			movemnt = movement;
			//Debug.Log(slipping);
			if (slipping)
			{
				airControlDampener = .25f;
				//Debug.Log("ADDING SLIP FORCE");
				rigidBody.AddForce(slipOutNorm);
				return;
			}
			// player has control if they are on the ground or if air control is allowed
			if (grounded)
			{
				airControlDampener = 1.0f;
				//footCollider.offset = new Vector2(footCollider.offset.x, -1.12f);
				// find and apply target velocity
				rigidBody.velocity = new Vector2(movement * 10f, rigidBody.velocity.y);

				if (movement > 0 && !goingRight)
				{
					// flip player sprite
					if (!wallSliding())
					{
						Flip();
					}
				}
				else if (movement < 0 && goingRight)
				{
					// flip player sprite
					if (!wallSliding())
					{
						Flip();
					}
				}
				if (jump)
				{
					//footCollider.offset = new Vector2(footCollider.offset.x, -1f);
					//Debug.Log(movement);
					//Debug.Log("Going right: " + goingRight);
					walledBehind = false;
					grounded = false;
					rigidBody.AddForce(new Vector2(0f, jumpHeight));
					lastJumpSource = null;
					jumpSound.Play();
				}
			}
			else
			{
				rigidBody.velocity = new Vector2(airControlDampener * movement * 10f, rigidBody.velocity.y);

				if (movement > 0 && !goingRight)
				{
					airControlDampener *= .69f;
					if (!wallSliding())
					{
						Flip();
					}
				}
				else if (movement < 0 && goingRight)
				{
					airControlDampener *= .69f;
					if (!wallSliding())
					{
						Flip();
					}
				}
				if (walledBehind)
				{
					airControlDampener = 1.0f;
					if (((jump && !grounded) && ((movement < 0 && !goingRight) || (movement > 0 && goingRight)) && movement != 0))
					{
						//Debug.Log("LAST JUMP SOURCE: " + lastJumpSource);
						//Debug.Log("THIS JUMP SOURCE: " + thisJumpSource);
						if (lastJumpSource != thisJumpSource)
						{
							lastJumpSource = thisJumpSource;
							//Debug.Log(movement);
							//Debug.Log("Going right: " + goingRight);
							walledBehind = false;
							grounded = false;
							rigidBody.AddForce(new Vector2(0f, jumpHeight));
							//Debug.Log("JUMPING FROM " + thisJumpSource);
							lastJumpSource = thisJumpSource;
							jumpSound.Play();
						}

					}
				}
			}
		}
        else
        {
			if(rigidBody.velocity.y == 0)
            {
				rigidBody.velocity = new Vector2(0f, 0f);
			}
        }
		/**
		// handle jump input
		if ((grounded && jump) || ((walledBehind && jump && !grounded) && ((movement < 0 && !goingRight) || (movement > 0 && goingRight)) && movement != 0))
		{
				lastJumpSource = thisJumpSource;
				Debug.Log("THIS IS THE JUMP");
				Debug.Log("JUMPING FROM " + thisJumpSource);
				jumpSound.Play();
				// add vertical force
				Debug.Log("________________________________");
				Debug.Log(movement);
				Debug.Log("goingRight: " + goingRight);
				Debug.Log("walledBehind: " + walledBehind);
				Debug.Log(grounded);
				walledAhead = false;
				walledBehind = false;
				grounded = false;
				rigidBody.AddForce(new Vector2(0f, jumpHeight));
		}*/
	}

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void Flip()
	{
		// note change in orientation
		goingRight = !goingRight;
		// flip local x scale
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool isGrounded()
    {
		return grounded;
    }

	public bool wallSliding()
    {
		return walledBehind && rigidBody.velocity.y < 3 && walledBehindHard;
    }
	private void ProgressStepCycle(float speed)
	{
		if (rigidBody.velocity.sqrMagnitude > 0 && (movemnt != 0 || movemnt != 0))
		{
			m_StepCycle += (rigidBody.velocity.magnitude + (speed * (m_RunstepLenghten))) *
						 Time.fixedDeltaTime;
		}

		if (!(m_StepCycle > m_NextStep))
		{
			return;
		}

		m_NextStep = m_StepCycle + m_StepInterval;

		PlayFootStepAudio();
	}

    private void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		slipOutNorm = collision.contacts[0].normal;
    }

    private void PlayFootStepAudio()
	{
		if (!grounded)
		{
			return;
		}
		// pick & play a random footstep sound from the array,
		// excluding sound at index 0
		int n = Random.Range(1, m_FootstepSounds.Length);
		m_AudioSource.clip = m_FootstepSounds[n];
		m_AudioSource.PlayOneShot(m_AudioSource.clip);
		// move picked sound to index 0 so it's not picked next time
		m_FootstepSounds[n] = m_FootstepSounds[0];
		m_FootstepSounds[0] = m_AudioSource.clip;
	}
	private void FixedUpdate()
	{
		//Debug.Log(behindRadius);
		bool wasGrounded = grounded;
		walledAhead = false;
		walledBehind = false;
		grounded = false;
		slipping = false;
		walledBehindHard = false;
		Collider2D[] behindColliders = Physics2D.OverlapCircleAll(wallBehindCheck.position, behindRadius, walls);
		Collider2D[] slideAnimatorColliders = Physics2D.OverlapCircleAll(wallBehindCheck.position, slideAnimatorRadius, walls);
		Collider2D[] aheadColliders = Physics2D.OverlapCircleAll(wallAheadCheck.position, rightRadius, walls);
		Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, flooring);
		Collider2D[] diagColliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, diags);
		for (int i = 0; i < groundColliders.Length; i++)
		{
			if (groundColliders[i].gameObject != gameObject)
			{
				grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
		for (int i = 0; i < diagColliders.Length; i++)
		{
			if (diagColliders[i].gameObject != gameObject)
			{
				slipping = true;
			}
		}
		for (int i = 0; i < behindColliders.Length; i++)
        {
			if (behindColliders[i].gameObject != gameObject)
			{
				walledBehind = true;
				thisJumpSource = behindColliders[i];
			}
		}
		for (int i = 0; i < slideAnimatorColliders.Length; i++)
		{
			if (slideAnimatorColliders[i].gameObject != gameObject)
			{
				walledBehindHard = true;
			}
		}
		for (int i = 0; i < aheadColliders.Length; i++)
		{
			if (aheadColliders[i].gameObject != gameObject)
			{
				walledAhead = true;
                if (!grounded)
                {
					Flip();/**
					walledBehind = true;
					walledAhead = false;
					wallSliding = true;*/
					animator.SetBool("Wallsliding", true);
				}
			}
		}
		ProgressStepCycle(rigidBody.velocity.x);
	}
}