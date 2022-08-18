using UnityEngine;

public static class Factory
{
  public static ICheckAble CreateMiniScreen(Transform copy, Transform source)
  {
    return new CopyMiniScreen(copy, source);
  }
  public static IExecuteAble CreateHandTrack(Transform annotation, Transform holder, Transform topLeft, Transform bottomRight)
  {
    return new HandTrack(annotation, holder, topLeft, bottomRight);
  }
  public static IExecuteVector2Able CreateScreenFollower(Transform follower, Transform screenTopLeft, Transform screenBottomRight, bool isLeft)
  {
    return new ScreenFollower(follower, screenTopLeft, screenBottomRight, isLeft);
  }
  public static IExecuteAble CreateTrailItem(Transform far, Transform near, bool isLeft)
  {
    return new TrailItemControl(far, near, isLeft);
  }
  public static IExecuteIntegerAble CreateFlowControl(Transform uiRoot, Transform holder)
  {
    return new FlowControl(uiRoot, holder);
  }
  public static IExecuteIntegerAble CreateScoreControl(Transform uiRoot)
  {
    return new ScoreDisplayControl(uiRoot);
  }
  public static IExecuteIntegerAble CreateComboControl(Transform uiRoot)
  {
    return new ComboCtrl(uiRoot);
  }
  public static IExecuteVector3Able CreateParticleControl()
  {
    return new ParticleControl();
  }
}
