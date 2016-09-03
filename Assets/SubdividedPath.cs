using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubdividedPath : MonoBehaviour
{
  public struct PathPoint
  {
    public Vector3 position;
    public Vector3 tangent;
    public Quaternion rotation;
  }

  public Path _path;
  public float _subdivisionSize = 1f; // will get changed slightly through initialization
  public float _width = 1f; // width of mesh
  public Material _material; // this will be assigned to the generated mesh

  List<SubdividedPath.PathPoint> _points;
  float _length = 0f; // will be the same as the original path length

  bool _initialized = false;

  void Awake ()
  {
    StartCoroutine(Initialize());
  }

  // TODO: this is a horrible hack. The whole curve/path system will need a bit of a refactor soon.
  IEnumerator Initialize ()
  {
    yield return new WaitForSeconds(1f);

    if (_path != null)
    {
      _points = _path.GetEvenlySubdividedPath(_subdivisionSize);
      if (_points != null)
      {
        _length = _path.Length;
        _subdivisionSize = (_points[1].position - _points[0].position).magnitude; // get actual subdivisionSize
        _initialized = true;

        GenerateMesh(_width);
      }
    }
    yield return null;
  }

  public PathPoint GetInterpolatedPointAtDistance (float distance)
  {
    if (_initialized)
      Debug.LogError("[SubdividedPath] Cannot get point from a path that has not been initialized.");

    distance = Mathf.Clamp(distance, 0f, _length);
    if (distance == 0f)
      return _points[0]; // TODO: check this is returned by value (copy)

    int maxIndex = _points.Count - 1;
    int index = (int)Mathf.Floor(distance / _length * maxIndex);
    if (index == maxIndex)
      return _points[maxIndex];
    float t = distance - index * (_length / maxIndex); // 0-1

    PathPoint a = _points[index];
    PathPoint b = _points[index + 1];

    PathPoint p = new PathPoint();
    p.position = Vector3.Lerp(a.position, b.position, t);
    p.rotation = Quaternion.Lerp(a.rotation, b.rotation, t);
    p.tangent = p.rotation * Vector3.forward;

    return p;
  }

  void GenerateMesh (float width, int subdivisionsWidth = 1)
  {
    int pointsWidth = subdivisionsWidth + 1;
    int pointsLength = _points.Count;

    if (!_initialized || pointsLength < 2 || pointsWidth < 2)
      return;

    // add required components
    MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
    MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
    meshRenderer.sharedMaterial = _material;
    meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    Mesh mesh = meshFilter.mesh;
    mesh.name = _path.name;
    mesh.Clear();

    // TODO: these could easily be arrays for an imperceptable performance bump :D
    List<Vector3> vertices = new List<Vector3>(pointsLength * pointsWidth);
    List<Vector2> uvs = new List<Vector2>(pointsLength * pointsWidth);
    List<int> triangles = new List<int>((pointsLength - 1) * subdivisionsWidth * 6); // 2 tris per quad, 3 verts per triangle

    // generate vertices and uvs
    for (int i = 0; i < pointsLength; ++i)
    {
      PathPoint p = _points[i];
      Vector3 perpendicular = Vector3.Cross(Vector3.up, p.tangent).normalized;
      Vector3 start = p.position - perpendicular * width * 0.5f;

      for (int j = 0; j < pointsWidth; ++j)
      {
        float u = (float)j / subdivisionsWidth;
        float v = (float)i / (pointsLength - 1);
        uvs.Add(new Vector2(u, v));

        Vector3 vertex = start + (perpendicular * u * width);
        vertices.Add(vertex);
      }
    }

    // index triangles (by quads, two tris per quad, a and b)
    for (int qi = 0; qi < pointsLength - 1; ++qi)
    {
      for (int qj = 0; qj < subdivisionsWidth; ++qj)
      {
        int vi = qi * pointsWidth + qj; // get first vertex index from quad index
        triangles.Add(vi); // a
        triangles.Add(vi + pointsWidth);
        triangles.Add(vi + pointsWidth + 1);
        triangles.Add(vi); // b
        triangles.Add(vi + pointsWidth + 1);
        triangles.Add(vi + 1);
      }
    }

    // TODO: don't need to copy all the data if change list to array
    mesh.vertices = vertices.ToArray();
    mesh.uv = uvs.ToArray();
    mesh.triangles = triangles.ToArray();
    mesh.RecalculateNormals();
  }

  // void OnDrawGizmos ()
  // {
  //   if (_initialized)
  //   {

  //     Gizmos.color = Color.red;
  //     //float scale = _subdivisionSize * 0.5f;
  //     foreach (PathPoint p in _points)
  //     {
  //       Vector3 perpendicular = Vector3.Cross(p.tangent, Vector3.up); // right of tangent
  //       Vector3 start = p.position - perpendicular * _width * 0.5f;
  //       Vector3 end = p.position + perpendicular * _width * 0.5f;

  //       Gizmos.DrawLine(start, end);
  //       //Gizmos.DrawLine(p.position, p.position + p.tangent * scale);
  //     }
  //   }
  // }
}
