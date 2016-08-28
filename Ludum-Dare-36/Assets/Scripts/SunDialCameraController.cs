using System;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Tizen;

public class SunDialCameraController : MonoBehaviour
{
    private Camera MainCamera;
    private Camera ThisCamera;
    private Camera Minimap;
    private GameObject Canvas;
    private EventManager manager;
    private bool YouWinGame=false;
    private Vector3 OldPosition;
    private Vector3 NewPosition;
    private GameObject YouWinTxt;
    
    [SerializeField]
    GameObject Player;

    [SerializeField]
    float TimeSwitchToCameraInSeconds = .5f;

    [SerializeField]
    float CameraLookTimeInSeconds = 5.0f;

    // Use this for initialization
    void Start ()
    {
        YouWinTxt = GameObject.Find("YouWinText");
        YouWinTxt.transform.position = new Vector3(Screen.width/2, Screen.height/2,0);
        YouWinTxt.gameObject.SetActive(false);
        Canvas = GameObject.Find("Canvas");
        Minimap = GameObject.Find("MiniMap").GetComponent<Camera>();
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
	void FixedUpdate () {
	    if (YouWinGame && gameObject.transform.position.y <100)
	    {
	        OldPosition = gameObject.transform.position;
	        NewPosition = new Vector3(OldPosition.x,OldPosition.y += .05f,OldPosition.z);
	        gameObject.transform.position = NewPosition;
	    }
	    else if (YouWinGame)
	    {
            Canvas.SetActive(true);
            YouWinTxt.gameObject.SetActive(true);
        }
	   
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
        Canvas.SetActive(!Canvas.active);
        MainCamera.enabled = !MainCamera.enabled;
        ThisCamera.enabled = !ThisCamera.enabled;
    }

    void YouWin()
    {
        Invoke("CameraSwitch", TimeSwitchToCameraInSeconds);   //Half a second it goes to camera
        Invoke("CameraRiseTrue",6);
    }

    void CameraRiseTrue()
    {
        YouWinGame = true;
        Invoke("ResetGame",5);
    }

    void ResetGame()
    {
        SceneManager.LoadScene(0);  //Loads this scene 
    }
}
