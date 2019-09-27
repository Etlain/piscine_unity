using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public string     race = "Human"; // Human or Orc
    public string     player = "Human"; // Human or IA
    public List<Unit> units = new List<Unit>();

    private delegate void   delegateAction();
    private delegateAction action;

    GameObject        clickedObject;
    Unit              clickedUnit;
    int               nbrSelectedUnits = 0;
    string            raceTag = null;

    // Start is called before the first frame update
    void Start()
    {
        if (race.Equals("Orc"))
            raceTag = "OrcUnit";
        else
            raceTag = "HumanUnit";
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
        Debug.Log("test delegate");
    }

    void actionPlayer()
    {
        // SELECTION
        if (Input.GetMouseButtonDown(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            managerSelectionUnits(false);
        else if (Input.GetMouseButtonDown(0))
            managerSelectionUnits(true); // deselected when use click left two time for selected units
        // MOVE
        else if (Input.GetMouseButtonDown(1))
            moveUnits();
    }

    void managerSelectionUnits(bool canDeselected)
    {
        clickedObject = getClickedObject();
        if (clickedObject && clickedObject.tag.Equals(raceTag))
        {
            if (nbrSelectedUnits > 0 && canDeselected)
                deselectedUnits();
            else
            {
                clickedObject.GetComponent<Unit>().selectedCharacter();
                nbrSelectedUnits++;
            }
        }
        else if (canDeselected)
            deselectedUnits();
    }

    GameObject getClickedObject()
    {
        Collider2D clickedCollider;

        clickedCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (clickedCollider)
            return (clickedCollider.gameObject);
        return (null);
    }

    void moveUnits()
    {
        clickedObject = getClickedObject();
        if (clickedObject && clickedObject.tag.Equals(raceTag))
            return ;
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
            if (unit.getIsSelected())
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
}
