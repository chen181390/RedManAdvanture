using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SawType
{
    HorMoveUntilBlock,
    VerMoveUntilBlock
}
public class Saw : MonoBehaviour
{
    public Vector2 direct;
    public SawType sawType;
    public float speed;
    private Vector3 iniPos;
    private CharacterBehaviour character;


    // Start is called before the first frame update
    void Start()
    {
        this.iniPos = this.transform.position;
        this.direct = this.direct.normalized;
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.sawType)
        {
            case SawType.HorMoveUntilBlock:
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

        this.direct = - this.direct;
    }
}
