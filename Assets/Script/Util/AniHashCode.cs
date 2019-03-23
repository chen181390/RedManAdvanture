using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniHashCode
{
    // common
    public static readonly int Idle = Animator.StringToHash("Idle");
    public static readonly int triggerReset = Animator.StringToHash("triggerReset");

    // HomePageUI
    public static readonly int triggerStartGame = Animator.StringToHash("triggerStartGame");

    // Character
    #region
    public static readonly int isGround = Animator.StringToHash("isGround");
    public static readonly int isBtnRun = Animator.StringToHash("isBtnRun");
    public static readonly int isBtnJump = Animator.StringToHash("isBtnJump");
    public static readonly int vecHeight = Animator.StringToHash("vecHeight");
    public static readonly int horSpeed = Animator.StringToHash("horSpeed");
    public static readonly int triggerDead = Animator.StringToHash("triggerDead");
    public static readonly int triggerRebirth = Animator.StringToHash("triggerRebirth");
    #endregion

    // SpikeTrap
    #region
    public static readonly int PopUp = Animator.StringToHash("PopUp");
    public static readonly int triggerPopUp = Animator.StringToHash("triggerPopUp");
    #endregion

    // shootMace
    #region 
    public static readonly int triggerAwake = Animator.StringToHash("triggerAwake");
    public static readonly int triggerSleep = Animator.StringToHash("triggerSleep");
    #endregion

    // jumpMace
    #region 
    public static readonly int triggerAttack = Animator.StringToHash("triggerAttack");
    public static readonly int IdleToAttackJump = Animator.StringToHash("Idle -> AttackJump");
    #endregion

    // Main Camera
    #region 
    public static readonly int triggerShake = Animator.StringToHash("triggerShake");
    public static readonly int triggerEndShake = Animator.StringToHash("triggerEndShake");
    public static readonly int Shake = Animator.StringToHash("Shake");
    public static readonly int ShakeToIdle = Animator.StringToHash("ShakeToIdle");
    #endregion

    // cloud
    public static readonly int triggerFloat = Animator.StringToHash("triggerFloat");
    public static readonly int triggerIdle = Animator.StringToHash("triggerIdle");

    // SniperMace
    public static readonly int triggerShoot = Animator.StringToHash("triggerShoot");
    public static readonly int triggerShootEnd = Animator.StringToHash("triggerShootEnd");

}
