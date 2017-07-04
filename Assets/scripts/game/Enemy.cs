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
	[Observing("PlayerStore")] int playerScore;
	void Start () {
		player = GameObject.FindObjectOfType<Player>();
		renderer = this.GetComponent<SpriteRenderer>();
		anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//this.transform.position =  Vector3.Lerp(this.transform.position, player.transform.position, 0.5f * Time.deltaTime);

		if(knockBack) {
			if ((Vector2)transform.position == knockBackVec) knockBack = false;
			this.transform.position = Vector2.Lerp(this.transform.position, knockBackVec, 1f * Time.deltaTime);
			var distance = Vector3.Distance(transform.position, knockBackVec);
   			// anim.Play("Idle");
			if(distance < 0.5f)
   				knockBack = false;
		} else {
			anim.Play("Walk Goo");
			Move();
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

	void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Weapon") {
			this.knockBack = true;
			this.knockBackVec = (this.transform.position - coll.gameObject.transform.position);
			this.knockBackVec.Normalize();
			this.monsterLife -= 10;

			if (this.monsterLife <= 0) {
				PlayerStore.Instance.Set<int>("playerScore", playerScore + 10);
				Destroy(this.gameObject);
			}
		}
    }
}
