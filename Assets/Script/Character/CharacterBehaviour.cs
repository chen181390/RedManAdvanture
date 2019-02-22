﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterBehaviour : MonoBehaviour
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
    private float jumpIniSpeed = 10;

    public EventTrigger leftTrigger;
    public EventTrigger rightTrigger;
    public EventTrigger jumpTrigger;
    public Sprite[] btnLeftImgs = new Sprite[2];
    public Sprite[] btnRightImgs = new Sprite[2];
    public Sprite[] btnJumpImgs = new Sprite[2];

    public delegate void ResetMissionDelegate();
    public event ResetMissionDelegate resetMissionEvent;





    // Start is called before the first frame update
    void Start()
    {
        this.moveAnimator = GetComponent<Animator>();
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.characterIniPos = this.transform.position;
        this.characterIniRot = this.transform.rotation;
        this.jumpLeftSeg = this.jumpMaxSeg;
        this.registerTrigger();
    }

    public void setCharacterIniPos(Vector3 iniPos)
    {
        this.characterIniPos = iniPos;
    }

    private void registerTrigger()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnLeftDown);
        this.leftTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnLeftUp);
        this.leftTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnLeftUp);
        this.leftTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnRightDown);
        this.rightTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnRightUp);
        this.rightTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnRightUp);
        this.rightTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnJumpDown);
        this.jumpTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnJumpUp);
        this.jumpTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnJumpUp);
        this.jumpTrigger.triggers.Add(entry);

    }

    private void onBtnLeftDown(BaseEventData eventData = null)
    {
        Image btnImg = this.leftTrigger.GetComponent<Image>();
        btnImg.sprite = this.btnLeftImgs[1];
        this.isRun = true;
        if (this.isRightDir)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            this.isRightDir = false;
        }
    }

    private void onBtnLeftUp(BaseEventData eventData = null)
    {   
        Image btnImg = this.leftTrigger.GetComponent<Image>();
        btnImg.sprite = this.btnLeftImgs[0];
         if (this.isRun && !this.isRightDir)
        {
            this.isRun = false;
        }
    }

    public void onBtnRightDown(BaseEventData eventData = null)
    {
        Image btnImg = this.rightTrigger.GetComponent<Image>();
        btnImg.sprite = this.btnRightImgs[1];
        this.isRun = true;
        if (!this.isRightDir)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            this.isRightDir = true;
        }
    }

    public void onBtnRightUp(BaseEventData eventData = null)
    {
        Image btnImg = this.rightTrigger.GetComponent<Image>();
        btnImg.sprite = this.btnRightImgs[0];
        if (this.isRun && this.isRightDir)
        {
            this.isRun = false;
        }
    }

    private void onBtnJumpDown(BaseEventData eventData = null)
    {
        Image btnImg = this.jumpTrigger.GetComponent<Image>();
        btnImg.sprite = this.btnJumpImgs[1];
        if (this.jumpLeftSeg > 0 && !isJump)
        {
            this.isJump = true;
            this.rigidBody.velocity = new Vector2(this.rigidBody.velocity.x, this.jumpIniSpeed);
            this.jumpLeftSeg--;
        }
    }

    private void onBtnJumpUp(BaseEventData eventData = null)
    {
        Image btnImg = this.jumpTrigger.GetComponent<Image>();
        btnImg.sprite = this.btnJumpImgs[0];
        if (this.isJump)
        {
            this.rigidBody.AddForce(new Vector2(0, -180));
            this.isJump = false;
        }
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            this.onBtnLeftDown();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            this.onBtnRightDown();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            this.onBtnJumpDown();
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            this.onBtnLeftUp();
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            this.onBtnRightUp();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            this.onBtnJumpUp();
        }

        if (this.isJump && this.rigidBody.velocity.y <= 0)
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
            }
            else
            {
                this.isGround = false;
            }
            this.moveAnimator.SetBool(AniHashCode.isGround, this.isGround);
        }


    }

    public void resetMission()
    {
        this.rigidBody.velocity = Vector2.zero;
        this.rigidBody.angularVelocity = 0;
        this.isRightDir = true;
        transform.SetPositionAndRotation(this.characterIniPos, this.characterIniRot);

        this.resetMissionEvent();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.isGround)
        {
            this.jumpLeftSeg = this.jumpMaxSeg;
        }
    }
}
