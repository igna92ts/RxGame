using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Observer {

	// Use this for initialization
	Player player;
	SpriteRenderer renderer;
	private Vector2 knockBackVec;
	private bool knockBack = false;
	private Animator anim;
	private int monsterLife = 20;
	private bool hitRecently = false;
	[Observing("PlayerStore")] int playerScore;
	void Start () {
		player = GameObject.FindObjectOfType<Player>();
		renderer = this.GetComponent<SpriteRenderer>();
		anim = this.GetComponent<Animator>();
	}

	void OnEnable() {
		hitRecently = false;
		knockBack = false;
		monsterLife = 20;
	}
	
	// Update is called once per frame
	void Update () {
		//this.transform.position =  Vector3.Lerp(this.transform.position, player.transform.position, 0.5f * Time.deltaTime);

		// if(knockBack) {
		// 	// if ((Vector2)transform.position == knockBackVec) knockBack = false;
		// 	// this.transform.position = Vector2.Lerp(this.transform.position, knockBackVec, 1f * Time.deltaTime);
		// 	var distance = Vector3.Distance(transform.position, knockBackVec);
   		// 	// anim.Play("Idle");
		// 	if(distance < 0.5f)
   		// 		knockBack = false;
		// } else {
			
		// }
		anim.Play("Walk Goo");
			Move();

		if (Vector2.Distance(this.transform.position, player.transform.position) > 20) {
			this.gameObject.SetActive(false);
			EnemyStore.Instance.Set<int>("currentEnemies", currentEnemies - 1);
		}
	}

	void Move() {
		this.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, 0.5f * Time.deltaTime);

		if(this.transform.position.x > player.transform.position.x) {
			transform.localScale = new Vector3(-1, 1, 1);
		} else {
			transform.localScale = new Vector3(1, 1, 1);
		}
	}

	[Observing("EnemyStore")] int currentEnemies;
	[Observing("EnemyStore")] int maxEnemies;
	void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Weapon" && !hitRecently) {
			this.knockBack = true;
			// this.knockBackVec.Normalize();
			// We then get the opposite (-Vector3) and normalize it

			this.monsterLife -= 10;
			var ps = GetComponentInChildren<ParticleSystem>();
			ps.Emit(3);
			StartCoroutine("Hit");
			if (this.monsterLife <= 0) {
				PlayerStore.Instance.Set<int>("playerScore", playerScore + 50);
 				EnemyStore.Instance.Set<int>("currentEnemies", currentEnemies - 1);
				knockBack = false;
				if (playerScore / 100 > maxEnemies) {
					EnemyStore.Instance.Set<int>("maxEnemies", maxEnemies + 1 );
				}
				this.gameObject.SetActive(false);
			}
		}
    }
	IEnumerator Hit() {
		hitRecently = true;
		yield return new WaitForSeconds(0.1f);
		hitRecently = false;
	}
}
