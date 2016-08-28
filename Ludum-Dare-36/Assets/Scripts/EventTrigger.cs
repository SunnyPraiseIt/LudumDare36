using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour
{
    [SerializeField]
    EventManager EM;
	// Use this for initialization
	void Start () {
	    if (EM == null)
	    {
	        EM = GameObject.Find("EventManager").GetComponent<EventManager>();
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartTrigger(int Index)
    {
        Debug.Log("TriggerCamera");
        EM.Activate(Index);
    }
}
