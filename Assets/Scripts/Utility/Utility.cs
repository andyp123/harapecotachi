using UnityEngine;
using System.Collections;

public static class Utility
{
  // This is based on code by Forrest Smith (https://blog.forrestthewoods.com/solving-ballistic-trajectories-b0165523348c#.63e4vx350)
  // Some of the code and comments are directly copied.
  // In this version, time is an input instead of lateralSpeed.
  public static bool SolveBallisticArc (Vector3 startPos, Vector3 targetPos, float time, float maxHeight, out Vector3 fireVelocity, out float gravity)
  {
    fireVelocity = Vector3.zero;
    gravity = 0f;

    Vector3 diffXZ = new Vector3(targetPos.x - startPos.x, 0f, targetPos.z - startPos.z);
    float lateralDistance = diffXZ.magnitude;

    if (lateralDistance < 0.001f || time < 0.001f) // TODO: do I need an epsilon?
      return false;

    float lateralSpeed = lateralDistance / time;
    //float time = lateralDistance / lateralSpeed;

    fireVelocity = diffXZ.normalized * lateralSpeed;

    // System of equations. Hit max_height at t=.5*time. Hit target at t=time.
    //
    // peak = y0 + vertical_speed*halfTime + .5*gravity*halfTime^2
    // end = y0 + vertical_speed*time + .5*gravity*time^s
    // Wolfram Alpha: solve b = a + .5*v*t + .5*g*(.5*t)^2, c = a + vt + .5*g*t^2 for g, v
    float a = startPos.y;       // initial
    float b = maxHeight;       // peak
    float c = targetPos.y;     // final

    gravity = -4 * (a - 2f * b + c) / (time * time);
    fireVelocity.y = -(3f * a - 4f * b + c) / time;

    return true;
  }
}
