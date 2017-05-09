using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerRpgController : MonoBehaviour {
	
	private Rigidbody rigid;
	private Animator anim;
	private static GameObject camara;
	float h=0f;
	float v=0f;
	public string estado="idle";

	bool canjump=false;

	float selfrotation=0;
	Vector3 Jump_dir;
	float speedfactor=2.7f;
	float currentheight;

	float oldheight;
	float trace;

	void Start () {
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody>();
		camara = GameObject.FindGameObjectWithTag ("MainCamera");

	}
	
	void FixedUpdate () {
		
		Falling ();
	
		if (Input.GetMouseButtonDown (0)) {
			estado="punch";
			Punch ();
		}
		Debug.Log (estado);

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("golpe 1") || anim.GetCurrentAnimatorStateInfo (0).IsName ("golpe 2")) {
			anim.SetBool ("IsPunch", false);
			anim.SetBool ("IsRepunch", false);
			estado = "idle";
		}
			
		if ((Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0)) {
			v = Input.GetAxis ("Vertical");
			h = Input.GetAxis ("Horizontal");
		} else if (estado!="punch"){
			estado = "idle";
		}


		if ((Input.GetAxisRaw ("Vertical")!=0 || Input.GetAxisRaw ("Horizontal")!=0)) {
			estado = "walk";
			if (Input.GetKey (KeyCode.LeftShift) && estado=="walk") {
				speedfactor = 1.7f;
				estado = "run";
			}
			if (Input.GetKeyUp (KeyCode.LeftShift) && estado == "run") {

				speedfactor = 2.7f;
				estado = "walk";

			} 
			MovePlayer ();
		} else {
			
			rigid.MovePosition (transform.position + transform.forward/2f*Input.GetAxis ("Vertical")*Input.GetAxis ("Vertical")+transform.forward/2f*Input.GetAxis ("Horizontal")*Input.GetAxisRaw ("Horizontal"));
			anim.SetBool ("IsWalk", false);
			anim.SetBool ("IsRun", false);
		}

		if (Input.GetKey (KeyCode.Space) && canjump) {
			
			Jump_dir = new Vector3 (h,0,v) ;
			Jump ();
		}

	}

	void MovePlayer ()
	{
		if (speedfactor < 2.7f && estado != "idle" && estado!="walk") {
			
			anim.SetBool ("IsRun", true);
			anim.SetBool ("IsWalk", false);
		} else {
			speedfactor = 2.7f;
			anim.SetBool ("IsWalk", true);
			anim.SetBool ("IsRun", false);
		} 
	

		selfrotation = Mathf.Round(transform.rotation.y);

		if (h != 0 && Mathf.Round(Input.GetAxisRaw("Vertical"))==0) {
			
			Quaternion delta = Quaternion.Euler (0f, Input.GetAxis("Horizontal")*90, 0f);

				
			Quaternion fix = Quaternion.Euler (0f,camara.transform.rotation.eulerAngles.y,0f);
			rigid.MoveRotation ( fix* delta);


			rigid.MovePosition (transform.position + transform.forward / (speedfactor));
//
		} else if (v > 0 && h != 0) {
			Quaternion delta = Quaternion.Euler (0f, 45 * h, 0f);

			Quaternion fix = Quaternion.Euler (0f,camara.transform.rotation.eulerAngles.y,0f);
			rigid.MoveRotation ( fix* delta);
			rigid.MovePosition (transform.position + transform.forward /(speedfactor));


		} else if (v > 0 && h == 0) {
			Quaternion delta = Quaternion.Euler (Vector3.zero);
			rigid.MovePosition (transform.position + transform.forward / (speedfactor));
			Quaternion fix = Quaternion.Euler (0f,camara.transform.rotation.eulerAngles.y,0f);
			rigid.MoveRotation ( fix* delta);

		} else if (v < 0 && h != 0) {
			Quaternion delta = Quaternion.Euler (0f, 180 - 45 * h, 0f);
			rigid.MovePosition (transform.position + transform.forward / (speedfactor));
			Quaternion fix = Quaternion.Euler (0f,camara.transform.rotation.eulerAngles.y,0f);
			rigid.MoveRotation ( fix* delta);

		} else if (v < 0) {
			Quaternion delta = Quaternion.Euler (0f, 180f * -1, 0f);

			if (v > -1) {
				Quaternion fix = Quaternion.Euler (0f,camara.transform.rotation.eulerAngles.y,0f);
				rigid.MoveRotation ( fix* delta);
			}
			rigid.MovePosition (transform.position + transform.forward / (speedfactor));
		}


}
	void Jump()
	{
		
		estado = "jump";
		anim.SetBool ("IsJump", true);
		canjump = false;
		rigid.AddForce (0f,1000f,0f);
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Floor") {
			canjump =true;
			anim.SetBool ("IsJump", false);
			anim.SetBool ("IsFall", false);
			estado = "idle";
		}

		if (other.gameObject.tag == "Restart") {
			
			Vector3 pos = new Vector3 (41.8f,3.66f,5.7f);
			transform.position=pos;
		} 


	}
	void Falling()
	{
		
		currentheight = transform.position.y;
		trace = oldheight-currentheight;

		oldheight = currentheight;
		if (trace > 0f && canjump==false) {
			anim.SetBool ("IsFall", true);
			anim.SetBool ("IsJump", false);
		} 

	}
	void Punch()
	{
		
		if ( canjump==true) {
			anim.SetBool ("IsPunch", true);

			estado = "punch";
		}
		if (Input.GetMouseButton (0) && anim.GetBool("IsPunch")) {
			anim.SetBool ("IsRepunch", true);

			estado = "punch";
		}

	}
	void OnTriggerEnter(Collider Other)
	{
		
	}

}