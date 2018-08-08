using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour {


	public int distanceMove = 5;
	public int speedMove = 5;
	public bool upMove = false;

	private GameObject	platform;
	private float		pos;
	private bool		changeDirection = false;
	// Use this for initialization
	void Start () {
		platform = this.gameObject;
		if (!upMove)
			pos = platform.transform.position.x;
		else
			pos = platform.transform.position.y;
	}

	// Update is called once per frame
	void Update () {

		if (!upMove)
			ApplyChangeDirection(platform.transform.position.x);
		else
			ApplyChangeDirection(platform.transform.position.y);
		PlatformMoveLaunch();
	}

	void ApplyChangeDirection(float xOrY)
	{
		if (xOrY > pos + distanceMove)
			changeDirection = true;
		else if (xOrY < pos)
			changeDirection = false;
	}

	void PlatformMoveLaunch()
	{
		if (!upMove)
		{
			if (!changeDirection)
				platform.transform.Translate(Vector3.right * speedMove * Time.deltaTime);
			else
				platform.transform.Translate(Vector3.left * speedMove * Time.deltaTime);
		}
		else
		{
			if (!changeDirection)
				platform.transform.Translate(Vector3.up * speedMove * Time.deltaTime);
			else
				platform.transform.Translate(Vector3.down * speedMove * Time.deltaTime);
		}
	}

}
