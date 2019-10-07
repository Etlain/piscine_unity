using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public float      life = 500;
    // Start is called before the first frame update
    void Start()
    {

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
        Debug.Log("Ho no, my house !");
    }

    void dead()
    {
        //unitAnimator.SetBool("Alive", false);
        //order = (int)UnitOrder.DEAD;
        //damage = 0;
        destroyUnit();
        Debug.Log("You dead bwahahaha");
    }

    public void destroyUnit()
    {
        //Debug.Log("dest");
        Destroy(this.gameObject);
    }

}
