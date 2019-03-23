using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperMace : MonoBehaviour
{
    public GameObject sniperBullet;
    public GameObject staticBullet;
    public float bulletExistTime;
    public Vector2 bulletImpulse;
    public float shootInterval;

    private bool isRight;
    private bool isInRange;
    private float preShootTime = -1000;
    private CharacterBehaviour character;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.character = References.character;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isInRange)
        {
            if (Time.time - this.preShootTime >= this.shootInterval)
            {
                this.startShoot();
                this.preShootTime = Time.time;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "characterCollider")
        {
            this.isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "characterCollider")
        {
            this.isInRange = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Character")
        {
            character.setCharacterDead();
        }
    }

    private void startShoot()
    {  
        this.animator.SetTrigger(AniHashCode.triggerShoot);
    }

    public void shootEnd()
    {
        this.animator.SetTrigger(AniHashCode.triggerShootEnd);
        var bullet = Instantiate(this.sniperBullet, this.staticBullet.transform.position, this.staticBullet.transform.rotation, this.transform);
        var bulletScript = bullet.GetComponent<SnipeBullet>();
        bulletScript.setShootParam(this.bulletImpulse, this.bulletExistTime);
    }
}
