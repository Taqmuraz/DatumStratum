using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu : MonoBehaviour {

	public List<string> names = new List<string>();
	public string[] selected;

	// Use this for initialization
	void CheckStart () {
		RedPlayer.Main r = new RedPlayer.Main ();
		GreenPlayer.Main g = new GreenPlayer.Main ();
		BluePlayer.Main b = new BluePlayer.Main ();
		WhitePlayer.Main w = new WhitePlayer.Main ();
		mayToStart = (r.ToString().Length > -1) && 
			(g.ToString().Length > -1) && 
				(b.ToString().Length > -1) && 
				(w.ToString().Length > -1);
	}
	void Start () {
		mayToStart = false;
		CheckStart ();


		if (mayToStart) {
			selected = new string[4];
			for (int i = 0; i < selected.Length; i++) {
				selected[i] = "null";
			}
			if (!Main.strategiaStarted) {
				string[] allNames = {"Red", "Green", "Blue", "White"};
				Main.commandesAll = allNames;
				names.Clear();
				names.AddRange(allNames);
			}
			names.Clear ();
			names.AddRange (Main.commandesAll);
			
			ReSelect ();
		}
	}

	bool mayToStart;
	// Update is called once per frame
	void Update () {
	}
	void OnGUI () {
		if (mayToStart) {
			int length = 0;

			for (int i = 0; i < Main.commandesAll.Length; i++) {
				if (Main.commandesAll[i] != "null") {
					length ++;
				}
			}


			if (length > 1) {


				string commandesScroll = "";
				string selectedScroll = "";
				for (int i = 0; i < names.Count; i++) {
					commandesScroll = commandesScroll + "" + " - " + names [i] + " Очки : " + Main.commandesScores [i] + '\n';
				}
				for (int i = 0; i < selected.Length; i++) {
					selectedScroll = selectedScroll + "" + (i + 1) + " - " + selected [i] + '\n';
				}

				selected = Main.commandesAll;
				
				GUI.Label (new Rect (15, 100, 300, 50 * names.Count), "Команды в игре : " + '\n' + commandesScroll);
				GUI.Label (new Rect (Screen.width - 300, 100, 300, 50 * names.Count), "Команды в раунде : " + '\n' + selectedScroll);
				/*if (GUI.Button (new Rect (0, 0, 150, 50), "> Перевыбрать <")) {
					ReSelect ();
				}*/
				if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 25, 150, 50), "> Стратегия <")) {
					Main.comandaName_1 = selected [0];
					Main.comandaName_2 = selected [1];
					Main.comandaName_3 = selected [2];
					Main.comandaName_4 = selected [3];
					Application.LoadLevelAsync (1);
				}
			} else {
				int wen = -1;

				for (int i = 0; i < Main.commandesAll.Length; i++) {
					if (Main.commandesAll[i] != "null") {
						wen = i;
					}
				}
				string won = "";

				if (wen > -1) {
					won = "Поздравляем отважного " + Main.commandesAll [wen] + "," +
						"победителя королевского турнира! Теперь, как полагается, он получит от короля половину земель и прекрасную" +
							" принцессу в жены! Он сражался отважно, и король оценил это!";
				} else {
					won = "К сожалению, все отважные воины пали, не сумев вынести все проклятое золото. Король в растерянности.";
				}

				GUI.Label (new Rect (100, 100, Screen.width - 200, Screen.height - 200), won);
			}
		} else {
			GUI.skin.label.fontSize = 35;
			GUI.Label(new Rect(Screen.width / 4, Screen.height / 8, Screen.width / 2, Screen.height - 50), "Для работы программы необходимы DLL файлы!" +
			          '\n' + "Возможно, вы их переименовали или заменяли их несоответствующуми файлами." + '\n' +
			          "Пользуясь инструкцией, исправьте ошибку и повторите попытку.");
			if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height - 50, 150, 50), "> Инструкция <")) {
				Application.OpenURL("https://drive.google.com/file/d/0B067zMXzfDrldGNia3NlYUtjUDA/view");
			}
		}
		if (GUI.Button(new Rect(0, Screen.height - 50, 150, 50), "> Выйти из игры <")) {
			Application.Quit();
		}
	}
	void ReSelect () {
		/*for (int i = 0; i < 4; i++) {
			int n = Random.Range(0, names.Count - 1);
			if (names.Count > 0) {
				selected[i] = names[n];
				names.RemoveAt(n);
			}
		}*/
		names.Clear();
		names.AddRange(Main.commandesAll);
		for (int i = 0; i < names.Count; i++) {
			selected[i] = names[i];
		}
	}
}


















