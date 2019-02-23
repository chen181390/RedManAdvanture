using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CameraFollowType
{
    OnlyX,
    Both
}
public class CharacterCamera : MonoBehaviour
{
    public CameraFollowType followType = CameraFollowType.OnlyX;
    private CharacterBehaviour character;
    private Camera characterCamera;
    private float cameraCharacterDistanceX;
    private float cameraCharacterDistanceY;
    public float startPosLimit;
    public float endPosLimit;

    // Start is called before the first frame update
    void Start()
    {
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.characterCamera = this.GetComponent<Camera>();
        float defaultSize = this.characterCamera.orthographicSize;
        this.characterCamera.orthographicSize = (float)Screen.height / 100 / 2;

        this.startPosLimit += this.characterCamera.orthographicSize - defaultSize;
        this.endPosLimit -= this.characterCamera.orthographicSize - defaultSize;

        this.cameraCharacterDistanceX = (0.5f - 0.382f) * this.characterCamera.orthographicSize * 2;
        this.cameraCharacterDistanceY = 3.12f;
        this.resetCamera();

        this.character.resetMissionEvent += this.resetCamera;
    }

    public void resetCamera()
    {
        this.followType = CameraFollowType.OnlyX;
        this.transform.Translate(new Vector2(this.startPosLimit - this.transform.position.x, 
        this.cameraCharacterDistanceY + this.character.transform.position.y - this.transform.position.y));
    }


    // Update is called once per frame
    void Update()
    {
        float deltaMoveX = this.character.transform.position.x - this.transform.position.x + this.cameraCharacterDistanceX;
        float deltaMoveY = 0;
        if (this.followType == CameraFollowType.Both)
        {
            deltaMoveY = this.cameraCharacterDistanceY + this.character.transform.position.y - this.transform.position.y;
        }
        if (this.transform.position.x + deltaMoveX <= this.startPosLimit)
        {
            this.transform.Translate(new Vector2(this.startPosLimit - this.transform.position.x, deltaMoveY));
        }
        else if (this.transform.position.x + deltaMoveX > this.endPosLimit)
        {
            this.transform.Translate(new Vector2(this.endPosLimit - this.transform.position.x, deltaMoveY));
        }
        else
        {
            this.transform.Translate(new Vector2(deltaMoveX, deltaMoveY));
        }
    }
}