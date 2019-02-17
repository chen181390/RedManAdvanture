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
        this.characterCamera.orthographicSize = Camera.main.orthographicSize * 1920 / 1080 * Screen.height / Screen.width;
        this.cameraCharacterDistance = (0.5f - 0.382f) * this.characterCamera.orthographicSize*2;
        this.setCameraPos();
    }

    // Update is called once per frame
    void Update()
    {
        this.setCameraPos();
    }

    private void setCameraPos()
    {
        float deltaMove = this.character.transform.position.x - this.transform.position.x + this.cameraCharacterDistance;
        if (this.transform.position.x + deltaMove >= this.startPosLimit && this.transform.position.x + deltaMove <= this.endPosLimit)
        {
            this.transform.Translate(new Vector2(deltaMove, 0));
        }
    }
}