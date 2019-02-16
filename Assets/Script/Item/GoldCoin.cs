using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    public int value;
    private GameObject moveTarget;
    private float moveSpeed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.moveTarget)
        {
            Vector2 direct = Vector3.Normalize(this.moveTarget.transform.position - this.transform.position);
            this.transform.Translate(this.moveSpeed * Time.deltaTime * direct);
            if (Physics2D.OverlapCircle(this.transform.position, 0.1f))
            {
                Destroy(this.gameObject, 0.5f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.name == "characterCollider")
        {
            this.moveTarget = collider.gameObject;
        }
    }

    
}
