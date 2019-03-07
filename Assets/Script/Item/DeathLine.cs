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
    [Tooltip("进入下一关前黑色淡入的速度（每帧）")]
    public float fadeInSpeed;
    public int nextScene;

    private AsyncOperation asyncOperation;
    private bool isLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.deathLineType)
        {
            case DeathLineType.Right:
                if (!this.isLoading)
                    return;
                if (this.asyncOperation.progress == 0.9f && this.touchBlock.color.a == 1f)
                {
                    this.asyncOperation.allowSceneActivation = true;
                } else if (this.touchBlock.color.a < 1) {
                    if (this.touchBlock.color.a + this.fadeInSpeed > 1) {
                       this.touchBlock.color = Color.black;
                    } else {
                         var color = this.touchBlock.color;
                        this.touchBlock.color = new Color(color.r, color.g, color.b, (float)(color.a + this.fadeInSpeed));
                    }
                }
                break;
        }
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
                    if (this.isLoading) return;
                    this.asyncOperation = SceneManager.LoadSceneAsync(this.nextScene);
                    this.asyncOperation.allowSceneActivation = false;
                    this.isLoading = true;
                    this.touchBlock.gameObject.SetActive(true);
                    break;


            }
        }



    }
}
