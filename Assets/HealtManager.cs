using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
	public int maxHealth = 100;
	private int currentHealth;

	public float respawnDelay = 5f;
	public Transform respawnPoint;

	private SpriteRenderer spriteRenderer;
	private Collider2D col;
	private Rigidbody2D rb;

	public Slider healthBar;
	private MonoBehaviour[] scriptsToDisable;

	public enum ControlType { player1, player2 }
	public ControlType controlType = ControlType.player1;

	public int player1deaths = 0;
	public int player2deaths = 0;

	public int player1kills = 0;
	public int player2kills = 0;

	void Start()
	{
		currentHealth = maxHealth;
		spriteRenderer = GetComponent<SpriteRenderer>();
		col = GetComponent<Collider2D>();
		rb = GetComponent<Rigidbody2D>();


		scriptsToDisable = GetComponents<MonoBehaviour>();

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
		switch (controlType)
		{
			case ControlType.player1:
				player1deaths++;
				player2kills++;
				Debug.Log("Player 1 has died " + player1deaths + "times");
				Debug.Log("Player 2 has " + player2kills + "kills");
				break;

			case ControlType.player2:
				player2deaths++;
				player1kills++;
				Debug.Log("Player 2 has died " + player2deaths + "times");
				Debug.Log("Player 1 has " + player1kills + "kills");
				break;
		}

			
		Debug.Log(gameObject.name + " died.");

		spriteRenderer.enabled = false;
		col.enabled = false;
		rb.simulated = false;

		if (healthBar != null)
			healthBar.gameObject.SetActive(false);

		// Schakel actieve scripts uit behalve dit script zelf
		foreach (var script in scriptsToDisable)
		{
			if (script != this)
				script.enabled = false;
		}

		Invoke(nameof(Respawn), respawnDelay);
	}

	void Respawn()
	{
		currentHealth = maxHealth;

		if (respawnPoint != null)
			transform.position = respawnPoint.position;

		spriteRenderer.enabled = true;
		col.enabled = true;
		rb.simulated = true;

		if (healthBar != null)
		{
			healthBar.value = currentHealth;
			healthBar.gameObject.SetActive(true);
		}

		foreach (var script in scriptsToDisable)
		{
			if (script != this)
				script.enabled = true;
		}

		Debug.Log(gameObject.name + " respawned.");
	}
}

