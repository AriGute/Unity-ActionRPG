using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlaw : MonoBehaviour
{
    GameObject target;
    private float distanceX;
    private float distanceY;
    private float moveSpeed = 2;

    public void setTarget(GameObject target)
    {
        this.target = target;
        this.gameObject.tag = "MainCamera";
    }
    void Update()
    {
        if (target != null)
        {
            Vector3 targetPos = target.transform.position;
            Vector3 cameraPos = transform.position;

            distanceX = Mathf.Abs(targetPos.x - cameraPos.x);
            distanceY = Mathf.Abs(targetPos.y - cameraPos.y);



            if (distanceX > 1)
            {
                if (targetPos.x > cameraPos.x)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * (distanceX-1));
                }
                else if (targetPos.x < cameraPos.x)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * moveSpeed * (distanceX-1));

                }
            }
            if (distanceY > 1)
            {
                if (targetPos.y+1 > cameraPos.y)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * moveSpeed * distanceY);

                }
                else if (targetPos.y < cameraPos.y)
                {
                    transform.Translate(Vector3.down * Time.deltaTime * moveSpeed * distanceY);
                }
            }
        }
    }

    private float calcDistance()
    {
        float dis = 0;
        Vector3 targetPos = target.transform.position;
        Vector3 cameraPos = transform.position;
        dis = Mathf.Sqrt(Mathf.Pow((targetPos.x - cameraPos.x), 2) + Mathf.Pow((targetPos.y - cameraPos.y), 2));
        return dis;
    }
}
