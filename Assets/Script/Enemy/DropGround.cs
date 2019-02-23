using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGround : MonoBehaviour
{
    public float gravityScaleOnTrigger;
    private Rigidbody2D rigidBody;
    private CharacterBehaviour character;
    private GameObject DeathLineBottom;
    private Vector2 iniPos;
    private Quaternion iniRot;
    // Start is called before the first frame update
    void Start()
    {
        this.iniPos = this.transform.position;
        this.iniRot = this.transform.rotation;
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.rigidBody.gravityScale = 0;
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.DeathLineBottom = GameObject.Find("DeathLineBottom");
        this.character.resetMissionEvent += this.resetDropGround;
    }

    private void resetDropGround()
    {
        this.rigidBody.gravityScale = 0;
        this.transform.position = this.iniPos;
        this.transform.rotation = this.iniRot;
        this.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == this.character.transform)
        {
            this.rigidBody.gravityScale = this.gravityScaleOnTrigger;
        }
        else if (collision.transform == this.DeathLineBottom.transform)
        {
            this.gameObject.SetActive(false);
        }
    }
}
