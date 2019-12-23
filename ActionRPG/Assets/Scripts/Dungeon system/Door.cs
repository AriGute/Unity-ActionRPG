using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject objRepresents;
    private GameObject _targetDoor;
    public Transform target;
    public GameObject customTarget;
    public bool exitWorldMap = false;
    private void Start()
    {
        if (exitWorldMap == false) {
            if (customTarget != null) {
                targetDoor = customTarget;
            }
        }
    }
    public void bindDoors(Door other)
    {
        other.targetDoor = this.gameObject;
        this.targetDoor = other.gameObject;
    }

    public GameObject targetDoor
    {
        get
        {
            return _targetDoor;
        }

        set
        {
            _targetDoor = value;
        }
    }

    public IEnumerator useDoor(GameObject obj, EventMaster EventMaster)
    {
        if (exitWorldMap == false)
        {
            if (target != null)
            {
                obj.GetComponent<Player>().idle = true;
                _targetDoor.transform.parent.gameObject.SetActive(true);
                obj.transform.position = _targetDoor.GetComponent<Door>().target.position;
                this.transform.parent.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                obj.GetComponent<Player>().idle = false;
            }
            else
            {
                print("[Door.usedoor: Target is undefined]");
            }
        }
        else
        {
            GameObject.FindGameObjectWithTag("EventMaster").GetComponent<EventMaster>().worldMap(true);
            Destroy(this.transform.parent.parent.gameObject);
        }
    }
}
