using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveGroup
{
  public int _groupID = 1;

  public List<Wave> _waves;

  public WaveGroup ()
  {
    _waves = new List<Wave>();
  }

  public void AddWave (Wave wave)
  {
    if (wave._groupID == _groupID)
      _waves.Add(wave);
  }
}

public class WaveManager : MonoBehaviour
{
  public float _waveTriggerDelay = 15f;
  public bool _waitForPreviousWave = false;

  public List<WaveGroup> _waveGroups;
  private int _currentGroupIndex = 0;
  private float _lastWaveStartTime = -10000f;

  void Awake ()
  {
    _waveGroups = new List<WaveGroup>();

    Wave[] waves = FindObjectsOfType(typeof(Wave)) as Wave[];

    foreach (Wave wave in waves)
    {
      AddWaveToGroup (wave);
      wave.enabled = false;
    }

    _waveGroups.Sort((x, y) => x._groupID.CompareTo(y._groupID));
  }

  void Update ()
  {
    if (_currentGroupIndex >= _waveGroups.Count)
      return;

    if (Time.time >= _lastWaveStartTime + _waveTriggerDelay)
    {
      WaveGroup wg = _waveGroups[_currentGroupIndex];
      foreach (Wave w in wg._waves)
      {
        w.enabled = true;
      }
      _currentGroupIndex++;
      _lastWaveStartTime = Time.time;
    }
  }

  private void AddWaveToGroup (Wave wave)
  {
    WaveGroup waveGroup = null;

    foreach (WaveGroup wg in _waveGroups)
    {
      if (wg._groupID == wave._groupID)
      {
        waveGroup = wg;
        break;
      }
    }

    if (waveGroup == null)
    {
      waveGroup = new WaveGroup();
      waveGroup._groupID = wave._groupID;
      _waveGroups.Add(waveGroup);
    }
    waveGroup.AddWave(wave);
  }
}
