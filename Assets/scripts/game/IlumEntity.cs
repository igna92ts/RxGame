using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlumEntity : MonoBehaviour {

	private float start;
	private float end;
	private float t = 0.0f;

	public Light light;

	void Start() {

		start = light.intensity;
 
		end = light.intensity/5;
	}

	void Update () {
			t += Time.deltaTime;
			if (light.intensity <= end) {
				light.intensity = Mathf.Lerp (start, end, t / 2);
			} 
			// if (light.intensity >= start){
			// 	light.intensity = Mathf.InverseLerp (start, end, t / 2);
			// }
			
	}
}
