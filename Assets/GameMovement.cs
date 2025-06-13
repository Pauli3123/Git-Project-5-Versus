using UnityEngine;

public class GameMovement : MonoBehaviour
{
	public enum ControlType { AWSD, Arrows }
	public ControlType controlType = ControlType.AWSD;

	public float moveSpeed = 5f;
	public float jumpForce = 7f;
	public Transform groundCheck;
	public LayerMask groundLayer;

	private Rigidbody2D rb;
	private Vector2 movement;
	private bool isGrounded;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		// Kies welke input gebruikt wordt
		switch (controlType)
		{
			case ControlType.AWSD:
				movement.x = (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0);
				if (Input.GetKeyDown(KeyCode.W) && isGrounded)
				{
					rb.AddForce(Vector2.up * jumpForce);
				}
				break;

			case ControlType.Arrows:
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

	void OnDrawGizmosSelected()
	{
		if (groundCheck != null)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
		}
	}
}
