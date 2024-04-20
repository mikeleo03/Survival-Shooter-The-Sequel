using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent<Vector3>> eventDictionaryVector;
    private Dictionary<string, UnityEvent<bool>> eventDictionaryBool;
    private Dictionary<string, UnityEvent> eventDictionaryNull;


    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionaryVector == null)
        {
            eventDictionaryVector = new Dictionary<string, UnityEvent<Vector3>>();
        }
        if (eventDictionaryBool == null)
        {
            eventDictionaryBool = new Dictionary<string, UnityEvent<bool>>();
        }
        if (eventDictionaryNull == null)
        {
            eventDictionaryNull = new Dictionary<string, UnityEvent>();
        }
    }

#region EventVector
    [System.Serializable]
    public class VectorEvent : UnityEvent<Vector3>
    {

    }

    public static void StartListening(string eventName, UnityAction<Vector3> listener)
    {
        UnityEvent<Vector3> thisEvent = null;
        if (instance.eventDictionaryVector.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new VectorEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryVector.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<Vector3> listener)
    {
        if (eventManager == null) return;
        UnityEvent<Vector3> thisEvent = null;
        if (instance.eventDictionaryVector.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, Vector3 vector)
    {
        UnityEvent<Vector3> thisEvent = null;
        if (instance.eventDictionaryVector.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(vector);
        }
    }
#endregion

    #region EventBool
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool>
    {

    }

    public static void StartListening(string eventName, UnityAction<bool> listener)
    {
        UnityEvent<bool> thisEvent = null;
        if (instance.eventDictionaryBool.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new BoolEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryBool.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<bool> listener)
    {
        if (eventManager == null) return;
        UnityEvent<bool> thisEvent = null;
        if (instance.eventDictionaryBool.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, bool toggle)
    {
        UnityEvent<bool> thisEvent = null;
        if (instance.eventDictionaryBool.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle);
        }
    }
    #endregion

    #region EventNone
    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionaryNull.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryNull.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionaryNull.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionaryNull.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
    #endregion
}