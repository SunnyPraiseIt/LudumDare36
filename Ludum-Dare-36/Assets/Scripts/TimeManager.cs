using UnityEngine;
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
    //Loss condition - Tell the main camera to play the loss cut scene
    /////////////////////////////////////////////////////////////////////

    [SerializeField]
    GameObject Sun;


    [SerializeField]
    float Wait, RotSpeed;

    public int sunPos;

    float Rotation, internalClock;
    bool move;

  
	void Start ()
    {
        if (Sun == null)
            Sun = GameObject.Find("Directional Light");

        if (RotSpeed == 0)
            RotSpeed = 1;

        Rotation = 180 / 12;
        sunPos = 1;
        internalClock = 0;
	}
	
	void Update ()
    {
        if (internalClock < Wait)
            internalClock += Time.deltaTime;
        else if(internalClock >= Wait && !move)
        {
            move = true;
            Rotation += Sun.transform.rotation.y;
        }

        if (move)
            RotateSun();      
	}

    void RotateSun()
    {
        if (Sun.transform.rotation.y < Rotation)
            Sun.transform.Rotate(new Vector3(Time.deltaTime / RotSpeed, Time.deltaTime / RotSpeed, 0));
        else
            move = false;
    }

}
