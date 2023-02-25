using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    public float rotateSpeed;
    public float bobSpeed;
    [Range(0,1)] public float bobDistance = 0.25f;
    private float currYPos;

    

    private void Start() {
        currYPos = transform.position.y;
    }
    private void FixedUpdate() {
        transform.Rotate(0, rotateSpeed, 0);
        transform.position = new Vector3(transform.position.x, currYPos + (bobDistance * Mathf.Sin(bobSpeed * Time.time)), transform.position.z);
    }
}
