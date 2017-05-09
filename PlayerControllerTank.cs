using UnityEngine;

public class PlayerControllerTank : MonoBehaviour
{
	public float moveSpeed = 50f;
	public float turnSpeed = 90f;

	Rigidbody playerRigid;
	private float verticalAxisInput;
	private float horizontalAxisInput;

	void Awake()
	{
		playerRigid = GetComponent<Rigidbody>();
	}

	void Turn()
	{
		float turn = horizontalAxisInput * turnSpeed * Time.deltaTime;
		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);
		playerRigid.MoveRotation (playerRigid.rotation * turnRotation);
	}

	void Move()
	{
		Vector3 movement = transform.forward * verticalAxisInput * moveSpeed * Time.deltaTime;
		playerRigid.MovePosition(playerRigid.position + movement);
	}

	void Update ()
	{
		verticalAxisInput = Input.GetAxis ("Vertical");
		horizontalAxisInput = Input.GetAxis ("Horizontal");
	}
	void FixedUpdate ()
	{
		Move();
		Turn();
	}
}
