using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
  /// <summary>
  /// Gets or add a component. Usage example:
  /// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
  /// </summary>
  static public T GetOrAddComponent<T> (this Component child) where T: Component
  {
    T result = child.GetComponent<T>();
    if (result == null)
    {
      result = child.gameObject.AddComponent<T>();
    }
    return result;
  }

  // Replacement for transform root for when objects are grouped under another gameobject.
  // Requires TransformRoot component (which can be an empty monobehaviour named TransformRoot)
  static public GameObject FindRoot(this GameObject gameObject)
  {
    TransformRoot root = gameObject.GetComponentInParent<TransformRoot>();
    return (root != null) ? root.gameObject : gameObject;
  }
}
