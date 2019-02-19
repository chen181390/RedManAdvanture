﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TrapType
{
    Motionless,
    Hiden,
    DropDown
}
public class SpikeTrap : MonoBehaviour
{
    private CharacterBehaviour character;
    public TrapType trapType;
    private Vector2 iniPos;
    private Collider2D[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.character.resetMissionEvent += this.iniSpikeTrap;
        this.iniPos = this.transform.position;
        this.colliders = this.GetComponents<Collider2D>();

        this.iniSpikeTrap();
    }

    public void iniSpikeTrap()
    {
        switch (this.trapType)
        {
            case TrapType.Motionless:
                this.colliders[1].enabled = true;
                break;

            case TrapType.Hiden:
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    var obj = this.transform.GetChild(i).gameObject;
                    obj.SetActive(false);
                    obj.transform.localScale = new Vector3(0.4f, 0.4f, 1);
                    var animator = obj.GetComponent<Animator>();
                    if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != AniHashCode.Idle)
                    {
                        animator.SetTrigger(AniHashCode.triggerReset);
                    }
                }
                this.colliders[1].enabled = true;
                break;

            case TrapType.DropDown:
                var rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
                if (!rigidBody)
                {
                    rigidBody = this.gameObject.AddComponent<Rigidbody2D>();
                    rigidBody.freezeRotation = true;
                }
                rigidBody.gravityScale = 0;
                rigidBody.velocity = Vector2.zero;
                this.transform.position = this.iniPos;
                this.colliders[1].enabled = true;
                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.name == "characterCollider")
        {
            if (!this.colliders[1].enabled)
            {
                this.character.resetMission();
                return;
            }

            switch (this.trapType)
            {
                case TrapType.Hiden:
                    this.colliders[1].enabled = false;
                    for (int i = 0; i < this.transform.childCount; i++)
                    {
                        var obj = this.transform.GetChild(i).gameObject;
                        obj.SetActive(true);
                        var animator = obj.GetComponent<Animator>();
                        animator.SetTrigger(AniHashCode.triggerPopUp);
                    }
                    break;

                case TrapType.DropDown:
                    this.colliders[1].enabled = false;
                    var rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
                    rigidBody.gravityScale = 1;
                    break;

            }
        }
        else if (collider.transform.name == "DeathLineBottom")
        {
            if (this.trapType == TrapType.DropDown)
            {
                var rigidBody = this.GetComponent<Rigidbody2D>();
                rigidBody.gravityScale = 0;
                rigidBody.velocity = Vector2.zero;
            }
        }
    }


}
