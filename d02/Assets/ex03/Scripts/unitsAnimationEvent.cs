using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitsAnimationEvent : MonoBehaviour
{
    // Start is called before the first frame update
    Unit unitClass;

    void Start()
    {
        unitClass = this.GetComponentInParent<Unit>();
    }

    void launchAttack()
    {
        //Debug.Log("tes");
        unitClass.attack();
    }

    void launchDead()
    {
        Debug.Log("troy");
        //unitClass.destroyUnit();
        unitClass.destroyUnit();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
