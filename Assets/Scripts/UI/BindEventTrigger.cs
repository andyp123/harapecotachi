using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BindEventTrigger : MonoBehaviour
{
  public enum DelegateType
  {
    TogglePause,
    ToggleLanguage,
    RestartCurrentScene
  }

  public EventTriggerType _eventTriggerType = EventTriggerType.PointerClick;
  public DelegateType _delegateType = DelegateType.TogglePause;

  static Dictionary<DelegateType, UnityEngine.Events.UnityAction<BaseEventData>> _delegateFunctionMap;

  void Awake ()
  {
    InitializeDelegateFunctionMap();

    EventTrigger trigger = transform.GetOrAddComponent<EventTrigger>();
    EventTrigger.Entry entry = new EventTrigger.Entry( );
    entry.eventID = _eventTriggerType;
    entry.callback.AddListener( _delegateFunctionMap[_delegateType] );
    trigger.triggers.Add( entry );
  }

  static void InitializeDelegateFunctionMap()
  {
    if (_delegateFunctionMap == null)
    {
      _delegateFunctionMap = new Dictionary<DelegateType, UnityEngine.Events.UnityAction<BaseEventData>>();
    
      _delegateFunctionMap.Add(DelegateType.TogglePause, ( data ) => DelegateFunctions.TogglePause( (BaseEventData)data ));
      _delegateFunctionMap.Add(DelegateType.ToggleLanguage, ( data ) => DelegateFunctions.ToggleLanguage( (BaseEventData)data ));
      _delegateFunctionMap.Add(DelegateType.RestartCurrentScene, ( data ) => DelegateFunctions.RestartCurrentScene( (BaseEventData)data ));
    }
  }
}

public static class DelegateFunctions
{
  public static void ToggleLanguage (BaseEventData data)
  {
    string languageCode = (Localization.GetCurrentLanguage() == "en") ? "ja" : "en";
    Game.Instance.GUIManager.ApplyLanguage(languageCode);
  }

  public static void RestartCurrentScene (BaseEventData data)
  {
    // TODO:  should have a ResetValue function and store more data about each value (e.g. current, default, min, max)
    GameData.GetIntData("VAL_KILLED").Reset();
    GameData.GetIntData("VAL_SPAWNED").Reset();
    GameData.GetIntData("VAL_CHANCES").Reset();

    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public static void TogglePause (BaseEventData data)
  {
    Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;
  }
}