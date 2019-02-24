using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HillPathType
{
    Up,
    Down
}
public class HillPath : MonoBehaviour
{
    public HillPathType pathType;
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

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform == this.character.transform)
        {
            switch (this.pathType)
            {
                case HillPathType.Up:
                    this.character.movePathType = MovePathType.UpHillPath;
                    this.characterCamera.followType = CameraFollowType.Both;
                    break;

                case HillPathType.Down:
                    this.character.movePathType = MovePathType.DownHillPath;
                    this.characterCamera.followType = CameraFollowType.Both;
                    break;
            }

        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == this.character.transform)
        {
            this.character.movePathType = MovePathType.FlatPath;
        }
    }
}
