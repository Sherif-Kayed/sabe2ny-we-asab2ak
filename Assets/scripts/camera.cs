﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    public GameObject car;
    public float carx;
    public float cary;
    public float carz;

	
	// Update is called once per frame
	void Update () {
        carx = car.transform.eulerAngles.x;
        cary = car.transform.eulerAngles.y;
        carz = car.transform.eulerAngles.z;
        transform.eulerAngles = new Vector3(carx - carx, cary, carz - carz);
    }
}
