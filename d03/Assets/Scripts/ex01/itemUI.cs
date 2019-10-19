using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class itemUI : MonoBehaviour
{


    // user collider for detect empty case and drag item
    // create new script and exit gameObject towerSprite in editor unity for the script

    public GameObject tower;

    private GameObject  towerSprite;
    private Vector3 screenPoint;
    private Vector3 offset;
    private LayerMask mask;

    bool isDrag = false;
    // Start is called before the first frame update
    void Start()
    {
        // create simple game object sprite ui when drag
        towerSprite = new GameObject();
        towerSprite.AddComponent(typeof(SpriteRenderer));
        towerSprite.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        towerSprite.transform.localScale *= 2f;
        towerSprite.GetComponent<SpriteRenderer>().enabled = false;
        // initialize mask layer for found good position to drag tower
        mask = LayerMask.GetMask("Default");
        towerSprite.layer = LayerMask.NameToLayer("Default");
        //Debug.Log("start :"+mask);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("is drag : "+isDrag);
        if (isDrag)
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);
            Debug.DrawRay(Input.mousePosition, Vector2.zero * 100f, Color.blue);
            //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);

            // If it hits something...
            if (hit.collider != null)
            {
                Debug.Log("test :"+hit.collider.tag);
            }
        }
    }

    void OnMouseDown()
    {
        isDrag = true;
        Debug.Log("A");

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        towerSprite.transform.position = getPosition();
        towerSprite.GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnMouseDrag()
    {
        towerSprite.transform.position = getPosition();
    }

    void OnMouseUp()
    {
        //towerSprite.transform.position = curPosition;

        /*RaycastHit hit; Use this in update
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            Debug.Log("log");
            // Do something with the object that was hit by the raycast.
        }*/
        Instantiate(tower, getPosition(), Quaternion.identity);

        towerSprite.transform.position = gameObject.transform.position;
        towerSprite.GetComponent<SpriteRenderer>().enabled = false;
        isDrag = false;
    }

    Vector3 getPosition() // need screenpoint for work and offset
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition   = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        return (curPosition);
    }
}
