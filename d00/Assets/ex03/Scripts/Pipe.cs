using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

	public GameObject bird;

	[HideInInspector]
	private GameObject pipe;

	// Use this for initialization
	void Start () {
		pipe = this.gameObject;
	}

	// Update is called once per frame
	void Update () {
		// mouvement pipe
		/*pipe.transform.Translate(Vector3.left * Time.deltaTime); // -7, 7
		//print("camera :"+Camera.main.rect.x);
		//print("pipe pos: "+pipe.transform.position.x);
		if (pipe.transform.position.x <= -7)
			pipe.transform.Translate(Vector3.right * 14);
		else if (bird.transform.position.x <= pipe.transform.position.x + 1 && bird.transform.position.x >= pipe.transform.position.x - 1)
		{
			if (bird.transform.position.y <= pipe.transform.position.y + 0.5 && bird.transform.position.y >= pipe.transform.position.y - 0.5)
			{
				print("bird in pipe");
			}
			//print("bird with pipe");
		}
		else
			print("finish");*/
		//	print("i m visible");
		//if (pipe.transform.localPosition.x < )
		// gestion collision avec l'oiseau
	}

	void OnBecameInvisible()
    {
        //enabled = false;
    }

}
