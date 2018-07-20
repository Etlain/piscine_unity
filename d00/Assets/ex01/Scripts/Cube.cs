using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	public GameObject line_down;
	public GameObject line_up;
	// Use this for initialization
	//public GameObject line;

	[HideInInspector]
	private int speed = 3;
	private int maxSpeed = 5;
	private int minSpeed = 3;
	private float distance = 0;

	void Start () {
		float tmp = 0;

		speed = Random.Range(minSpeed, maxSpeed + 1);
		tmp = line_down.transform.position.y;
		if (tmp < 0)
			distance = tmp * -1;
		else
			distance = tmp;
		tmp = line_up.transform.position.y;
		if (tmp < 0)
			distance += tmp * -1;
		else
			distance += tmp;
		//print(this.gameObject.name);
		//print(pos_line_down_y);
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
		float precision = 0;

		if (this.gameObject.name == name && Input.GetKeyDown(key))
		{
			float key_pos_y = this.gameObject.transform.position.y;
			float line_down_y = line_down.transform.position.y;
			float line_up_y = line_up.transform.position.y;
			//print("position y key:"+key_pos_y);
			if (line_down_y > 0 && line_up_y > 0)
				precision = (key_pos_y / distance) * 100;
			else if (line_down_y < 0 && line_up_y < 0)
				precision = (key_pos_y / distance) * 100;
			else if (line_down_y < 0 && line_up_y > 0)
				precision = ((line_down_y - key_pos_y) / distance) * 100;
			Debug.Log("Precision : "+precision);
			Destroy(this.gameObject);
		}
	}

	void OnBecameInvisible() {
		//Debug.Log("destroy");
   		Destroy(this.gameObject);
	}
}
