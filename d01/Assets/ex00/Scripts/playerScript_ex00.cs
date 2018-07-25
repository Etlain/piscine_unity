using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex00 : MonoBehaviour {

	public int speed = 2;
	public int jump = 10;

	[HideInInspector]
	private playerScript_ex00 player;
	private bool 			  b_jump = false;
	private static int 		  selected_player = 0;
	private int 			  id_player = 1;
	//private static int 		  selected_player2 = 1;


	// Use this for initialization
	void Start () {
		player = this;
		//print(selected_player2);
		selected_player++;
		id_player = selected_player;

	}

	// Update is called once per frame
	void Update () {
		print("selected_player : "+selected_player+"id_player : "+id_player);
		if (selected_player == id_player)
		{
			if (Input.GetKey("left"))
				player.transform.Translate(Vector3.left * speed * Time.deltaTime);
			else if (Input.GetKey("right"))
				player.transform.Translate(Vector3.right * speed * Time.deltaTime);
			// application load level for restart
			if (Input.GetKeyDown("space") && b_jump == false)
			{
				player.GetComponent<Rigidbody2D>().AddForce(Vector3.up * jump, ForceMode2D.Impulse);
				b_jump = true;
			}
		}
		else if (Input.GetKey("a"))
			selected_player = 1;
		else if (Input.GetKey("z"))
			selected_player = 2;
		else if (Input.GetKey("e"))
			selected_player = 3;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
			b_jump = false;
	}
}
