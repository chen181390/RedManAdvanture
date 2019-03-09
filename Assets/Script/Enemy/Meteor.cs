using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Vector3 target;
    public bool isShootMeteor;
    public float impulse;
    private Rigidbody2D rigidBody;
    private Animator cameraAnim;
    private CharacterBehaviour character;

    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.cameraAnim = GameObject.Find("Main Camera").GetComponent<Animator>();
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.character.resetMissionEvent += resetMeteor;
    }

    public void resetMeteor()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isShootMeteor)
        {
            this.isShootMeteor = false;
            var direct = (this.target - this.transform.position).normalized;
            this.rigidBody.AddForce(direct * this.impulse, ForceMode2D.Impulse);
            this.cameraAnim.SetTrigger(AniHashCode.triggerShake);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.cameraAnim.GetCurrentAnimatorStateInfo(0).shortNameHash == AniHashCode.Shake)
            this.cameraAnim.SetTrigger(AniHashCode.triggerEndShake);

        if (collision.transform.name == "Character")
        {
            this.character.setCharacterDead();
        } 
        else if (collision.transform.name == "DeathLineBottom")
        {
            this.character.resetMissionEvent -= this.resetMeteor;
            Destroy(this.gameObject, 1);
        }
    }

}
