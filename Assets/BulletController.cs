using UnityEngine;

public class BulletController : MonoBehaviour
{
	public GameObject bulletPrefab;
	public Transform firePoint;
	public float bulletForce = 20f;

	public enum ControlType { player1, player2 }
	public ControlType controlType = ControlType.player1;

	private bool facingRight = true;

	void Update()
	{
		switch (controlType)
		{
			case ControlType.player1:
				if (Input.GetKeyDown(KeyCode.A))
					facingRight = false;
				else if (Input.GetKeyDown(KeyCode.D))
					facingRight = true;

				if (Input.GetKeyDown(KeyCode.S))
					Shoot();
				break;

			case ControlType.player2:
				if (Input.GetKeyDown(KeyCode.LeftArrow))
					facingRight = false;
				else if (Input.GetKeyDown(KeyCode.RightArrow))
					facingRight = true;

				if (Input.GetKeyDown(KeyCode.DownArrow))
					Shoot();
				break;
		}
	}

	void Shoot()
	{
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

		if (!facingRight)
			bullet.transform.Rotate(0f, 0f, 180f);

		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		Vector2 forceDirection = facingRight ? firePoint.right : -firePoint.right;
		rb.AddForce(forceDirection * bulletForce, ForceMode2D.Impulse);

		Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}
}



