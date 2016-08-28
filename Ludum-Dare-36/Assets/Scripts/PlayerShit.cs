using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerShit : MonoBehaviour
{

    float pickUpDist = 200f;
    [SerializeField]
    GameObject carriedObj = null;
    Vector3 objCenter;
    [SerializeField]
    Text message = null;

    bool holdingSomething = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)), out hit, 2) && !holdingSomething)
        {
            if (hit.transform.tag == "Active")
            {
                message.text = "Left Click to Activate";
            }
            else if (hit.transform.tag == "PickUp")
            {
                message.text = "Left Click to Pick Up";
            }

        }
        else if (holdingSomething)
        {
            message.text = "Right Click to Drop";

            //if not in the center lerp there
            if (carriedObj.transform.localPosition != new Vector3(0, 0, 2))
                carriedObj.transform.localPosition = Vector3.Lerp(carriedObj.transform.localPosition, new Vector3(0, 0, 2), .01f);

            carriedObj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
        else
        {
            message.text = null;
        }

        //Actions

        #region Actions
        if ((Input.GetMouseButtonDown(0) && !holdingSomething))
        {
            PickItemUp();
        }
        else if (Input.GetMouseButtonDown(1) && holdingSomething)
        {
            DropItem();
        }
        #endregion
    }

    void PickItemUp()
    {
        RaycastHit hit;
        Ray rey = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        bool why = Physics.Raycast(rey, out hit, 25);
        Debug.DrawLine(transform.position, hit.point, Color.red, 300);
        if (why)
        {
            PickUP p = hit.collider.GetComponent<PickUP>();
            objCenter = hit.point;
            if (p != null)
            {
                carriedObj = p.gameObject;
                carriedObj.transform.parent = Camera.main.transform;
                holdingSomething = true;
                p.gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }

    void DropItem()
    {
        carriedObj.gameObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObj.transform.parent = null;
        carriedObj = null;
        holdingSomething = false;
    }

}
