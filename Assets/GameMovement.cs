using UnityEngine;

public class GameMovement : MonoBehaviour
{
	public enum ControlType { player1, player2 }
	public ControlType controlType = ControlType.player1;

	public float moveSpeed = 5f;
	public float jumpForce = 7f;
	public Transform groundCheck;
	public LayerMask groundLayer;

	public float normalGravity = 1f;

	private Rigidbody2D rb;
	private Vector2 movement;
	private bool isGrounded;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	
	}

	void Update()
	{
		
		switch (controlType)
		{
			case ControlType.player1:
				movement.x = (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0);
				if (Input.GetKeyDown(KeyCode.W) && isGrounded)
				{
					rb.AddForce(Vector2.up * jumpForce);
				}
				break;

			case ControlType.player2:
				movement.x = (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0) + (Input.GetKey(KeyCode.RightArrow) ? 1 : 0);
				if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
				{

					rb.AddForce(Vector2.up * jumpForce);
				}
				break;
		}
		

		isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	}


}

