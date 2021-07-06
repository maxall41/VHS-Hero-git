using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;

	float jumpPressRemember;

	public AudioSource walkSFX;

	public Animator animator;
	
	// Update is called once per frame
	void Update () {

		jumpPressRemember -= Time.deltaTime;
		groundTimer -= Time.deltaTime;

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jumpPressRemember = 0.25F;
		}

	}

	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;

	private float groundTimer;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}


	private void FixedUpdate()
	{
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
            {
				m_Grounded = true;
				groundTimer = 0.25F;
			}
		}


		Move(horizontalMove * Time.fixedDeltaTime);
	}


	public void Move(float move)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			if (move > 0.3 || move < -0.3)
            {
				animator.SetBool("walk", true);
			} else
            {
				animator.SetBool("walk", false);
			}
			if (move > 0.3 || move < -0.3 && walkSFX.isPlaying == false && m_Grounded)
			{
				//TODO: Fix walking sound
				//walkSFX.volume = Random.Range(0.8F, 1);
				//walkSFX.pitch = Random.Range(0.8F, 1.1F);
				//walkSFX.Play();
			}

				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
				m_CrouchDisableCollider.enabled = true;


			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);


			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		} else
        {
			animator.SetBool("walk", false);
		}
		// If the player should jump...
		if ((jumpPressRemember > 0) && (groundTimer > 0))
		{
			GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_jump(); // Play jump sound effect
																				// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			groundTimer = 0;
			jumpPressRemember = 0;
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
