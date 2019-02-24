using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Sprite[] waterAtlas;
    private int currIndex = 0;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        this.spriteRenderer.sprite = this.waterAtlas[this.currIndex];
        this.currIndex ++;
        if (this.currIndex > this.waterAtlas.Length - 1)
            this.currIndex = 0;
    }
}
