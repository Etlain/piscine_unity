using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unity : MonoBehaviour
{
    public GameObject       selectedZone;
    public GameObject       character;
    public float            speed = 1;
    public float            angleOffsetAnimation = 20; // use to differentiate right movements and diagonal movements of animations

    private GameObject      unity;
    private Animator        unityAnimator;
    private AudioSource     unityAudioSource;
    private SpriteRenderer  selectedZoneRenderer;
    private Vector3         clickTarget;
    private float           step;
    private float           angleUnityTarget = 0;

    private bool            isSelected = false;
    private bool            isClickTarget = false;
    // Start is called before the first frame update
    void Start()
    {
        unity = this.gameObject;
        unityAnimator = character.GetComponent<Animator>();
        unityAudioSource = this.GetComponent<AudioSource>();
        if (selectedZone)
            selectedZoneRenderer = selectedZone.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // INPUT
        if (Input.GetMouseButtonDown(0) && isSelected == true)
            deselectedCharacter();
        else if (Input.GetMouseButtonDown(1) && isSelected == true)
            getClickTarget();

        // MOVE
        if (isClickTarget)
            moveToTarget();
    }

    void moveToTarget()
    {
        step = speed * Time.deltaTime;
        unity.transform.position = Vector3.MoveTowards(unity.transform.position, clickTarget, step);
        if (Vector3.Distance(unity.transform.position, clickTarget) < step)
        {
            modifyDirectionAnimation();
            unityAnimator.SetFloat("Speed", 0);
            isClickTarget = false;
        }
        else
        {
            modifyDirectionAnimation();
            unityAnimator.SetFloat("Speed", 1);
        }
    }

    void modifyDirectionAnimation()
    {
        Vector2 vectorUnityToTarget = clickTarget - unity.transform.position;

        angleUnityTarget = Vector2.SignedAngle(Vector2.right, vectorUnityToTarget);
        // DOWN LEFT
        if (angleUnityTarget < -90 - angleOffsetAnimation && angleUnityTarget > -180 + angleOffsetAnimation)
            setDirectionAnimation(-1, -1);
        // DOWN RIGHT
        else if (angleUnityTarget < 0 - angleOffsetAnimation && angleUnityTarget > -90 + angleOffsetAnimation)
            setDirectionAnimation(1, -1);
        // UP LEFT
        else if (angleUnityTarget < 180 - angleOffsetAnimation && angleUnityTarget > 90 + angleOffsetAnimation)
            setDirectionAnimation(-1, 1);
        // UP RIGHT
        else if (angleUnityTarget < 90 - angleOffsetAnimation && angleUnityTarget > 0 + angleOffsetAnimation)
            setDirectionAnimation(1, 1);
        // RIGHT
        else if (angleUnityTarget >= 0 - angleOffsetAnimation && angleUnityTarget <= 0 + angleOffsetAnimation)
            setDirectionAnimation(1, 0);
        // UP
        else if (angleUnityTarget >= 90 - angleOffsetAnimation && angleUnityTarget <= 90 + angleOffsetAnimation)
            setDirectionAnimation(0, 1);
        // DOWN
        else if (angleUnityTarget >= -90 - angleOffsetAnimation && angleUnityTarget <= -90 + angleOffsetAnimation)
            setDirectionAnimation(0, -1);
        // LEFT
        else if (angleUnityTarget >= 180 - angleOffsetAnimation || angleUnityTarget <= -180 + angleOffsetAnimation)
            setDirectionAnimation(-1, 0);
    }

    void setDirectionAnimation(float horizontal, float vertical)
    {
        unityAnimator.SetFloat("Horizontal", horizontal);
        unityAnimator.SetFloat("Vertical", vertical);
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
        unityAudioSource.Play(0);
        selectedZoneRenderer.enabled = true;
        isSelected = true;
    }

    void deselectedCharacter()
    {
        selectedZoneRenderer.enabled = false;
        isSelected = false;
    }

    void OnGUI()
    {
        //Output the angle found above
        GUI.Label(new Rect(25, 25, 200, 40), "Angle Between Objects" + angleUnityTarget);
    }
}
