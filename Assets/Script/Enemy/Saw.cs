﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SawType
{
    DirectByRoute
}
public class Saw : MonoBehaviour
{
    public Vector2[] routePoints;
    public SawType sawType;
    private Vector2 direct;
    public float speed;
    private CharacterBehaviour character;
    private int routeTarget = 1;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0 ; i < this.routePoints.Length ; i++)
        {
            this.routePoints[i] = this.transform.parent.TransformPoint(this.routePoints[i]);
        }
        this.transform.position = this.routePoints[0];
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.character.resetMissionEvent += this.resetSaw;
    }

    private void resetSaw()
    {
        this.transform.position = this.routePoints[0];
        this.routeTarget = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.sawType)
        {
            case SawType.DirectByRoute:
                var direct = (this.routePoints[routeTarget] - (Vector2)this.transform.position).normalized;
                var delta = direct * this.speed * Time.deltaTime;
                var nextPos = (Vector2)this.transform.position + delta;
                var nextDirect = (this.routePoints[routeTarget] - nextPos).normalized;
                // 到达目标点
                if (nextDirect != direct)
                {
                    this.transform.Translate(this.routePoints[routeTarget] - (Vector2)this.transform.position, Space.World);
                    if (this.routeTarget == this.routePoints.Length - 1)
                    {
                        this.routeTarget = 0;
                    }
                    else
                    {
                        this.routeTarget ++;
                    }
  
                }
                else
                {
                    this.transform.Translate(delta, Space.World);
                }
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Character")
        {
            this.character.setCharacterDead();
            return;
        }
    }
}
