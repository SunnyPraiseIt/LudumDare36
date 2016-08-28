using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////
    //This script is for managing:
    //
    //Sun movement - Sun moves over time -
    //
    //Sun effect events - Opening new levels -
    //                    Set beacons -
    //                    Particles on dial for finished level sections -
    //
    //Loss / win condition - Tell the main camera to play the loss / win cut scene -
    /////////////////////////////////////////////////////////////////////

    [SerializeField]
    GameObject Sun;

    [SerializeField]
    float RotSpeed;

    [SerializeField]
    EventManager manager;

    public int sunPos;
    public float nextRot;

    int increment;
    bool lvl1, lvl2, lvl3, lvl4;

	void Start ()
    {
        if (Sun == null)
            Sun = GameObject.Find("Directional Light");

        if (RotSpeed == 0)
            RotSpeed = 0.0025f;

        if (manager == null)
            manager = GameObject.Find("EventManager").GetComponent<EventManager>();

        manager.AddListener((int)EventManager.EVENTS.Puzzle1Complete, Level1Complete);
        manager.AddListener((int)EventManager.EVENTS.Puzzle2Complete, Level2Complete);
        manager.AddListener((int)EventManager.EVENTS.Puzzle3Complete, Level3Complete);
        manager.AddListener((int)EventManager.EVENTS.Puzzle4Complete, Level4Complete);

        nextRot = Sun.transform.rotation.x + 15;
        sunPos = 1;
        increment = 0;
        lvl1 = lvl2 = lvl3 = lvl4 = false;
	}

    void FixedUpdate()
    {
        increment++;

        Sun.transform.Rotate(new Vector3(RotSpeed, 0, 0));

        if(increment * RotSpeed >= nextRot)
        {
            sunPos++;
            nextRot += 15;
        }

        switch(sunPos)
        {
            case 3:
                manager.Activate((int)EventManager.EVENTS.Puzzle2Start);
                break;
            case 6:
                manager.Activate((int)EventManager.EVENTS.Puzzle3Start);
                break;
            case 9:
                manager.Activate((int)EventManager.EVENTS.Puzzle4Start);
                break;
            case 12:
                if(lvl1 && lvl2 && lvl3 && lvl4)
                    manager.Activate((int)EventManager.EVENTS.WinGame);
                else
                    manager.Activate((int)EventManager.EVENTS.LoseGame);
                break;
        }
    }

    void Level1Complete()
    {
        lvl1 = true;
    }

    void Level2Complete()
    {
        lvl2 = true;
    }

    void Level3Complete()
    {
        lvl3 = true;
    }

    void Level4Complete()
    {
        lvl4 = true;

        switch(sunPos)
        {
            case 3:
                manager.Activate((int)EventManager.EVENTS.Puzzle2Start);
                break;
            case 6:
                manager.Activate((int)EventManager.EVENTS.Puzzle3Start);
                break;
            case 9:
                manager.Activate((int)EventManager.EVENTS.Puzzle4Start);
                break;
            case 12:
                if (lvl1 && lvl2 && lvl3 && lvl4)
                    manager.Activate((int)EventManager.EVENTS.WinGame);
                else
                    manager.Activate((int)EventManager.EVENTS.LoseGame);
                break;
        }
    }
}
