using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpeedTripType
{
    motionless
}
public class SpeedTrip : MonoBehaviour
{
    // public SpeedTripType type;
    public float speedRate;
    private CharacterBehaviour character;
    private Rigidbody2D characterRigidBody;
    private bool isInRange;
    private List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();

    // Start is called before the first frame update
    void Start()
    {
        this.character = References.character;
        this.characterRigidBody = this.character.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isInRange)
        {
            // this.characterRigidBody.velocity *= this.speedRate;
            foreach(Rigidbody2D rigidbody in this.rigidbodies)
            {
                if (rigidbody != null)
                    rigidbody.velocity *= this.speedRate;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "characterCollider")
        {
            this.isInRange = true;
            this.character.isReleaseSpeed = true;
            this.rigidbodies.Add(this.characterRigidBody);
        }
        else if (collider.name == "bullet")
        {
            this.isInRange = true;
            var rigidbody = collider.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
                this.rigidbodies.Add(rigidbody);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "characterCollider")
        {
            this.character.isReleaseSpeed = false;
            this.rigidbodies.Remove(this.characterRigidBody);
            if (this.rigidbodies.Count == 0)
                this.isInRange = false;
        }
        else if (collider.name == "bullet")
        {
            var rigidbody = collider.GetComponent<Rigidbody2D>();
            this.rigidbodies.Remove(rigidbody);
            if (this.rigidbodies.Count == 0)
                this.isInRange = false;
        }

  
    }
}
