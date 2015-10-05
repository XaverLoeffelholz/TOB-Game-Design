﻿using UnityEngine;
using System.Collections;

public class trophyScript : MonoBehaviour
{
    private Transform trophy;

    // Use this for initialization
    void Start()
    {
        trophy = this.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
		// rotate the trophy to make it more spectacular
        trophy.Rotate(0, 25.0f * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Score").GetComponent<ScoreCalculation>().showScore();
    }
}