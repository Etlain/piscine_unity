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
    private float           angleUnityTarget = 0;

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

        if (Vector3.Distance(unity.transform.position, clickTarget) < step)
        {
            unityAnimator.SetFloat("Speed", 0);
            //if (Mathf.Abs(distanceX) > )
            //unityAnimator.SetFloat("Horizontal", distanceX);
            //unityAnimator.SetFloat("Vertical", distanceY);
            isClickTarget = false;
        }
        else
        {
            distanceX = clickTarget.x - unity.transform.position.x;
            distanceY = clickTarget.y - unity.transform.position.y;

            //if (Mathf.Abs(distanceX) > 0.1)
            //angleUnityTarget = Vector2.SignedAngle(clickTarget, unity.transform.position);
            angleUnityTarget = SignedAngle(unity.transform.position, clickTarget, Vector2.right);

            float angleOffset = 20;
            // LEFT
            if (angleUnityTarget >= 180 - angleOffset || angleUnityTarget <= -180 + angleOffset)
            {
                unityAnimator.SetFloat("Horizontal", -1);
                unityAnimator.SetFloat("Vertical", 0);
            }
            // UP
            else if (angleUnityTarget >= 90 - angleOffset || angleUnityTarget <= 90 + angleOffset)
            {
                unityAnimator.SetFloat("Horizontal", 0);
                unityAnimator.SetFloat("Vertical", 1);
            }
            else
            {
                unityAnimator.SetFloat("Horizontal", 0);
                unityAnimator.SetFloat("Vertical", 0);
            }
            //else if (angleUnityTarget >= 90 - angleOffset || angleUnityTarget <= 90 + angleOffset)
            // 180 - 90 upleft
            // 180 left
            // 90 up

            //if ()
            //    unityAnimator.SetFloat("Horizontal", distanceX);
            //if (Mathf.Abs(distanceY) > 0.1)
            //    unityAnimator.SetFloat("Vertical", distanceY);
            unityAnimator.SetFloat("Speed", 1);
        }
        Debug.Log("distanceX :"+distanceX+", distanceY :"+distanceY+", angle :"+angleUnityTarget);
    }

    public static float SignedAngle( Vector3 origin, Vector3 to, Vector3 normal )
    {
        // angle in [0,180]
        float angle = Vector3.Angle( origin, to );
        float sign = Mathf.Sign( Vector3.Dot( normal, Vector3.Cross( origin, to ) ) );
        return angle * sign;
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
