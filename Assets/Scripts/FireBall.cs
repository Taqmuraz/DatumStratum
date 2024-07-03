using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {
	public float damage = 15;
	public Character target;
	public Transform trans;
	Vector3 last;
	bool tacked;
	Character.Comanda cmd;
	public bool arrow = false;

	// Use this for initialization
	void Start () {
		trans = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (!arrow) {
			if (!tacked) {
				if (target) {
					cmd  = target.comanda;
					trans.position += ((target.trans.position + Vector3.up * 1.5f) - (trans.position)).normalized * Time.deltaTime * 10;
					if (Vector3.Distance (trans.position, target.trans.position + Vector3.up * 1.5f) < 1) {
						target.ApplyDamage (damage);
						tacked = true;
					}
					last = target.trans.position;
				} else {
					Character[] chars = Spawn.GetCharsFromComanda(cmd);
					if (chars.Length > 0) {
						float dist = 200;
						int index = -1;
						for (int i = 0; i < chars.Length; i++) {
							if ((trans.position - chars[i].trans.position).magnitude < dist) {
								index = i;
								dist = (trans.position - chars[i].trans.position).magnitude;
							}
						}
						if (index > -1) {
							target = chars[index];
						}
					}
				}
			} else {
				trans.localScale = Vector3.one * 3;
				Destroy (gameObject, 0.25f);
			}
		} else {
			trans.position += trans.forward * 64 * Time.deltaTime;
		}
		damage -= Time.deltaTime / 4;
		damage = Mathf.Clamp (damage, 1, 45);
	}
}











