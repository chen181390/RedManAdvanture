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
    public CharacterBehaviour characterMove;
    public DeathLineType deathLineType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        switch(this.deathLineType) 
        {
            case DeathLineType.down:
                this.characterMove.resetCharacter();
                break;
            default:
                break;
        }

    }
}
