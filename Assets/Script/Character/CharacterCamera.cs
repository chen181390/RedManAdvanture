using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterCamera : MonoBehaviour
{
    private CharacterBehaviour character;
    private Camera characterCamera;
    private float cameraCharacterDistanceX;
    private float cameraCharacterDistanceY;
    public float startPosLimitX;
    private float endPosLimitX;
    public float maxLimitY;
    public float minLimitY;
    public GameObject deathLineRight;
    public GameObject deathLineLeft;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        this.character = References.character;
        this.characterCamera = this.GetComponent<Camera>();
        float defaultSize = this.characterCamera.orthographicSize;
        this.characterCamera.orthographicSize = (float)Screen.height / 100 / 2;

        this.startPosLimitX = this.deathLineLeft.transform.position.x + (float)Screen.width / 100 / 2;
        this.endPosLimitX = this.deathLineRight.transform.position.x - (float)Screen.width / 100 / 2;
        this.maxLimitY -= this.characterCamera.orthographicSize - defaultSize;
        this.minLimitY += this.characterCamera.orthographicSize - defaultSize;


        this.cameraCharacterDistanceX = (0.5f - 0.382f) * this.characterCamera.orthographicSize * 2;
        this.cameraCharacterDistanceY = this.characterCamera.transform.position.y - this.character.transform.position.y;
        this.resetCamera();

        this.character.resetMissionEvent += this.resetCamera;
    }

    public void resetCamera()
    {
        this.transform.Translate(new Vector2(this.startPosLimitX - this.transform.position.x, 
        this.cameraCharacterDistanceY + this.character.transform.position.y - this.transform.position.y));
    }

    // Update is called once per frame
    void Update()
    {

        float nextX = this.character.transform.position.x + this.cameraCharacterDistanceX;
        float nextY = this.cameraCharacterDistanceY + this.character.transform.position.y;
        float deltaMoveX;
        float deltaMoveY;

        if (nextY <= this.minLimitY)
        {
            deltaMoveY = this.minLimitY - this.transform.position.y;
        }
        else if (nextY > this.maxLimitY)
        {
            deltaMoveY = this.maxLimitY - this.transform.position.y;
        }
        else
        {
            deltaMoveY = nextY - this.transform.position.y;
        }


        if (nextX <= this.startPosLimitX)
        {
            deltaMoveX = this.startPosLimitX - this.transform.position.x;
        }
        else if (nextX > this.endPosLimitX)
        {
            deltaMoveX = this.endPosLimitX - this.transform.position.x;
        }
        else
        {
            deltaMoveX = nextX - this.transform.position.x;
        }

        this.transform.Translate(new Vector2(deltaMoveX, deltaMoveY));
    }
}