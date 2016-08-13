using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class GameData
{
  // supported types should go in here
  public enum DataType
  {
    Int,
    Float,
    String,
    Unknown
  }

  public static DataType GetDataTypeFromString (string typeString)
  {
    switch (typeString)
    {
      case "int":
        return DataType.Int;
      case "float":
        return DataType.Float;
      case "string":
        return DataType.String;
    }

    return DataType.Unknown;
  }

  public class Data<T>
  {
    public delegate void OnChange(T v);

    T _val;

    OnChange _onChange = null;
    OnChange _onChangeUI = null;

    public T Value
    {
      get { return _val; }
      set
      {
        if (ReadOnly) 
          return;

        _val = value;
        if (_onChange != null)
          _onChange.Invoke(_val);
        if (_onChangeUI != null)
          _onChangeUI.Invoke(_val);
      }
    }
    public T Default { get; set; }
    public bool ReadOnly { get; set; }

    public void Reset ()
    {
      if (ReadOnly)
        return;
        
      Value = Default;
    }

    public void RegisterOnChange (OnChange onChange)
    {
      if (_onChange == null)
        _onChange = onChange;
      else
        Debug.LogError(string.Format("[GameData.Data<{0}>] OnChange function already registered.", typeof(T)));
    }

    public void DeregisterOnChange ()
    {
      _onChange = null;
    }

    public void RegisterOnChangeUI (OnChange onChange)
    {
      if (_onChangeUI == null)
        _onChangeUI = onChange;
      else
        Debug.LogError(string.Format("[GameData.Data<{0}>] OnChange UI function already registered.", typeof(T)));
    }

    public void DeregisterOnChangeUI ()
    {
      _onChangeUI = null;
    }

    public override string ToString ()
    {
      return _val.ToString();
    }
  }


  static Dictionary<string, Data<int>> _intData;
  static Dictionary<string, Data<float>> _floatData;
  static Dictionary<string, Data<string>> _stringData;

  public static void Initialize ()
  {
    _intData = new Dictionary<string, Data<int>>();
    _floatData = new Dictionary<string, Data<float>>();
    _stringData = new Dictionary<string, Data<string>>();
  }

  // INT VALUES
  public static Data<int> AddIntData (string key, int initialValue, int defaultValue = 0)
  {
    if (!_intData.ContainsKey(key))
    {
      Data<int> data = new Data<int>();
      data.Value = initialValue;
      data.Default = defaultValue;
      _intData.Add(key, data);
      return data;
    }
    else
      Debug.LogError(string.Format("[GameData] Int data '{0}' is already registered.", key));

    return null;
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
  public static Data<float> AddFloatData (string key, float initialValue, float defaultValue = 0f)
  {
    if (!_floatData.ContainsKey(key))
    {
      Data<float> data = new Data<float>();
      data.Value = initialValue;
      data.Default = defaultValue;
      _floatData.Add(key, data);
      return data;
    }
    else
      Debug.LogError(string.Format("[GameData] Float data '{0}' is already registered.", key));
  
    return null;
  }

  public static Data<float> GetFloatData (string key)
  {
    Data<float> data;
    if (_floatData.TryGetValue(key, out data))
      return data;

    Debug.LogError(string.Format("[GameData] Float data '{0}' is not registered.", key));
    return null;
  }

  // STRING VALUES
  public static Data<string> AddStringData (string key, string initialValue, string defaultValue = "")
  {
    if (!_stringData.ContainsKey(key))
    {
      Data<string> data = new Data<string>();
      data.Value = initialValue;
      data.Default = defaultValue;
      _stringData.Add(key, data);
      return data;
    }
    else
      Debug.LogError(string.Format("[GameData] String data '{0}' is already registered.", key));
  
    return null;
  }

  public static Data<string> GetStringData (string key)
  {
    Data<string> data;
    if (_stringData.TryGetValue(key, out data))
      return data;

    Debug.LogError(string.Format("[GameData] String data '{0}' is not registered.", key));
    return null;
  }
}
