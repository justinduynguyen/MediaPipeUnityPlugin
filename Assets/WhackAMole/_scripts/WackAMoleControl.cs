using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WackAMoleControl : MonoBehaviour
{
  public static WackAMoleControl Instant;

  public Transform screenTopLeft, screenBottomRight;
  public Transform[] moles;
  private IExecuteAble _resCtrl;
  private void Start()
  {
    if (Instant == null)
    {
      Instant = this;
      _resCtrl = Factory.CreateResolutionControl(screenTopLeft, screenBottomRight, moles);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void Update()
  {
    if (Instant != null)
    {
      _resCtrl.Execute();
    }    
  }
}
