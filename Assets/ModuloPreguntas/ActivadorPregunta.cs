using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorPregunta : MonoBehaviour {
	public GameObject Pregunta;
	// Use this for initialization
	void Start () {
		Pregunta.SetActive (false);
	}

	void OnTriggerEnter(Collider col){
		switch (col.gameObject.tag){
		case "personaje":
			Pregunta.SetActive (true);
			Cursor.lockState = CursorLockMode.Confined;
			break;
		}
	}
	void OnTriggerExit(Collider col){
		switch (col.gameObject.tag){
		case "personaje":
			Pregunta.SetActive (false);
			Cursor.lockState = CursorLockMode.Locked;
			break;
		}
	}
}
