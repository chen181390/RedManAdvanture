﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MaceType
{
    TriggerThenTraceGlobal
}

public enum MaceStatus
{
    Untriggered,
    InTrace,
    InAttack
}

public class Mace : MonoBehaviour
{
    public MaceType maceType;
    public float traceSpeed;
    public float attackSpeed;
    private Vector2 iniPos;
    private MaceStatus status = MaceStatus.Untriggered;
    private CharacterBehaviour character;
    private Rigidbody2D characterRigidBody;
    private Collider2D triggerCollider;


    // Start is called before the first frame update
    void Start()
    {
        this.iniPos = this.transform.position;
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.characterRigidBody = this.character.GetComponent<Rigidbody2D>();
        this.triggerCollider = this.GetComponentsInChildren<Collider2D>()[2];
        this.character.resetMissionEvent += resetMace;
    }

    private void resetMace()
    {
        this.transform.position = this.iniPos;
        this.status = MaceStatus.Untriggered;
        this.triggerCollider.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(this.status)
        {
            case MaceStatus.InTrace:
                var direct = (this.character.transform.position - this.transform.position).normalized;
                this.transform.Translate(direct * this.traceSpeed * Time.deltaTime);
                break;

            case MaceStatus.InAttack:
                var direct2 = (this.character.transform.position - this.transform.position).normalized;
                this.transform.Translate(direct2 * this.attackSpeed * Time.deltaTime);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.name == "characterCollider")
        {
            switch (this.status)
            {
                case MaceStatus.Untriggered:
                    this.status = MaceStatus.InTrace;
                    this.triggerCollider.enabled = false;
                    break;

                case MaceStatus.InTrace:
                    this.status = MaceStatus.InAttack;
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.name == "characterCollider")
        {
            switch (this.status)
            {
                case MaceStatus.InAttack:
                    this.status = MaceStatus.InTrace;
                    break;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == this.character.transform)
        {
            this.character.setCharacterDead();
        }
    }
}
