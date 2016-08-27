﻿using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////
    //This script is for managing:
    //
    //Sun movement - Sun moves over time
    //
    //Sun effects - Opening new levels
    //              Light path to current level
    //              Particles on dial for finished level sections
    //
    //Loss / win condition - Tell the main camera to play the loss / win cut scene
    /////////////////////////////////////////////////////////////////////

    [SerializeField]
    GameObject Sun;

    [SerializeField]
    float RotSpeed;

    //Event Manager Var

    public int sunPos;
    public float nextRot;

    int increment;

	void Start ()
    {
        if (Sun == null)
            Sun = GameObject.Find("Directional Light");

        if (RotSpeed == 0)
            RotSpeed = 0.0025f;

        nextRot = Sun.transform.rotation.x + 15;
        sunPos = 1;
        increment = 0;
	}

    void FixedUpdate()
    {
        increment++;

        Sun.transform.Rotate(new Vector3(RotSpeed, RotSpeed, 0));

        if(increment * RotSpeed >= nextRot)
        {
            sunPos++;
            nextRot += 15;
        }

        //switch(sunPos)
        //{
        //    case 1:
        //        //open first door
        //        break;
        //    case 3:
        //        //open second door
        //        break;
        //    case 6:
        //        //open thrid door
        //        break;
        //    case 9:
        //        //open fourth door
        //        break;
        //    case 12:
        //        //if not all things finished lose condition
        //        //else win condition
        //        break;
        //}
    }
}