using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CloudType
{
    Motionless,
    CallMeteor
}
public class Cloud : MonoBehaviour
{
    public CloudType type;
    public GameObject MeteorPrefab;
    public Vector2 meteorPos;
    public float meteorImpulse;
    private bool isMeteor;
    private CharacterBehaviour character;

    // Start is called before the first frame update
    void Start()
    {
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.character.resetMissionEvent += resetCloud;
    }

    public void resetCloud()
    {
        switch (this.type)
        {
            case CloudType.CallMeteor:
                this.isMeteor = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.transform.name == "Character")
        {
            switch (this.type)
            {
                case CloudType.CallMeteor:
                    if (!this.isMeteor)
                    {
                        var meteor = Instantiate(this.MeteorPrefab, this.meteorPos, Quaternion.Euler(0, 0, 0));
                        Meteor meteorScript = meteor.GetComponent<Meteor>();
                        meteorScript.target = this.transform.position;
                        meteorScript.impulse = this.meteorImpulse;
                        meteorScript.isShootMeteor = true;
                    }

                    this.isMeteor = true;
                    break;
            }
        }
    }
}
