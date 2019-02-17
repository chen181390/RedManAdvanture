using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    public GameObject character;
    private Camera characterCamera;
    private float cameraCharacterDistance;
    public float startPosLimit;
    public float endPosLimit;

    // Start is called before the first frame update
    void Start()
    {
        this.characterCamera = this.GetComponent<Camera>();
        float defaultSize = this.characterCamera.orthographicSize;
        this.characterCamera.orthographicSize = (float)Screen.height / 100 / 2;

        this.startPosLimit += this.characterCamera.orthographicSize - defaultSize;
        this.endPosLimit -= this.characterCamera.orthographicSize - defaultSize;

        this.cameraCharacterDistance = (0.5f - 0.382f) * this.characterCamera.orthographicSize * 2;
        this.transform.Translate(new Vector2(this.startPosLimit - this.transform.position.x, 0));
    }

    // Update is called once per frame
    void Update()
    {
        float deltaMove = this.character.transform.position.x - this.transform.position.x + this.cameraCharacterDistance;
        if (this.transform.position.x + deltaMove <= this.startPosLimit)
        {
            this.transform.Translate(new Vector2(this.startPosLimit - this.transform.position.x, 0));
        }
        else if (this.transform.position.x + deltaMove > this.endPosLimit)
        {
            this.transform.Translate(new Vector2(this.endPosLimit - this.transform.position.x, 0));
        }
        else
        {
            this.transform.Translate(new Vector2(deltaMove, 0));
        }
    }
}