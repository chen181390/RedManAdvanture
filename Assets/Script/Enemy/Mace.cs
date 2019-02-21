using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MaceType
{
    TriggerThenTrace
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
    public Vector2[] traceRoutePoints;
    public float traceSpeed;
    public float attackSpeed;
    private Vector2 iniPos;
    private MaceStatus status = MaceStatus.Untriggered;
    private CharacterBehaviour character;
    private Rigidbody2D characterRigidBody;
    private int targetIndex;
    private int finalTargetIndex;

    // Start is called before the first frame update
    void Start()
    {
        this.iniPos = this.transform.position;
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.characterRigidBody = this.character.GetComponent<Rigidbody2D>();

        for(int i = 0 ; i < this.traceRoutePoints.Length ; i++)
        {
            this.traceRoutePoints[i] = this.transform.parent.TransformPoint(this.traceRoutePoints[i]);
        }

    }

    private void resetMace()
    {
        this.transform.position = this.iniPos;
        this.status = MaceStatus.Untriggered;
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.status)
        {
            case MaceStatus.InTrace:
                if (this.transform.position.x > this.character.transform.position.x)
                {
                    for (int i = this.traceRoutePoints.Length - 1; i > 0; i--)
                    {
                        if (this.traceRoutePoints[i].x >= this.transform.position.x && this.traceRoutePoints[i - 1].x < this.transform.position.x)
                        {
                            this.targetIndex = i - 1;
                            break;
                        }
                    }
                }
                else if (this.transform.position.x < this.character.transform.position.x)
                {
                    for (int i = 0; i < this.traceRoutePoints.Length - 1; i++)
                    {
                        // 确定当前目标点
                        if (this.traceRoutePoints[i].x <= this.transform.position.x && this.traceRoutePoints[i + 1].x > this.transform.position.x)
                        {
                            this.targetIndex = i + 1;
                            break;
                        }
                    }
                }

                // 移动
                var direct = (this.traceRoutePoints[targetIndex] - (Vector2)this.transform.position).normalized;
                var delta = direct * this.traceSpeed * Time.deltaTime;
                var nextPos = (Vector2)this.transform.position + delta;
                var nextDirect = (this.traceRoutePoints[targetIndex] - nextPos).normalized;
                if (nextDirect != direct)
                {
                    this.transform.Translate(this.traceRoutePoints[targetIndex] - (Vector2)this.transform.position, Space.World);
                }
                else
                {
                    this.transform.Translate(delta, Space.World);
                }
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
                    break;

            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == this.character.transform)
        {
            this.character.resetMission();
        }
    }
}
