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
        DEAD = 3,
        END = 4
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

    public AudioClip        selectedSound;
    public AudioClip        attackSound;
    public AudioClip        deadSound;

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
    private float           maxLife;

    private bool            isSelected = false;
    private bool            isClickTarget = false;
    // Start is called before the first frame update
    void Start()
    {
        maxLife = life;
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
        else if (order == (int)UnitOrder.ATTACK && target)
            attackTarget(target);
        else if (order == (int)UnitOrder.STAY)
        {
            unitAnimator.SetFloat("Speed", 0);
            unitAnimator.SetBool("Attack", false);
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("test :"+other.tag);
        //setOrder((int)UnitOrder.STAY);
        if (other && isEnemyRace(other.tag))
        {
            //Debug.Log("test 2 :"+other.tag);

            target = other.gameObject;
            setOrder((int)UnitOrder.ATTACK);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("test :"+other.tag);
        //setOrder((int)UnitOrder.STAY);
        if (other && order == (int)UnitOrder.STAY && isEnemyRace(other.tag))
        {
            //Debug.Log("test 2 :"+other.tag);

            target = other.gameObject;
            setOrder((int)UnitOrder.ATTACK);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("test :"+other.tag);
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
            modifyDirectionClickTarget();
            unitAnimator.SetFloat("Speed", 0);
            unitAnimator.SetBool("Attack", false);
            isClickTarget = false;
        }
        else
        {
            modifyDirectionClickTarget();
            unitAnimator.SetFloat("Speed", 1);
            unitAnimator.SetBool("Attack", false);
        }
    }

    void modifyDirectionClickTarget()
    {
        Vector2 vectorUnitToTarget = clickTarget - unit.transform.position;

        angleUnitTarget = Vector2.SignedAngle(Vector2.right, vectorUnitToTarget);
        modifyDirection(angleUnitTarget);
    }

    void modifyDirectionAttack()
    {
        Vector2 vectorUnitToTarget = target.transform.position - unit.transform.position;

        angleUnitTarget = Vector2.SignedAngle(Vector2.right, vectorUnitToTarget);
        modifyDirection(angleUnitTarget);
    }

    void modifyDirection(float angle)
    {
        // DOWN LEFT
        if (angle < -90 - angleOffsetAnimation && angle > -180 + angleOffsetAnimation)
            setDirectionAnimation(-1, -1);
        // DOWN RIGHT
        else if (angle < 0 - angleOffsetAnimation && angle > -90 + angleOffsetAnimation)
            setDirectionAnimation(1, -1);
        // UP LEFT
        else if (angle < 180 - angleOffsetAnimation && angle > 90 + angleOffsetAnimation)
            setDirectionAnimation(-1, 1);
        // UP RIGHT
        else if (angle < 90 - angleOffsetAnimation && angle > 0 + angleOffsetAnimation)
            setDirectionAnimation(1, 1);
        // RIGHT
        else if (angle >= 0 - angleOffsetAnimation && angle <= 0 + angleOffsetAnimation)
            setDirectionAnimation(1, 0);
        // UP
        else if (angle >= 90 - angleOffsetAnimation && angle <= 90 + angleOffsetAnimation)
            setDirectionAnimation(0, 1);
        // DOWN
        else if (angle >= -90 - angleOffsetAnimation && angle <= -90 + angleOffsetAnimation)
            setDirectionAnimation(0, -1);
        // LEFT
        else if (angle >= 180 - angleOffsetAnimation || angle <= -180 + angleOffsetAnimation)
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
        unitAudioSource.clip = selectedSound;
        unitAudioSource.Play();
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
        //Debug.Log("attack Target");
        if (isEnemyRace(target.tag))
        {
            //Debug.Log("kill this unit is an order");
            unitAnimator.SetFloat("Speed", 0);
            modifyDirectionAttack();
            unitAnimator.SetBool("Attack", true);

            // target.GetComponent<Unit>().loseLife(damage);
        }
    }

    public void attack()
    {
        if (target)
        {
            if (target.GetComponent<Unit>())
            {
                Unit tmpUnit = target.GetComponent<Unit>();
                Debug.Log(tmpUnit.getStringRace()+" Unit ["+tmpUnit.getLife()+"/"+tmpUnit.getMaxLife()+"] has been attacked");
                tmpUnit.loseLife(damage);
            }
            else if (target.GetComponent<Buildings>())
            {
                Buildings tmpBuilding = target.GetComponent<Buildings>();
                Debug.Log(getStringOppositeRace() + " Building : "+target.name+" ["+tmpBuilding.getLife()+"/"+tmpBuilding.getMaxLife()+"] has been attacked");
                tmpBuilding.loseLife(damage);
            }
            unitAudioSource.clip = attackSound;
            unitAudioSource.Play();

        }

    }

    public float getLife()
    {
        return (life);
    }

    public float getMaxLife()
    {
        return (maxLife);
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
        unitAnimator.SetBool("Alive", false);
        unitAudioSource.clip = deadSound;
        unitAudioSource.Play();
        unit.GetComponent<Collider2D>().enabled = false;
        order = (int)UnitOrder.DEAD;
        damage = 0;
        Destroy(gameObject, deadSound.length);
        Debug.Log("You dead bwahahaha");
    }

    public void destroyUnit()
    {
        Debug.Log("dest");
        dead();

    }

// Function Race

    public string getStringRace()
    {
        if (getRace() == (int)Unit.UnitRace.ORC)
            return ("ORC");
        return ("HUMAN");
    }

    public string getStringOppositeRace()
    {
        if (getRace() == (int)Unit.UnitRace.ORC)
            return ("HUMAN");
        return ("ORC");
    }


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

    public bool getIsAlive()
    {
        if (order == (int)UnitOrder.DEAD)
            return false;
        return true;
    }
}
