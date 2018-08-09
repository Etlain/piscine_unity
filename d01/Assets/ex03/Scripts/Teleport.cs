using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {


	public int		idTeleport = 0;
	public bool		isIn = false;

	private GameObject teleport;
	private static bool	isTeleport = false;
	private static GameObject player = null;

	// Use this for initialization
	void Start () {
		teleport = this.gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (isTeleport && !isIn && player && this.tag == "TeleportOut")
		{
			player.transform.position = teleport.transform.position;
			isTeleport = false;
		}

	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (this.tag == "TeleportIn")
		{
			player = collider.transform.gameObject;
			isTeleport = true;
		}
	}

}
