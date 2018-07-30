using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

	// Use this for initialization

//	public GameObject player;

	[HideInInspector]
	//private Vector3 playerPosition = new Vector3(0, 0, 0);
	private GameObject player = null;
	private Vector3 offset = new Vector3(0, 0, 0);

	void Start () {
		//offset = this.transform.position - player.transform.position;
	}

	// LateUpdate is called once per frame
	void Update () {
		if (player)
			this.transform.position = player.transform.position + offset;
		//print("pos cam :"+);
	}

	public void setOffset(GameObject player)
	{
		this.player = player;
		this.transform.position = player.transform.position + offset + new Vector3(0, 0, this.transform.position.z);
		offset = this.transform.position - player.transform.position;
		//print("offset : "+pl);
		//player = player;
	}
}
