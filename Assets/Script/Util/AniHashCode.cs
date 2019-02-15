﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniHashCode
{
    // CharacterMove
    #region 
    public static int Idle = Animator.StringToHash("Idle");
    public static int StartRun = Animator.StringToHash("StartRun");
    public static int Running = Animator.StringToHash("Running");
    public static int Brake = Animator.StringToHash("Brake");
    public static int Jump = Animator.StringToHash("Jump");
    public static int HighDrop = Animator.StringToHash("HighDrop");

    public static int IdleToStartRun = Animator.StringToHash("IdleToStartRun");
    public static int IdleToJump = Animator.StringToHash("IdleToJump");
    public static int IdleToHighDrop = Animator.StringToHash("Idle -> HighDrop");
    public static int IdleToBrake = Animator.StringToHash("Idle -> Brake");
    public static int StartRunToIdle = Animator.StringToHash("StartRun -> Idle");
    public static int StartRunToRunning = Animator.StringToHash("StartRun -> Running");
    public static int RunningToBrake = Animator.StringToHash("Running -> Brake");
    public static int RunningToIdle = Animator.StringToHash("Running -> Idle");
    public static int BrakeToIdle = Animator.StringToHash("Brake -> Idle");
    public static int JumpToHighDrop = Animator.StringToHash("Jump -> HighDrop");
    public static int JumpToIdle = Animator.StringToHash("Jump -> Idle");
    public static int HighDropToIdle = Animator.StringToHash("HighDrop -> Idle");

    public static int startRun = Animator.StringToHash("startRun");
    public static int startJump = Animator.StringToHash("startJump");
    public static int startHighDrop = Animator.StringToHash("startHighDrop");
    public static int startBrake = Animator.StringToHash("startBrake");
    public static int toIdle = Animator.StringToHash("toIdle");
    #endregion
}
