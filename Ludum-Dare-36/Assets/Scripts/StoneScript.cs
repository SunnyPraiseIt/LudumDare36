using UnityEngine;
using System.Collections;

public class StoneScript : MonoBehaviour
{


    [SerializeField]
    EventManager.EVENTS trigger;

    private EventTrigger ET;
    [SerializeField]
    GameObject Emiter;

    void Start()
    {
        ET = GetComponent<EventTrigger>();

    }
    public void ActivateMe()
    {
        //The event that needs to be called PATRICK
        ET.StartTrigger((int)trigger);
        Emiter.SetActive(true);
    }
}
