using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(Path))]
public class PathEditor : Editor
{
  private Path _path;
  private int _selectedPointIndex = -1;

  private float _handleScale = 0.05f;

  private Tool _prevTool = Tool.None;

  void OnEnable()
  {
    _path = target as Path;
    _prevTool = Tools.current;
    Tools.current = Tool.None;

    // hide default transform in inspector
     _path.transform.hideFlags = HideFlags.HideInInspector;
  }

  void OnDisable()
  {
    Tools.current = _prevTool;
  }

  void OnSceneGUI()
  {
    _path = target as Path;

    Handles.color = Color.white;

    //draw lines
    if (_path.NumPoints > 1)
    {
      Vector3 a = _path.GetPoint(0);
      for (int i = 1; i < _path.NumPoints; ++i)
      {
        Vector3 b = _path.GetPoint(i);
        Handles.DrawLine(a, b);
        a = b;
      }
    }

    // draw handles
    for (int i = 0; i < _path.NumPoints; ++i )
    {
      DrawHandle(i);
    }

    EditorUtility.SetDirty(_path);
  }

  void DrawHandle(int index)
  {
    Handles.color = (index == _selectedPointIndex) ? Color.yellow : Color.white;

    Vector3 point = _path.GetPoint(index);
    float size = HandleUtility.GetHandleSize(point) * _handleScale;
    
    // first detect click
    if ( Handles.Button(point, Quaternion.identity, size, size, Handles.DotCap) )
    {
      _selectedPointIndex = index;
      Repaint();
    }    

    // handle moving the handle
    if (_selectedPointIndex == index)
    {
      EditorGUI.BeginChangeCheck();
      // free move handle does not work well. Ideally one click+drag to select and move,
      // which means custom hover + click detection is required
      //point = Handles.FreeMoveHandle(point, Quaternion.identity, size, _handleSnap, Handles.DotCap);
      point = Handles.DoPositionHandle(point, Quaternion.identity);
      if (EditorGUI.EndChangeCheck())
      {
        Undo.RecordObject(_path, "move path point");
        _path.SetPoint(index, point);
        EditorUtility.SetDirty(_path);
      }
    }
  }

  public override void OnInspectorGUI()
  {
    Path path = target as Path;

    if (GUILayout.Button("Add Point"))
    {
      Undo.RecordObject(path, "add path point");
      path.AddPoint(Vector3.zero);
      EditorUtility.SetDirty(path);

      _selectedPointIndex = path.NumPoints - 1;
    }
    
    if (_selectedPointIndex >= 0)
    {
      Vector3 oldPoint = path.GetPoint(_selectedPointIndex);
      Vector3 newPoint = EditorGUILayout.Vector3Field("Selected Point (" + _selectedPointIndex + ")", oldPoint);
      if (oldPoint != newPoint)
      {
        Undo.RecordObject(path, "move path point");
        path.SetPoint(_selectedPointIndex, newPoint);
        EditorUtility.SetDirty(path);
      }

      if (GUILayout.Button("Remove Selected Point"))
      {
        Undo.RecordObject(path, "remove path point");
        path.RemovePoint(_selectedPointIndex);
        EditorUtility.SetDirty(path);

        if (_selectedPointIndex >= path.NumPoints)
          _selectedPointIndex = -1;
      }
    }

    // if (GUI.changed)
    //   EditorUtility.SetDirty(path);
  }
}
