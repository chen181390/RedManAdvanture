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
    public Image btnPreMission;
    public Sprite[] btnPreMissionImgs;
    public Image btnNextMission;
    public Sprite[] btnNextMissionImgs;

    private bool isFadeOutOver = false;
    private DeathLine rightDeathLine;

    // Start is called before the first frame update
    void Start()
    {
        if (!this.isHomePage)
        {
            this.touchBlock.color = Color.black;
            this.gameController.SetActive(false);
        }
        this.btnPause.gameObject.SetActive(false);
        this.btnNextMission.gameObject.SetActive(false);
        this.btnPreMission.gameObject.SetActive(false);
        this.rightDeathLine = GameObject.Find("DeathLineRight").GetComponent<DeathLine>();
        this.registerEvent();
    }

    private void registerEvent()
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

        var preMissionTrigger = this.btnPreMission.GetComponent<EventTrigger>();
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnPreMissionDown);
        preMissionTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnPreMissionUp);
        preMissionTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnPreMissionExit);
        preMissionTrigger.triggers.Add(entry);

        var nextMissionTrigger = this.btnNextMission.GetComponent<EventTrigger>();
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnNextMissionDown);
        nextMissionTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnNextMissionUp);
        nextMissionTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(this.onBtnNextMissionExit);
        nextMissionTrigger.triggers.Add(entry);
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
                this.btnNextMission.gameObject.SetActive(true);
                this.btnPreMission.gameObject.SetActive(true);
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
            Time.timeScale = 0;
            this.touchBlock.color = new Color(this.touchBlock.color.r, this.touchBlock.color.g, this.touchBlock.color.b, 0.5f);
            this.touchBlock.gameObject.SetActive(true);
            References.character.isCollectFrameData = false;
        }
        else if (this.btnPause.sprite == this.btnPauseImgs[3])
        {
            this.btnPause.sprite = this.btnPauseImgs[0];
            Time.timeScale = 1;
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

    public void onBtnPreMissionDown(BaseEventData eventData)
    {
        this.btnPreMission.sprite = this.btnPreMissionImgs[1];
    }

    public void onBtnPreMissionUp(BaseEventData eventData)
    {
        if (this.btnPreMission.sprite == this.btnPreMissionImgs[1]) {
            this.btnPreMission.sprite = this.btnPreMissionImgs[0];
            Time.timeScale = 1;
            this.rightDeathLine.loadPreScene();
        }
    }

    public void onBtnPreMissionExit(BaseEventData eventData)
    {
        this.btnPreMission.sprite = this.btnPreMissionImgs[0];
    }

    public void onBtnNextMissionDown(BaseEventData eventData)
    {
        this.btnNextMission.sprite = this.btnNextMissionImgs[1];
    }

    public void onBtnNextMissionUp(BaseEventData eventData)
    {
        if (this.btnNextMission.sprite == this.btnNextMissionImgs[1])
        {
            this.btnNextMission.sprite = this.btnNextMissionImgs[0];
            Time.timeScale = 1;
            this.rightDeathLine.loadNextScene();
        }
    }

    public void onBtnNextMissionExit(BaseEventData eventData)
    {
        this.btnNextMission.sprite = this.btnNextMissionImgs[0];
    }
}
