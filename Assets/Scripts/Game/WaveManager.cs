using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveGroup
{
  public int _groupID = 1;
  public List<Wave> _waves;

  bool _startedSpawning = false;
  public bool StartedSpawning
  {
    get { return _startedSpawning; }
  }
  public bool FinishedSpawning
  {
    get { return HasFinishedSpawning(); }
  }


  public WaveGroup ()
  {
    _waves = new List<Wave>();
  }

  public void AddWave (Wave wave)
  {
    if (wave._groupID == _groupID)
      _waves.Add(wave);
  }

  public void StartSpawning ()
  {
    if (!_startedSpawning)
    {
      foreach (Wave w in _waves)
      {
        w.enabled = true;
      }
      _startedSpawning = true;
    }
  }

  bool HasFinishedSpawning ()
  {
    foreach (Wave w in _waves)
    {
      if (!w.FinishedSpawning)
        return false;
    }
    return true;
  }
}

public class WaveManager : MonoBehaviour
{
  public float _waveTriggerDelay = 15f;
  public bool _waitForPreviousWave = false;

  List<WaveGroup> _waveGroups;

  int _currentGroupIndex = 0;
  float _nextWaveStartTime = 0f;

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

    WaveGroup currentGroup = _waveGroups[_currentGroupIndex];

    if (!currentGroup.StartedSpawning && Time.time >= _nextWaveStartTime)
    {
      currentGroup.StartSpawning();
    }

    if (currentGroup.FinishedSpawning)
    {
      _currentGroupIndex++;
      _nextWaveStartTime = Time.time + _waveTriggerDelay;
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
