using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPointer : MonoBehaviour
{
    public EventMaster eventMaster;

    private Vector2 mousePos;
    private Vector2 target;
    private float speed = 20;
    private float distance = 0;

    private GameObject targetObj;
    private GameObject enterButton;

    private float stepSize = 5;
    private float StepTime;

    Collider2D collider;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collider.IsTouching(other))
        {
            enterButton.transform.position = other.transform.position;
            enterButton.GetComponent<EnterButton>().mapHolder = other.GetComponent<TerrainScript>().mapHolder;
            enterButton.SetActive(true);
           //print("tuch");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enterButton.SetActive(false);
        //print("unTuch");

    }
    private void Start()
    {
        StepTime = stepSize;

        targetObj = addToTheMap(Resources.Load<GameObject>("WorldMap/target"));

        enterButton = addToTheMap(Resources.Load<GameObject>("WorldMap/Buttons/enterButton"));
        enterButton.GetComponent<EnterButton>().eventMaster = this.eventMaster;

        collider = GetComponent<CapsuleCollider2D>();
        target = transform.position;

    }

    void Update()
    {




        if (Input.GetButtonDown("Fire1"))
        {
            mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            target = mousePos;
            targetObj.transform.position = target;
            targetObj.SetActive(true);

        }

        distance = Vector2.Distance(this.transform.position, target);

        if (distance > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (StepTime > 0) {
                StepTime -= Time.deltaTime;
            }
            else
            {
                StepTime = stepSize;
                if(Random.Range(0,7) >= 2)
                {
                    eventMaster.startEvent();
                }
            }
        }
        else
        {
            targetObj.SetActive(false);
        }
    }

    private GameObject addToTheMap(GameObject go)
    {
        GameObject newGo = Instantiate(go);
        newGo.transform.parent = this.transform.parent;
        newGo.transform.SetAsLastSibling();
        newGo.SetActive(false);

        return newGo;
    }

}
