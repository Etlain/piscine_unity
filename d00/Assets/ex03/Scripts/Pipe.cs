using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

	public Bird bird;

	[HideInInspector]
	private GameObject	pipe;
	private bool		pipe_passed;
	private static int	pipe_speed;

	// Use this for initialization
	void Start () {
		pipe = this.gameObject;
		pipe_passed = false;
		pipe_speed = 3;
	}

	// Update is called once per frame
	void Update () {
		// mouvement pipe
		if (bird.alive)
		{
			pipe.transform.Translate(Vector3.left * Time.deltaTime * pipe_speed);
			if (pipe.transform.position.x <= -7)
			{
				pipe.transform.Translate(Vector3.right * 14);
				pipe_passed = false;
			}
			else if (bird.transform.position.x <= pipe.transform.position.x + 1 && bird.transform.position.x >= pipe.transform.position.x - 1)
			{
				if (bird.transform.position.y > pipe.transform.position.y + 1 || bird.transform.position.y < pipe.transform.position.y - 1.3)
				{
					bird.transform.Translate(Vector3.down * (bird.transform.position.y - bird.ground.transform.position.y));
					bird.dead();
				}
			}
			else if (!pipe_passed && bird.transform.position.x > pipe.transform.position.x + 1)
			{
				bird.score += 5;
				pipe_passed = true;
				pipe_speed++;
			}
		}
	}

}
