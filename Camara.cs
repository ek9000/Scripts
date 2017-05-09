using UnityEngine;
using System.Collections;

public class Camara : MonoBehaviour {
	public float yanglemin = -20f;
	public float yanglemax = 26f;

	public Transform Lookat;
	public Transform camTransform;

	private Camera Cam;

	public float distancia = 14f;
	private float actualx = 0f;
	private float actualy=0f;
	private float sensx=4f;
	private float sensy = 1f;

	private void Start(){
		camTransform = transform;
		Cam = Camera.main;
	}
	private void Update()
	{
		actualx += Input.GetAxis ("Mouse X")*sensx;
		actualy += Input.GetAxis ("Mouse Y")*sensy*-1;
		actualy = Mathf.Clamp (actualy,yanglemin,yanglemax);
	
	}

	private void LateUpdate()
	{
		Vector3 dir = new Vector3 (0f,7f,-distancia);
		Quaternion rotation = Quaternion.Euler (actualy,actualx,0f);
		camTransform.position = Lookat.position + rotation* dir;
		camTransform.LookAt (Lookat);
	}
		
		

}