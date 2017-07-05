using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : Observer {

	[Observing("PlayerStore")] int playerLife;

	private Animator anim;
	private SpriteRenderer renderer;
   	private float _nextUse = 0.0f;
	private bool readyToRoll = false;
	private bool hitRecently = false;
	
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
			readyToRoll = false;
			if(Input.GetKeyDown(KeyCode.Space)) {
				anim.Play("Attack");
				TriggerCooldown(0.1f);
			}
			if(CheckRoll()) {
				anim.Play("Roll");
				readyToRoll = true;
				TriggerCooldown(0.7f);
			} else
				Walk();
		}

		if (readyToRoll) {
			Roll();
		}

		// Walk();
	}

	void Walk() {
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) ||
		   Input.GetKey(KeyCode.D) || CrossPlatformInputManager.GetAxis("Horizontal") != 0 || CrossPlatformInputManager.GetAxis("Vertical") != 0) {
			anim.Play("Walk");
		}

		if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) &&
		   !Input.GetKey(KeyCode.D) && CrossPlatformInputManager.GetAxis("Vertical") == 0 && CrossPlatformInputManager.GetAxis("Horizontal") == 0) {
			anim.Play("Idle");
		} 

		if(Input.GetKey(KeyCode.W) || CrossPlatformInputManager.GetAxis("Vertical") > 0) {
			this.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 0.1f), 2 * Time.deltaTime);
		} else if (Input.GetKey(KeyCode.S) || CrossPlatformInputManager.GetAxis("Vertical") < 0) {
			this.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - 0.1f), 2 * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.A) || CrossPlatformInputManager.GetAxis("Horizontal") < 0) {
			this.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 0.1f, transform.position.y), 2 * Time.deltaTime);
			transform.localScale = new Vector3(-1, 1, 1);
		} else if (Input.GetKey(KeyCode.D) || CrossPlatformInputManager.GetAxis("Horizontal") > 0) {
			transform.localScale = new Vector3(1, 1, 1);
			this.transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + 0.1f, transform.position.y), 2 * Time.deltaTime);
		}

		Debug.Log("HORIZONTAL:" + CrossPlatformInputManager.GetAxis("Horizontal"));
		Debug.Log("vERTICAL:" + CrossPlatformInputManager.GetAxis("Vertical"));

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

	public void Roll() {
		var newVec = this.transform.position;
		if((Input.GetKey(KeyCode.W) && previousKey != KeyCode.S) || previousKey == KeyCode.W) {
			newVec.y += 1;
		}
		if ((Input.GetKey(KeyCode.A) && previousKey != KeyCode.D) || previousKey == KeyCode.A) {
			// renderer.flipX = true;
			transform.localScale = new Vector3(-1, 1, 1);
			newVec.x -=1;
		}
		if ((Input.GetKey(KeyCode.S) && previousKey != KeyCode.W) || previousKey == KeyCode.S) {
			newVec.y -= 1;
		}
		if ((Input.GetKey(KeyCode.D) && previousKey != KeyCode.A) || previousKey == KeyCode.D) {
			// renderer.flipX = false;
			transform.localScale = new Vector3(1, 1, 1);
			newVec.x += 1;
		}

		this.transform.position = Vector2.MoveTowards(transform.position, newVec, 3.5f * Time.deltaTime);
	}

	void OnCollisionStay2D(Collision2D coll) {
		if(!hitRecently) {
			PlayerStore.Instance.Set<int>("playerLife", playerLife - 10);
			StartCoroutine("CollideFlash");
		}
    }

	IEnumerator CollideFlash() {
		var renderer = this.GetComponent<SpriteRenderer>();
		hitRecently = true;
		renderer.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		renderer.color = Color.white;
		for (int i = 0; i < 3; i++) {
			renderer.enabled = false;
			yield return new WaitForSeconds(.2f);
			renderer.enabled = true;
			yield return new WaitForSeconds(.2f);
		}           
		hitRecently = false;
	}

}
