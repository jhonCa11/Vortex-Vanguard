using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResponderPregunta : MonoBehaviour {


	public Text canva_puntos;
	public int puntosPorRespuesta=0;
	public GameObject activadorPregunta;
	public GameObject Pregunta;


	public static int puntos;

	void Start (){

		canva_puntos.text = "puntos: " + puntos;
	}

	public void Preguntas(){

		puntos+=puntosPorRespuesta;
		StartCoroutine (res_correcta ());
	}

	IEnumerator res_correcta(){

		gameObject.GetComponent<AudioSource> ().Play();
		canva_puntos.text = "puntos: " + puntos;
		yield return new WaitForSeconds (0.5f);
		canva_puntos.text = "puntos: " + puntos;
		Destroy (Pregunta);
		Cursor.lockState = CursorLockMode.Locked;
		Destroy (activadorPregunta);
		//activadorPregunta.GetComponent<Collider> ().enabled = false;
	}

}
