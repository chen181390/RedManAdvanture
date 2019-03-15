using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFrameData
{
    public Vector3 pos;
    public Vector2 velocity;
    public float angularVelocity;

    // 动画状态机相关
    public bool isRun;
    public bool isJump;
    public float horSpeed;
    public float vecHight;
    public bool  isGround;
    public bool triggerDead;
}
