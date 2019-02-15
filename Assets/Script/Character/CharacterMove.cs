using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private bool isGround = true;
    private Vector3 characterIniPos;
    private Quaternion characterIniRot;
    private bool isRun = false;
    private bool isJump = false;
    private Animator moveAnimator;
    private Rigidbody2D rigidBody;
    private bool isRightDir = true;
    private int jumpMaxSeg = 1;
    private int jumpLeftSeg;
    private Vector2 LeftMoveForce = new Vector2(-8, 0);
    private Vector2 rightMoveForce = new Vector2(8, 0);
    private float maxRunSpeed = 10;
    private Vector2 jumpImpulse = new Vector2(0, 10f);


    // private int currState;
    // private int currTrans;
    // private int nextState;
    // private bool isInTrans;


    // Start is called before the first frame update
    void Start()
    {
        this.moveAnimator = GetComponent<Animator>();
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.characterIniPos = transform.position;
        this.characterIniRot = transform.rotation;
        this.jumpLeftSeg = this.jumpMaxSeg;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            this.isRun = true;
            if (Input.GetKey(KeyCode.A) && this.isRightDir)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                this.isRightDir = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            this.isRun = true;
            if (Input.GetKey(KeyCode.D) && !this.isRightDir)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                this.isRightDir = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && this.jumpLeftSeg > 0)
        {
            this.isJump = true;
        }

        if (this.isRun && (!this.isRightDir && Input.GetKeyUp(KeyCode.A) || this.isRightDir && Input.GetKeyUp(KeyCode.D)))
        {
            this.isRun = false;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            this.isJump = false;
        }

        if (this.isRun)
        {
            if (System.Math.Abs(this.rigidBody.velocity.x) < this.maxRunSpeed)
            {
                if (this.isRightDir)
                {
                    this.rigidBody.AddForce(this.rightMoveForce);
                }
                else
                {
                    this.rigidBody.AddForce(this.LeftMoveForce);
                }
            }
            else
            {
                if (this.rigidBody.velocity.x >= 0)
                {
                    this.rigidBody.velocity = new Vector2(this.maxRunSpeed, this.rigidBody.velocity.y);
                }
                else
                {
                     this.rigidBody.velocity = new Vector2(-this.maxRunSpeed, this.rigidBody.velocity.y);
                }

            }
        }

        if (this.isJump && this.jumpLeftSeg > 0)
        {
            this.rigidBody.AddForce(this.jumpImpulse, ForceMode2D.Impulse);
            this.jumpLeftSeg--;
        }

        // 给动画状态机参数赋值
        this.moveAnimator.SetBool(AniHashCode.isBtnRun, this.isRun);
        this.moveAnimator.SetBool(AniHashCode.isBtnJump, this.isJump);
        this.moveAnimator.SetFloat(AniHashCode.horSpeed, System.Math.Abs(this.rigidBody.velocity.x));

        RaycastHit2D hit2D = Physics2D.Raycast(this.transform.position, Vector2.down, 50, 511);
        if (hit2D.collider)
        {
            this.moveAnimator.SetFloat(AniHashCode.vecHeight, hit2D.distance);
            if (hit2D.distance <= 1)
            {
                this.isGround = true;
                this.moveAnimator.SetBool(AniHashCode.isGround, this.isGround);
                print(this.isGround);
            }
        }


    }

    public void resetCharacter()
    {
        this.rigidBody.velocity = Vector2.zero;
        this.isRightDir = true;
        transform.SetPositionAndRotation(this.characterIniPos, this.characterIniRot);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.isGround)
        {
            this.jumpLeftSeg = this.jumpMaxSeg;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
    }

}
