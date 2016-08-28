using UnityEngine;
using System.Collections;

public class SunDialCameraController : MonoBehaviour
{
    private Camera MainCamera;
    private Camera ThisCamera;
    private EventManager manager;
    
    [SerializeField]
    GameObject Player;

    [SerializeField]
    float TimeSwitchToCameraInSeconds = .5f;

    [SerializeField]
    float CameraLookTimeInSeconds = 5.0f;

    // Use this for initialization
    void Start ()
	{
        
	    ThisCamera = GetComponent<Camera>();
	    ThisCamera.enabled = false;
	    MainCamera = Player.GetComponent<Camera>();
	   // MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	    if (manager == null)
	    {
	        manager = GameObject.Find("EventManager").GetComponent<EventManager>();
	    }
        manager.AddListener((int)EventManager.EVENTS.Puzzle1Complete, PanToSunDial);
        manager.AddListener((int)EventManager.EVENTS.Puzzle2Complete, PanToSunDial);
        manager.AddListener((int)EventManager.EVENTS.Puzzle3Complete, PanToSunDial);
        manager.AddListener((int)EventManager.EVENTS.Puzzle4Complete, PanToSunDial);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void PanToSunDial()     //this turns on the sundial camera
    {
        Debug.Log("Panning Camera");
      Invoke("CameraSwitch",TimeSwitchToCameraInSeconds);   //Half a second it goes to camera
      Invoke("CameraSwitch",CameraLookTimeInSeconds);  //Pan Back 5 seconds later;
    }

    void CameraSwitch()
    {
        Debug.Log("CameraSwitch");
        MainCamera.enabled = !MainCamera.enabled;
        ThisCamera.enabled = !ThisCamera.enabled;
    }
}
