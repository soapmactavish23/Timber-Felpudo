using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public GameObject player;
	public GameObject felpudoIdle;
	public GameObject felpudoBate;

	float escalaJogadorHorizontal;

	// Use this for initialization
	void Start () {
		escalaJogadorHorizontal = transform.localScale.x;
		felpudoBate.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//Quando tocar na tela
		if(Input.GetButtonDown("Fire1")){
			if (Input.mousePosition.x > Screen.width / 2) {
				bateDireita ();
			} else {
				bateEsquerda ();
			}
		}
	}

	void bateDireita(){
		felpudoBate.SetActive (true);
		felpudoIdle.SetActive (false);
		player.transform.position = new Vector2 (2f, player.transform.position.y);
		player.transform.localScale = new Vector2 (-escalaJogadorHorizontal, player.transform.localScale.y);
		Invoke ("voltaAnimacao", 0.25f);
	}

	void bateEsquerda(){
		felpudoBate.SetActive (true);
		felpudoIdle.SetActive (false);
		player.transform.position = new Vector2 (-2f, player.transform.position.y);
		player.transform.localScale = new Vector2 (escalaJogadorHorizontal, player.transform.localScale.y);
		Invoke ("voltaAnimacao", 0.25f);
	}

	void voltaAnimacao(){
		felpudoBate.SetActive (false);
		felpudoIdle.SetActive (true);
	}
}
