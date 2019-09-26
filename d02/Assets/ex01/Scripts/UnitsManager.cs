using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();

    GameObject        clickedObject;
    Unit              clickedUnit;
    int               nbrSelectedUnits = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        // SELECTION
        if (Input.GetMouseButtonDown(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
        {
            managerSelectionUnits(false);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            managerSelectionUnits(true); // deselected when use click left two time for selected units
        }
        // MOVE
        else if (Input.GetMouseButtonDown(1))
            moveUnits();
    }

    void managerSelectionUnits(bool canDeselected)
    {
        clickedObject = getClickedObject();
        if (clickedObject && clickedObject.tag.Equals("Unit"))
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
        if (clickedObject && clickedObject.tag.Equals("Unit"))
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

    // function check if just one unit is selected
}
