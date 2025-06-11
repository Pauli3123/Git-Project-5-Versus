using UnityEngine;
using UnityEngine.UI;

public class HealtManager : MonoBehaviour
{
	public int maxHealth = 100;
	private int currentHealth;

	public float respawnDelay = 5f;
	public Transform respawnPoint; 

	private SpriteRenderer spriteRenderer;
	private Collider2D col;

	public Slider healthBar;

	void Start()
	{
		currentHealth = maxHealth;
		spriteRenderer = GetComponent<SpriteRenderer>();
		col = GetComponent<Collider2D>();

		if (healthBar != null)
		{
			healthBar.maxValue = maxHealth;
			healthBar.value = currentHealth;
		}
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		Debug.Log(gameObject.name + " took " + damage + " damage. HP left: " + currentHealth);

		if (healthBar != null)
		{
			healthBar.value = currentHealth;
		}

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Debug.Log(gameObject.name + " died.");
	
		spriteRenderer.enabled = false;
		col.enabled = false;
		GetComponent<Rigidbody2D>().simulated = false;
		
			healthBar.gameObject.SetActive(false);
		

		Invoke("Respawn", respawnDelay);
	}

	void Respawn()
	{
		currentHealth = maxHealth;

		// Zet speler terug op de respawnlocatie
		if (respawnPoint != null)
		{
			transform.position = respawnPoint.position;
		}

		// Re-enable visueel en botsing
		spriteRenderer.enabled = true;
		col.enabled = true;
		GetComponent<Rigidbody2D>().simulated = true;

		Debug.Log(gameObject.name + " respawned.");
	}
}
