using UnityEngine;

public class MouseAiming : MonoBehaviour
{
	void Update()
	{
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = mouseWorldPos - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
