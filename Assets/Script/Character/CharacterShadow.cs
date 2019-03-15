using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShadow : MonoBehaviour
{
    private CharacterBehaviour character;
    private CharacterFrameData[] frameDatas;
    private int playIndex = 0;
    private Animator animator;

    public void setCharacterFrameDatas(CharacterFrameData[] frameDatas)
    {
        this.frameDatas = frameDatas;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>() as CharacterBehaviour;
       this.animator = this.GetComponent<Animator>();
       this.character.resetMissionEvent += this.resetShadow;
    //    this.gameObject.SetActive(false);
    }

    void resetMission()
    {
    }

    void resetShadow()
    {
        this.playIndex = 0;
        this.animator.CrossFade(AniHashCode.Idle, 0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.frameDatas == null)
            return;

        if (this.playIndex >= this.frameDatas.Length)
        {
            this.frameDatas = null;
            this.playIndex = 0;
            return;
        }
    
        var frameData = this.frameDatas[this.playIndex];

        this.gameObject.SetActive(true);

        // 播放移动旋转
        this.transform.SetPositionAndRotation(frameData.pos, frameData.rot);

        this.animator.SetBool(AniHashCode.isBtnRun, frameData.isRun);
        this.animator.SetBool(AniHashCode.isBtnJump, frameData.isJump);
        this.animator.SetFloat(AniHashCode.horSpeed, frameData.horSpeed);
        this.animator.SetFloat(AniHashCode.vecHeight, frameData.vecHight);
        this.animator.SetBool(AniHashCode.isGround, frameData.isGround);
        if (frameData.triggerDead)
        {
            this.animator.SetTrigger(AniHashCode.triggerDead);
            this.playIndex = 0;
        }
        else
        {
            this.playIndex++;
        }

    }
}
