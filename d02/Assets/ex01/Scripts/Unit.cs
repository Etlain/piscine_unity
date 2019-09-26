using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum UnitOrder
    {
        STAY = 0,
        MOVE = 1,
        ATTACK = 2
    };

    public GameObject       selectedZone;
    public GameObject       character;
    public float            speed = 1;
    public float            angleOffsetAnimation = 20; // use to differentiate right movements and diagonal movements of animations

    private GameObject      unit;
    private Animator        unitAnimator;
    private AudioSource     unitAudioSource;
    private SpriteRenderer  selectedZoneRenderer;
    private Vector3         clickTarget;
    private int             order;
    private float           step;
    private float           angleUnitTarget = 0;

    private bool            isSelected = false;
    private bool            isClickTarget = false;
    // Start is called before the first frame update
    void Start()
    {
        order = (int)UnitOrder.STAY;
        unit = this.gameObject;
        unitAnimator = character.GetComponent<Animator>();
        unitAudioSource = this.GetComponent<AudioSource>();
        if (selectedZone)
            selectedZoneRenderer = selectedZone.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (order == (int)UnitOrder.MOVE && isClickTarget)
            moveToTarget();
    }

    void moveToTarget()
    {
        step = speed * Time.deltaTime;
        unit.transform.position = Vector3.MoveTowards(unit.transform.position, clickTarget, step);
        if (Vector3.Distance(unit.transform.position, clickTarget) < step)
        {
            modifyDirectionAnimation();
            unitAnimator.SetFloat("Speed", 0);
            isClickTarget = false;
        }
        else
        {
            modifyDirectionAnimation();
            unitAnimator.SetFloat("Speed", 1);
        }
    }

    void modifyDirectionAnimation()
    {
        Vector2 vectorUnitToTarget = clickTarget - unit.transform.position;

        angleUnitTarget = Vector2.SignedAngle(Vector2.right, vectorUnitToTarget);
        // DOWN LEFT
        if (angleUnitTarget < -90 - angleOffsetAnimation && angleUnitTarget > -180 + angleOffsetAnimation)
            setDirectionAnimation(-1, -1);
        // DOWN RIGHT
        else if (angleUnitTarget < 0 - angleOffsetAnimation && angleUnitTarget > -90 + angleOffsetAnimation)
            setDirectionAnimation(1, -1);
        // UP LEFT
        else if (angleUnitTarget < 180 - angleOffsetAnimation && angleUnitTarget > 90 + angleOffsetAnimation)
            setDirectionAnimation(-1, 1);
        // UP RIGHT
        else if (angleUnitTarget < 90 - angleOffsetAnimation && angleUnitTarget > 0 + angleOffsetAnimation)
            setDirectionAnimation(1, 1);
        // RIGHT
        else if (angleUnitTarget >= 0 - angleOffsetAnimation && angleUnitTarget <= 0 + angleOffsetAnimation)
            setDirectionAnimation(1, 0);
        // UP
        else if (angleUnitTarget >= 90 - angleOffsetAnimation && angleUnitTarget <= 90 + angleOffsetAnimation)
            setDirectionAnimation(0, 1);
        // DOWN
        else if (angleUnitTarget >= -90 - angleOffsetAnimation && angleUnitTarget <= -90 + angleOffsetAnimation)
            setDirectionAnimation(0, -1);
        // LEFT
        else if (angleUnitTarget >= 180 - angleOffsetAnimation || angleUnitTarget <= -180 + angleOffsetAnimation)
            setDirectionAnimation(-1, 0);
    }

    void setDirectionAnimation(float horizontal, float vertical)
    {
        unitAnimator.SetFloat("Horizontal", horizontal);
        unitAnimator.SetFloat("Vertical", vertical);
    }

    public void setOrder(int cmd)
    {
        order = cmd;
    }

    int getOrder()
    {
        return ((int)order);
    }

    // target, is the position of the movement
    public void setClickTarget(Vector3 target)
    {
        clickTarget = target;
        clickTarget.z = -Camera.main.transform.position.z;
        clickTarget = Camera.main.ScreenToWorldPoint(clickTarget);
        isClickTarget = true;
    }

    public bool getIsSelected()
    {
        return (isSelected);
    }

    public void selectedCharacter()
    {
        unitAudioSource.Play(0);
        selectedZoneRenderer.enabled = true;
        isSelected = true;
    }

    public void deselectedCharacter()
    {
        selectedZoneRenderer.enabled = false;
        isSelected = false;
    }
}
