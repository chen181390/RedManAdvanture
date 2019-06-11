using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PathBlockType
{
    FlatPath,
    LeftHillPath,
    RightHillPath,
}

public enum PathBlockBehaviourType
{
    Motionless,
    DirectByRoute,
    Drop,
    TriggerThenDirectByRoute,
    CallMeteor
}

public class PathBlock : MonoBehaviour
{
    public PathBlockType pathType;
    public PathBlockBehaviourType behaviourType;
    public Vector2[] routePoints;
    public float moveSpeed;
    public float dropGravity;
    public GameObject meteorPrefab;
    public Vector2 meteorPos;
    public float meteorImpulse;
    public bool isMoveOnce;

    private CharacterBehaviour character;
    private CharacterCamera characterCamera;
    private Rigidbody2D rigidBody;
    private Vector2 iniPos;
    private Quaternion iniRot;
    private int routeTarget = 1;
    private PathBlockBehaviourType iniBehaviourType;
    private bool isMeteor;

    // Start is called before the first frame update
    void Start()
    {
        this.character = GameObject.Find("Character").GetComponent<CharacterBehaviour>();
        this.characterCamera = GameObject.Find("Main Camera").GetComponent<CharacterCamera>();
        this.iniBehaviourType = this.behaviourType;

        switch (this.behaviourType)
        {
            case PathBlockBehaviourType.DirectByRoute:
            case PathBlockBehaviourType.TriggerThenDirectByRoute:
                for (int i = 0; i < this.routePoints.Length; i++)
                {
                    this.routePoints[i] = this.transform.parent.TransformPoint(this.routePoints[i]);
                }
                this.iniPos = this.routePoints[0];
                this.transform.position = this.iniPos;
                this.iniRot = this.transform.rotation;
                break;

            case PathBlockBehaviourType.Drop:
                this.rigidBody = this.GetComponent<Rigidbody2D>();
                this.rigidBody.gravityScale = 0;
                this.iniPos = this.transform.position;
                this.iniRot = this.transform.rotation;
                break;
        }
        this.character.resetMissionEvent += this.resetPathBlock;
    }

    private void resetPathBlock()
    {
        this.behaviourType = this.iniBehaviourType;
        switch (this.behaviourType)
        {
            case PathBlockBehaviourType.DirectByRoute:
            case PathBlockBehaviourType.TriggerThenDirectByRoute:
                this.transform.position = this.routePoints[0];
                this.routeTarget = 1;
                break;

            case PathBlockBehaviourType.Drop:
                this.rigidBody.gravityScale = 0;
                this.rigidBody.velocity = Vector2.zero;
                this.rigidBody.angularVelocity = 0;
                this.transform.position = this.iniPos;
                this.transform.rotation = this.iniRot;
                this.gameObject.SetActive(true);
                break;

            case PathBlockBehaviourType.CallMeteor:
                this.isMeteor = false;
                break;
        }
    }



    // Update is called once per frame
    void Update()
    {
        switch (this.behaviourType)
        {
            case PathBlockBehaviourType.DirectByRoute:
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
                        if (!this.isMoveOnce)
                            this.routeTarget = 0;
                        else
                            this.behaviourType = PathBlockBehaviourType.Motionless;
                    }
                    else
                    {
                        this.routeTarget++;
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
            this.character.pathType = this.pathType;

            switch (this.behaviourType)
            {
                case PathBlockBehaviourType.Drop:
                    this.rigidBody.gravityScale = this.dropGravity;
                    break;

                case PathBlockBehaviourType.TriggerThenDirectByRoute:
                    this.behaviourType = PathBlockBehaviourType.DirectByRoute;
                    break;

                case PathBlockBehaviourType.CallMeteor:
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

            }
        }
        else if (collision.transform.name == "DeathLineBottom")
        {
            this.gameObject.SetActive(false);
        }
    }
}
