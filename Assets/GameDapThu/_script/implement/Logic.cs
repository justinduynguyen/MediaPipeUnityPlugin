public class Logic : IExecutable
{
  private bool _isStarted;
  private readonly IExecutable[] _monsters;
  private readonly IExecutable _tracker;
  public Logic()
  {
    _isStarted = false;
    _tracker = Factory.CreateTracker();
    _monsters = new IExecutable[5];
    for (var i = 0; i < _monsters.Length; i++) { _monsters[i] = Factory.CreatMonster(i); }
  }
  public void Execute()
  {
    if (_isStarted == false)
    {
      StartSession();
      for (var i = 0; i < _monsters.Length; i++) { Control.Instant.holderRoot.GetChild(0).GetChild(i).GetChild(1).gameObject.SetActive(false); }
    }
    else
    {
      _tracker.Execute();
      for (var i = 0; i < _monsters.Length; i++) { _monsters[i].Execute(); }
      if (Control.Instant.life < 1) { EndSession(); }
    }
  }
  private void StartSession()
  {
    _isStarted = true;
    for (var i = 0; i < _monsters.Length; i++) { Control.Instant.holderRoot.GetChild(0).GetChild(i).GetChild(1).gameObject.SetActive(false); }
  }
  private void EndSession()
  {
    _isStarted = false;
    Control.Instant.NextFlowState();
  }
}
