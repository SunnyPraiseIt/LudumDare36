using UnityEngine;
using System.Collections;

public class RuneLightUp : MonoBehaviour
{
    private bool RuneOn = false;
    private bool Complete = false;
    EventManager EM;
    private SpriteRenderer SR;

    [SerializeField]
    EventManager.EVENTS Event;
   
    [SerializeField]
    float R = 0;
    [SerializeField]
    float G = 0;
    [SerializeField]
    float B = 0;
    [SerializeField]
    float TimeToFadeInSeconds;

    private Color OldColor;
    private Color NewColor;
    private float t = 0;    //starting time

    // Use this for initialization
    void Start ()
    {
        SR = GetComponent<SpriteRenderer>();
        if (EM ==null)
        {
            EM = GameObject.Find("EventManager").GetComponent<EventManager>();
        }
        EM.AddListener((int)Event,RuneActivate);
        OldColor = SR.color;
        NewColor = new Color(R/255,G/255,B/255,1);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	    if (RuneOn == true && Complete == false)
	    {
	       Debug.Log("ChangingColor");
	            SR.color = Color.Lerp(OldColor, NewColor,t);
                if (t < 1)
                { 
                    t += Time.deltaTime / TimeToFadeInSeconds;
                }
                else
                {
                    Complete = true;
                }
	    }
	
	}

    void RuneActivate()
    {
        Debug.Log("RuneOn");
        RuneOn = true;
    }
    
}
