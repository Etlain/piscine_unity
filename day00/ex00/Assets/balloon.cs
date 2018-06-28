﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloon : MonoBehaviour {

	public float LevelScale;
	public float MaxScale;
	public float UpScale;
	public float LoseScale = 0.2f;


	public int CostStamina;
	public int MaxStamina;
	public int RecoverStamina;
	public float RecoverTimeStamina = 1.0f;

	[HideInInspector]
	private GameObject baloon;
	private int Stamina;
	private float ElapsedTimeStamina = 0f;

	// Use this for initialization
	void Start () {
		baloon = this.gameObject; //GameObject.Find("baloon"); // this.gameObject
		Stamina = MaxStamina;
	}

	// Update is called once per frame
	void Update () {
		ElapsedTimeStamina += Time.deltaTime;
		if (Input.GetKeyDown (KeyCode.Space)) {
			Stamina = Stamina - CostStamina;
			if (Stamina >= 0)
				baloon.transform.localScale += new Vector3 (UpScale, UpScale, 0);
			else
				Stamina = 0;
			if (baloon.transform.localScale.x > MaxScale) {
				Debug.Log("Balloon life time: "+Mathf.RoundToInt(Time.time)+"s");
				Destroy(baloon,0);
			}
			ElapsedTimeStamina = 0;
		} else if (ElapsedTimeStamina >= RecoverTimeStamina)
		{
			if (Stamina < MaxStamina)
				Stamina += RecoverStamina;
			else if (Stamina > MaxStamina)
				Stamina = MaxStamina;
			baloon.transform.localScale -= new Vector3 (LoseScale, LoseScale, 0);
			if (baloon.transform.localScale.x < 0) {
				Debug.Log("Balloon life time: "+Mathf.RoundToInt(Time.time)+"s");
				Destroy (baloon, 0);
			}
			ElapsedTimeStamina -= RecoverTimeStamina;
			Debug.Log(Stamina);
		}
	}
}
