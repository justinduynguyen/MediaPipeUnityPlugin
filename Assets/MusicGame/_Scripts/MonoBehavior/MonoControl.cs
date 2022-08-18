using UnityEngine;

public class MonoControl : MonoBehaviour
{
  public static MonoControl Instant;
  public Transform annotationRoot, uiRoot, holder, topLeft, bottomRight;
  public GameObject handFollow, itemOriginal, itemOriginalRight, particle;

  private ICheckAble _miniScreen;
  private IExecuteAble _handTrack, _leftTrail, _rightTrail;
  private IExecuteIntegerAble _flow, _comboCtrl, _scoreCtrl;
  private ITargetCheckAble _leftSide, _rightSide;
  private IExecuteVector3Able _particleCtrl;
  private bool _isInitialized;
  private FlowState _state;
  private float _gameTime;
  private int _combo, _score;
  private void Awake()
  {
    if (Instant == null)
    {
      Instant = this;
      _isInitialized = false;
      _miniScreen = Factory.CreateMiniScreen(uiRoot.GetChild(0).GetChild(0), annotationRoot);
      _handTrack = Factory.CreateHandTrack(annotationRoot, holder, topLeft, bottomRight);
      _leftTrail = Factory.CreateTrailItem(holder.GetChild(1).GetChild(2), holder.GetChild(1).GetChild(1), true);
      _leftSide = (ITargetCheckAble)_leftTrail;
      _rightTrail = Factory.CreateTrailItem(holder.GetChild(1).GetChild(4), holder.GetChild(1).GetChild(3), false);
      _rightSide = (ITargetCheckAble)_rightTrail;
      _flow = Factory.CreateFlowControl(uiRoot, holder);
      _comboCtrl = Factory.CreateComboControl(uiRoot);
      _scoreCtrl = Factory.CreateScoreControl(uiRoot);
      _particleCtrl = Factory.CreateParticleControl();
      _state = FlowState.title;
      ChangeFlowState(0);
    }
    else if (Instant != this) { Destroy(gameObject); }
  }
  private void Update()
  {
    if (_isInitialized == false) { _isInitialized = _miniScreen.Check(); }
    else
    {
      if(_state == FlowState.play)
      {
        if (_gameTime > 0f) { _gameTime -= Time.deltaTime; }
        else { ChangeFlowState(2); }
        _handTrack.Execute();
        _leftTrail.Execute();
        _rightTrail.Execute();
      }
    }
  }
  public GameObject GetHandFollower() { return Instantiate(handFollow, handFollow.transform.parent); }
  public void CheckLeftSide(TargetCheck target)
  {
    if (_leftSide.Check(target))
    {
      Debug.Log("Check LEFT side: " + target);
      _combo += 1;
      _comboCtrl.Execute(_combo);
      _score += 10;
      _scoreCtrl.Execute(_score);
    }
  }
  public void CheckRightSide(TargetCheck target)
  {
    if (_rightSide.Check(target))
    {
      Debug.Log("Check RIGHT side: " + target);
      _combo += 1;
      _comboCtrl.Execute(_combo);
      _score += 10;
      _scoreCtrl.Execute(_score);
    }
  }
  public void MissCombo()
  {
    _combo = 0;
    _comboCtrl.Execute(_combo);
  }
  public void ChangeFlowState(int state)
  {
    _state = (FlowState)state;
    _flow.Execute(state);
    if (_state == FlowState.play)
    {
      _gameTime = 30f;
      _combo = 0;
      _comboCtrl.Execute(_combo);
      _score = 0;
      _scoreCtrl.Execute(_score);
      for (var i = 0; i < itemOriginal.transform.parent.childCount; i++)
      {
        itemOriginal.transform.parent.GetChild(i).gameObject.SetActive(false);
      }
    }
  }
  public void Showparticle(Vector3 position) { _particleCtrl.Execute(position); }
  public GameObject SpawnItem(bool isLeft) { return Instantiate(isLeft ? itemOriginal : itemOriginalRight, itemOriginal.transform.parent); }
  public GameObject SpawnParticle() { return Instantiate(particle, particle.transform.parent); }
}
public enum TargetCheck { top = 0, bottom = 1, left = 2, right = 3 }
public enum FlowState { title = 0, play = 1, result = 2 }
