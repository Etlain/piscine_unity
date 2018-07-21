using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Club club;
	public GameObject up_border;
	public GameObject down_border;
	public GameObject hole;

	[HideInInspector]

	bool start_speed_ball = false;
	float ball_speed = 0;
	bool direction_up = true;
	private GameObject ball;
	bool game = true;

	// Use this for initialization
	void Start () {
		ball = this.gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (club.move_ball)
		{
			if (!start_speed_ball)
			{
				ball_speed = club.force_distance * (100.0f);
				if (ball_speed < 0)
					ball_speed = ball_speed * -1;
				//start_speed_ball = true; // modifier plus bas pour gerer le score
				if (!club.ball_up_hole)
					direction_up = true;
				else
					direction_up = false;
			}
			float ball_distance = ball_speed * Time.deltaTime;
			if (direction_up)
			{
				if (ball_speed < 10.0f && ball.transform.localPosition.y <= hole.transform.localPosition.y && ball.transform.localPosition.y > hole.transform.localPosition.y - 0.7)
				{
					ball.transform.localScale = new Vector3 (0, 0, 0);
					ball_speed = 0;
					if (club.score <= 0)
						Debug.Log("Victory :)");
					club.score-=5;
					game = false;
				}
				else if (up_border.transform.localPosition.y > ball_distance + ball.transform.localPosition.y)
					ball.transform.Translate(Vector3.up * ball_distance);
				else
				{
					ball.transform.Translate(Vector3.up * (up_border.transform.localPosition.y - ball.transform.localPosition.y));
					direction_up = false;
				}
			}
			else
			{
				if (ball_speed < 10.0f && ball.transform.localPosition.y <= hole.transform.localPosition.y + 0.7 && ball.transform.localPosition.y > hole.transform.localPosition.y)
				{
					ball.transform.localScale = new Vector3 (0, 0, 0);
					ball_speed = 0;
					if (club.score <= 0)
						Debug.Log("Victory :)");
					else
						Debug.Log("Defeat :(");
					club.score-=5;
					game = false;
				}
				else if (down_border.transform.localPosition.y < ball.transform.localPosition.y - ball_distance)
					ball.transform.Translate(Vector3.down * ball_distance);
				else
				{
					ball.transform.Translate(Vector3.down * (ball.transform.localPosition.y - down_border.transform.localPosition.y));
					direction_up = true;
				}
			}
			if (!start_speed_ball && game)
			{
				club.score+=5;
				Debug.Log("Score : "+club.score);
				start_speed_ball = true;
			}
			ball_speed = ball_speed - 1.0f;
			if (ball_speed < 0)
			{
				club.force_distance = 0;
				club.move_ball = false;
				start_speed_ball = false;
				if (game)
					club.transform.localPosition = new Vector3(club.transform.localPosition.x, ball.transform.localPosition.y, -1);
			}
			if (ball.transform.localPosition.y > hole.transform.localPosition.y)
				club.ball_up_hole = true;
			else
				club.ball_up_hole = false;
		}
	}
}
