using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject target;
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public float speed;

    void Update()
    {
        Vector3 fixedPos
            = new Vector3(target.transform.position.x + offsetX,
                            offsetY,
                            target.transform.position.z + offsetZ);

        transform.position = Vector3.Lerp(transform.position, fixedPos, speed * Time.deltaTime);
    }
}
