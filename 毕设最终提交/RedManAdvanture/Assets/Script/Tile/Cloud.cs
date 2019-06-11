using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CloudType
{
    Motionless,
    CallMeteor,
    DirectByRoute,
    TriggerThenDirectByRoute
}
public class Cloud : MonoBehaviour
{
    public CloudType type;
    public GameObject meteorPrefab;
    public Vector2 meteorPos;
    public float meteorImpulse;
    public Vector2[] routePoints;
    public float moveSpeed;
    private bool isMeteor;
    private CharacterBehaviour character;
    private bool isForward;
    private int routeTarget;
    private CloudType iniType;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.animator = this.GetComponentInChildren<Animator>();
        this.iniType = this.type;
        switch (this.type)
        {
            case CloudType.DirectByRoute:
                if (this.transform.parent)
                {
                    for (int i = 0; i < this.routePoints.Length; i++)
                    {
                        this.routePoints[i] = this.transform.parent.TransformPoint(this.routePoints[i]);
                    }
                }
                this.transform.position = this.routePoints[0];
                break;

            case CloudType.TriggerThenDirectByRoute:
                if (this.transform.parent)
                {
                    for (int i = 0; i < this.routePoints.Length; i++)
                    {
                        this.routePoints[i] = this.transform.parent.TransformPoint(this.routePoints[i]);
                    }
                }
                this.transform.position = this.routePoints[0];
                this.animator.SetTrigger(AniHashCode.triggerFloat);
                break;

            default:
                this.animator.SetTrigger(AniHashCode.triggerFloat);
                break;
        }


        this.character.resetMissionEvent += resetCloud;
    }

    public void resetCloud()
    {
        this.type = this.iniType;
        switch (this.type)
        {
            case CloudType.CallMeteor:
                this.isMeteor = false;
                break;

            case CloudType.DirectByRoute:
            case CloudType.TriggerThenDirectByRoute:
                this.transform.position = this.routePoints[0];
                this.routeTarget = 1;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.type)
        {
            case CloudType.DirectByRoute:
                var direct = (this.routePoints[routeTarget] - (Vector2)this.transform.position).normalized;
                var delta = direct * this.moveSpeed * Time.deltaTime;
                var nextPos = (Vector2)this.transform.position + delta;
                var nextDirect = (this.routePoints[routeTarget] - nextPos).normalized;
                // 到达目标点
                if (nextDirect != direct || nextDirect == Vector2.zero)
                {
                    this.transform.Translate(this.routePoints[routeTarget] - (Vector2)this.transform.position, Space.World);
                    if (this.routeTarget == this.routePoints.Length - 1)
                    {
                        this.routeTarget--;
                        this.isForward = false;
                    }
                    else if (this.routeTarget == 0)
                    {
                        this.routeTarget++;
                        this.isForward = true;
                    }
                    else
                    {
                        if (this.isForward)
                            this.routeTarget++;
                        else
                            this.routeTarget--;
                    }

                }
                else
                {
                    this.transform.Translate(delta, Space.World);
                }
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Character")
        {
            switch (this.type)
            {
                case CloudType.CallMeteor:
                    if (!this.isMeteor)
                    {
                        var meteor = Instantiate(this.meteorPrefab, this.meteorPos, Quaternion.Euler(0, 0, 0));
                        Meteor meteorScript = meteor.GetComponent<Meteor>();
                        meteorScript.target = this.character.transform.position;
                        meteorScript.impulse = this.meteorImpulse;
                        meteorScript.isShootMeteor = true;
                        this.isMeteor = true;
                    }
                    break;

                case CloudType.TriggerThenDirectByRoute:
                    this.type = CloudType.DirectByRoute;
                    this.animator.SetTrigger(AniHashCode.triggerIdle);
                    break;
            }
        }
    }
}
