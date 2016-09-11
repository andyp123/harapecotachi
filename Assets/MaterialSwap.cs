using UnityEngine;

public class MaterialSwap : MonoBehaviour
{
  public bool _swapOnAwake = false;
  public Material[] _defaultMaterials;
  public Material[] _swapMaterials;

  void Awake ()
  {
    if (_swapOnAwake)
      SwapMaterials();
  }

  public void SwapMaterials ()
  {
    if (_swapMaterials.Length % _defaultMaterials.Length != 0)
      Debug.Log("[MaterialSwap] Material array lengths differ. Expect errors.");

    Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
    foreach (Renderer r in renderers)
    {
      int index = System.Array.IndexOf(_defaultMaterials, r.sharedMaterial);
      if (index >= 0)
      {
        r.material = _swapMaterials[index];
      }
    }
  }
}
