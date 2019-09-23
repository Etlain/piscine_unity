using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unity : MonoBehaviour
{
    public GameObject   selectedZone;
    public GameObject   character;
    public float        speed = 1;
    private GameObject  unity;
    private Animator    unityAnimator;

    private SpriteRenderer  selectedZoneRenderer;
    private Vector3         clickTarget;
    private float           step;
    private float           distanceX = 1;
    private float           distanceY = 0;

    private bool isSelected = false;
    private bool isClickTarget = false;
    // Start is called before the first frame update
    void Start()
    {
        unity = this.gameObject;
        unityAnimator = character.GetComponent<Animator>();
        if (selectedZone)
            selectedZoneRenderer = selectedZone.GetComponent<SpriteRenderer>();
        //Debug.Log("start");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0) && isSelected == true)
            deselectedCharacter();
        else if (Input.GetMouseButtonDown(1) && isSelected == true)
            getClickTarget();

        if (isClickTarget)
            moveToTarget();
    }

    void moveToTarget()
    {
        step = speed * Time.deltaTime;
        unity.transform.position = Vector3.MoveTowards(unity.transform.position, clickTarget, step);
        //unityAnimator.setFloat("Horizontal", "");
        //Debug.Log("target pos :"+clickTarget);
        //Debug.Log("unity pos :"+unity.transform.position);

        if (Vector3.Distance(unity.transform.position, clickTarget) < step + 0.1)
        {
            unityAnimator.SetFloat("Speed", 0);
            //if (Mathf.Abs(distanceX) > )
            unityAnimator.SetFloat("Horizontal", distanceX);
            unityAnimator.SetFloat("Vertical", distanceY);
            isClickTarget = false;
        }
        else
        {
            distanceX = clickTarget.x - unity.transform.position.x;
            distanceY = clickTarget.y - unity.transform.position.y;

            //if (Mathf.Abs(distanceX) > 0.1)
                unityAnimator.SetFloat("Horizontal", distanceX);
            //if (Mathf.Abs(distanceY) > 0.1)
                unityAnimator.SetFloat("Vertical", distanceY);
            unityAnimator.SetFloat("Speed", 1);
        }
        Debug.Log("distanceX :"+distanceX);
        Debug.Log("distanceY :"+distanceY);
    }

    void getClickTarget()
    {
        clickTarget = Input.mousePosition;
        clickTarget.z = -Camera.main.transform.position.z;
        clickTarget = Camera.main.ScreenToWorldPoint(clickTarget);
        isClickTarget = true;
    }

    void OnMouseDown()
    {
        if (isSelected == false)
            selectedCharacter();
    }

    void selectedCharacter()
    {
        selectedZoneRenderer.enabled = true;
        isSelected = true;
    }

    void deselectedCharacter()
    {
        selectedZoneRenderer.enabled = false;
        isSelected = false;
    }
}
