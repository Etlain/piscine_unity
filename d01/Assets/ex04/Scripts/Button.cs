using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	public GameObject door;

	private GameObject button;
	// Use this for initialization
	void Start () {
		button = this.gameObject;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		print(door.tag);
		if (collision.gameObject.tag == "Thomas" && door.tag == "RedDoor")
			OpenDoor();
		else if (collision.gameObject.tag == "John" && door.tag == "YellowDoor")
			OpenDoor();
		else if (collision.gameObject.tag == "Claire" && door.tag == "BlueDoor")
			OpenDoor();
	}

	void OpenDoor()
	{
		button.transform.localScale = new Vector3 (0, 0, 0);
		door.transform.localScale = new Vector3 (0, 0, 0);
	}
}
