using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Sprite[] waterAtlas;
    private int currAtlasIndex = 0;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        this.spriteRenderer.sprite = this.waterAtlas[this.currAtlasIndex];
        this.currAtlasIndex ++;
        if (this.currAtlasIndex > this.waterAtlas.Length - 1)
            this.currAtlasIndex = 0;
    }
}
