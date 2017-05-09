using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 6f;
	int floorMsk;
	float camRayLength = 200f;
	Rigidbody playerRigid;
	Vector3 movement;

	void Awake()
	{
		playerRigid = GetComponent<Rigidbody>();
		floorMsk = LayerMask.GetMask("Floor");
	}

	void Move (float horizontal, float vertical)
	{
		movement.Set(horizontal, 0f, vertical);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigid.MovePosition(transform.position + movement);
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit floorHit;
		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMsk))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigid.MoveRotation(newRotation);
		}
	}
	
	void FixedUpdate ()
	{
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Move(horizontal, vertical);
		Turning();
	}
}
