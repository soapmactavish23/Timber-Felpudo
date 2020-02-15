using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public GameObject player;
	public GameObject felpudoIdle;
	public GameObject felpudoBate;
	public GameObject barril;
	public GameObject inimigoDir;
	public GameObject inimigoEsq;

	float escalaJogadorHorizontal;

	// Use this for initialization
	void Start () {
		escalaJogadorHorizontal = transform.localScale.x;
		felpudoBate.SetActive (false);

		CriaBarrisInicio ();
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
	//Codigos para o Player
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

	//Codigos para os objetos
	GameObject CriaNovoBarril(Vector2 posicao){
		GameObject novoBarril;

		if (Random.value > 0.5f) {
			novoBarril = Instantiate (barril);
		} else {
			if (Random.value > 0.5f) {
				novoBarril = Instantiate (inimigoDir);
			}else{
				novoBarril = Instantiate (inimigoEsq);
			}
		}

		novoBarril.transform.position = posicao;

		return novoBarril;
	}

	void CriaBarrisInicio(){
		for (int i = 0; i <= 7; i++) {
			CriaNovoBarril (new Vector2 (0, -3.25f+(i*1.7f)));
		}
	}
}
