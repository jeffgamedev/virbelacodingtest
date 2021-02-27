using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The MoveListener keeps track of registered InteractiveObjects.
/// Determines the closest InteractiveObject and sends the state it should be in.
/// </summary>
public class MoveListener : MonoBehaviour
{
    private static MoveListener _instance;

    private List<InteractiveObject> _objects;
    private InteractiveObject _closestObject;

    /// <summary>
    /// Receiver method when a movement is detected by any MoveDetector components.
    /// </summary>
    public static void ReportMoveDetected()
    {
        if (_instance != null)
        {
            _instance.MoveDetected();
        }
    }

    /// <summary>
    /// Registers an InteractiveObject to be tracked for movement.
    /// </summary>
    /// <param name="obj">InteractiveObject to register.</param>
    /// <returns></returns>
    public static bool RegisterObject(InteractiveObject obj)
    {
        if (_instance != null)
        {
            if (!_instance._objects.Contains(obj))
            {
                _instance._objects.Add(obj);
                _instance.FindClosestObject();
            }
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// Unregisters
    /// </summary>
    /// <param name="obj">InteractiveObject to deregister.</param>
    public static void UnregisterObject(InteractiveObject obj)
    {
        if (_instance != null && _instance._objects.Contains(obj))
        {
            _instance._objects.Remove(obj);
        }
    }


    /// <summary>
    /// Called when the component is enabled. Registers this as the instance.
    /// If an existing instance is detected, the component is disabled.
    /// </summary>
    private void OnEnable()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError("More than one MoveListener detected in the scene. There can only be one. This component will be disabled.");
            this.enabled = false;
            return;
        }
        _instance = this;
        _objects = new List<InteractiveObject>();
    }

    /// <summary>
    /// Called when the component is disabled. Unsets the instance if it is the instance.
    /// </summary>
    private void OnDisable()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void MoveDetected()
    {
        FindClosestObject();
    }

    private void FindClosestObject()
    {
        InteractiveObject closestObject = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(InteractiveObject obj in _objects)
        {
            Vector3 direction = obj.transform.position - currentPosition;
            float dSqrToTarget = direction.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestObject = obj;
            }
        }

        // if we have a change in the closest object
        if (_closestObject != closestObject)
        {
            // if there is a previously closest object that is not null
            if (_closestObject != null) 
            {
                // send an update to the old closest object for the new state
                _closestObject.SendState(false);
            }

            // set the new closest object
            _closestObject = closestObject;

            // send an update to the object for the new state if it was not null
            if (_closestObject != null)
            {
                _closestObject.SendState(true);
            }
        }
    }
}
