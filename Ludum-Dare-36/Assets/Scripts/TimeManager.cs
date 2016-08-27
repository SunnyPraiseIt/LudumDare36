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
    float RotSpeed;

    public int sunPos;

    public float nextRot;

	void Start ()
    {
        if (Sun == null)
            Sun = GameObject.Find("Directional Light");

        if (RotSpeed == 0)
            RotSpeed = 0.0025f;

        nextRot = Sun.transform.rotation.x + 15;
        sunPos = 1;
	}

    void FixedUpdate()
    {
        Sun.transform.Rotate(new Vector3(RotSpeed, RotSpeed, 0));

        if(Sun.transform.rotation.x >= nextRot)
        {
            sunPos++;
            nextRot = Sun.transform.rotation.x + 15;
        }
    }
}
