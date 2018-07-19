using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {

	public GameObject	a;
	public GameObject	s;
	public GameObject	d;

	[HideInInspector]
	private float		SpawnElapsedTime = 0;
	private float		SpawnLimitTime = 0;
	private float		SpawnMaxTime = 5;
	private	int 		random_asd = -1;
	private int 		asd_prev = -1;
	// Use this for initialization
	void Start () {
		initVar();
	}

	// Update is called once per frame
	void Update () {
		SpawnElapsedTime += Time.deltaTime;
		if (SpawnElapsedTime >= SpawnLimitTime)
		{

			if (random_asd == 0)
				GameObject.Instantiate(a);
			else if (random_asd == 1)
				GameObject.Instantiate(s);
			else if (random_asd == 2)
				GameObject.Instantiate(d);
			SpawnElapsedTime -= SpawnLimitTime;
			initVar();
		}

		//key.transform.Translate(Vector3.down * 5 * Time.deltaTime);
	}

	void initVar()
	{
		SpawnLimitTime  = Random.Range(1, SpawnMaxTime + 1);
		while (random_asd == asd_prev)
				random_asd = Random.Range(0, 3);
		asd_prev = random_asd;
		Debug.Log(random_asd);
	}
}
