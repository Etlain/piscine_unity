using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript_ex01 : MonoBehaviour {

	public camera	cam;
	public int		speed = 2;
	public int		jump = 10;

	[HideInInspector]
	private playerScript_ex01 player;
	private bool 			  b_jump = false;
	private static int 		  selected_player = 0;
	private int 			  id_player = 1;
	private bool			  b_camera = true;
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
		//print("selected_player : "+selected_player+"id_player : "+id_player);
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

			// modifier personnage suivie par la camera
			if (b_camera)
			{
				cam.setOffset(this.gameObject);
				b_camera = false;
			}
		}
		else if (Input.GetKey("r"))
		{
			selected_player = 0;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else if (Input.GetKey("a"))
		{
			selected_player = 1;
			b_camera = true;
		}
		else if (Input.GetKey("z"))
		{
			selected_player = 2;
			b_camera = true;
		}
		else if (Input.GetKey("e"))
		{
			selected_player = 3;
			b_camera = true;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
			b_jump = false;
		else if (collision.gameObject.tag == "Player")
			b_jump = false;
	}
}
