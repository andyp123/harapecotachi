using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(Sensor))]
public class SensorEditor : Editor
{
  public override void OnInspectorGUI()
  {
    Sensor sensor = target as Sensor;

    float radius = EditorGUILayout.FloatField("Sensor Radius", sensor.SensorRadius);
    if (radius != sensor.SensorRadius)
    {
      Undo.RecordObject(sensor, "Change Sensor Radius");
      sensor.SensorRadius = radius;
      EditorUtility.SetDirty(sensor);
    }
  }
}