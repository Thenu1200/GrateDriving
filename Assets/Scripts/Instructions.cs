using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject item;
    public float countDownTime = 10f;

    // Update is called once per frame
    void Update()
    {
        if (countDownTime > 0) {
            countDownTime -= Time.deltaTime;
        } else {
            item.SetActive(false);
        }
    }
}
