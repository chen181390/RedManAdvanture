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
    private Animator animator;
    private Rigidbody2D rigidBody;
    private bool isRightDir = true;
    private int jumpMaxSeg = 1;
    private int jumpLeftSeg;
    private float maxRunSpeed = 10;
    private float jumpIniSpeed = 7;
    private bool isDeading = false;
    public PathBlockType pathType;
    private CharacterCamera characterCamera;


    public float flatRunForce = 8;
    public float upHillRunForce = 10;
    public float downHillRunForce = 8;
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
        this.characterCamera = GameObject.Find("Main Camera").GetComponent<CharacterCamera>();
        this.animator = GetComponent<Animator>();
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
        if (this.jumpLeftSeg > 0 && !isJump && this.isGround)
        {
            this.isJump = true;
            this.rigidBody.velocity += new Vector2(0, this.jumpIniSpeed);

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
                    Vector2 force;
                    if (!this.isJump)
                    {
                        switch (this.pathType)
                        {
                            case PathBlockType.FlatPath:
                                force = this.flatRunForce * Vector2.right;
                                break;

                            case PathBlockType.LeftHillPath:
                                force = this.upHillRunForce * (new Vector2(1, 1).normalized);
                                break;

                            default:
                                force = this.downHillRunForce * (new Vector2(1, -1).normalized);
                                break;

                        }
                    } 
                    else
                    {
                        force = this.flatRunForce * Vector2.right;
                    }
                    this.rigidBody.AddForce(force);
                }
                else
                {
                    Vector2 force;
                    if (!this.isJump)
                    {
                        switch (this.pathType)
                        {
                            case PathBlockType.FlatPath:
                                force = this.flatRunForce * Vector2.left;
                                break;

                            case PathBlockType.LeftHillPath:
                                force = this.downHillRunForce * (new Vector2(-1, -1).normalized);
                                break;

                            default:
                                force = this.upHillRunForce * (new Vector2(-1, 1).normalized);
                                break;
                        }
                    }
                    else
                    {
                        force = this.flatRunForce * Vector2.left;
                    }
                    this.rigidBody.AddForce(force);
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
        this.animator.SetBool(AniHashCode.isBtnRun, this.isRun);
        this.animator.SetBool(AniHashCode.isBtnJump, this.isJump);
        this.animator.SetFloat(AniHashCode.horSpeed, System.Math.Abs(this.rigidBody.velocity.x));

        RaycastHit2D hit2D = Physics2D.Raycast(this.transform.position, Vector2.down, 50, 511);
        if (hit2D.collider)
        {
            this.animator.SetFloat(AniHashCode.vecHeight, hit2D.distance);
            if (hit2D.distance <= 1.3)
            {
                this.isGround = true;
            }
            else
            {
                this.isGround = false;
            }
            this.animator.SetBool(AniHashCode.isGround, this.isGround);
        }


    }

    public void setCharacterDead()
    {
        if (this.isDeading)
        {
            return;
        }
        this.rigidBody.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
        this.isDeading = true;
        this.animator.SetTrigger(AniHashCode.triggerDead);
        this.leftTrigger.transform.parent.gameObject.SetActive(false);
        this.rigidBody.angularVelocity = 0;
        this.isRun = false;
        this.isJump = false;
    }

    public void resetMission()
    {
        this.rigidBody.velocity = Vector2.zero;
        this.rigidBody.angularVelocity = 0;
        this.isRightDir = true;
        transform.SetPositionAndRotation(this.characterIniPos, this.characterIniRot);
        this.animator.SetTrigger(AniHashCode.triggerRebirth);
        this.leftTrigger.transform.parent.gameObject.SetActive(true);

        this.onBtnJumpUp();
        this.onBtnLeftUp();
        this.onBtnRightUp();

        this.resetMissionEvent();
        this.isDeading = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.isGround)
        {
            this.jumpLeftSeg = this.jumpMaxSeg;
        }
    }
}
