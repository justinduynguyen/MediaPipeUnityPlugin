using UnityEngine;

public static class Factory
{
  public static IExecuteAble CreateResolutionControl(Transform topLeft, Transform bottomRight, Transform[] moles) { return new ResolutionCalculator(topLeft, bottomRight, moles); }
}
