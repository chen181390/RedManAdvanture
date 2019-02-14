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
    public GameObject footHold;
    private CharacterStandStatus standStatus;
    private Vector3 characterIniPos;
    private Quaternion characterIniRot;
    public bool isRun = false;
    public bool isJump = false;
    private Animator moveAnimator;
    private Rigidbody2D rigidBody;
    private bool isRightDir = true;
    private int jumpMaxSeg = 1;
    private int jumpLeftSeg;
    private Vector2 LeftMoveForce = new Vector2(-8, 0);
    private Vector2 rightMoveForce = new Vector2(8, 0);
    private Vector2 jumpForce = new Vector3(0,30);
    private float highyDropHeight = 3.5f;


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
        this.moveControl();
        this.moveUpdate();
                    print("startHighDrop:" + this.moveAnimator.GetBool("startHighDrop"));
    }

    private void moveControl() {
        // 按下按键
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.triggerSatetTrans("startRun");
            this.isRun = true;
            if (this.isRightDir)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                this.isRightDir = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.triggerSatetTrans("startRun");
            this.isRun = true;
            if (!this.isRightDir)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                this.isRightDir = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && this.jumpLeftSeg -- > 0)
        {
            this.triggerSatetTrans("startJump");
            this.isJump = true;
        }

        // 松开按键
        if (this.isRun && (!this.isRightDir && Input.GetKeyUp(KeyCode.A) || this.isRun && this.isRightDir && Input.GetKeyUp(KeyCode.D)))
        {
            this.triggerSatetTrans("stopRun");
            this.isRun = false;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            this.isJump = false;
        }
    }

    private void moveUpdate() 
    {

        if (this.isRun) {
            if (this.isRightDir) {
                this.rigidBody.AddForce(this.rightMoveForce);
            } else {
                this.rigidBody.AddForce(this.LeftMoveForce);
            }
        }

        if (this.isJump)
        {
            this.rigidBody.AddForce(this.jumpForce);
        }
        
        RaycastHit2D hit2D = Physics2D.Raycast(this.footHold.transform.position, Vector2.down, 0.01f);
        if (hit2D.collider)
        {
            this.standStatus = CharacterStandStatus.onGround;
        }
        else
        {
            this.standStatus = CharacterStandStatus.inAir;
        }

        if (this.standStatus == CharacterStandStatus.inAir)
        {
            hit2D = Physics2D.Raycast(this.footHold.transform.position, Vector2.down, this.highyDropHeight);
            if (!hit2D.collider)
            {
                this.triggerSatetTrans("startHighDrop");
            }

        }     
    }

    private void triggerSatetTrans(string param)
    {
        int layer = 0;
        switch(param)
        {
            case "startRun":
                if (this.moveAnimator.GetCurrentAnimatorStateInfo(layer).IsName("Idle"))
                {
                    this.moveAnimator.SetTrigger(param);
                }
                break;
            case "stopRun":
                if (this.moveAnimator.GetCurrentAnimatorStateInfo(layer).IsName("Running") || this.moveAnimator.GetCurrentAnimatorStateInfo(layer).IsName("StartRun"))
                {
                    this.moveAnimator.SetTrigger(param);
                }
                break;
            case "startJump":
                if (this.moveAnimator.GetCurrentAnimatorStateInfo(layer).IsName("Idle") || this.moveAnimator.GetCurrentAnimatorStateInfo(layer).IsName("StartRun") 
                || this.moveAnimator.GetCurrentAnimatorStateInfo(layer).IsName("Running"))
                {
                    this.moveAnimator.SetTrigger(param);
                }
                break;
            case "lowDropOver":
                if (this.moveAnimator.GetCurrentAnimatorStateInfo(layer).IsName("Jump"))
                {
                    this.moveAnimator.SetTrigger(param);
                }
                break;
            case "startHighDrop":
                if (this.moveAnimator.GetCurrentAnimatorStateInfo(layer).IsName("Idle") || this.moveAnimator.GetCurrentAnimatorStateInfo(layer).IsName("StartRun")
                || this.moveAnimator.GetCurrentAnimatorStateInfo(layer).IsName("Running") || this.moveAnimator.GetCurrentAnimatorStateInfo(layer).IsName("Jump"))
                {
                    this.moveAnimator.SetTrigger(param);
                }
                break;
            case "highDropOver":
                if (this.moveAnimator.GetCurrentAnimatorStateInfo(layer).IsName("HighDrop"))
                {
                    this.moveAnimator.SetTrigger(param);
                }
                break;
            default:
                print("参数有误");
                break;

        }
    }

    public void resetCharacter()
    {
        this.rigidBody.isKinematic = true;
        this.isRightDir = true;
        transform.SetPositionAndRotation(this.characterIniPos, this.characterIniRot);
        this.rigidBody.isKinematic = false;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name.Substring(0, 5) == "Grass" && this.standStatus == CharacterStandStatus.onGround)
        {
            this.triggerSatetTrans("highDropOver");
            this.triggerSatetTrans("lowDropOver");

            this.jumpLeftSeg = this.jumpMaxSeg;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
    }
    
}
