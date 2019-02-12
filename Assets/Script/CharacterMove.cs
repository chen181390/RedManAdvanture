using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    
    public bool isMoveLeft = false;
    public bool isMoveRight = false;
    public bool isJump = false;
    private Animator moveAnimator;
    private float walkSpeed = 5;
    private float jumpMaxHeight = 5;
    private float currJumpHeight = 0;
    private float jumpSpeed = 0.5f;
    private bool isRightDir = true;
    private Vector3 characterIniPos;
    private Quaternion characterIniRot;



    // Start is called before the first frame update
    void Start()
    {
        this.moveAnimator = GetComponent<Animator>();
        this.characterIniPos = transform.position;
        this.characterIniRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        this.moveControl();
        this.moveUpdate();
    }

    private void moveControl() {
         if (Input.GetKeyDown(KeyCode.A))
        {
            this.moveAnimator.SetTrigger("startWalk");
            this.isMoveLeft = true;
            if (this.isRightDir)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                this.isRightDir = false;
            }
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            this.moveAnimator.SetTrigger("startWalk");
            this.isMoveRight = true;
            if (!this.isRightDir)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                this.isRightDir = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            this.moveAnimator.SetTrigger("stopWalk");
            this.isMoveLeft = false;
        }

      
        if (Input.GetKeyUp(KeyCode.D))
        {
            gameObject.transform.Rotate(Vector3.right);
            this.moveAnimator.SetTrigger("stopWalk");
            this.isMoveRight = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            this.moveAnimator.SetTrigger("startJump");
            this.currJumpHeight = 0;
            this.isJump = true;
        }
    }

    private void moveUpdate() 
    {
        if (this.isMoveLeft || this.isMoveRight) {
            gameObject.transform.Translate(Vector3.right * this.walkSpeed * Time.deltaTime);
        }

        if (this.isJump && this.currJumpHeight < this.jumpMaxHeight) 
        {
            float delta = this.jumpSpeed;
            if (this.currJumpHeight + this.jumpSpeed >= this.jumpMaxHeight)
            {
                delta = this.jumpMaxHeight - this.currJumpHeight;
                this.isJump = false;
            }
            gameObject.transform.Translate(Vector3.up * delta);
            this.currJumpHeight += delta;
        }
    }

    public void resetCharacter() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.isRightDir = true;
        transform.SetPositionAndRotation(this.characterIniPos, this.characterIniRot);
    }

    
}
