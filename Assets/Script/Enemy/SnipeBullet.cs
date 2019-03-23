using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnipeBullet : MonoBehaviour
{
    private Vector2 bulletImpulse;

    private float existTime;
    private Rigidbody2D rigidBody;
    private CharacterBehaviour character;

    public void setShootParam(Vector2 impulse, float existTime)
    {
        this.name = "bullet";
        this.bulletImpulse = impulse;
        this.existTime = existTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.character = References.character;
        this.character.resetMissionEvent += this.resetSnipeBullet;
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.rigidBody.AddForce(this.bulletImpulse, ForceMode2D.Impulse);
        Destroy(this.gameObject, this.existTime);
    }

    private void resetSnipeBullet()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void  OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.name == "characterCollider")
        {
            this.character.setCharacterDead();
        }
    }
}
