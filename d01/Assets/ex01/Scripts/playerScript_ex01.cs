using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript_ex01 : MonoBehaviour {

	public camera	cam;
	public int		speed = 2;
	public int		jump = 10;

	[HideInInspector]
	private GameObject		  player;

	private bool 			  b_jump = false;
	private bool			  b_camera = true;

	private static int 		  selected_player = 0;
	private int 			  id_player = 1;

	private const int		  numberCharacter = 3;
	private static bool[]	  tab_finished = new bool[numberCharacter];


	void initTabFinished()
	{
		int i;

		i = 0;
		while (i < numberCharacter)
		{
			tab_finished[i] = false;
			i++;
		}
	}

	// Use this for initialization
	void Start () {
		player = this.gameObject;
		if (selected_player == 0)
			initTabFinished();
		//print(selected_player2);
		if (this.tag == "Thomas")
			id_player = 0;
		else if (this.tag == "John")
			id_player = 1;
		else if (this.tag == "Claire")
			id_player = 2;
	}

	void FixedUpdate () {
		//print("selected_player : "+selected_player+"id_player : "+id_player);
		if (selected_player == id_player)
		{
			if (Input.GetKey("left"))
				//player.GetComponent<Rigidbody2D>().AddForce(Vector3.left * speed, ForceMode2D.Force);
				player.transform.Translate(Vector3.left * speed * Time.deltaTime);
			else if (Input.GetKey("right"))
				//player.GetComponent<CharacterController>().Move();
				//player.GetComponent<Rigidbody2D>().MovePosition(player.GetComponent<Rigidbody2D>().position + Vector2.right * speed * Time.deltaTime);
				player.transform.Translate(Vector3.right * speed * Time.deltaTime);
			// application load level for restart
			if (Input.GetKeyDown("space") && b_jump == false)
			{
				//print("jump");
				player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
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
		else if (Input.GetKey("1") || Input.GetKey("a"))
		{
			selected_player = 0;
			b_camera = true;
		}
		else if (Input.GetKey("2") || Input.GetKey("z"))
		{
			selected_player = 1;
			b_camera = true;
		}
		else if (Input.GetKey("3") || Input.GetKey("e"))
		{
			selected_player = 2;
			b_camera = true;
		}
		else if (Input.GetKey("n"))
		{
			Debug.Log("n");
			NextLevel();
		}
		if (IsVictory())
		{
			Debug.Log("Victory");
			NextLevel();
		}
	}


	void OnCollisionEnter2D(Collision2D collision)
	{
		//print("On collision enter");
		if (collision.gameObject.tag == "Ground")
		{
				//print("player pos y : "+(player.transform.position.y - player.transform.localScale.y)+"collision gameobject y : "+(collision.gameObject.transform.position.y));
				//print("player : (" + player.transform.position.y + ", " + player.transform.localScale.y + ", "+player.GetComponent<Collider2D>().bounds.size.y+")");
				//print("object : (" + collision.gameObject.transform.position.y + ", " + collision.gameObject.transform.localScale.y + ")");
				//print("stop jump");
				player.transform.parent = collision.gameObject.transform;
				//print("player pos y : "+(player.transform.position.y - player.GetComponent<Collider2D>().bounds.size.y / 2)+"collision gameobject y : "+(collision.gameObject.transform.position.y + collision.gameObject.GetComponent<Collider2D>().bounds.size.y));
				if (player.transform.position.y - player.GetComponent<Collider2D>().bounds.size.y / 2 > collision.gameObject.transform.position.y)
					b_jump = false;
			//}
		}
		else if (collision.gameObject.tag == "Wall")
			player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		else if (collision.gameObject.tag == "Claire" || collision.gameObject.tag == "Thomas" || collision.gameObject.tag == "John")
			b_jump = false;
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		//print("On collision exit");
		if (collision.gameObject.tag == "Ground")
		{
			player.transform.parent = null;
			//b_jump = true;
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		/*print(collider.gameObject.tag);
		print(this.tag);*/
		if (collider.gameObject.tag == "ThomasFinish" && this.tag == "Thomas")
			tab_finished[selected_player] = true;
		else if (collider.gameObject.tag == "JohnFinish" && this.tag == "John")
			tab_finished[selected_player] = true;
		else if (collider.gameObject.tag == "ClaireFinish" && this.tag == "Claire")
			tab_finished[selected_player] = true;
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		/*print(collider.gameObject.tag);
		print(this.tag);*/
		if (collider.gameObject.tag == "ThomasFinish" && this.tag == "Thomas")
			tab_finished[selected_player] = false;
		if (collider.gameObject.tag == "JohnFinish" && this.tag == "John")
			tab_finished[selected_player] = false;
		if (collider.gameObject.tag == "ClaireFinish" && this.tag == "Claire")
			tab_finished[selected_player] = false;
	}

	void NextLevel()
	{
		int idScene;

		idScene = SceneManager.GetActiveScene().buildIndex + 1;
		if (idScene >= SceneManager.sceneCountInBuildSettings)
			idScene = 0;
		SceneManager.LoadScene(idScene);
	}

	bool IsVictory()
	{
		int i;

		i = 0;
		while (i < numberCharacter)
		{
			if (tab_finished[i] == false)
				return (false);
			i++;
		}
		initTabFinished();
		return true;
	}
}
