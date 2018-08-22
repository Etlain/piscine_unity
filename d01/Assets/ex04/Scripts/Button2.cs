using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button2 : MonoBehaviour {

	public int			id;
	public bool			isDoor = false;
	public bool			isPlatform = false;

	private GameObject	obj;
	private static int	id_active = 0;
	private bool 		bInit = false;
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
			print("id active : "+id_active+", id :"+id);
			if (isDoor)
			{
				if (tabColor[0] && obj.tag == "RedDoor")
				{
					OpenDoor(obj);
					bInit = true;
				}
				else if (tabColor[1] && obj.tag == "YellowDoor")
				{
					OpenDoor(obj);
					bInit = true;
				}
				else if (tabColor[2] && obj.tag == "BlueDoor")
				{
					OpenDoor(obj);
					bInit = true;
				}
			}
			if (isPlatform)
			{
				if (tabColor[0])
				{
					//OpenDoor(obj);
					obj.GetComponent<SpriteRenderer>().color = Color.red;
					obj.layer = 9;
				}
				else if (tabColor[1])
				{
					//OpenDoor(obj);
					obj.GetComponent<SpriteRenderer>().color = Color.yellow;
					obj.layer = 11;
				}
				else if (tabColor[2])
				{
					//OpenDoor(obj);
					obj.GetComponent<SpriteRenderer>().color = Color.blue;
					obj.layer = 10;
				}
			}
			if (bInit)
			{
				id_active = 0;
				InitTabColor();
				bInit = false;
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
