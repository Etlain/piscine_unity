using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public string     race = "Human"; // Human or Orc
    public string     player = "Human"; // Human or IA
    public List<Unit> units = new List<Unit>();
    public Transform  targetIA;

    private delegate void   delegateAction();
    private delegateAction action;

    GameObject        clickedObject = null;
    Unit              clickedUnit;
    int               nbrSelectedUnits = 0;
    string            raceTag = null;
    string            enemyRaceTag = null;

    // Start is called before the first frame update
    void Start()
    {
        if (race.Equals("Orc"))
        {
            raceTag = "OrcUnit";
            enemyRaceTag = "HumanUnit";
        }
        else
        {
            raceTag = "HumanUnit";
            enemyRaceTag = "OrcUnit";
        }
        if (!player.Equals("Human"))
        {
            action = actionIA;
            player = "IA";
        }
        else
            action = actionPlayer;

    }

    void FixedUpdate()
    {
        action();
    }

    void actionIA()
    {
        /*for (int i = 0; i < units.Count; i++)
        {
            if (!units[i])
                units.Remove(units[i]);
            else if (units[i].getOrder() == (int)Unit.UnitOrder.STAY)
            {
                Debug.Log(targetIA.position.x);
                //Debug.Log(targetIA.position.y);
                //Vector2 t = new Vector2(0,0);
                //units[i].setClickTarget(t);
                units[i].setClickTargetWhithoutCamera(targetIA.position);
                units[i].setOrder((int)Unit.UnitOrder.MOVE);
            }
        }*/
        //Debug.Log("test delegate");
    }

    void actionPlayer()
    {
        verifyIsAlive();
        // SELECT UNITS CLICK LEFT AND MAJ
        if (Input.GetMouseButtonDown(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            actionClickLeftAndMaj();
        // SELECT UNITS OR ATTACK WITH CLICK LEFT
        else if (Input.GetMouseButtonDown(0))
            actionClickLeft();
        // MOVE OR ATTACK CLICK RIGHT
        else if (Input.GetMouseButtonDown(1))
            actionClickRight();
    }

    GameObject getClickedObject()
    {
        Collider2D clickedCollider;

        clickedCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (clickedCollider)
            return (clickedCollider.gameObject);
        return (null);
    }

    string  getTagObject(GameObject gameObject)
    {
        if (!gameObject)
            return ("");
        else
            return (gameObject.tag);
    }

// Action Input

    void actionClickLeftAndMaj()
    {
        verifyList();
        clickedObject = getClickedObject();
        if (clickedObject && clickedObject.tag.Equals(raceTag))
        {
            clickedObject.GetComponent<Unit>().selectedCharacter();
            nbrSelectedUnits++;
        }
        clickedObject = null;
    }

    void actionClickLeft()
    {
        string tagClickedObject;

        verifyList();
        clickedObject = getClickedObject();
        tagClickedObject = getTagObject(clickedObject);
        if (nbrSelectedUnits > 0 && tagClickedObject.Equals(enemyRaceTag))
            attackUnits(clickedObject);
        else if (tagClickedObject.Equals(raceTag))
        {
            if (nbrSelectedUnits > 0)
                deselectedUnits();
            else
            {
                clickedObject.GetComponent<Unit>().selectedCharacter();
                nbrSelectedUnits++;
            }
        }
        else
            deselectedUnits();
        clickedObject = null;
    }

    void actionClickRight()
    {
        string tagClickedObject;

        verifyList();
        clickedObject = getClickedObject();
        tagClickedObject = getTagObject(clickedObject);
        if (tagClickedObject.Equals(raceTag))
            return ;
        else if (tagClickedObject.Equals(enemyRaceTag))
            attackUnits(clickedObject);
        else
            moveUnits();
        clickedObject = null;
    }

// functions Units

    void attackUnits(GameObject targetOfAttack)
    {
        //  Debug.Log("Vous allez périiiir vil démon");
        foreach (Unit unit in units)
        {
            if (unit.getIsSelected())
            {
                //unit.setAttackTarget(targetOfAttack);
                unit.setClickTarget(Input.mousePosition);
                unit.setOrder((int)Unit.UnitOrder.MOVE);
                /*unit.setAttackTarget(targetOfAttack);
                unit.setOrder((int)Unit.UnitOrder.ATTACK);*/
            }
        }
    }

    void moveUnits()
    {
        foreach (Unit unit in units)
        {
            if (unit.getIsSelected())
            {
                unit.setClickTarget(Input.mousePosition);
                unit.setOrder((int)Unit.UnitOrder.MOVE);
            }
        }
    }

    void deselectedUnits()
    {
        foreach (Unit unit in units)
        {
            if (!unit)
                units.Remove(unit);
            else if (unit.getIsSelected())
            {
                unit.deselectedCharacter();
            }
        }
        nbrSelectedUnits = 0;
    }

    public void addUnit(GameObject unit)
    {
         units.Add(unit.GetComponent<Unit>());
    }

    void verifyList()
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (!units[i])
                units.Remove(units[i]);
        }
    }

    void verifyIsAlive()
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (!units[i].getIsAlive() && units[i].getIsSelected())
                nbrSelectedUnits--;
        }
        if (nbrSelectedUnits < 0)
            nbrSelectedUnits = 0;
    }

}
