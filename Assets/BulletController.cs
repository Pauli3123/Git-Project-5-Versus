using UnityEngine;

public enum ControlType
{
	AWSD,
	Arrows
}

public class BulletController : MonoBehaviour
{
	public GameObject bulletPrefab;     // De kogel prefab
	public Transform firePoint;         // Waar de kogel vandaan komt
	public float bulletForce = 20f;     // Hoe snel de kogel vliegt

	public ControlType controlType;
	public int playerID;

	void Update()
	{
		
		
			if (playerID == 1 && Input.GetKeyDown(KeyCode.S))
		{
			Shoot();
		}
		else if (playerID == 2 && Input.GetKeyDown(KeyCode.DownArrow))
		{
			Shoot();
		}
	
	}

	void Shoot()
	{
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
		Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}
}


