using UnityEngine;
using System.Collections;

public class TestingEvents : MonoBehaviour
{
    private EventTrigger ET;
	// Use this for initialization
    private bool passed = false;
	void Start ()
	{
	    ET = GetComponent<EventTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider other)
    {
        passed = !passed;
        if (passed)
        {
        Debug.Log("Collision");
        ET.StartTrigger((int)EventManager.EVENTS.Puzzle4Complete);     
        }
    }
}
