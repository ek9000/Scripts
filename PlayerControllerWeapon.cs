using UnityEngine;

public class PlayerControllerWeapon : MonoBehaviour 
{
	Rigidbody weaponRigid;
	int floorMsk;
	float camRayLength = 200f;
	private float verticalAxisInput;
	private float horizontalAxisInput;

	public Rigidbody tankRigid;
	public float moveSpeed = 50f;
	public float turnSpeed = 90f;

	void Awake()
	{
		weaponRigid = GetComponent<Rigidbody>();
		floorMsk = LayerMask.GetMask("Floor");
	}

	void Turn()
	{
		float turn = horizontalAxisInput * turnSpeed * Time.deltaTime;
		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
		weaponRigid.MoveRotation (weaponRigid.rotation * turnRotation);
	}

	void Move()
	{
		Vector3 movement = tankRigid.transform.forward * verticalAxisInput * moveSpeed * Time.deltaTime;
		weaponRigid.MovePosition(weaponRigid.position + movement);
	}

	void Update ()
	{
		verticalAxisInput = Input.GetAxis ("Vertical");
		horizontalAxisInput = Input.GetAxis ("Horizontal");
	}

	void TurnWeapon()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit floorHit;
		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMsk))
		{
			Vector3 playerToMouse = floorHit.point - weaponRigid.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			weaponRigid.MoveRotation(newRotation);
		}
	}

	void FixedUpdate () 
	{
		TurnWeapon();
		Move();
		Turn();
	}
}
