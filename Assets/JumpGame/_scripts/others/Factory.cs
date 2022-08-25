using UnityEngine;
public static class Factory
{  
  public static IExecuteAble CreateFlow() { return new Flow(); }
  public static IExecuteAble CreateLogic() { return new GameLogic(); }
  public static IExecuteAble CreateFootTracker() { return new FootTrack(); }
  public static IExecuteVector2Able CreateFeetCode() { return new FeetCode(); }
  public static IFallBlockAble CreateBlockCtrl() { return new FallBlock(); }
  public static INodeAble CreateNode(Transform trans) { return new Node(trans); }
  public static IExecuteVector3Able CreateFireworks() { return new FireWorks(); }
}
