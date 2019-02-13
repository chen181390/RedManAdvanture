using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ChacterStatus
{
    onGround,
    inAir
}
public class CharacterMove : MonoBehaviour
{
    public bool isMoveLeft = false;
    public bool isMoveRight = false;
    public bool isJump = false;
    private Animator moveAnimator;
    private Rigidbody2D rigidBody;
    private float runSpeed = 5;
    private float jumpMaxHeight = 5;
    private float currJumpHeight = 0;
    private float jumpSpeed = 0.3f;
    private bool isRightDir = true;
    private Vector3 characterIniPos;
    private Quaternion characterIniRot;
    private int jumpAllSegNum = 200;
    private int jumpSegNum;
    private int touchesNum = 0;
    private bool isDrop = false;

    // Start is called before the first frame update
    void Start()
    {
        this.moveAnimator = GetComponent<Animator>();
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.characterIniPos = transform.position;
        this.characterIniRot = transform.rotation;
        this.jumpSegNum = this.jumpAllSegNum;
    }

    // Update is called once per frame
    void Update()
    {
        this.moveControl();
        this.moveUpdate();
        if (this.touchesNum <= 0 && !this.isJump && !this.isDrop)
        {
            this.isDrop = true;
            this.jumpSegNum = 0;
            this.moveAnimator.SetTrigger("startDrop");
        }
    }

    private void moveControl() {
        // 按下按键
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.moveAnimator.SetTrigger("startRun");
            this.isMoveLeft = true;
            if (this.isRightDir)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                this.isRightDir = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.moveAnimator.SetTrigger("startRun");
            this.isMoveRight = true;
            if (!this.isRightDir)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                this.isRightDir = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && this.jumpSegNum -- > 0)
        {
            this.isJump = true;
        }

        // 松开按键
        if (Input.GetKeyUp(KeyCode.A) && this.isMoveLeft)
        {
            this.moveAnimator.SetTrigger("stopRun");
            this.isMoveLeft = false;
        }

      
        if (Input.GetKeyUp(KeyCode.D) && this.isMoveRight)
        {
            gameObject.transform.Rotate(Vector3.right);
            this.moveAnimator.SetTrigger("stopRun");
            this.isMoveRight = false;
        }

        if (Input.GetKeyUp(KeyCode.W) && this.isJump)
        {
            if (this.currJumpHeight > 2.5) {
                this.moveAnimator.SetTrigger("startDrop");
            }
            this.isJump = false;
            this.currJumpHeight = 0;
            this.isDrop = true;
        }
    }

    private void moveUpdate() 
    {
        if (this.isMoveLeft || this.isMoveRight) {
            gameObject.transform.Translate(Vector3.right * this.runSpeed * Time.deltaTime);
        }

        if (this.isJump && this.currJumpHeight < this.jumpMaxHeight) 
        {
            float delta = this.jumpSpeed;
            if (this.currJumpHeight + this.jumpSpeed >= this.jumpMaxHeight)
            {
                delta = this.jumpMaxHeight - this.currJumpHeight;
                this.isJump = false;
                this.moveAnimator.SetTrigger("startDrop");
                if (this.isMoveLeft || this.isMoveRight)
                {
                    this.moveAnimator.SetTrigger("stopRun");
                }
                this.currJumpHeight = 0;
                this.isDrop = true;

            }
            gameObject.transform.Translate(Vector3.up * delta);
            this.currJumpHeight += delta;
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
        this.touchesNum ++;
        if (collision.transform.name.Substring(0, 5) == "Grass" && this.isDrop && this.rigidBody.velocity == Vector2.zero)
        {
            this.moveAnimator.SetTrigger("DropOver");
            this.jumpSegNum = this.jumpAllSegNum;
            this.isDrop = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        this.touchesNum --;
    }
    
}
