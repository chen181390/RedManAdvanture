using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ShootMaceStatus
{
    sleeped,
    Awaking,
    Awaked
}

public class ShootMace : MonoBehaviour
{
    public GameObject prefabSpike;
    public Vector2 awakeMoveTarget;
    public float awakeMoveSpeed = 1;
    public float shootInterval = 1;
    private float shootLeftTime = 0;
    public bool isTrace;
    public float traceLeftLimitX;
    public float traceRightLimitX;
    public float traceSpeed;

    private ShootMaceStatus status = ShootMaceStatus.sleeped;
    private Animator animator;
    private bool isInRange = false;
    private CharacterBehaviour character;
    private Vector2 iniPos;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.transform.Find("Mace").GetComponent<Animator>();
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.iniPos = this.transform.position;
        this.awakeMoveTarget = this.transform.parent.TransformPoint(this.awakeMoveTarget);

        if (this.isTrace)
        {
            this.traceLeftLimitX = this.transform.parent.TransformPoint(new Vector2(this.traceLeftLimitX, 0)).x;
            this.traceRightLimitX = this.transform.parent.TransformPoint(new Vector2(this.traceRightLimitX, 0)).x;
        }

        this.character.resetMissionEvent += this.resetShootMace;
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.status)
        {
            case ShootMaceStatus.Awaking:
                var direct = (this.awakeMoveTarget - (Vector2)this.transform.position).normalized;
                var delta = direct * this.awakeMoveSpeed * Time.deltaTime;
                var nextPos = (Vector2)this.transform.position + delta;
                var nextDirect = (this.awakeMoveTarget - nextPos).normalized;
                // 到达目标点
                if (nextDirect != direct)
                {
                    this.transform.Translate(this.awakeMoveTarget- (Vector2)this.transform.position, Space.World);
                    this.status = ShootMaceStatus.Awaked;
                }
                else
                {
                    this.transform.Translate(delta, Space.World);
                }
                break;

            case ShootMaceStatus.Awaked:
                if (isInRange && this.shootLeftTime <= 0)
                {
                    this.shootLeftTime = this.shootInterval;
                    GameObject spike = (GameObject)Instantiate(this.prefabSpike, this.transform.position, this.transform.rotation, this.transform);
                }
                else
                {
                    this.shootLeftTime -= Time.deltaTime;
                }

                if (this.isTrace)
                {
                    int traceDirect = 0;
                    if (this.character.transform.position.x - this.transform.position.x > 1)
                    {
                        traceDirect = 1;
                    }
                    else if (this.character.transform.position.x - this.transform.position.x < -1)
                    {
                        traceDirect = -1;
                    }


                    var dist = this.traceSpeed * Time.deltaTime * traceDirect;
                    var nextPointX = this.transform.position.x + dist;
                    if (nextPointX < this.traceLeftLimitX)
                    {
                        nextPointX = this.traceLeftLimitX;
                    }
                    else if (nextPointX > this.traceRightLimitX)
                    {
                        nextPointX = this.traceRightLimitX;
                    }
                    
                    this.transform.Translate(new Vector2(nextPointX - this.transform.position.x, 0), Space.World);

                }
                break;
        }
    }

    private void resetShootMace()
    {
        this.transform.position = this.iniPos;
        if (this.animator.GetCurrentAnimatorStateInfo(0).shortNameHash != AniHashCode.Idle)
        {
            this.animator.SetTrigger(AniHashCode.triggerReset);
        }
        this.status = ShootMaceStatus.sleeped;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "characterCollider")
        {
            switch (status)
            {
                case ShootMaceStatus.sleeped:
                    this.animator.SetTrigger(AniHashCode.triggerAwake);
                    this.status = ShootMaceStatus.Awaking;
                    break;
            }
            this.isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "characterCollider")
        {
            this.isInRange = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == this.character.transform)
        {
            this.character.setCharacterDead();
        }
    }
}
