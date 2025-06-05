using UnityEngine;

public class GameMovement : MonoBehaviour
{
	public float moveSpeed = 5f;               // Snelheid van de speler
	private Rigidbody2D rb;                    // Referentie naar de Rigidbody2D component
	private Vector2 movement;                  // Beweging vector

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();      // Haalt de Rigidbody2D op bij het starten
	}

	void Update()
	{
		
		movement.x = Input.GetAxisRaw("Horizontal");  // A (-1) <-> D (+1)
		movement.y = Input.GetAxisRaw("Vertical");    // W (+1) <-> S (-1)
	}

	void FixedUpdate()
	{
		
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	}
}
