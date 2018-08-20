using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button2 : MonoBehaviour {

	public int			id;
	public bool			isDoor = false;
	public bool			isPlatform = false;

	private GameObject	obj;
	private static int	id_active = 0;

												// Thomas red, John yellow, Claire blue
	private static bool[] tabColor = new bool[3] {false, false, false};

	// Use this for initialization
	void Start () {
		obj = this.gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (id_active == id)
		{
			if (isDoor)
			{
				if (tabColor[0] && obj.tag == "RedDoor")
				{
					OpenDoor(obj);
					id_active = 0;
					InitTabColor();
				}
				else if (tabColor[1] && obj.tag == "YellowDoor")
				{
					OpenDoor(obj);
					id_active = 0;
					InitTabColor();
				}
				else if (tabColor[2] && obj.tag == "BlueDoor")
				{
					OpenDoor(obj);
					id_active = 0;
					InitTabColor();
				}
			}
			else if (isPlatform)
			{
				if (tabColor[0])
				{
					//OpenDoor(obj);
					obj.GetComponent<SpriteRenderer>().color = Color.red;
					obj.layer = 9;
					id_active = 0;
					InitTabColor();
				}
				else if (tabColor[1])
				{
					//OpenDoor(obj);
					obj.GetComponent<SpriteRenderer>().color = Color.yellow;
					obj.layer = 11;
					id_active = 0;
					InitTabColor();
				}
				else if (tabColor[2])
				{
					//OpenDoor(obj);
					obj.GetComponent<SpriteRenderer>().color = Color.blue;
					obj.layer = 10;
					id_active = 0;
					InitTabColor();
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		//print(obj.tag);
		if (isDoor || isPlatform)
			return ;
		if (collision.gameObject.tag == "Thomas")
		{
			obj.GetComponent<SpriteRenderer>().color = Color.red;
			id_active = id;
			tabColor[0] = true;
		}
		else if (collision.gameObject.tag == "John")
		{
			obj.GetComponent<SpriteRenderer>().color = Color.yellow;
			id_active = id;
			tabColor[1] = true;
		}
		else if (collision.gameObject.tag == "Claire")
		{
			obj.GetComponent<SpriteRenderer>().color = Color.blue;
			id_active = id;
			tabColor[2] = true;
		}
		//else if (obj.gameObject.tag == "RedDoor" )

		// selon la couleur du personnage on change la couleur de cette interrupteur
		// selon la couleur de l interrupteur on ouvre les portes concernee
	}

	void InitTabColor()
	{
		int i;

		i = 0;
		while (i < 3)
		{
			tabColor[i] = false;
			i++;
		}
	}

	void OpenDoor(GameObject door)
	{
		//button.transform.localScale = new Vector3 (0, 0, 0);
		door.transform.localScale = new Vector3 (0, 0, 0);
	}

}
