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
		HealtManager health = collision.gameObject.GetComponent<HealtManager>();
		if (health != null)
		{
			health.TakeDamage(20); // Of elk ander getal
		}

		Destroy(gameObject); // Verwijder de kogel na impact
	}
}
