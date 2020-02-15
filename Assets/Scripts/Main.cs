using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	public GameObject player;
	public GameObject felpudoIdle;
	public GameObject felpudoBate;
	public GameObject barril;
	public GameObject inimigoDir;
	public GameObject inimigoEsq;

	float escalaJogadorHorizontal;

	private List<GameObject> listaBlocos;

	// Use this for initialization
	void Start () {
		escalaJogadorHorizontal = transform.localScale.x;
		felpudoBate.SetActive (false);

		listaBlocos = new List<GameObject> ();
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
			listaBlocos.RemoveAt (0);
			ReposicionaBlocos ();
		}
	}
	//Codigos para o Player
	void bateDireita(){
		felpudoBate.SetActive (true);
		felpudoIdle.SetActive (false);
		player.transform.position = new Vector2 (2f, player.transform.position.y);
		player.transform.localScale = new Vector2 (-escalaJogadorHorizontal, player.transform.localScale.y);
		Invoke ("voltaAnimacao", 0.25f);
		listaBlocos [0].SendMessage ("BateDireita");
	}

	void bateEsquerda(){
		felpudoBate.SetActive (true);
		felpudoIdle.SetActive (false);
		player.transform.position = new Vector2 (-2f, player.transform.position.y);
		player.transform.localScale = new Vector2 (escalaJogadorHorizontal, player.transform.localScale.y);
		Invoke ("voltaAnimacao", 0.25f);
		listaBlocos [0].SendMessage ("BateEsquerda");
	}

	void voltaAnimacao(){
		felpudoBate.SetActive (false);
		felpudoIdle.SetActive (true);
	}

	//Codigos para os objetos
	GameObject CriaNovoBarril(Vector2 posicao){
		GameObject novoBarril;

		if (Random.value > 0.5f || listaBlocos.Count < 2) {
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
			GameObject objetoBarril = CriaNovoBarril (new Vector2 (0, -3.25f+(i*1.7f)));
			listaBlocos.Add(objetoBarril);
		}
	}

	void ReposicionaBlocos(){
		GameObject objetoBarril = CriaNovoBarril (new Vector2 (0, -3.25f+(7*1.7f)));
		listaBlocos.Add(objetoBarril);
		for (int i = 0; i <= 7; i++) {
			listaBlocos [i].transform.position = new Vector2 (listaBlocos [i].transform.position.x, listaBlocos [i].transform.position.y-1.7f);
		}
	}
}
