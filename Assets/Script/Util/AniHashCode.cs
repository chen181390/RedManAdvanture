using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniHashCode
{
    // common
    public static int Idle = Animator.StringToHash("Idle");
    public static int triggerReset = Animator.StringToHash("triggerReset");

    // HomePageUI
    public static int triggerStartGame = Animator.StringToHash("triggerStartGame");

    // Character
    #region 
    public static int isGround = Animator.StringToHash("isGround");
    public static int isBtnRun = Animator.StringToHash("isBtnRun");
    public static int isBtnJump = Animator.StringToHash("isBtnJump");
    public static int vecHeight = Animator.StringToHash("vecHeight");
    public static int horSpeed = Animator.StringToHash("horSpeed");
    public static int triggerDead = Animator.StringToHash("triggerDead");
    public static int triggerRebirth = Animator.StringToHash("triggerRebirth");
    #endregion

    // SpikeTrap
    #region
    public static int PopUp = Animator.StringToHash("PopUp");
    public static int triggerPopUp = Animator.StringToHash("triggerPopUp");
    #endregion

    // shootMace
    #region 
    public static int triggerAwake = Animator.StringToHash("triggerAwake");
    public static int triggerSleep = Animator.StringToHash("triggerSleep");
    #endregion

    // jumpMace
    #region 
    public static int triggerAttack = Animator.StringToHash("triggerAttack");
    public static int IdleToAttackJump = Animator.StringToHash("Idle -> AttackJump");
    #endregion

    // Main Camera
    #region 
    public static int triggerShake = Animator.StringToHash("triggerShake");
    public static int triggerEndShake = Animator.StringToHash("triggerEndShake");
    public static int Shake = Animator.StringToHash("Shake");
    public static int ShakeToIdle = Animator.StringToHash("ShakeToIdle");
    #endregion

    // cloud
    public static int triggerFloat = Animator.StringToHash("triggerFloat");
    public static int triggerIdle = Animator.StringToHash("triggerIdle");

}
