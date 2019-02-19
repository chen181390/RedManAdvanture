using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniHashCode
{
    // common
    public static int Idle = Animator.StringToHash("Idle");

    // CharacterMove
    #region 
    public static int isGround = Animator.StringToHash("isGround");
    public static int isBtnRun = Animator.StringToHash("isBtnRun");
    public static int isBtnJump = Animator.StringToHash("isBtnJump");
    public static int vecHeight = Animator.StringToHash("vecHeight");
    public static int horSpeed = Animator.StringToHash("horSpeed");
    #endregion

    // SpikeTrap
    #region
    public static int PopUp = Animator.StringToHash("PopUp");
    public static int triggerPopUp = Animator.StringToHash("triggerPopUp");
    public static int triggerReset = Animator.StringToHash("triggerReset");
    #endregion
}
