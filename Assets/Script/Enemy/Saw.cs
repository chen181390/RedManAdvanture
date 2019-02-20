using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SawType
{
    MoveUntilBlock,
}
public class Saw : MonoBehaviour
{
    public Vector2 direct;
    public SawType sawType;
    public float speed;
    private Vector3 iniPos;
    private CharacterBehaviour character;
    private GameObject posCheckPoint;
    private GameObject negCheckPoint;
    private float colliderRadius;


    // Start is called before the first frame update
    void Start()
    {
        this.iniPos = this.transform.position;
        this.direct.Normalize();
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.colliderRadius = this.GetComponent<CircleCollider2D>().radius;
        this.setCheckPoint();
    }

    private void setCheckPoint()
    {
        var checkPoint1 = this.transform.parent.Find("checkPoint1").gameObject;
        var checkPoint2 = this.transform.parent.Find("checkPoint2").gameObject;
        var direct1 = checkPoint1.transform.position - this.transform.parent.transform.position;
        var direct2 = checkPoint2.transform.position - this.transform.parent.transform.position;
        if (Vector2.Angle(direct, direct1) < Vector2.Angle(direct, direct2))
        {
            this.posCheckPoint = checkPoint1;
            this.negCheckPoint = checkPoint2;
        }
        else
        {
            this.posCheckPoint = checkPoint2;
            this.negCheckPoint = checkPoint1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.sawType)
        {
            case SawType.MoveUntilBlock:
                var hit = Physics2D.Raycast((Vector2)this.transform.position + this.direct * (this.colliderRadius + 0.1f), this.direct, 0.1f);
                if (hit.transform == this.posCheckPoint.transform || hit.transform == this.negCheckPoint.transform)
                {
                    this.direct = - this.direct;
                }
                this.transform.Translate(this.direct * Time.deltaTime * speed);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "characterCollider")
        {
            this.character.resetMission();
            return;
        }
    }
}
