using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class TriggerEventManager : MonoBehaviour {

    public enum ConditionType
    {
        CollisionEnter,
        CollisionStay,
        CollisionExit,
        TriggerEnter,
        TriggerStay,
        TriggerExit
    }
    public struct TriggerArgs
    {
        public ConditionType TriggerOn;
        public UnityEvent TriggerEvents;
    }
    [SerializeField]
    bool DisplayGizmo = true;
    [SerializeField]
    Color GizmoColor = Color.yellow;
    [SerializeField]
    bool WireFrameGizmo = false;
    Mesh GizmoMesh;
    [SerializeField]
    List<TriggerArgs> Triggers = new List<TriggerArgs>();
    [SerializeField]
    UnityEvent OnTrigger = null;

    void OnValidate()
    {
        if (DisplayGizmo)
            GizmoMesh = GetComponent<MeshFilter>().sharedMesh;
    }

    void OnDrawGizmos()
    {
        if (DisplayGizmo)
        {
            Gizmos.color = GizmoColor;
            if (WireFrameGizmo)
                Gizmos.DrawWireMesh(GizmoMesh, transform.position, transform.rotation, transform.localScale);
            else
                Gizmos.DrawMesh(GizmoMesh, transform.position, transform.rotation, transform.localScale);
        }
    }

    void OnTriggerEnter(Collider Other)
    {
        foreach (TriggerArgs T in Triggers)
            if (T.TriggerEvents != null && T.TriggerOn == ConditionType.TriggerEnter)
                OnTrigger.Invoke();
    }

    void OnTriggerStay(Collider Other)
    {
        foreach (TriggerArgs T in Triggers)
            if (T.TriggerEvents != null && T.TriggerOn == ConditionType.TriggerStay)
                OnTrigger.Invoke();
    }

    void OnTriggerExit(Collider Other)
    {
        foreach (TriggerArgs T in Triggers)
            if (T.TriggerEvents != null && T.TriggerOn == ConditionType.TriggerExit)
                OnTrigger.Invoke();
    }
}
