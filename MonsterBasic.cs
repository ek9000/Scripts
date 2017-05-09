using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBasic : MonoBehaviour {
	private Transform player;

	private float life=20f;
	private GameObject pj;
	Animator anim;
	Animator selfanim;
	// Use this for initialization
	void Start () {
		
		pj = GameObject.FindGameObjectWithTag ("Player");
		player =pj.transform;
		anim = pj.GetComponent<Animator>();
		selfanim = GetComponent<Animator>();

	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (life <= 0f) {
			Destroy (this.gameObject);


		}
	}

	void OnTriggerStay(Collider obj) {
		if(obj.gameObject.tag == "Hitcollider"){
			
		transform.LookAt (player);
		if (anim.GetBool ("IsPunch")) {
			selfanim.SetBool ("Hit",true);
				life -= 0.7f;

		}
		if (selfanim.GetCurrentAnimatorStateInfo (0).IsName ("Hiteado")) {
			selfanim.SetBool ("Hit",false);

			
			}}
	}

}
