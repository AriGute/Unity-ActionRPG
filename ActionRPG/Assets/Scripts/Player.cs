using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IPointerClickHandler , IDragHandler
{
    private float speed;
    private float runSpeed;
    private float attackSpeed = 1f;
    private bool playerFaceRight = true;
    private bool Walking = false;
    private bool blocking = false;
    private bool running = false;
    public bool idle = false;

    private Bag bag;

    public EventMaster eventMaster;

    float sizeScale = 0.8f;

    public GameObject playerCamera;

    private float attackTime = 0.0f;

    /*
     *There is two 'animators' one for the upper body and one for lower boddy. 
     */
    private Animator animTop;
    private Animator animBottom;

    Collider2D collider;
    Vector2 mousePos;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collider != null && other != null)
        {
            if (other.tag == "Door")
            {
                if (collider.IsTouching(other))
                {
                    StartCoroutine(other.GetComponent<Door>().useDoor(this.gameObject, eventMaster));
                }
            }
        }
    }

    void Start()
    {
        this.transform.localScale = new Vector2(1 * sizeScale, 1 * sizeScale);
        playerCamera = Instantiate<GameObject>(playerCamera);
        playerCamera.GetComponent<CameraFlaw>().setTarget(this.gameObject);
        speed = 2;
        runSpeed = speed * 2;
        animBottom = transform.Find("skeletonRig").GetComponent<Animator>();
        animTop = transform.Find("skeletonRig").Find("pants").Find("stomach").GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();

        bag = new Bag();
        bag.initBag(true);
        //bag.rightHandMountObj = GameObject.Find("skeletonRig/pants/stomach/torso/right_top_arm/mid_arm/mount/_sprite");
        //bag.leftHandMountObj = GameObject.Find("skeletonRig/pants/stomach/torso/left_top_arm/mid_arm/mount/_sprite");
        mousePos = new Vector2(0,0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("click");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("drag");
    }

    private void uiMouseSelect()
    {
        //UI mouse click to select.
        PointerEventData pointerEvent = new PointerEventData(EventSystem.current);
        List<RaycastResult> resultList = new List<RaycastResult>();
        pointerEvent.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointerEvent, resultList);

        for (int i = 0; i < resultList.Count; i++)
        {
            if (resultList[i].gameObject.tag != "Icon")
            {
                resultList.RemoveAt(i);
                i--;
            }
        }

        if (resultList.Count != 0)
        {
            //the current icon the mouse is point on.
            print(resultList[0].gameObject.name);
        }

    }

    private void worldMouseSelect()
    {
        //World space mouse click to select.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        mousePos = new Vector2(ray.origin.x, ray.origin.y);

        RaycastHit2D rayHit = Physics2D.Raycast(mousePos, ray.direction, 100);
        if (rayHit.transform != null)
        {
            string tag = rayHit.transform.tag;
            switch (tag)
            {
                case "Loot":
                    rayHit.transform.GetComponent<Loot>().showLoot();
                    print("Loot.");
                    break;

                case null:
                    break;
            }

        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            worldMouseSelect();
        }

        if (Input.GetButtonDown("Inventory"))
        {
            print("show bag.");
            bag.openBag("My bag");
        }

        if (attackTime > 0)
        {
            attackTime -= Time.deltaTime;
        }
        else
        {
            animTop.SetBool("Attack1", false);
            animTop.SetBool("RunAttack", false);
            attackTime = 0;
        }

        if (idle == false)
        {

            if (Input.GetButton("Run"))
            {
                running = true;
            }


            if (Input.GetButton("Fire2") && attackTime == 0)
            {
                //Block while stand/walk/running.
                animTop.SetBool("Block", true);
                blocking = true;
            }
            else
            {
                animTop.SetBool("Block", false);
                blocking = false;
            }


            if (Input.GetButtonDown("Fire1") && (attackTime - 0.2f) <= 0)
            {
                //Attack while running.
                if (running == true)
                {
                    animTop.SetBool("Attack1", false);
                    animTop.SetBool("RunAttack", true);
                    if (1f * attackSpeed > 0.3f)
                    {
                        animTop.SetFloat("AttackSpeed", 1 / attackSpeed);
                        attackTime = 1f * attackSpeed;
                    }
                    else
                    {
                        animTop.SetFloat("AttackSpeed", 1 / 0.3f);
                        attackTime = 0.3f;
                    }
                }
                else if (running == false)
                {
                    //Attack while stand/walk.
                    animTop.SetBool("RunAttack", false);
                    animTop.SetBool("Attack1", true);
                    if (1f * attackSpeed > 0.3f)
                    {
                        animTop.SetFloat("AttackSpeed", 1 / attackSpeed);
                        attackTime = 1f * attackSpeed;
                    }
                    else
                    {
                        animTop.SetFloat("AttackSpeed", 1 / 0.3f);
                        attackTime = 0.3f;
                    }
                }
            }

            

            if (Input.GetButton("Left"))
            {
                this.transform.Translate(Vector2.left * Time.deltaTime * speed);
                playerFaceRight = true;
                this.transform.localScale = new Vector2(-1 * sizeScale, 1 * sizeScale);
                Walking = true;
            }
            if (Input.GetButton("Right"))
            {
                this.transform.Translate(Vector2.right * Time.deltaTime * speed);
                playerFaceRight = true;
                this.transform.localScale = new Vector2(1 * sizeScale, 1 * sizeScale);
                Walking = true;

            }

            if (Input.GetButton("Up"))
            {
                this.transform.Translate(Vector2.up * Time.deltaTime * (speed - (speed / 3)));
                Walking = true;


            }

            if (Input.GetButton("Down"))
            {
                this.transform.Translate(Vector2.down * Time.deltaTime * (speed - (speed / 3)));
                Walking = true;
            }

            animationState();
        }
        else
        {
            animTop.Play("idle_top");
            animBottom.Play("idle_bottom");
        }
    }


    private void animationState()
    {
        if (Walking == true && running == true)
        {
            //Run state.
            animBottom.SetBool("Run", true);
            animBottom.SetBool("Walk", true);
            speed = runSpeed;
            running = false;
            Walking = false;
        }
        else if (Walking == true && running == false)
        {
            //Walk state.
            speed = runSpeed / 2;
            animBottom.SetBool("Run", false);
            animBottom.SetBool("Walk", true);
            Walking = false;
        }
        else
        {
            //Idle state.
            animBottom.SetBool("Walk", false);
            animBottom.SetBool("Run", false);
            running = false;
            Walking = false;
        }

    }

  
}
