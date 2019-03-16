using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShadow : MonoBehaviour
{
    private CharacterBehaviour character;
    private CharacterFrameData[] frameDatas;
    private int playIndex = 0;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private bool isAllowPlay;

    CharacterShadow() : base()
    {
        References.characterShadow = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.character = References.character;
        this.animator = this.GetComponent<Animator>();
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.transform.position = this.character.characterIniPos;
        this.transform.rotation = this.character.characterIniRot;
        this.character.resetMissionEvent += this.resetShadow;
        this.gameObject.SetActive(false);
    }

    public void setCharacterFrameDatas(CharacterFrameData[] frameDatas)
    {
        this.frameDatas = frameDatas;
        this.isAllowPlay = false;
    }

    void resetShadow()
    {
        this.gameObject.SetActive(true);
        this.playIndex = 0;
        this.animator.CrossFade(AniHashCode.Idle, 0, 0, 0, 0);
        this.isAllowPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.frameDatas == null || this.playIndex >= this.frameDatas.Length)
            return;

        var frameData = this.frameDatas[this.playIndex];

        if (frameData.isFirstFrame) 
        {
            this.transform.SetPositionAndRotation(frameData.iniPos, frameData.iniRot);
        }

        // 播放移动旋转
        this.rigidBody.velocity = frameData.velocity;
        this.rigidBody.angularVelocity = frameData.angularVelocity;

        this.animator.SetBool(AniHashCode.isBtnRun, frameData.isRun);
        this.animator.SetBool(AniHashCode.isBtnJump, frameData.isJump);
        this.animator.SetFloat(AniHashCode.horSpeed, frameData.horSpeed);
        this.animator.SetFloat(AniHashCode.vecHeight, frameData.vecHight);
        this.animator.SetBool(AniHashCode.isGround, frameData.isGround);
        if (frameData.triggerDead)
        {
            this.animator.SetTrigger(AniHashCode.triggerDead);
        }
        this.playIndex++;

    }

    void resetMission() { }

}
