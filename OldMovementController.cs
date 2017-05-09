using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMovementController : MonoBehaviour {

	private Rigidbody rigid;
	private Animator anim;
	Vector3 angleSpeed;
	// Use this for initialization
	void Start () {
		angleSpeed.y = 800f;
		rigid = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (transform.rotation.y);
		float pressing = (Input.GetAxis("Vertical")*Input.GetAxis("Vertical")+Input.GetAxis ("Horizontal")*Input.GetAxis ("Horizontal"));

		rigid.MovePosition (rigid.position+transform.forward*pressing/2f);


		if (Input.GetAxis ("Vertical") > 0 &&Input.GetAxis("Horizontal")==0 && transform.rotation.y>0) {
			
			rigid.MoveRotation (rigid.rotation * Quaternion.Euler (angleSpeed *-1*Time.deltaTime));
		}
		else if (Input.GetAxis ("Vertical") > 0 &&Input.GetAxis("Horizontal")==0 && transform.rotation.y<0) {

			rigid.MoveRotation (rigid.rotation * Quaternion.Euler (angleSpeed *Time.deltaTime));
		}
		if (Input.GetAxis ("Vertical") < 0 &&Input.GetAxis("Horizontal")==0 && transform.rotation.y>1) {

			rigid.MoveRotation (rigid.rotation * Quaternion.Euler (angleSpeed *-1*Time.deltaTime));
		}
		else if (Input.GetAxis ("Vertical") < 0 &&Input.GetAxis("Horizontal")==0 && transform.rotation.y<1) {

			rigid.MoveRotation (rigid.rotation * Quaternion.Euler (angleSpeed *Time.deltaTime));
		}



		if (Input.GetAxis ("Horizontal") > 0 && transform.rotation.y < 0.7 / 2 && Input.GetAxis ("Vertical") > 0) {
			rigid.MoveRotation (rigid.rotation * Quaternion.Euler (angleSpeed * Time.deltaTime));
		} 
		else if (Input.GetAxis ("Horizontal") < 0 && transform.rotation.y > -0.7 / 2 && Input.GetAxis ("Vertical") > 0) {
			rigid.MoveRotation (rigid.rotation * Quaternion.Euler (angleSpeed * -1 * Time.deltaTime));
		} 


		if (Input.GetAxis ("Horizontal") > 0 && transform.rotation.y < 0.7 && Input.GetAxis ("Vertical") == 0) {
			rigid.MoveRotation (rigid.rotation * Quaternion.Euler (angleSpeed * Time.deltaTime));
		}
		else if (Input.GetAxis ("Horizontal") < 0 && transform.rotation.y > -0.7 && Input.GetAxis ("Vertical")==0) {
			rigid.MoveRotation (rigid.rotation * Quaternion.Euler (angleSpeed * -1 * Time.deltaTime));
		}


		if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) {
				anim.SetBool ("IsWalk", true);
			
		} else {
			anim.SetBool ("IsWalk", false);
		}
	}
}
