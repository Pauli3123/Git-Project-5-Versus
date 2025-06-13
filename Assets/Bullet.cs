using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float lifetime = 2f;

	void Start()
	{
		Destroy(gameObject, lifetime);  // Verwijdert de kogel na 2 seconden
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		HealthManager health = collision.gameObject.GetComponent<HealthManager>();
		if (health != null)
		{
			health.TakeDamage(20); 
		}

		Destroy(gameObject); 

		
	}
}
