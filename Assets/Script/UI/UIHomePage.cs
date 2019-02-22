using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum UIHomePageStatus
{
    Unstarted,
    Starting,
    Started
}
public class UIHomePage : MonoBehaviour,IPointerClickHandler
{
    private UIHomePageStatus status = UIHomePageStatus.Unstarted;
    public CharacterBehaviour character;
    public GameObject gameController;
    public Camera characterCamera;
    public GameObject deathLineLeft;
    private Animator animator;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status == UIHomePageStatus.Starting)
        {
            var pos = this.characterCamera.WorldToScreenPoint(this.character.transform.position);
            if (pos.x >= Screen.width / 4)
            {
                this.character.onBtnRightUp();
                this.gameController.SetActive(true);
                this.status = UIHomePageStatus.Started;
                this.deathLineLeft.SetActive(true);
                this.panel.SetActive(false);
                this.character.setCharacterIniPos(this.character.transform.position);
            }
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (this.status == UIHomePageStatus.Unstarted)
        {
            this.animator.SetTrigger(AniHashCode.triggerStartGame);
            this.character.onBtnRightDown();
            this.status = UIHomePageStatus.Starting;
        }
    }
}
