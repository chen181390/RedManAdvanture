using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SavePointStatus
{
    Unactive,
    Activating,
    Active
}
public class SavePoint : MonoBehaviour
{
    public Sprite[] savePiontAtlas;
    private CharacterBehaviour character;
    private SavePointStatus status;
    private int currAtlasIndex = 0;
    private SpriteRenderer spRender;

    // Start is called before the first frame update
    void Start()
    {
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.spRender = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.status)
        {
            case SavePointStatus.Activating:
                this.spRender.sprite = this.savePiontAtlas[++ this.currAtlasIndex];
                if (this.currAtlasIndex >= this.savePiontAtlas.Length - 1)
                {
                    this.status = SavePointStatus.Active;
                    this.save();
                }
                break;
                
        }
    }

    private void save()
    {
        this.character.setCharacterIniPos(this.character.transform.position);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.name == "characterCollider")
        {
            switch (this.status)
            {
                case SavePointStatus.Unactive:
                    this.status = SavePointStatus.Activating;
                    break;
            }
        }
    }
}
