using UnityEngine;
using System.Collections;

public class Crouch : MonoBehaviour
{
	void FixedUpdate ()
    {
	    if(Input.GetKeyDown(KeyCode.LeftControl))
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
        else if(Input.GetKeyUp(KeyCode.LeftControl))
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
    }
}
