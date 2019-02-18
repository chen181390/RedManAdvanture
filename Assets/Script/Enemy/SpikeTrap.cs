using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TrapType
{
    Motionless,
    Hiden
}
public class SpikeTrap : MonoBehaviour
{
    private CharacterBehaviour character;
    public TrapType trapType;
    private bool isNear = false;

    // Start is called before the first frame update
    void Start()
    {
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        if (this.trapType == TrapType.Hiden)
        {
            for (int i = 0 ; i < this.transform.childCount ; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (this.trapType == TrapType.Hiden)
        {
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.trapType == TrapType.Hiden && !this.isNear)
        {
            this.isNear = true;
            this.gameObject.SetActive(true);
            for(int i = 0 ; i < this.transform.childCount ; i++){
                var obj = this.transform.GetChild(i).gameObject;
                obj.SetActive(true);
                var animator = obj.GetComponent<Animator>();
                animator.SetTrigger(AniHashCode.triggerPopUp);
            }
            return;
        }

        if (collider.transform.name == "characterCollider")
        {
            this.character.resetCharacter();
        }
    }
}
