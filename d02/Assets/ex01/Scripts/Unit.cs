using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum UnitOrder
    {
        STAY = 0,
        MOVE = 1,
        ATTACK = 2,
        END = 3
    };

    public enum UnitRace
    {
        HUMAN = 0,
        ORC = 1
    };

    public GameObject       selectedZone;
    public GameObject       character;
    public float            life = 100;
    public float            damage = 20;
    public float            speed = 1;
    public float            angleOffsetAnimation = 20; // use to differentiate right movements and diagonal movements of animations

    private GameObject      unit;
    private Animator        unitAnimator;
    private AudioSource     unitAudioSource;
    private SpriteRenderer  selectedZoneRenderer;
    private Vector3         clickTarget;
    private int             race = 0;
    private int             order;
    private float           step;
    private float           angleUnitTarget = 0;
    private GameObject      target = null;

    private bool            isSelected = false;
    private bool            isClickTarget = false;
    // Start is called before the first frame update
    void Start()
    {
        order = (int)UnitOrder.STAY;
        unit = this.gameObject;
        unitAnimator = character.GetComponent<Animator>();
        unitAudioSource = this.GetComponent<AudioSource>();
        if (unit && unit.tag.IndexOf("Orc") >= 0)
            race = (int)UnitRace.ORC;
        else
            race = (int)UnitRace.HUMAN;
        if (selectedZone)
            selectedZoneRenderer = selectedZone.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (order == (int)UnitOrder.MOVE && isClickTarget)
            moveToTarget();
        else if (order == (int)UnitOrder.ATTACK/* && target*/)
            attackTarget(target);
        else if (order == (int)UnitOrder.STAY)
        {
            unitAnimator.SetFloat("Speed", 0);
            unitAnimator.SetBool("Attack", false);
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("test :"+other.tag);
        //setOrder((int)UnitOrder.STAY);
        if (isEnemyRace(other.tag))
        {
            Debug.Log("test 2 :"+other.tag);

            target = other.gameObject;
            setOrder((int)UnitOrder.ATTACK);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("test :"+other.tag);
        //setOrder((int)UnitOrder.STAY);
        if (order == (int)UnitOrder.ATTACK)
        {
            target = null;
            setOrder((int)UnitOrder.STAY);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("OnCollisionEnter2D");
    }

// FUNCTION MOVE

    void moveToTarget()
    {
        step = speed * Time.deltaTime;
        unit.transform.position = Vector3.MoveTowards(unit.transform.position, clickTarget, step);
        if (Vector3.Distance(unit.transform.position, clickTarget) < step)
        {
            modifyDirectionAnimation();
            unitAnimator.SetFloat("Speed", 0);
            unitAnimator.SetBool("Attack", false);
            isClickTarget = false;
        }
        else
        {
            modifyDirectionAnimation();
            unitAnimator.SetFloat("Speed", 1);
            unitAnimator.SetBool("Attack", false);
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

// FUNCTION ORDER

    public void setOrder(int cmd)
    {
        order = cmd;
    }

    int getOrder()
    {
        return ((int)order);
    }


// FUNCTION SELECTION

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

// FUNCTION ATTACK

    void        attackTarget(GameObject target)
    {
        // if target is units
        Debug.Log("attack Target");
        if (isEnemyRace(target.tag))
        {
            Debug.Log("kill this unit is an order");
            unitAnimator.SetFloat("Speed", 0);
            unitAnimator.SetBool("Attack", true);
            // target.GetComponent<Unit>().loseLife(damage);
        }
    }

// FUNCTION DEAD

    public void loseLife(float damageTaken)
    {
        life -= damageTaken;
        if (life <= 0)
            dead();
    }

    void dead()
    {
        Debug.Log("You dead bwahahaha");
    }

// Function Race

    public int getRace()
    {
        return (race);
    }

    bool isEnemyRace(string rc)
    {
        if (race == (int)UnitRace.ORC && rc.IndexOf("Human") >= 0)
            return true;
        else if (race == (int)UnitRace.HUMAN && rc.IndexOf("Orc") >= 0)
            return true;
        return false;
    }

    public void setAttackTarget(GameObject trgt)
    {
        if (trgt && isEnemyRace(trgt.tag))
            target = trgt;
    }
}
