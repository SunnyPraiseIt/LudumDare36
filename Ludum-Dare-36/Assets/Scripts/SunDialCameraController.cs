using System;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Tizen;

public class SunDialCameraController : MonoBehaviour
{
    //camerashake stuff
    private Vector3 originPosition;
    private Quaternion originRotation;
    //private Quaternion MainCameraOriginRotation;
    public float shake_decay;
    public float shake_intensity;

    //Screenfade
    private Image Fade;
    //camera and minimap 
    private Camera MainCamera;
    private Camera ThisCamera;
    private Camera Minimap;
    private GameObject Canvas;
    private EventManager manager;
    private bool YouWinGame=false;
    private bool GameReset = false;
    private bool YouLoseGame = false;
    private Vector3 OldPosition;
    private Vector3 NewPosition;
    private GameObject YouWinTxt;
    
    //Unity fields
    [SerializeField]
    GameObject Player;

    [SerializeField]
    float TimeSwitchToCameraInSeconds = .5f;

    [SerializeField]
    float CameraLookTimeInSeconds = 5.0f;

    // Use this for initialization
    void Start ()
    {
        //UI and Cameras
        YouWinTxt = GameObject.Find("YouWinText");
        YouWinTxt.transform.position = new Vector3(Screen.width/2, Screen.height/2,0);
        YouWinTxt.gameObject.SetActive(false);
        Canvas = GameObject.Find("Canvas");
        Minimap = GameObject.Find("MiniMap").GetComponent<Camera>();
	    ThisCamera = GetComponent<Camera>();
	    ThisCamera.enabled = false;
	    MainCamera = Player.GetComponent<Camera>();
        Fade = GameObject.Find("FadeToBlack").GetComponent<Image>();
        
	   // MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	    if (manager == null)
	    {
	        manager = GameObject.Find("EventManager").GetComponent<EventManager>();
	    }
        //Listeners
        manager.AddListener((int)EventManager.EVENTS.Puzzle1Complete, PanToSunDial);
        manager.AddListener((int)EventManager.EVENTS.Puzzle2Complete, PanToSunDial);
        manager.AddListener((int)EventManager.EVENTS.Puzzle3Complete, PanToSunDial);
        manager.AddListener((int)EventManager.EVENTS.Puzzle4Complete, YouWin);
        manager.AddListener((int)EventManager.EVENTS.LoseGame,YouLose);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //you win
	    if (YouWinGame && gameObject.transform.position.y <951)
	    {
	        OldPosition = gameObject.transform.position;
	        NewPosition = new Vector3(OldPosition.x,OldPosition.y += 1f,OldPosition.z);
	        gameObject.transform.position = NewPosition;
	    }
	    else if (YouWinGame)
	    {
            Canvas.SetActive(true);
            YouWinTxt.gameObject.SetActive(true);
            if (GameReset != true)
            {
                GameReset = true;
                Invoke("ResetGame", 5);
            }
        }
        //you lose
	    if (YouLoseGame)
	    {
            if (shake_intensity > 0)
            {
                //camerashake
                if (GameReset)
                {
                    Color Temp = Fade.color;
                    Fade.color = new Color(Temp.r,Temp.g,Temp.b,Temp.a +=.008f);
                    transform.position = originPosition + UnityEngine.Random.insideUnitSphere * shake_intensity;
                    transform.rotation = new Quaternion(
                    originRotation.x + UnityEngine.Random.Range(-shake_intensity, shake_intensity) * .2f,
                    originRotation.y + UnityEngine.Random.Range(-shake_intensity, shake_intensity) * .2f,
                    originRotation.z + UnityEngine.Random.Range(-shake_intensity, shake_intensity) * .2f,
                    originRotation.w + UnityEngine.Random.Range(-shake_intensity, shake_intensity) * .2f);
                    shake_intensity -= shake_decay;
                }
                else
                {
                    MainCamera.gameObject.transform.position = MainCamera.gameObject.transform.position + UnityEngine.Random.insideUnitSphere * shake_intensity;
                    MainCamera.gameObject.transform.rotation = new Quaternion(
                    MainCamera.gameObject.transform.rotation.x + UnityEngine.Random.Range(-shake_intensity, shake_intensity) * .2f,
                    MainCamera.gameObject.transform.rotation.y + UnityEngine.Random.Range(-shake_intensity, shake_intensity) * .2f,
                    MainCamera.gameObject.transform.rotation.z + UnityEngine.Random.Range(-shake_intensity, shake_intensity) * .2f,
                    MainCamera.gameObject.transform.rotation.w + UnityEngine.Random.Range(-shake_intensity, shake_intensity) * .2f);
                    shake_intensity -= shake_decay;
                }
            }
            else
            {
               
                if (!GameReset)
                {
                    GameReset = true;
                    MoveToVolcano();
                    Invoke("YouLoseText",4);
                    Invoke("ResetGame", 10);
                }
                //you lose text
            }
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

    void MoveToVolcano()
    {
        MainCamera.enabled = !MainCamera.enabled;
        ThisCamera.enabled = !ThisCamera.enabled;
        ThisCamera.gameObject.transform.position = new Vector3(135.7f, 93f, -1.2f);
        ThisCamera.gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        originPosition = transform.position;
        originRotation = transform.rotation;
        shake_intensity = .3f;
        shake_decay = 0.002f;
    }

    void YouWin()
    {
        Invoke("CameraSwitch", TimeSwitchToCameraInSeconds);   //Half a second it goes to camera
        Invoke("CameraRiseTrue",6);
       
    }
    void YouLose()
    {
        //PlayVolcanoNoices
        originPosition = transform.position;
        originRotation = transform.rotation;
        shake_intensity = .5f;
        shake_decay = 0.005f;
        YouLoseGame = true;
    }

    void YouLoseText()
    {
        YouWinTxt.GetComponent<Text>().text = "You Lose";
        YouWinTxt.GetComponent<Text>().color = Color.red;
        YouWinTxt.SetActive(true);
    }
    void CameraRiseTrue()
    {
        YouWinGame = true;
    }

    void ResetGame()
    {
        SceneManager.LoadScene(0);  //Loads this scene 
    }
}
