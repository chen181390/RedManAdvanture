using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SawType
{
    MoveUntilBlock,
}
public class Saw : MonoBehaviour
{
    public Vector2 iniDirect;
    public SawType sawType;
    public float speed;
    private Vector3 iniPos;
    private CharacterBehaviour character;
    private float sawRadius;
    private GameObject checkBlockPoint1;
    private GameObject checkBlockPoint2;
    private Vector2 direct;


    // Start is called before the first frame update
    void Start()
    {
        this.iniPos = this.transform.position;
        this.iniDirect = this.iniDirect.normalized;
        this.direct = this.iniDirect;
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        var rigidbody = this.GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
        this.sawRadius = this.GetComponent<CircleCollider2D>().radius;
        this.checkBlockPoint1 = GameObject.Find("checkBlockPoint1");
        this.checkBlockPoint2 = GameObject.Find("checkBlockPoint2");

    }

    // Update is called once per frame
    void Update()
    {
        switch(this.sawType)
        {
            case SawType.MoveUntilBlock:
                RaycastHit2D hit;
                if (direct.Equals(this.iniDirect))
                {
                    hit = Physics2D.Raycast(this.checkBlockPoint1.transform.position, this.direct, 0.1f);
                }
                else
                {
                    hit = Physics2D.Raycast(this.checkBlockPoint2.transform.position, this.direct, 0.1f);
                }
                if (hit.transform)
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
