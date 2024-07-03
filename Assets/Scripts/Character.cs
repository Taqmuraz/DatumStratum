using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public enum Comanda {
		red,
		green,
		blue,
		white
	}
	public enum Class {
		Ninja,
		Mag,
		Warrior,
		Striker
	}
	public Class thisIs;
	public int thisIsInInt;
	public Comanda comanda;
	public Transform trans;
	public NavMeshAgent agent;
	public Animator anims;
	public float health = 100;
	public float damage = 0;
	public float speed = 3.5f;
	public int selfNumber = 0;
	float spawnTime = 0;
	public float scores = 0;
	public AnimationClip attackClip;

	// Use this for initialization
	void Start () {
		spawnTime = Time.time;
		trans = transform;
		agent = GetComponent<NavMeshAgent>();
		anims = GetComponent<Animator>();
		if (thisIs == Class.Warrior) {
			thisIsInInt = 0;
			health = 35;
			damage = 3.5f * 5 / 4;
			speed = 3.5f;
		}
		if (thisIs == Class.Mag) {
			thisIsInInt = 1;
			health = 3.5f * 5;
			damage = 3.5f * 4 / 4;
			speed = 3.5f * 1.5f;
		}
		if (thisIs == Class.Striker) {
			thisIsInInt = 2;
			health = 3.5f * 7.5f;
			damage = 3.5f * 3 / 4;
			speed = 3.5f * 2;
		}
		if (thisIs == Class.Ninja) {
			thisIsInInt = 3;
			health = 3.5f * 7;
			damage = 3.5f * 4 / 4;
			speed = 3.5f * 2.5f;
		}
		agent.speed = speed;
		agent.stoppingDistance = 2;
		agent.angularSpeed = 45000;
		agent.radius = 0.5f;
		agent.avoidancePriority = 50;
	}

	// Parameters

	public bool attacking;

	public bool SphereNear () {
		GameObject sphere = GameObject.FindWithTag ("Sphere" + comanda);
		if (sphere) {
			return (Vector3.Distance (trans.position, sphere.transform.position) < 3);
		} else {
			return false;
		}
	}
	public Transform Sphere () {
		GameObject sphere = GameObject.FindWithTag ("Sphere" + comanda);
		return sphere.transform;
	}


	//
	
	// Update is called once per frame
	void Update () {
		if (Time.time > spawnTime + 1 && Main.main.timeToEnd > 0) {
			Control ();
			attacking = !(timeToAttack < Time.time);
		}
	}
	Spawn GetSpawn () {
		Spawn ps = null;

		if (comanda == Comanda.red) {
			ps = Main.main.spawn_1;
		}
		if (comanda == Comanda.green) {
			ps = Main.main.spawn_2;
		}
		if (comanda == Comanda.blue) {
			ps = Main.main.spawn_3;
		}
		if (comanda == Comanda.white) {
			ps = Main.main.spawn_4;
		}
		return ps;
	}
	public float ShowScores () {
		scores = Mathf.Clamp (scores,0, 100);
		return scores;
	}
	void OnGUI () {
		Vector3 screen = Camera.main.WorldToScreenPoint (trans.position + Vector3.up * 3);
		Rect rect = new Rect (screen.x - 37.5f, Screen.height - (screen.y) - 25, 75, 30);
		Color color = new Color ();
		if (comanda == Character.Comanda.red) {
			color = Color.red;
		}
		if (comanda == Character.Comanda.green) {
			color = Color.green;
		}
		if (comanda == Character.Comanda.blue) {
			color = Color.blue;
		}
		if (comanda == Character.Comanda.white) {
			color = Color.white;
		}
		GUI.color = color;
		GUI.Box (rect, "Здоровье : " + + Mathf.RoundToInt(health) + '\n' + "Очки : " + Mathf.RoundToInt(ShowScores()));
	}
	string GetComandaName () {
		string txt = "";
		if (comanda == Comanda.red) {
			txt = Main.comandaName_1;
		}

		if (comanda == Comanda.green) {
			txt = Main.comandaName_2;
		}

		if (comanda == Comanda.blue) {
			txt = Main.comandaName_3;
		}

		if (comanda == Comanda.white) {
			txt = Main.comandaName_4;
		}

		return txt;
	}
	void Control () {
		Animate ();
		if (health > 0) {
			RedPlayer.Command cmd = new RedPlayer.Command();

			GreenPlayer.Command cmd_1 = new GreenPlayer.Command();

			BluePlayer.Command cmd_2 = new BluePlayer.Command();

			WhitePlayer.Command cmd_3 = new WhitePlayer.Command();

			if (GetComandaName() == "Red") {
				if (GetSpawn().ToDoRedPlayer().Length > selfNumber) {
					cmd = GetSpawn().ToDoRedPlayer()[selfNumber];
				}
				if (cmd.toDo == "Go") {
					GoTo(new Vector3(cmd.first_float, 0, cmd.second_float));
				}
				if (cmd.toDo == "Protect") {
					Protect();
				}
				if (cmd.toDo == "Attack") {
					if (thisIsInInt == 0) {
						Debug.Log("WarOk");
					}
					int type = 0;
					bool far = false;
					if (thisIsInInt == 0) {
						type = Random.Range(0, 1);
					}
					if (thisIsInInt == 1 || thisIsInInt == 2) {
						far = true;
						type = 0;
					}
					if (thisIsInInt == 3) {
						type = Random.Range(0, 2);
					}
					Attack(type, far, GetSpawn().GetEnemies()[Mathf.RoundToInt(cmd.first_float)]);
				}
				if (cmd.toDo == "Port" && thisIsInInt == 1) {
					ToTeleport(new Vector3(cmd.first_float, 0, cmd.second_float));
				}
			}
			if (GetComandaName() == "Green") {
				if (GetSpawn().ToDoGreenPlayer().Length > selfNumber) {
					cmd_1 = GetSpawn().ToDoGreenPlayer()[selfNumber];
				}

				if (cmd_1.toDo == "Go") {
					GoTo(new Vector3(cmd_1.first_float, 0, cmd_1.second_float));
				}
				if (cmd_1.toDo == "Protect") {
					Protect();
				}
				if (cmd_1.toDo == "Attack") {
					if (thisIsInInt == 0) {
						Debug.Log("WarOk");
					}
					int type = 0;
					bool far = false;
					if (thisIsInInt == 0) {
						type = Random.Range(0, 1);
					}
					if (thisIsInInt == 1 || thisIsInInt == 2) {
						far = true;
						type = 0;
					}
					if (thisIsInInt == 3) {
						type = Random.Range(0, 2);
					}
					if (!((GetSpawn().GetEnemies().Length - 1) < Mathf.RoundToInt(cmd_1.first_float))) {
						Attack(type, far, GetSpawn().GetEnemies()[Mathf.RoundToInt(cmd_1.first_float)]);
					}
				}
				if (cmd_1.toDo == "Port" && thisIsInInt == 1) {
					ToTeleport(new Vector3(cmd_1.first_float, 0, cmd_1.second_float));
				}
			}
			if (GetComandaName() == "Blue") {
				if (GetSpawn().ToDoBluePlayer().Length > selfNumber) {
					cmd_2 = GetSpawn().ToDoBluePlayer()[selfNumber];
				}

				if (cmd_2.toDo == "Go") {
					GoTo(new Vector3(cmd_2.first_float, 0, cmd_2.second_float));
				}
				if (cmd_2.toDo == "Protect") {
					Protect();
				}
				if (cmd_2.toDo == "Attack") {
					if (thisIsInInt == 0) {
						Debug.Log("WarOk");
					}
					int type = 0;
					bool far = false;
					if (thisIsInInt == 0) {
						type = Random.Range(0, 1);
					}
					if (thisIsInInt == 1 || thisIsInInt == 2) {
						far = true;
						type = 0;
					}
					if (thisIsInInt == 3) {
						type = Random.Range(0, 2);
					}
					Attack(type, far, GetSpawn().GetEnemies()[Mathf.RoundToInt(cmd_2.first_float)]);
				}
				if (cmd_2.toDo == "Port" && thisIsInInt == 1) {
					ToTeleport(new Vector3(cmd_2.first_float, 0, cmd_2.second_float));
				}
			}
			if (GetComandaName() == "White") {
				if (GetSpawn().ToDoWhitePlayer().Length > selfNumber) {
					cmd_3 = GetSpawn().ToDoWhitePlayer()[selfNumber];
				}

				if (cmd_3.toDo == "Go") {
					GoTo(new Vector3(cmd_3.first_float, 0, cmd_3.second_float));
				}
				if (cmd_3.toDo == "Protect") {
					Protect();
				}
				if (cmd_3.toDo == "Attack") {
					if (thisIsInInt == 0) {
						Debug.Log("WarOk");
					}
					int type = 0;
					bool far = false;
					if (thisIsInInt == 0) {
						type = Random.Range(0, 1);
					}
					if (thisIsInInt == 1 || thisIsInInt == 2) {
						far = true;
						type = 0;
					}
					if (thisIsInInt == 3) {
						type = Random.Range(0, 2);
					}
					Attack(type, far, GetSpawn().GetEnemies()[Mathf.RoundToInt(cmd_3.first_float)]);
				}
				if (cmd_3.toDo == "Port" && thisIsInInt == 1) {
					ToTeleport(new Vector3(cmd_3.first_float, 0, cmd_3.second_float));
				}
			}
			if (attacking) {
				Stay();
			}
			if (thisIsInInt == 0 && underProtection > Time.time) {
				Stay();
			}
		} else {
			Die();
		}
	}
	float underProtection;
	void Protect () {
		if (!attacking) {
			if (thisIsInInt == 1 && !GameObject.FindWithTag ("Sphere" + comanda)) {
				GameObject sphere = (Instantiate(Resources.Load("Sphere" + comanda) as GameObject) as GameObject);
				sphere.tag = "Sphere" + comanda;
				sphere.transform.position = trans.position + Vector3.up;
				Destroy(sphere, 5);
				anims.SetInteger ("AttackInt", 2);
			}
			if (thisIsInInt != 1) {
				Stay();
			}
			if (thisIsInInt == 0) {
				underProtection = Time.time + 1;
			}
		}
	}
	void Stay () {
		agent.SetDestination (trans.position);
	}
	void GoTo (Vector3 to) {
		if (!attacking) {
			agent.SetDestination (to);
		}
	}
	void Die () {
		anims.SetBool ("Dead", true);
		Destroy (gameObject, 5);
		Destroy (agent);
		Destroy (this);
	}
	void Animate () {
		anims.SetBool ("Stay", !(agent.velocity.magnitude > 0.1f));
		anims.SetBool ("Attack", attacking);
		if (thisIsInInt == 0 && underProtection > Time.time) {
			anims.SetInteger ("AttackInt", 2);
		} else {
			if (anims.GetInteger("AttackInt") == 2) {
				anims.SetInteger ("AttackInt", 0);
			}
		}
		if (thisIsInInt == 1) {
			anims.SetBool ("Port", port);
		}
	}
	public void ApplyDamage (float damage_get) {
		if (thisIsInInt == 0 && underProtection > Time.time) {
			damage_get /= 8;
		}
		health -= damage_get;

	}
	void Attack (int type, bool far, Character at) {
		bool may = thisIsInInt != 0 || (thisIsInInt == 0 && underProtection < Time.time);
		if (timeToAttack < Time.time && may) {

			if (thisIsInInt == 1) {
				LookAt(at.trans.position);
				FireBall fireball = (Instantiate(Resources.Load("Fireball") as GameObject) as GameObject).GetComponent<FireBall>();
				if (SphereNear()) {
					fireball.damage = damage;
				} else {
					fireball.damage = damage * 2;
				}
				fireball.transform.position = trans.position + Vector3.up * 1.5f;
				fireball.target = at;
				anims.SetInteger ("AttackInt", type);
				timeToAttack = Time.time + attackClip.length;
			} else {
				if ((Vector3.Distance (at.trans.position, trans.position) < 1.5f || far)) {
					LookAt(at.trans.position);
					at.ApplyDamage (damage);
					anims.SetInteger ("AttackInt", type);
					timeToAttack = Time.time + attackClip.length;
					if (thisIsInInt == 2) {
						GameObject arrow = (Instantiate(Resources.Load("Arrow") as GameObject) as GameObject);
						arrow.transform.position = trans.position + Vector3.up * 1.5f;
						arrow.transform.LookAt(at.trans.position + Vector3.up * 1.5f);
						arrow.GetComponent<FireBall>().damage = damage;
						Destroy(arrow, Vector3.Distance(trans.position, at.trans.position) / 64);
					}
				} else {
					GoTo(at.trans.position);
				}
			}
		}
	}
	float timeToAttack = 0;
	void ToTeleport (Vector3 at) {
		if (!port) {
			portAt = at;
			port = true;
			Invoke ("Teleport", 1);
			anims.SetInteger ("AttackInt", 1);
		}
	}
	Vector3 portAt;
	void Teleport () {
		Vector3 plus = new Vector3 (Random.Range (-1.0f, 1.0f), 0, Random.Range (-1.0f, 1.0f));
		agent.Warp (portAt + plus);
		port = true;
		anims.SetInteger ("AttackInt", 1);
		Invoke ("OutPort", 1);
	}
	bool port = false;
	void LookAt (Vector3 at) {
		trans.LookAt (new Vector3(at.x, trans.position.y, at.z));
	}
	void OutAttack () {
		attacking = false;
	}
	void OutPort () {
		port = false;
	}
}














