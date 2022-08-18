using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class test : MonoBehaviour
{
  public Transform far, near;
  public float lerp;
  private float _lerp;

  private void Update()
  {
    if (lerp != _lerp)
    {
      _lerp = Mathf.Clamp01(lerp);
      lerp = _lerp;
      transform.position = Vector3.Lerp(far.position, near.position, _lerp);
    }
  }
}
