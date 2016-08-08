using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// GameData.Data<int>
// GameData.Data<string>

public static class GameData
{
  public class Data<T>
  {
    public delegate void OnChange(T v);

    T _val;

    OnChange _onChange = null;

    public T Value
    {
      get { return _val; }
      set
      {
        _val = value;
        if (_onChange != null)
          _onChange.Invoke(_val);
      }
    }
    public T Default { get; set; }

    public void Reset ()
    {
      Value = Default;
    }

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


  static Dictionary<string, Data<int>> _intData;
  static Dictionary<string, Data<float>> _floatData;

  public static void Initialize ()
  {
    _intData = new Dictionary<string, Data<int>>();
    _floatData = new Dictionary<string, Data<float>>();
  }

  // INT VALUES
  public static void AddIntData (string key, int initialValue, int defaultValue = 0)
  {
    if (!_intData.ContainsKey(key))
    {
      Data<int> data = new Data<int>();
      data.Value = initialValue;
      data.Default = defaultValue;
      _intData.Add(key, data);
    }
    else
      Debug.LogError(string.Format("[GameData] Int data '{0}' is already registered.", key));
  }

  public static Data<int> GetIntData (string key)
  {
    Data<int> data;
    if (_intData.TryGetValue(key, out data))
      return data;

    Debug.LogError(string.Format("[GameData] Int data '{0}' is not registered.", key));
    return null;
  }

  // FLOAT VALUES
  public static void AddFloatData (string key, float initialValue, float defaultValue = 0f)
  {
    if (!_floatData.ContainsKey(key))
    {
      Data<float> data = new Data<float>();
      data.Value = initialValue;
      data.Default = defaultValue;
      _floatData.Add(key, data);
    }
    else
      Debug.LogError(string.Format("[GameData] Float data '{0}' is already registered.", key));
  }

  public static Data<float> GetFloatData (string key)
  {
    Data<float> data;
    if (_floatData.TryGetValue(key, out data))
      return data;

    Debug.LogError(string.Format("[GameData] Float data '{0}' is not registered.", key));
    return null;
  }
}
