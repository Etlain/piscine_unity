using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

	public Bird bird;

	[HideInInspector]
	private GameObject pipe;

	// Use this for initialization
	void Start () {
		pipe = this.gameObject;
	}

	// Update is called once per frame
	void Update () {
		// mouvement pipe
		pipe.transform.Translate(Vector3.left * Time.deltaTime); // -7, 7
		if (pipe.transform.position.x <= -7)
			pipe.transform.Translate(Vector3.right * 14);
		else if (bird.transform.position.x <= pipe.transform.position.x + 1 && bird.transform.position.x >= pipe.transform.position.x - 1)
		{
			if (bird.transform.position.y > pipe.transform.position.y + 1 || bird.transform.position.y < pipe.transform.position.y - 1)
			{
				bird.transform.Translate(Vector3.down * (bird.transform.position.y - bird.ground.transform.position.y));
				bird.dead();
			}
		}
	}

}
