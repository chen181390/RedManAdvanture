using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CharacterStandStatus
{
    inAir,
    onGround
}
public class CharacterMove : MonoBehaviour
{
    private CharacterStandStatus standStatus;
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
    private float heightDropThreshold = 3f;
    private float brakeSpeedThreshold = 3f;
    private float maxRunSpeed = 5;
    private Vector2 jumpImpulse = new Vector2(0, 5f);
    private float maxJumpSpeed = 0.5f;

    private int currState;
    private int currTrans;
    private int nextState;
    private bool isInTrans;


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
        this.currState = this.moveAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        this.nextState = this.moveAnimator.GetNextAnimatorStateInfo(0).shortNameHash;
        this.currTrans = this.moveAnimator.GetAnimatorTransitionInfo(0).nameHash;
        this.isInTrans = this.moveAnimator.IsInTransition(0);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (this.currState == AniHashCode.Idle && !this.isInTrans)
            {
                this.moveAnimator.SetTrigger(AniHashCode.startRun);
            }

            if (this.currState == AniHashCode.Running
            || this.currState == AniHashCode.Jump
            || this.currState == AniHashCode.HighDrop)
            {
                this.isRun = true;
                if (Input.GetKey(KeyCode.A) && this.isRightDir)
                {
                    transform.Rotate(new Vector3(0, 180, 0));
                    this.isRightDir = false;
                }
                else if (Input.GetKey(KeyCode.D) && !this.isRightDir)
                {
                    transform.Rotate(new Vector3(0, 180, 0));
                    this.isRightDir = true;
                }
            }
        }

        if (Input.GetKey(KeyCode.W) && this.jumpLeftSeg > 0 && !this.isInTrans)
        {
            if (this.currState != AniHashCode.Idle)
            {
                this.moveAnimator.SetTrigger(AniHashCode.toIdle);
            }
            else
            {
                this.moveAnimator.SetTrigger(AniHashCode.startJump);
                this.isJump = true;
                this.jumpLeftSeg--;
            }
        }


        // 松开按键
        if (this.isRun && (!this.isRightDir && Input.GetKeyUp(KeyCode.A) || this.isRightDir && Input.GetKeyUp(KeyCode.D)))
        {
            if ((currState == AniHashCode.startRun || nextState == AniHashCode.startRun
            || currState == AniHashCode.Running || nextState == AniHashCode.Running))
            {
                this.moveAnimator.SetTrigger(AniHashCode.toIdle);
            }

            this.isRun = false;
        }

        // TODO:
        if (Input.GetKeyUp(KeyCode.W))
        {
            this.isJump = false;
        }

        if (currState == AniHashCode.Idle && System.Math.Abs(this.rigidBody.velocity.x) > this.brakeSpeedThreshold && !this.isInTrans)
        {
            this.moveAnimator.SetTrigger(AniHashCode.startBrake);
        }

        if (currState == AniHashCode.Brake && System.Math.Abs(this.rigidBody.velocity.x) <= 0.5 && !this.isInTrans)
        {
            this.moveAnimator.SetTrigger(AniHashCode.toIdle);
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

        if (this.isJump)
        {
            this.rigidBody.AddForce(this.jumpImpulse, ForceMode2D.Impulse);
            this.isJump = false;
        }

        RaycastHit2D hit2D = Physics2D.Raycast(this.transform.position, Vector2.down, this.heightDropThreshold, 511);
        if (!hit2D.collider)
        {
            if (this.currState != AniHashCode.Idle && !this.isInTrans)
            {
                this.moveAnimator.SetTrigger(AniHashCode.Idle);
                this.moveAnimator.SetTrigger(AniHashCode.startHighDrop);
            }
        }
    }

     public void resetCharacter()
     {
        this.rigidBody.isKinematic = true;
        this.isRightDir = true;
        this.rigidBody.velocity = Vector3.zero;
        this.rigidBody.freezeRotation = true;
        transform.SetPositionAndRotation(this.characterIniPos, this.characterIniRot);
        // Fixme : 我们不需要举手庆祝重生
        //this.moveAnimator.SetTrigger(AniHashCode.toIdle);
        //this.moveAnimator.SetTrigger(AniHashCode.HighDropToIdle);
        this.rigidBody.isKinematic = false;
        this.rigidBody.freezeRotation = false;

     }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name.Substring(0, 5) == "Grass")
        {
            if (this.currState == AniHashCode.HighDrop || this.currState == AniHashCode.Jump)
            {
                this.moveAnimator.SetTrigger(AniHashCode.toIdle);
            }

            this.jumpLeftSeg = this.jumpMaxSeg;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
    }

}
