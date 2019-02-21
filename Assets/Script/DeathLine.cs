using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeathLineType
{
    up,
    down,
    left,
    right,

}
public class DeathLine : MonoBehaviour
{    
    private CharacterBehaviour character;
    public DeathLineType deathLineType;

    // Start is called before the first frame update
    void Start()
    {
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch(this.deathLineType) 
        {
            case DeathLineType.down:
                if (collision.transform == this.character.transform)
                {
                    this.character.resetMission();
                }
                break;
        }

    }
}
