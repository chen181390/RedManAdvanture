using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGround : MonoBehaviour
{
    public float gravityScaleOnTrigger = 20;
    private Rigidbody2D rigidBody;
    private CharacterBehaviour character;
    private GameObject DeathLineBottom;
    private Vector2 iniPos;
    private Quaternion iniRot;
    private CharacterCamera characterCamera;
    // Start is called before the first frame update
    void Start()
    {
        this.iniPos = this.transform.position;
        this.iniRot = this.transform.rotation;
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.rigidBody.gravityScale = 0;
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.DeathLineBottom = GameObject.Find("DeathLineBottom");
        this.characterCamera = GameObject.Find("Main Camera").GetComponent<CharacterCamera>();
        this.character.resetMissionEvent += this.resetDropGround;
    }

    private void resetDropGround()
    {
        this.rigidBody.gravityScale = 0;
        this.rigidBody.velocity = Vector2.zero;
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
            if (this.character.movePathType != MovePathType.FlatPath)
            {
                this.characterCamera.resetCamera();
            }
        }
        else if (collision.transform == this.DeathLineBottom.transform)
        {
            this.gameObject.SetActive(false);
        }
    }
}
