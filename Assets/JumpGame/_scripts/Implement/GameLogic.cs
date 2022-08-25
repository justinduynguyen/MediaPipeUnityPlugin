public class GameLogic : IExecuteAble, IResetAble
{
  private readonly IExecuteAble _tracker;
  private readonly IFallBlockAble _fallBlock;

  public GameLogic()
  {
    _tracker = Factory.CreateFootTracker();
    _fallBlock = Factory.CreateBlockCtrl();
  }
  public void Execute()
  {
    _tracker.Execute();
    _fallBlock.Fall();
    if (MonoControl.Instant.CheckCode())
    {
      MonoControl.Instant.AddScore();
      _fallBlock.Swap();
    }
    MonoControl.Instant.SpentTime();
  }
  public void Reset()
  {
    _fallBlock.Reset();
  }
}
