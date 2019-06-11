using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpike : MonoBehaviour
{
    private float force = 12;
    private float existTime = 5;
    private CharacterBehaviour character;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update


    void Start()
    {
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.shoot();

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= this.existTime)
        {
            Destroy(this.gameObject, 1);
        }
    }
    public void shoot()
    {
        var direct = (this.character.transform.position - this.transform.position).normalized;
        this.rigidBody.AddForce(this.force * direct, ForceMode2D.Impulse);
        this.rigidBody.AddTorque(1000);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.name == "characterCollider")
        {
            this.character.setCharacterDead();
        }
    }
}
