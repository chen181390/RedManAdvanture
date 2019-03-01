using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum DeathLineType
{
    Up,
    Down,
    Left,
    Right,

}
public class DeathLine : MonoBehaviour
{    
    private CharacterBehaviour character;
    public DeathLineType deathLineType;
    public Image touchBlock;

    private AsyncOperation asyncOperation;
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
        if (collision.transform == this.character.transform)
        {
            switch (this.deathLineType)
            {
                case DeathLineType.Down:
                    this.character.setCharacterDead();
                    break;

                case DeathLineType.Right:
                    // this.asyncOperation = SceneManager.LoadSceneAsync(2);
                    break;


            }
        }



    }
}
