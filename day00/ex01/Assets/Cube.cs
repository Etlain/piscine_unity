using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	// Use this for initialization
	//public GameObject line;

	[HideInInspector]
	private int speed = 3;
	private int maxSpeed = 5;
	private int minSpeed = 3;

	void Start () {
		speed = Random.Range(minSpeed, maxSpeed + 1);
		//print(this.gameObject.name);
	}

	// Update is called once per frame
	void Update () {
		this.gameObject.transform.Translate(Vector3.down * speed * Time.deltaTime);
		KeyEvent();
	}

	void KeyEvent()
	{
		IsKey("a(Clone)", "a");
		IsKey("s(Clone)", "s");
		IsKey("d(Clone)", "d");
	}

	void IsKey(string name, string key)
	{
		if (this.gameObject.name == name && Input.GetKeyDown(key))
		{
			print(name);
			Destroy(this.gameObject);
		}
	}

	void OnBecameInvisible() {
		//Debug.Log("destroy");
   		Destroy(this.gameObject);
	}
}
