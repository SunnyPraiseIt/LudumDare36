using UnityEngine;
using System.Collections;

public class Crouch : MonoBehaviour
{
    bool crouched = false;

	void FixedUpdate ()
    {
	    if(Input.GetKeyDown(KeyCode.LeftControl) && !crouched)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
            crouched = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl) && crouched)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
            crouched = false;
        }
    }
}
