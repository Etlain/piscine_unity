using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour
{


    // user collider for detect empty case and drag item
    // create new script and exit gameObject towerSprite in editor unity for the script

    public ManagerUI        managerUIScript;
    public GameObject       towerPrefab;
    public GameObject       towerSpriteSelected;

    //private towerScript     towerPrefabScript;
    private SpriteRenderer  towerSpriteRendererSelected;
    //private int     towerSpriteSelectedZ;
    private Vector3 screenPoint;
    private Vector3 offset;
    private LayerMask mask;
    private Vector2   towerPosition;
    private int         energy;

    bool isDrag = false;
    bool isEmpty = false;
    //bool isUI = false;
    // Start is called before the first frame update
    void Start()
    {
        towerSpriteRendererSelected = towerSpriteSelected.GetComponent<SpriteRenderer>();
        //towerPrefabScript = towerPrefab.GetComponent<towerScript>();
        energy = towerPrefab.GetComponent<towerScript>().energy;
        //towerSpriteSelectedZ = towerSpriteSelected.gameObject.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("is drag : "+isEmpty);
        if (isDrag)
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Default"));
            //Debug.DrawRay(Input.mousePosition, Vector2.zero * 100f, Color.blue);
            //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);

            // If it hits something...
            if (hit.collider != null)
            {
                //Debug.Log("test :"+hit.collider.tag);
                if (hit.collider.tag == "empty")
                {
                    towerSpriteRendererSelected.color = Color.green;
                    isEmpty = true;
                }
                else
                {
                    towerSpriteRendererSelected.color = Color.red;
                    isEmpty = false;
                }
                towerPosition = hit.collider.gameObject.transform.position;
            }
            else
            {
                //Debug.Log("hi");
                isEmpty = false;
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask("UI"));
                if (hit.collider != null)
                {
                    towerPosition = getPosition();
                    towerSpriteRendererSelected.color = Color.red;
                }
            }
        }
    }

    void OnMouseDown()
    {
        isDrag = true;
        //Debug.Log("A");

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        towerSpriteSelected.transform.position = getPosition();
        towerSpriteSelected.GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnMouseDrag()
    {
        towerSpriteSelected.transform.position = towerPosition;
        //towerSpriteSelected.transform.position = getPosition();
    }

    void OnMouseUp()
    {
        //towerSprite.transform.position = curPosition;

        if (isEmpty)
        {
            //Debug.Log("create");
            //towerPrefabScript.
            managerUIScript.downEnergy(energy);
            Instantiate(towerPrefab, towerPosition, Quaternion.identity);
            isEmpty = false;
            //Instantiate(towerPrefab, getPosition(), Quaternion.identity);
        }

        //towerSpriteSelected.transform.position = gameObject.transform.position;
        towerSpriteSelected.GetComponent<SpriteRenderer>().enabled = false;
        isDrag = false;
    }

    Vector3 getPosition() // need screenpoint for work and offset
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition   = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        //curPosition.z = -50;
        return (curPosition);
    }
}
