using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Main : MonoBehaviour {

	public GameObject player;
	public GameObject felpudoIdle;
	public GameObject felpudoBate;
	public GameObject barril;
	public GameObject inimigoDir;
	public GameObject inimigoEsq;
	public GameObject fundo;
	public Text txtPontos;
	public AudioClip somBate;
	public AudioClip somPerde;
	public Button btnSair;

	float escalaJogadorHorizontal;
	int score = 0;
	private List<GameObject> listaBlocos;

	bool ladoInimigo;

	bool comecou;
	bool acabou;

	// Use this for initialization
	void Start () {
		escalaJogadorHorizontal = transform.localScale.x;
		felpudoBate.SetActive (false);

		listaBlocos = new List<GameObject> ();
		CriaBarrisInicio ();

		txtPontos.transform.position = new Vector2 (Screen.width / 2, Screen.height / 2 + 100);
		txtPontos.text = "Toque para Iniciar!";
		txtPontos.fontSize = 25;

		btnSair.onClick.AddListener (Sair);

	}
	
	// Update is called once per frame
	void Update () {
		//Quando tocar na tela
		if(!acabou){
			if(Input.GetButtonDown("Fire1")){
				GetComponent<AudioSource> ().PlayOneShot (somBate);
				if (Input.mousePosition.x > Screen.width / 2) {
					bateDireita ();
				} else {
					bateEsquerda ();
				}
				listaBlocos.RemoveAt (0);
				ReposicionaBlocos ();
				ConfereJogada ();
			}
		}
		if(player.transform.position.y < -5){
			Application.LoadLevel (0);
		}
	}

	//Codigos para o Player
	void bateDireita(){
		ladoInimigo = true;
		felpudoBate.SetActive (true);
		felpudoIdle.SetActive (false);
		player.transform.position = new Vector2 (2f, player.transform.position.y);
		player.transform.localScale = new Vector2 (-escalaJogadorHorizontal, player.transform.localScale.y);
		Invoke ("voltaAnimacao", 0.25f);
		listaBlocos [0].SendMessage ("BateDireita");
	}

	void bateEsquerda(){
		ladoInimigo = false;
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
		GameObject objetoBarril = CriaNovoBarril (new Vector2 (0, -3.25f+(8*1.7f)));
		listaBlocos.Add(objetoBarril);
		for (int i = 0; i <= 7; i++) {
			listaBlocos [i].transform.position = new Vector2 (listaBlocos [i].transform.position.x, listaBlocos [i].transform.position.y-1.7f);
		}
	}

	void ConfereJogada(){
		if (listaBlocos [0].gameObject.CompareTag ("Inimigo")) {
			if ((listaBlocos [0].name == "inimigoDir(Clone)" && !ladoInimigo) || (listaBlocos [0].name == "inimigoEsq(Clone)" && ladoInimigo)) {
				MarcarPonto ();
			} else {
				GameOver ();
			}
		} else {
			MarcarPonto ();
		}
	}

	void MarcarPonto(){
		score++;
		txtPontos.text = "Score: " + score.ToString();
		txtPontos.fontSize = 50;
		txtPontos.color = new Color (0.95f, 1.0f, 0.35f);
	}
	void GameOver(){
		GetComponent<AudioSource> ().PlayOneShot (somPerde);
		acabou = true;
		felpudoBate.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 0.35f, 0.35f);
		felpudoIdle.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 0.35f, 0.35f);
		player.GetComponent<Rigidbody2D> ().isKinematic = false;

		if (ladoInimigo) {
			player.GetComponent<Rigidbody2D> ().AddTorque(-500.0f);
			player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (10.0f, 5.0f);
		} else {
			player.GetComponent<Rigidbody2D> ().AddTorque(500.0f);
			player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-10.0f, 5.0f);
		}
	}

	void Sair(){
		Application.Quit ();	
	}
}