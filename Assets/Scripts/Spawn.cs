using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : MonoBehaviour {
	public Character.Comanda comanda;
	public Transform trans;
	//

	public GameObject Mag;
	public GameObject Ninja;
	public GameObject Woin;
	public GameObject Luchnik;

	//

	public string SpawnLL () {
		Character[] chars = new Character[0];
		int scores = 0;
		int to = -1;
		if (comanda == Character.Comanda.red) {
			scores = Mathf.RoundToInt(Main.main.scores_1);
			chars = Main.main.player_1;
		}
		if (comanda == Character.Comanda.green) {
			scores = Mathf.RoundToInt(Main.main.scores_2);
			chars = Main.main.player_2;
		}
		if (comanda == Character.Comanda.blue) {
			scores = Mathf.RoundToInt(Main.main.scores_3);
			chars = Main.main.player_3;
		}
		if (comanda == Character.Comanda.white) {
			scores = Mathf.RoundToInt(Main.main.scores_4);
			chars = Main.main.player_4;
		}
		int[] types = new int[0];
		if (chars.Length == 3) {
			int[] types_n = {chars[0].thisIsInInt, chars[1].thisIsInInt, chars[2].thisIsInInt};
			types = types_n;
		}
		if (chars.Length == 2) {
			int[] types_n = {chars[0].thisIsInInt, chars[1].thisIsInInt, -1};
			types = types_n;
		}
		if (chars.Length == 1) {
			int[] types_n = {chars[0].thisIsInInt, -1, -1};
			types = types_n;
		}
		if (chars.Length == 0) {
			int[] types_n = {-1, -1, -1};
			types = types_n;
		}
		List<int> enemyTypes = new List<int> ();
		Character[] enemies = GetEnemies ();
		for (int i = 0; i < enemies.Length; i++) {
			enemyTypes.Add(enemies[i].thisIsInInt);
		}

		if (comanda == Character.Comanda.red) {
			to = RedPlayer.Main.ToSpawn (types, enemyTypes.ToArray(), scores);
		}
		if (comanda == Character.Comanda.green) {
			to = GreenPlayer.Main.ToSpawn (types, enemyTypes.ToArray(), scores);
		}
		if (comanda == Character.Comanda.blue) {
			to = BluePlayer.Main.ToSpawn (types, enemyTypes.ToArray(), scores);
		}
		if (comanda == Character.Comanda.white) {
			to = WhitePlayer.Main.ToSpawn (types, enemyTypes.ToArray(), scores);
		}

		string spawnable = "";;
		if (to == 0) {
			spawnable = "Warrior";
		}
		if (to == 1) {
			spawnable = "Mag";
		}
		if (to == 2) {
			spawnable = "Striker";
		}
		if (to == 3) {
			spawnable = "Ninja";
		}
		return spawnable;
	}

	// Use this for initialization
	void Start () {
		trans = transform;

		if (comanda == Character.Comanda.red && Main.comandaName_1 != "null") {
			ffective = true;
		}
		
		if (comanda == Character.Comanda.green && Main.comandaName_2 != "null") {
			ffective = true;
		}
		
		if (comanda == Character.Comanda.blue && Main.comandaName_3 != "null") {
			ffective = true;
		}
		
		if (comanda == Character.Comanda.white && Main.comandaName_4 != "null") {
			ffective = true;
		}
	}
	public bool ffective = false;
	// Update is called once per frame
	void Update () {

		//

		if (ffective) {
			if (comanda == Character.Comanda.red) {
				for (int i = 0; i < Main.main.player_1.Length; i++) {
					Main.main.player_1 [i].selfNumber = i;
				}
			}
			if (comanda == Character.Comanda.green) {
				for (int i = 0; i < Main.main.player_2.Length; i++) {
					Main.main.player_2 [i].selfNumber = i;
				}
			}
			if (comanda == Character.Comanda.blue) {
				for (int i = 0; i < Main.main.player_3.Length; i++) {
					Main.main.player_3 [i].selfNumber = i;
				}
			}
			if (comanda == Character.Comanda.white) {
				for (int i = 0; i < Main.main.player_4.Length; i++) {
					Main.main.player_4 [i].selfNumber = i;
				}
			}
			GetScores ();
		} else {
			for (int i = 0; i < GetChars().Length; i++) {
				GetChars()[i].health = 0;
			}
		}
	}
	public void ToSpawn () {

		if (ffective) {
			string sp = SpawnLL ();
			if (sp == "Mag") {
				SpawnObject(Mag);
			}
			if (sp == "Ninja") {
				SpawnObject(Ninja);
			}
			if (sp == "Warrior") {
				SpawnObject(Woin);
			}
			if (sp == "Striker") {
				SpawnObject(Luchnik);
			}
		}
	}
	void SpawnObject (GameObject obj) {
		Instantiate (obj, transform.position + (-trans.position).normalized * 3, Quaternion.Euler(Vector3.zero));
	}
	void GetScores () {
		Character[] chars = GetChars ();
		for (int i = 0; i < chars.Length; i++) {
			if (Vector3.Distance(trans.position, chars[i].trans.position) < 1) {
				chars[i].agent.Warp(trans.position - (trans.position).normalized * 3);
			}
			if (Vector3.Distance(trans.position, chars[i].trans.position) < 4 && chars[i].scores > 0) {
				AddScore(Time.deltaTime);
				chars[i].scores -= Time.deltaTime;
			}
		}
	}
	void OnGUI () {
		if (ffective) {
			Vector3 screen = Camera.main.WorldToScreenPoint (trans.position + Vector3.up * 8);
			Rect rect = new Rect (screen.x - 50, Screen.height - (screen.y) - 25, 100, 50);
			Color color = new Color ();
			string text = "";
			if (comanda == Character.Comanda.red) {
				color = Color.red;
				text = Main.comandaName_1;
			}
			if (comanda == Character.Comanda.green) {
				color = Color.green;
				text = Main.comandaName_2;
			}
			if (comanda == Character.Comanda.blue) {
				color = Color.blue;
				text = Main.comandaName_3;
			}
			if (comanda == Character.Comanda.white) {
				color = Color.white;
				text = Main.comandaName_4;
			}
			GUI.color = color;
			GUI.Label (rect, text + '\n' + "Очки : " + Mathf.RoundToInt(ShowScores()));
		}
	}
	public void AddScore (float scores) {
		if (comanda == Character.Comanda.red) {
			Main.main.scores_1 += Time.deltaTime;
		}
		if (comanda == Character.Comanda.green) {
			Main.main.scores_2 += Time.deltaTime;
		}
		if (comanda == Character.Comanda.blue) {
			Main.main.scores_3 += Time.deltaTime;
		}
		if (comanda == Character.Comanda.white) {
			Main.main.scores_4 += Time.deltaTime;
		}
	}
	public float ShowScores () {
		float score = 0;
		if (comanda == Character.Comanda.red) {
			score = Main.main.scores_1;
		}
		if (comanda == Character.Comanda.green) {
			score = Main.main.scores_2;
		}
		if (comanda == Character.Comanda.blue) {
			score = Main.main.scores_3;
		}
		if (comanda == Character.Comanda.white) {
			score = Main.main.scores_4;
		}
		return score;
	}
	public Character[] GetEnemies () {
		List<Character> enemies = new List<Character> ();
		if (comanda != Character.Comanda.red) {
			enemies.AddRange(Main.main.player_1);
		}
		if (comanda != Character.Comanda.green) {
			enemies.AddRange(Main.main.player_2);
		}
		if (comanda != Character.Comanda.blue) {
			enemies.AddRange(Main.main.player_3);
		}
		if (comanda != Character.Comanda.white) {
			enemies.AddRange(Main.main.player_4);
		}
		return enemies.ToArray ();
	}
	public static Character[] GetEnemiesOutComanda (Character.Comanda comanda) {
		List<Character> enemies = new List<Character> ();
		if (comanda != Character.Comanda.red) {
			enemies.AddRange(Main.main.player_1);
		}
		if (comanda != Character.Comanda.green) {
			enemies.AddRange(Main.main.player_2);
		}
		if (comanda != Character.Comanda.blue) {
			enemies.AddRange(Main.main.player_3);
		}
		if (comanda != Character.Comanda.white) {
			enemies.AddRange(Main.main.player_4);
		}
		return enemies.ToArray ();
	}
	public Character[] GetChars () {
		Character[] chars = new Character[0];
		if (comanda == Character.Comanda.red) {
			chars = Main.main.player_1;
		}
		if (comanda == Character.Comanda.green) {
			chars = Main.main.player_2;
		}
		if (comanda == Character.Comanda.blue) {
			chars = Main.main.player_3;
		}
		if (comanda == Character.Comanda.white) {
			chars = Main.main.player_4;
		}
		return chars;
	}


	public static Character[] GetCharsFromComanda (Character.Comanda comandaChars) {
		Character[] chars = new Character[0];
		if (comandaChars == Character.Comanda.red) {
			chars = Main.main.player_1;
		}
		if (comandaChars == Character.Comanda.green) {
			chars = Main.main.player_2;
		}
		if (comandaChars == Character.Comanda.blue) {
			chars = Main.main.player_3;
		}
		if (comandaChars == Character.Comanda.white) {
			chars = Main.main.player_4;
		}
		return chars;
	}






	public RedPlayer.Command[] ToDoRedPlayer () {
		Character[] chars = new Character[0];
		int scores = 0;
		if (comanda == Character.Comanda.red) {
			scores = Mathf.RoundToInt(Main.main.scores_1);
			chars = Main.main.player_1;
		}
		if (comanda == Character.Comanda.green) {
			scores = Mathf.RoundToInt(Main.main.scores_2);
			chars = Main.main.player_2;
		}
		if (comanda == Character.Comanda.blue) {
			scores = Mathf.RoundToInt(Main.main.scores_3);
			chars = Main.main.player_3;
		}
		if (comanda == Character.Comanda.white) {
			scores = Mathf.RoundToInt(Main.main.scores_4);
			chars = Main.main.player_4;
		}
		//

		float[] positionsX = new float[3];
		float[] positionsY = new float[3];
		int[] types = new int[3];
		float[] healths = new float[3];
		bool[] canMove = new bool[3];
		bool[] spheresNear = new bool[3];
	
		//

		if (chars.Length == 3) {
			float[] positionsXn = {chars[0].trans.position.x, chars[1].trans.position.x, chars[2].trans.position.x};
			float[] positionsYn = {chars[0].trans.position.z, chars[1].trans.position.z, chars[2].trans.position.z};
			int[] typesn = {chars[0].thisIsInInt, chars[1].thisIsInInt, chars[2].thisIsInInt};
			float[] healthsn = {chars[0].health, chars[1].health, chars[2].health};
			bool[] canMoven = {chars[0].attacking, chars[1].attacking, chars[2].attacking};
			bool[] spheresNearn = {chars[0].SphereNear(), chars[1].SphereNear(), chars[2].SphereNear()};

			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;

		}
		if (chars.Length == 2) {
			float[] positionsXn = {chars[0].trans.position.x, chars[1].trans.position.x, 0};
			float[] positionsYn = {chars[0].trans.position.z, chars[1].trans.position.z, 0};
			int[] typesn = {chars[0].thisIsInInt, chars[1].thisIsInInt, -1};
			float[] healthsn = {chars[0].health, chars[1].health, -1};
			bool[] canMoven = {chars[0].attacking, chars[1].attacking, false};
			bool[] spheresNearn = {chars[0].SphereNear(), chars[1].SphereNear(), false};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
		}
		if (chars.Length == 1) {
			float[] positionsXn = {chars[0].trans.position.x, 0, 0};
			float[] positionsYn = {chars[0].trans.position.z, 0, 0};
			int[] typesn = {chars[0].thisIsInInt, -1, -1};
			float[] healthsn = {chars[0].health, -1, -1};
			bool[] canMoven = {chars[0].attacking, false, false};
			bool[] spheresNearn = {chars[0].SphereNear(), false, false};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
		}
		if (chars.Length == 0) {
			float[] positionsXn = {0, 0, 0};
			float[] positionsYn = {0, 0, 0};
			int[] typesn = {-1, -1, -1};
			float[] healthsn = {-1, -1, -1};
			bool[] canMoven = {false, false, false};
			bool[] spheresNearn = {false, false, false};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
		}
		int[] scoresInHands = new int[3];
		for (int i = 0; i < chars.Length; i++) {
			scoresInHands[i] = (Mathf.RoundToInt(chars[i].scores));
		}
		//

		List<float> enemiesPositionX = new List<float> ();
		List<float> enemiesPositionY = new List<float> ();
		Character[] enemies = GetEnemies ();

		List<int> enemyTypes = new List<int> ();
		List <int> enemyComands = new List<int> ();
		float[] enemyScores = new float[3];
		for (int i = 0; i < enemies.Length; i++) {
			enemiesPositionX.Add(enemies[i].trans.position.x);
			enemiesPositionY.Add(enemies[i].trans.position.z);
			enemyTypes.Add(enemies[i].thisIsInInt);

			if (enemies[i].comanda == Character.Comanda.red) {
				enemyComands.Add(0);
			}
			if (enemies[i].comanda == Character.Comanda.green) {
				enemyComands.Add(1);
			}
			if (enemies[i].comanda == Character.Comanda.blue) {
				enemyComands.Add(2);
			}
			if (enemies[i].comanda == Character.Comanda.white) {
				enemyComands.Add(3);
			}
		}
		int yourComanda = 0;
		if (comanda == Character.Comanda.red) {
			yourComanda = 0;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_2),Mathf.RoundToInt(Main.main.scores_3), Mathf.RoundToInt(Main.main.scores_4)};
			enemyScores = eS;
		}
		if (comanda == Character.Comanda.green) {
			yourComanda = 1;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_1),Mathf.RoundToInt(Main.main.scores_3), Mathf.RoundToInt(Main.main.scores_4)};
			enemyScores = eS;
		}
		if (comanda == Character.Comanda.blue) {
			yourComanda = 2;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_2),Mathf.RoundToInt(Main.main.scores_1), Mathf.RoundToInt(Main.main.scores_4)};
			enemyScores = eS;
		}
		if (comanda == Character.Comanda.white) {
			yourComanda = 3;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_2),Mathf.RoundToInt(Main.main.scores_3), Mathf.RoundToInt(Main.main.scores_1)};
			enemyScores = eS;
		}


		float sphereX = 0;
		float sphereY = 0;
		GameObject gSp = GameObject.FindWithTag ("Sphere" + comanda);
		if (gSp) {
			sphereX = gSp.transform.position.x;
			sphereY = gSp.transform.position.z;
		}
		return RedPlayer.Main.WhatDo(trans.position.x, trans.position.z, positionsX, positionsY, healths, types, canMove, canMove, Main.main.timeToEnd,
		                         scores, scoresInHands, spheresNear, sphereX, sphereY, enemiesPositionX.ToArray(), enemiesPositionY.ToArray(),
		                         enemyTypes.ToArray(),
		                         enemyScores, yourComanda, enemyComands.ToArray());
	}

	//

	public GreenPlayer.Command[] ToDoGreenPlayer () {
		Character[] chars = new Character[0];
		int scores = 0;
		if (comanda == Character.Comanda.red) {
			scores = Mathf.RoundToInt(Main.main.scores_1);
			chars = Main.main.player_1;
		}
		if (comanda == Character.Comanda.green) {
			scores = Mathf.RoundToInt(Main.main.scores_2);
			chars = Main.main.player_2;
		}
		if (comanda == Character.Comanda.blue) {
			scores = Mathf.RoundToInt(Main.main.scores_3);
			chars = Main.main.player_3;
		}
		if (comanda == Character.Comanda.white) {
			scores = Mathf.RoundToInt(Main.main.scores_4);
			chars = Main.main.player_4;
		}
		//
		
		float[] positionsX = new float[3];
		float[] positionsY = new float[3];
		int[] types = new int[3];
		float[] healths = new float[3];
		bool[] canMove = new bool[3];
		bool[] spheresNear = new bool[3];
		
		//
		
		if (chars.Length == 3) {
			float[] positionsXn = {chars[0].trans.position.x, chars[1].trans.position.x, chars[2].trans.position.x};
			float[] positionsYn = {chars[0].trans.position.z, chars[1].trans.position.z, chars[2].trans.position.z};
			int[] typesn = {chars[0].thisIsInInt, chars[1].thisIsInInt, chars[2].thisIsInInt};
			float[] healthsn = {chars[0].health, chars[1].health, chars[2].health};
			bool[] canMoven = {chars[0].attacking, chars[1].attacking, chars[2].attacking};
			bool[] spheresNearn = {chars[0].SphereNear(), chars[1].SphereNear(), chars[2].SphereNear()};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
			
		}
		if (chars.Length == 2) {
			float[] positionsXn = {chars[0].trans.position.x, chars[1].trans.position.x, 0};
			float[] positionsYn = {chars[0].trans.position.z, chars[1].trans.position.z, 0};
			int[] typesn = {chars[0].thisIsInInt, chars[1].thisIsInInt, -1};
			float[] healthsn = {chars[0].health, chars[1].health, -1};
			bool[] canMoven = {chars[0].attacking, chars[1].attacking, false};
			bool[] spheresNearn = {chars[0].SphereNear(), chars[1].SphereNear(), false};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
		}
		if (chars.Length == 1) {
			float[] positionsXn = {chars[0].trans.position.x, 0, 0};
			float[] positionsYn = {chars[0].trans.position.z, 0, 0};
			int[] typesn = {chars[0].thisIsInInt, -1, -1};
			float[] healthsn = {chars[0].health, -1, -1};
			bool[] canMoven = {chars[0].attacking, false, false};
			bool[] spheresNearn = {chars[0].SphereNear(), false, false};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
		}
		if (chars.Length == 0) {
			float[] positionsXn = {0, 0, 0};
			float[] positionsYn = {0, 0, 0};
			int[] typesn = {-1, -1, -1};
			float[] healthsn = {-1, -1, -1};
			bool[] canMoven = {false, false, false};
			bool[] spheresNearn = {false, false, false};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
		}
		int[] scoresInHands = new int[3];
		for (int i = 0; i < chars.Length; i++) {
			scoresInHands[i] = (Mathf.RoundToInt(chars[i].scores));
		}
		//
		
		List<float> enemiesPositionX = new List<float> ();
		List<float> enemiesPositionY = new List<float> ();
		Character[] enemies = GetEnemies ();
		
		List<int> enemyTypes = new List<int> ();
		List <int> enemyComands = new List<int> ();
		float[] enemyScores = new float[3];
		for (int i = 0; i < enemies.Length; i++) {
			enemiesPositionX.Add(enemies[i].trans.position.x);
			enemiesPositionY.Add(enemies[i].trans.position.z);
			enemyTypes.Add(enemies[i].thisIsInInt);
			
			if (enemies[i].comanda == Character.Comanda.red) {
				enemyComands.Add(0);
			}
			if (enemies[i].comanda == Character.Comanda.green) {
				enemyComands.Add(1);
			}
			if (enemies[i].comanda == Character.Comanda.blue) {
				enemyComands.Add(2);
			}
			if (enemies[i].comanda == Character.Comanda.white) {
				enemyComands.Add(3);
			}
		}
		int yourComanda = 0;
		if (comanda == Character.Comanda.red) {
			yourComanda = 0;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_2),Mathf.RoundToInt(Main.main.scores_3), Mathf.RoundToInt(Main.main.scores_4)};
			enemyScores = eS;
		}
		if (comanda == Character.Comanda.green) {
			yourComanda = 1;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_1),Mathf.RoundToInt(Main.main.scores_3), Mathf.RoundToInt(Main.main.scores_4)};
			enemyScores = eS;
		}
		if (comanda == Character.Comanda.blue) {
			yourComanda = 2;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_2),Mathf.RoundToInt(Main.main.scores_1), Mathf.RoundToInt(Main.main.scores_4)};
			enemyScores = eS;
		}
		if (comanda == Character.Comanda.white) {
			yourComanda = 3;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_2),Mathf.RoundToInt(Main.main.scores_3), Mathf.RoundToInt(Main.main.scores_1)};
			enemyScores = eS;
		}
		
		
		float sphereX = 0;
		float sphereY = 0;
		GameObject gSp = GameObject.FindWithTag ("Sphere" + comanda);
		if (gSp) {
			sphereX = gSp.transform.position.x;
			sphereY = gSp.transform.position.z;
		}
		return GreenPlayer.Main.WhatDo(trans.position.x, trans.position.z, positionsX, positionsY, healths, types, canMove, canMove, Main.main.timeToEnd,
		                         scores, scoresInHands, spheresNear, sphereX, sphereY, enemiesPositionX.ToArray(), enemiesPositionY.ToArray(),
		                         enemyTypes.ToArray(),
		                         enemyScores, yourComanda, enemyComands.ToArray());
	}

	//


	public BluePlayer.Command[] ToDoBluePlayer () {
		Character[] chars = new Character[0];
		int scores = 0;
		if (comanda == Character.Comanda.red) {
			scores = Mathf.RoundToInt(Main.main.scores_1);
			chars = Main.main.player_1;
		}
		if (comanda == Character.Comanda.green) {
			scores = Mathf.RoundToInt(Main.main.scores_2);
			chars = Main.main.player_2;
		}
		if (comanda == Character.Comanda.blue) {
			scores = Mathf.RoundToInt(Main.main.scores_3);
			chars = Main.main.player_3;
		}
		if (comanda == Character.Comanda.white) {
			scores = Mathf.RoundToInt(Main.main.scores_4);
			chars = Main.main.player_4;
		}
		//
		
		float[] positionsX = new float[3];
		float[] positionsY = new float[3];
		int[] types = new int[3];
		float[] healths = new float[3];
		bool[] canMove = new bool[3];
		bool[] spheresNear = new bool[3];
		
		//
		
		if (chars.Length == 3) {
			float[] positionsXn = {chars[0].trans.position.x, chars[1].trans.position.x, chars[2].trans.position.x};
			float[] positionsYn = {chars[0].trans.position.z, chars[1].trans.position.z, chars[2].trans.position.z};
			int[] typesn = {chars[0].thisIsInInt, chars[1].thisIsInInt, chars[2].thisIsInInt};
			float[] healthsn = {chars[0].health, chars[1].health, chars[2].health};
			bool[] canMoven = {chars[0].attacking, chars[1].attacking, chars[2].attacking};
			bool[] spheresNearn = {chars[0].SphereNear(), chars[1].SphereNear(), chars[2].SphereNear()};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
			
		}
		if (chars.Length == 2) {
			float[] positionsXn = {chars[0].trans.position.x, chars[1].trans.position.x, 0};
			float[] positionsYn = {chars[0].trans.position.z, chars[1].trans.position.z, 0};
			int[] typesn = {chars[0].thisIsInInt, chars[1].thisIsInInt, -1};
			float[] healthsn = {chars[0].health, chars[1].health, -1};
			bool[] canMoven = {chars[0].attacking, chars[1].attacking, false};
			bool[] spheresNearn = {chars[0].SphereNear(), chars[1].SphereNear(), false};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
		}
		if (chars.Length == 1) {
			float[] positionsXn = {chars[0].trans.position.x, 0, 0};
			float[] positionsYn = {chars[0].trans.position.z, 0, 0};
			int[] typesn = {chars[0].thisIsInInt, -1, -1};
			float[] healthsn = {chars[0].health, -1, -1};
			bool[] canMoven = {chars[0].attacking, false, false};
			bool[] spheresNearn = {chars[0].SphereNear(), false, false};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
		}
		if (chars.Length == 0) {
			float[] positionsXn = {0, 0, 0};
			float[] positionsYn = {0, 0, 0};
			int[] typesn = {-1, -1, -1};
			float[] healthsn = {-1, -1, -1};
			bool[] canMoven = {false, false, false};
			bool[] spheresNearn = {false, false, false};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
		}
		int[] scoresInHands = new int[3];
		for (int i = 0; i < chars.Length; i++) {
			scoresInHands[i] = (Mathf.RoundToInt(chars[i].scores));
		}
		//
		
		List<float> enemiesPositionX = new List<float> ();
		List<float> enemiesPositionY = new List<float> ();
		Character[] enemies = GetEnemies ();
		
		List<int> enemyTypes = new List<int> ();
		List <int> enemyComands = new List<int> ();
		float[] enemyScores = new float[3];
		for (int i = 0; i < enemies.Length; i++) {
			enemiesPositionX.Add(enemies[i].trans.position.x);
			enemiesPositionY.Add(enemies[i].trans.position.z);
			enemyTypes.Add(enemies[i].thisIsInInt);
			
			if (enemies[i].comanda == Character.Comanda.red) {
				enemyComands.Add(0);
			}
			if (enemies[i].comanda == Character.Comanda.green) {
				enemyComands.Add(1);
			}
			if (enemies[i].comanda == Character.Comanda.blue) {
				enemyComands.Add(2);
			}
			if (enemies[i].comanda == Character.Comanda.white) {
				enemyComands.Add(3);
			}
		}
		int yourComanda = 0;
		if (comanda == Character.Comanda.red) {
			yourComanda = 0;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_2),Mathf.RoundToInt(Main.main.scores_3), Mathf.RoundToInt(Main.main.scores_4)};
			enemyScores = eS;
		}
		if (comanda == Character.Comanda.green) {
			yourComanda = 1;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_1),Mathf.RoundToInt(Main.main.scores_3), Mathf.RoundToInt(Main.main.scores_4)};
			enemyScores = eS;
		}
		if (comanda == Character.Comanda.blue) {
			yourComanda = 2;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_2),Mathf.RoundToInt(Main.main.scores_1), Mathf.RoundToInt(Main.main.scores_4)};
			enemyScores = eS;
		}
		if (comanda == Character.Comanda.white) {
			yourComanda = 3;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_2),Mathf.RoundToInt(Main.main.scores_3), Mathf.RoundToInt(Main.main.scores_1)};
			enemyScores = eS;
		}
		
		
		float sphereX = 0;
		float sphereY = 0;
		GameObject gSp = GameObject.FindWithTag ("Sphere" + comanda);
		if (gSp) {
			sphereX = gSp.transform.position.x;
			sphereY = gSp.transform.position.z;
		}
		return BluePlayer.Main.WhatDo(trans.position.x, trans.position.z, positionsX, positionsY, healths, types, canMove, canMove, Main.main.timeToEnd,
		                           scores, scoresInHands, spheresNear, sphereX, sphereY, enemiesPositionX.ToArray(), enemiesPositionY.ToArray(),
		                           enemyTypes.ToArray(),
		                           enemyScores, yourComanda, enemyComands.ToArray());
	}

	//

	public WhitePlayer.Command[] ToDoWhitePlayer () {
		Character[] chars = new Character[0];
		int scores = 0;
		if (comanda == Character.Comanda.red) {
			scores = Mathf.RoundToInt(Main.main.scores_1);
			chars = Main.main.player_1;
		}
		if (comanda == Character.Comanda.green) {
			scores = Mathf.RoundToInt(Main.main.scores_2);
			chars = Main.main.player_2;
		}
		if (comanda == Character.Comanda.blue) {
			scores = Mathf.RoundToInt(Main.main.scores_3);
			chars = Main.main.player_3;
		}
		if (comanda == Character.Comanda.white) {
			scores = Mathf.RoundToInt(Main.main.scores_4);
			chars = Main.main.player_4;
		}
		//
		
		float[] positionsX = new float[3];
		float[] positionsY = new float[3];
		int[] types = new int[3];
		float[] healths = new float[3];
		bool[] canMove = new bool[3];
		bool[] spheresNear = new bool[3];
		
		//
		
		if (chars.Length == 3) {
			float[] positionsXn = {chars[0].trans.position.x, chars[1].trans.position.x, chars[2].trans.position.x};
			float[] positionsYn = {chars[0].trans.position.z, chars[1].trans.position.z, chars[2].trans.position.z};
			int[] typesn = {chars[0].thisIsInInt, chars[1].thisIsInInt, chars[2].thisIsInInt};
			float[] healthsn = {chars[0].health, chars[1].health, chars[2].health};
			bool[] canMoven = {chars[0].attacking, chars[1].attacking, chars[2].attacking};
			bool[] spheresNearn = {chars[0].SphereNear(), chars[1].SphereNear(), chars[2].SphereNear()};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
			
		}
		if (chars.Length == 2) {
			float[] positionsXn = {chars[0].trans.position.x, chars[1].trans.position.x, 0};
			float[] positionsYn = {chars[0].trans.position.z, chars[1].trans.position.z, 0};
			int[] typesn = {chars[0].thisIsInInt, chars[1].thisIsInInt, -1};
			float[] healthsn = {chars[0].health, chars[1].health, -1};
			bool[] canMoven = {chars[0].attacking, chars[1].attacking, false};
			bool[] spheresNearn = {chars[0].SphereNear(), chars[1].SphereNear(), false};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
		}
		if (chars.Length == 1) {
			float[] positionsXn = {chars[0].trans.position.x, 0, 0};
			float[] positionsYn = {chars[0].trans.position.z, 0, 0};
			int[] typesn = {chars[0].thisIsInInt, -1, -1};
			float[] healthsn = {chars[0].health, -1, -1};
			bool[] canMoven = {chars[0].attacking, false, false};
			bool[] spheresNearn = {chars[0].SphereNear(), false, false};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
		}
		if (chars.Length == 0) {
			float[] positionsXn = {0, 0, 0};
			float[] positionsYn = {0, 0, 0};
			int[] typesn = {-1, -1, -1};
			float[] healthsn = {-1, -1, -1};
			bool[] canMoven = {false, false, false};
			bool[] spheresNearn = {false, false, false};
			
			//
			positionsX = positionsXn;
			positionsY = positionsYn;
			types = typesn;
			healths = healthsn;
			canMove = canMoven;
			spheresNear = spheresNearn;
		}
		int[] scoresInHands = new int[3];
		for (int i = 0; i < chars.Length; i++) {
			scoresInHands[i] = (Mathf.RoundToInt(chars[i].scores));
		}
		//
		
		List<float> enemiesPositionX = new List<float> ();
		List<float> enemiesPositionY = new List<float> ();
		Character[] enemies = GetEnemies ();
		
		List<int> enemyTypes = new List<int> ();
		List <int> enemyComands = new List<int> ();
		float[] enemyScores = new float[3];
		for (int i = 0; i < enemies.Length; i++) {
			enemiesPositionX.Add(enemies[i].trans.position.x);
			enemiesPositionY.Add(enemies[i].trans.position.z);
			enemyTypes.Add(enemies[i].thisIsInInt);
			
			if (enemies[i].comanda == Character.Comanda.red) {
				enemyComands.Add(0);
			}
			if (enemies[i].comanda == Character.Comanda.green) {
				enemyComands.Add(1);
			}
			if (enemies[i].comanda == Character.Comanda.blue) {
				enemyComands.Add(2);
			}
			if (enemies[i].comanda == Character.Comanda.white) {
				enemyComands.Add(3);
			}
		}
		int yourComanda = 0;
		if (comanda == Character.Comanda.red) {
			yourComanda = 0;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_2),Mathf.RoundToInt(Main.main.scores_3), Mathf.RoundToInt(Main.main.scores_4)};
			enemyScores = eS;
		}
		if (comanda == Character.Comanda.green) {
			yourComanda = 1;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_1),Mathf.RoundToInt(Main.main.scores_3), Mathf.RoundToInt(Main.main.scores_4)};
			enemyScores = eS;
		}
		if (comanda == Character.Comanda.blue) {
			yourComanda = 2;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_2),Mathf.RoundToInt(Main.main.scores_1), Mathf.RoundToInt(Main.main.scores_4)};
			enemyScores = eS;
		}
		if (comanda == Character.Comanda.white) {
			yourComanda = 3;
			float[] eS = {Mathf.RoundToInt(Main.main.scores_2),Mathf.RoundToInt(Main.main.scores_3), Mathf.RoundToInt(Main.main.scores_1)};
			enemyScores = eS;
		}
		
		
		float sphereX = 0;
		float sphereY = 0;
		GameObject gSp = GameObject.FindWithTag ("Sphere" + comanda);
		if (gSp) {
			sphereX = gSp.transform.position.x;
			sphereY = gSp.transform.position.z;
		}
		return WhitePlayer.Main.WhatDo(trans.position.x, trans.position.z, positionsX, positionsY, healths, types, canMove, canMove, Main.main.timeToEnd,
		                           scores, scoresInHands, spheresNear, sphereX, sphereY, enemiesPositionX.ToArray(), enemiesPositionY.ToArray(),
		                           enemyTypes.ToArray(),
		                           enemyScores, yourComanda, enemyComands.ToArray());
	}
}








