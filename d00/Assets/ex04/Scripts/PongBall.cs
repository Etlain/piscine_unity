using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour {

	public	Player 		player1;
	public	Player 		player2;
	public int			speed = 2;

	[HideInInspector]
	private Vector3		direction;
	private PongBall	ball;
	private	bool		in_player = false;

	// Use this for initialization
	void Start () {
		ball = this;
		ball.init();
	}

	void init()
	{
		float tmp = Random.Range(0, 4);
		ball.transform.Translate(Vector3.right * ball.transform.position.x * -1);
		if (tmp == 0)
			direction = Vector3.up + Vector3.right;
		else if (tmp == 1)
			direction = Vector3.down + Vector3.right;
		else if (tmp == 2)
			direction = Vector3.up + Vector3.left;
		else
			direction = Vector3.down + Vector3.left;
	}

	bool intersection(Player player)
	{
		if (ball.transform.position.x <= player.transform.position.x + 0.8 && ball.transform.position.x >= player.transform.position.x - 0.8)
		{
			if (ball.transform.position.y <= player.transform.position.y + 1.1 && ball.transform.position.y >= player.transform.position.y - 1.1)
			{
				return (true);
			}
		}
		return (false);
	}
	// Update is called once per frame
	void Update () {
		ball.transform.Translate(direction * Time.deltaTime * speed);
		if (ball.transform.position.y <= -4.6 || ball.transform.position.y >= 4.6) // mur
		{
			direction.y *= -1;
		}
		else if (ball.transform.position.x < player1.transform.position.x - 1) // joueur 1 defeat
		{
			ball.init();
			in_player = false;
			player2.score++;
			Debug.Log("Player 1 : "+player1.score+" | Player 2 : "+player2.score);
		}
		else if (ball.transform.position.x > player2.transform.position.x + 1) // joueur 2 defeat
		{
			ball.init();
			in_player = false;
			player1.score++;
			Debug.Log("Player 1 : "+player1.score+" | Player 2 : "+player2.score);
		}
		else if (ball.intersection(player1) || ball.intersection(player2)) // intersection joueur
		{
			if (in_player == false)
			{
				direction.x *= -1;
				in_player = true;
			}
		}
		else
			in_player = false;

	}
}
