using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreEditor : MonoBehaviour
{
    public CarController player;
    public TMP_Text counter;
    private int total;

    private void Start() {
        counter.text = "Remaining: " + total.ToString();
    }
    void Update()
    {
        total = player.GetCheeseTotal();
        counter.text = "Remaining: " + total.ToString();
    }
}
