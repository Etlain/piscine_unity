using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public float      life = 500;

    private AudioSource buildingsAudioSource;
    private float       maxLife;
    // Start is called before the first frame update
    void Start()
    {
        maxLife = life;
        buildingsAudioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loseLife(float damageTaken)
    {
        life -= damageTaken;
        if (life <= 0)
            dead();
        //Debug.Log("Ho no, my house !");
    }

    void dead()
    {
        //unitAnimator.SetBool("Alive", false);
        //order = (int)UnitOrder.DEAD;
        //damage = 0;
        //destroyBuildings();
        buildingsAudioSource.Play();
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, buildingsAudioSource.clip.length);
        //Debug.Log("You dead bwahahaha");
    }

    public float getLife()
    {
        return (life);
    }

    public float getMaxLife()
    {
        return (maxLife);
    }

    public void destroyBuildings()
    {
        //Debug.Log("dest");
        dead();

    }


}
