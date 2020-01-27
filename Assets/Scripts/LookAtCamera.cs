using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    public float height;

    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + height, target.transform.position.z);
    }
}
