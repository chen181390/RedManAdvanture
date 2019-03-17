using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIMission : MonoBehaviour
{
    public bool isHomePage;
    public Image touchBlock;
    [Tooltip("进入关卡黑色淡出的速度（每帧")]
    public float fadeOutSpeed;
    public GameObject gameController;
    public Image btnPause;
    public Sprite[] btnPauseImgs;

    private bool isFadeOutOver = false;
    private float originTimeScale;

    // Start is called before the first frame update
    void Start()
    {
        if (!this.isHomePage)
        {
            this.touchBlock.color = Color.black;
            this.gameController.SetActive(false);
        }
        this.btnPause.gameObject.SetActive(false);
        this.registerBtnPauseGame();
    }

    private void registerBtnPauseGame()
    {
        var pauseTrigger = this.btnPause.GetComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnPauseGamDown);
        pauseTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnPauseGameUp);
        pauseTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnPauseGameExit);
        pauseTrigger.triggers.Add(entry);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isHomePage && !this.isFadeOutOver)
        {
            if (this.touchBlock.color.a - this.fadeOutSpeed <= 0)
            {
                this.touchBlock.color = new Color(this.touchBlock.color.r, this.touchBlock.color.g,
                this.touchBlock.color.b, 0);
            }
            else
            {
                this.touchBlock.color = new Color(this.touchBlock.color.r, this.touchBlock.color.g,
                this.touchBlock.color.b, this.touchBlock.color.a - this.fadeOutSpeed);
            }


            if (this.touchBlock.color.a == 0)
            {
                this.isFadeOutOver = true;
                this.gameController.SetActive(true);
                this.btnPause.gameObject.SetActive(true);
                this.touchBlock.gameObject.SetActive(false);
            }
        }
    }

    public void onBtnPauseGamDown(BaseEventData eventData)
    {
        if (this.btnPause.sprite == this.btnPauseImgs[0])
            this.btnPause.sprite = this.btnPauseImgs[1];
        else if (this.btnPause.sprite == this.btnPauseImgs[2])
            this.btnPause.sprite = this.btnPauseImgs[3];
    }

    public void onBtnPauseGameUp(BaseEventData eventData)
    {
        if (this.btnPause.sprite == this.btnPauseImgs[1])
        {
            this.btnPause.sprite = this.btnPauseImgs[2];
            this.originTimeScale = Time.timeScale;
            Time.timeScale = 0;
            this.touchBlock.color = new Color(this.touchBlock.color.r, this.touchBlock.color.g, this.touchBlock.color.b, 0.5f);
            this.touchBlock.gameObject.SetActive(true);
            References.character.isCollectFrameData = false;
        }
        else if (this.btnPause.sprite == this.btnPauseImgs[3])
        {
            this.btnPause.sprite = this.btnPauseImgs[0];
            Time.timeScale = this.originTimeScale;
            this.touchBlock.color = new Color(this.touchBlock.color.r, this.touchBlock.color.g, this.touchBlock.color.b, 0);
            this.touchBlock.gameObject.SetActive(false);
            References.character.isCollectFrameData = true;
        }
    }

    public void onBtnPauseGameExit(BaseEventData eventData)
    {
        if (this.btnPause.sprite == this.btnPauseImgs[1])
            this.btnPause.sprite = this.btnPauseImgs[0];
        else if (this.btnPause.sprite == this.btnPauseImgs[3])
            this.btnPause.sprite = this.btnPauseImgs[2];
    }
}
