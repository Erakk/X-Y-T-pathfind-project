using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public float speed = 6f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		CameraMoving ();
	}

	void CameraMoving()
	{
		if (Input.GetButton ("Horizontal")) {
			
			if (Input.GetAxis ("Horizontal") > 0) {
				transform.Translate (new Vector2(speed * Time.deltaTime,0));
			} else {
				transform.Translate (new Vector2(-speed * Time.deltaTime,0));
			}

		}
		if (Input.GetButton ("Vertical")) {

			if (Input.GetAxis ("Vertical") > 0) {
				transform.Translate (new Vector2(0, speed * Time.deltaTime));			
			} else {
				transform.Translate (new Vector2(0, -speed * Time.deltaTime));			
			}
		}
	}

}
