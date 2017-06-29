using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Animator anim;
	private SpriteRenderer renderer;
   	private float _nextUse = 0.0f;
	
	public bool IsCooldownDone{
		get{
			return Time.time >= _nextUse;
		}
	}
   
   public void TriggerCooldown(float cd){
     _nextUse = Time.time + cd;
   }


	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();
		renderer = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

		

		if(IsCooldownDone) {
			if(Input.GetKeyDown(KeyCode.Space)) {
				anim.Play("Attack");
				TriggerCooldown(0.1f);
			}
			if(CheckRoll()) {
				anim.Play("Roll");
				TriggerCooldown(0.7f);
			} else
				Walk();
		}

		// Walk();
	}

	void Walk() {
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
			anim.Play("Walk");
		}

		if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D)) {
			anim.Play("Idle");
		} 

		if(Input.GetKey(KeyCode.W)) {
			this.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 0.1f), 2 * Time.deltaTime);
		} else if (Input.GetKey(KeyCode.S)) {
			this.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - 0.1f), 2 * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.A)) {
			this.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 0.1f, transform.position.y), 2 * Time.deltaTime);
			renderer.flipX = true;
		} else if (Input.GetKey(KeyCode.D)) {
			renderer.flipX = false;
			this.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 0.1f, transform.position.y), 2 * Time.deltaTime);
		}

	}

 	private float lastTapTime = 0.0f;
    private float catchTime = 0.25f;
	private KeyCode currentKey = KeyCode.None;
	private KeyCode previousKey = KeyCode.None;

	public bool CheckRoll() {
		if(Input.GetKeyDown(KeyCode.W)) {
			currentKey = KeyCode.W;
		} else if (Input.GetKeyDown(KeyCode.A)) {
			currentKey = KeyCode.A;
		} else if (Input.GetKeyDown(KeyCode.S)) {
			currentKey = KeyCode.S;
		} else if (Input.GetKeyDown(KeyCode.D)) {
			currentKey = KeyCode.D;
		}

		if (currentKey != KeyCode.None) {
			if(Time.time - lastTapTime < catchTime && previousKey == currentKey){
				//double click
				lastTapTime = Time.time;
				previousKey = currentKey;
				currentKey = KeyCode.None;
				return true;
			}else{
				previousKey = currentKey;
				currentKey = KeyCode.None;
				lastTapTime = Time.time;
				return false;
			}
		} else
			return false;
	}

}
