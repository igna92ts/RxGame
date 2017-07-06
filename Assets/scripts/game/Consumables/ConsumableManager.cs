using System.Collections;
using System.Collections.Generic;
using Rx;
using UnityEngine;

public class ConsumableManager : Observer {

	public static ConsumableManager _instance;
	public static ConsumableManager Instance
	{
		get {
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<ConsumableManager>();
				
				if (_instance == null)
				{
					GameObject container = new GameObject("ConsumableManager");
					_instance = container.AddComponent<ConsumableManager>();
				}
			}
		
			return _instance;
		}
	}

	private List<Heal> heals = new List<Heal>();
	private List<StrengthUp> strUps = new List<StrengthUp>();
	private List<LifeUp> lifeUps = new List<LifeUp>();
	public LifeUp lifeUp;
	public StrengthUp strUp;
	public Heal healItem;
	[Observing("PlayerStore")] int maxHearts;
	void Start () {
		for (int i = 0; i < 5; i++) {
			var life = Instantiate(lifeUp);
			life.gameObject.SetActive(false);
			var heal = Instantiate(healItem);
			heal.gameObject.SetActive(false);
			var str = Instantiate(strUp);
			str.gameObject.SetActive(false);
			strUps.Add(str);
			heals.Add(heal);
			lifeUps.Add(life);
		}
	}

	public void Drop(Vector2 dropPosition) {
		var chance = Random.Range(0, 100);

		if(chance > 85 && maxHearts < 8) {
			DropLifeUp(dropPosition);
		} else if (chance > 65 && chance < 80) {
			DropStrUp(dropPosition);
		}
	}

	public void DropLifeUp(Vector2 dropPos) {
		for (int i = 0; i < lifeUps.Count; i++) {
			if (!lifeUps[i].gameObject.activeInHierarchy) {
				lifeUps[i].transform.position = dropPos;
				lifeUps[i].transform.rotation = Quaternion.identity;
				lifeUps[i].gameObject.SetActive(true);
				break;
			}
		}
	}

	public void DropStrUp(Vector2 dropPos) {
	for (int i = 0; i < lifeUps.Count; i++) {
		if (!strUps[i].gameObject.activeInHierarchy) {
			strUps[i].transform.position = dropPos;
			strUps[i].transform.rotation = Quaternion.identity;
			strUps[i].gameObject.SetActive(true);
			break;
		}
	}
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
