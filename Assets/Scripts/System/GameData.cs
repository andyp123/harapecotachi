using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class GameData
{
  public class IntValue
  {
    public delegate void OnChange(int v);

    int _val = 0;
    public int Value
    {
      get { return _val; }
      set
      {
        _val = value;
        if (_onChange != null)
          _onChange.Invoke(_val);
      }
    }

    OnChange _onChange = null; // be aware that these can be misused to create memory leaks!
  
    public void RegisterOnChange (OnChange onChange)
    {
      _onChange = onChange;
    }

    public void DeregisterOnChange ()
    {
      _onChange = null;
    }

    public override string ToString ()
    {
      return _val.ToString();
    }
  }

  static Dictionary<string, IntValue> _intValues;

  public static void Initialize ()
  {
    _intValues = new Dictionary<string, IntValue>();
  }

  public static void AddIntValue (string key, int initialValue)
  {
    if (!_intValues.ContainsKey(key))
    {
      IntValue intValue = new IntValue();
      intValue.Value = initialValue;
      _intValues.Add(key, intValue);
    }
    else
      Debug.LogError(string.Format("[GameData] IntValue '{0}' is already registered.", key));
  }

  public static IntValue GetIntValue (string key)
  {
    IntValue intValue;
    if (_intValues.TryGetValue(key, out intValue))
      return intValue;

    Debug.LogError(string.Format("[GameData] IntValue '{0}' is not registered.", key));
    return null;
  }
}
