using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatPath : MonoBehaviour
{
    private CharacterBehaviour character;
    private CharacterCamera characterCamera;
    // Start is called before the first frame update
    void Start()
    {
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.characterCamera = GameObject.Find("Main Camera").GetComponent<CharacterCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == this.character.transform)
        {
            if (this.character.movePathType != MovePathType.FlatPath)
            {
                this.characterCamera.resetCamera();
            }
        }
    }
}
