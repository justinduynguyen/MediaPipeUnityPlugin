using UnityEngine;

public class HandTracking : IExecutable
{
  private readonly Transform _annotation;
  private readonly Transform _handA, _handB;
  public HandTracking()
  {
    _annotation = Control.Instant.annotation;
    _handA = Control.Instant.holderRoot.GetChild(1).GetChild(0);
    _handB = Control.Instant.holderRoot.GetChild(1).GetChild(1);
  }
  public void Execute()
  {
    if (_annotation.childCount != 0)
    {
      if (_handA.gameObject.activeSelf == false) { _handA.gameObject.SetActive(true); }
      _handA.position = (_annotation.GetChild(15).position + _annotation.GetChild(17).position + _annotation.GetChild(19).position + _annotation.GetChild(21).position) / 4f;
      Control.Instant.handPosA = _handA.position;
      if (_handB.gameObject.activeSelf == false) { _handB.gameObject.SetActive(true); }
      _handB.transform.position = (_annotation.GetChild(16).position + _annotation.GetChild(18).position + _annotation.GetChild(20).position + _annotation.GetChild(22).position) / 4f;
      Control.Instant.handPosB = _handB.transform.position;
    }
    else
    {
      if (Control.Instant.handPosA != Vector3.zero) { Control.Instant.handPosA = Vector3.zero; }
      if (_handA.gameObject.activeSelf) { _handA.gameObject.SetActive(false); }
      if (Control.Instant.handPosB != Vector3.zero) { Control.Instant.handPosB = Vector3.zero; }
      if (_handB.gameObject.activeSelf) { _handA.gameObject.SetActive(false); }
    }
  }
}
