using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMace : MonoBehaviour
{
    public Vector2[] routePoints;
    public float moveSpeed;
    public float attackSpeed;
    private float iniMoveSpeed;
    private int routeTarget = 1;
    private CharacterBehaviour character;
    private bool isForward = true;
    private bool isInRange;
    private Animator animator;
    private Quaternion iniRot;
    private Vector2 attackTargetPos;


    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.parent)
        {
            for (int i = 0; i < this.routePoints.Length; i++)
            {
                this.routePoints[i] = this.transform.parent.TransformPoint(this.routePoints[i]);
            }
        }
        this.transform.position = this.routePoints[0];
        this.iniMoveSpeed = this.moveSpeed;
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.animator = this.GetComponentInChildren<Animator>();
        this.iniRot = this.transform.rotation;
        this.character.resetMissionEvent += this.resetJumpMace;

    }

    public void resetJumpMace()
    {
        var rigidBody = this.GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0;
        this.transform.position = this.routePoints[0];
        this.transform.rotation = this.iniRot;
        this.routeTarget = 1;
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target;
        int status = this.animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        int trans = this.animator.GetAnimatorTransitionInfo(0).nameHash;
        if (this.isInRange)
        {
            if (status == AniHashCode.Idle && trans != AniHashCode.IdleToAttackJump)
            {
                this.attackTargetPos = new Vector2(this.character.transform.position.x, this.routePoints[0].y);
                this.animator.SetTrigger(AniHashCode.triggerAttack);
            }

            target = this.attackTargetPos;
        }
        else
        {
            target = this.routePoints[this.routeTarget];
        }


        var direct = (target - (Vector2)this.transform.position).normalized;
        var delta = direct * this.moveSpeed * Time.deltaTime;
        var nextPos = (Vector2)this.transform.position + delta;
        var nextDirect = (target - nextPos).normalized;
        // 到达目标点
        if (nextDirect != direct)
        {
            this.transform.Translate(target - (Vector2)this.transform.position, Space.World);
            if (!this.isInRange)
            {
                if (this.routeTarget == this.routePoints.Length - 1)
                {
                    this.routeTarget--;
                    this.isForward = false;
                    this.transform.Rotate(new Vector3(0, 180, 0));
   
                }
                else if (this.routeTarget == 0)
                {
                    this.routeTarget++;
                    this.isForward = true;
                    this.transform.Rotate(new Vector3(0, 180, 0));
                }
                else
                {
                    if (this.isForward)
                        this.routeTarget++;
                    else
                        this.routeTarget--;
                }
            }
        }
        else
        {
            this.transform.Translate(delta, Space.World);
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.name == "characterCollider")
        {
            this.isInRange = true;
            this.moveSpeed = this.attackSpeed;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.name == "characterCollider")
        {
            this.isInRange = false;
            this.moveSpeed = this.iniMoveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Character")
        {
            this.character.setCharacterDead();
        }
        else if (collision.transform.name == "DeathLineBottom")
        {
            this.gameObject.SetActive(false);
        }
    }
}
