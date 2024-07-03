using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	public static Main main;

	//

	public static string[] commandesAll = new string[4];
	public static int[] commandesScores = new int[4];
	public static bool strategiaStarted = false;

	//

	public static string comandaName_1 = "";
	public static string comandaName_2 = "";
	public static string comandaName_3 = "";
	public static string comandaName_4 = "";

	//

	public Character[] player_1;
	public Character[] player_2;
	public Character[] player_3;
	public Character[] player_4;

	//

	public float scores_1 = 0;
	public float scores_2 = 0;
	public float scores_3 = 0;
	public float scores_4 = 0;

	//

	public Spawn spawn_1;
	public Spawn spawn_2;
	public Spawn spawn_3;
	public Spawn spawn_4;

	//

	public float timeStart;
	public float timeToEnd;

	//



	public static List<string> winners;
	public static bool gameEnded;


	//


	void Awake () {
		main = this;
	}

	//

	void Start () {
		ReStart ();
	}

	void ReStart () {
		timeStart = Time.time;
		strategiaStarted = true;
	}

	//

	void Sinhro () {
		//

		GetCharacters ();
		GetSpawns ();

		//

		RateSpawn ();
		CheckProgress ();

		//
	}

	//

	void CheckProgress () {
		timeToEnd = 120 - (Time.time - timeStart);

		//

		if (timeToEnd > 0) {

			for (int i = 0; i < player_1.Length; i++) {
				if (player_1[i].trans.position.magnitude < 3) {
					player_1[i].scores += 1 * Time.deltaTime;
				}
			}
			for (int i = 0; i < player_2.Length; i++) {
				if (player_2[i].trans.position.magnitude < 3) {
					player_2[i].scores += 1 * Time.deltaTime;
				}
			}
			for (int i = 0; i < player_3.Length; i++) {
				if (player_3[i].trans.position.magnitude < 3) {
					player_3[i].scores += 1 * Time.deltaTime;
				}
			}
			for (int i = 0; i < player_4.Length; i++) {
				if (player_4[i].trans.position.magnitude < 3) {
					player_4[i].scores += 1 * Time.deltaTime;
				}
			}

		} else {
			if (!calculated) {
				int redComanda = 0;
				int greenComanda = 0;
				int blueComanda = 0;
				int whiteComanda = 0;
				
				for (int i = 0; i < commandesAll.Length; i++) {
					if (commandesAll[i] == comandaName_1) {
						redComanda = i;
						commandesScores[i] = Mathf.RoundToInt(scores_1);
					}
					if (commandesAll[i] == comandaName_2) {
						greenComanda = i;
						commandesScores[i] = Mathf.RoundToInt(scores_2);
					}
					if (commandesAll[i] == comandaName_3) {
						blueComanda = i;
						commandesScores[i] = Mathf.RoundToInt(scores_3);
					}
					if (commandesAll[i] == comandaName_4) {
						whiteComanda = i;
						commandesScores[i] = Mathf.RoundToInt(scores_4);
					}
				}

				int index = -1;
				int max = 100000000;
				for (int i = 0; i < commandesScores.Length; i++) {
					if (commandesScores[i] < max && commandesAll[i] != "null") {
						max = commandesScores[i];
						index = i;
					}
					if (commandesScores[i] == 0) {
						commandesAll[i] = "null";
					}
				}

				commandesAll[index] = "null";



				/*if ((scores_1 < scores_2 && scores_1 < scores_3 && scores_1 < scores_4) || scores_1 == 0) {

					commandesAll[0] = "null";

				} else if ((scores_2 < scores_1 && scores_2 < scores_3 && scores_2 < scores_4) || scores_2 == 0) {

					commandesAll[1] = "null";


				} else if ((scores_3 < scores_2 && scores_3 < scores_1 && scores_3 < scores_4) || scores_3 == 0) {

					commandesAll[2] = "null";


				} else if ((scores_4 < scores_2 && scores_4 < scores_3 && scores_4 < scores_1) || scores_4 == 0) {

					commandesAll[3] = "null";

				}*/
				Application.LoadLevelAsync(0);


				calculated = true;
			}
		}

		//


	}

	bool calculated = false;

	//

	void RateSpawn () {
		if (player_1.Length < 3) {
			spawn_1.ToSpawn();
		}
		if (player_2.Length < 3) {
			spawn_2.ToSpawn();
		}
		if (player_3.Length < 3) {
			spawn_3.ToSpawn();
		}
		if (player_4.Length < 3) {
			spawn_4.ToSpawn();
		}
	}

	//

	void GetSpawns () {
		Spawn[] sps = Spawn.FindObjectsOfType<Spawn>();
		for (int i = 0; i < sps.Length; i++) {
			if (sps[i].comanda == Character.Comanda.red) {
				spawn_1 = sps[i];
			}
			if (sps[i].comanda == Character.Comanda.green) {
				spawn_2 = sps[i];
			}
			if (sps[i].comanda == Character.Comanda.blue) {
				spawn_3 = sps[i];
			}
			if (sps[i].comanda == Character.Comanda.white) {
				spawn_4 = sps[i];
			}
		}
	}

	//

	void GetCharacters () {
		List <Character> p_1 = new List<Character> ();
		List <Character> p_2 = new List<Character> ();
		List <Character> p_3 = new List<Character> ();
		List <Character> p_4 = new List<Character> ();
		Character[] chars = Character.FindObjectsOfType<Character>();
		for (int i = 0; i < chars.Length; i++) {
			if (chars[i].comanda == Character.Comanda.red) {
				p_1.Add(chars[i]);
			}
			if (chars[i].comanda == Character.Comanda.green) {
				p_2.Add(chars[i]);
			}
			if (chars[i].comanda == Character.Comanda.blue) {
				p_3.Add(chars[i]);
			}
			if (chars[i].comanda == Character.Comanda.white) {
				p_4.Add(chars[i]);
			}
		}
		player_1 = p_1.ToArray ();
		player_2 = p_2.ToArray ();
		player_3 = p_3.ToArray ();
		player_4 = p_4.ToArray ();
	}
	
	//

	void Update () {

		//
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevelAsync(0);
		}
		bool z = Input.GetKey (KeyCode.Z);
		bool m = Input.GetKey (KeyCode.M);
		bool w = Input.GetKey (KeyCode.W);
		bool p = Input.GetKey (KeyCode.P);

		for (int i = 0; i < 4; i++) {
			if (Input.GetKeyDown(("" + (i + 1)))) {
				Character.Comanda cmd = Character.Comanda.red;
				Spawn sp = spawn_1;
				if (i == 1) {
					cmd = Character.Comanda.green;
					sp = spawn_2;
				}
				if (i == 2) {
					cmd = Character.Comanda.blue;
					sp = spawn_3;
				}
				if (i == 3) {
					cmd = Character.Comanda.white;
					sp = spawn_4;
				}

				if (z && m && w && p) {
					Character[] enemies = Spawn.GetEnemiesOutComanda(cmd);
					for (int n = 0; n < enemies.Length; n++) {
						FireBall(enemies[n], sp.trans.position);
					}
				}


				break;
			}
		}

		Sinhro ();
	}
	void FireBall (Character at, Vector3 from) {
		FireBall fireball = (Instantiate(Resources.Load("Fireball") as GameObject) as GameObject).GetComponent<FireBall>();
		fireball.damage = 45;
		fireball.transform.position = from + Vector3.up * 1.5f;
		fireball.target = at;
	}
	void OnGUI () {
		Rect rect = new Rect (Screen.width / 2 - 300, 0, 600, 50);
		GUI.color = Color.cyan;
		GUIStyle style = GUI.skin.label;
		style.fontSize = 20;
		GUI.skin.box.fontSize = 10;
		GUI.Label (rect, "Время" + " : " + Mathf.RoundToInt(timeToEnd), style);
		/*rect = new Rect (0, 50, 300, 50);
		GUI.Label (rect, comandaName_2 + " : " + scores_2);
		rect = new Rect (0, 100, 300, 50);
		GUI.Label (rect, comandaName_3 + " : " + scores_3);
		rect = new Rect (0, 150, 300, 50);
		GUI.Label (rect, comandaName_4 + " : " + scores_4);*/
	}
}








