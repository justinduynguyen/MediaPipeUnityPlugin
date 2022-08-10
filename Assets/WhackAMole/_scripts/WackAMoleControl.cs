using UnityEngine;

public class WackAMoleControl : MonoBehaviour
{
  public static WackAMoleControl Instant;

  public GameObject molesCollection, instruction, playUI, startButton, brandMessage, buffParticle, holder;
  public Transform screenTopLeft, screenBottomRight, uiRoot, rectListAnnotation, followObject;
  public Transform[] moles;
  private IExecuteAble _resCtrl, _handCtrl;
  private IExecuteFloatAble _moleCtrl;
  private IExecuteBoolean _scoreCtrl;
  private PlayState _state;
  private float _timePlay;

  private void Start()
  {
    if (Instant == null)
    {
      Instant = this;
      _resCtrl = Factory.CreateResolutionControl();
      _moleCtrl = Factory.CreateMoleController();
      _scoreCtrl = Factory.CreatScoreDisplay();
      _handCtrl = Factory.CreateFollowerControl();
      _state = PlayState.title;
      InitGame();
    }
    else { Destroy(gameObject); }
  }
  public void InitGame()
  {
    SetState(PlayState.title);
  }
  public void Replay()
  {
    _timePlay = 30f;
    _scoreCtrl.Execute(true);
    SetState(PlayState.play);
  }
  public void SetState(PlayState state)
  {
    _state = state;
    instruction.SetActive(_state == PlayState.play);
    molesCollection.SetActive(_state == PlayState.play);
    holder.SetActive(_state == PlayState.play);
    playUI.SetActive(_state == PlayState.play || _state == PlayState.result);
    startButton.SetActive(_state == PlayState.title);
    brandMessage.SetActive(_state == PlayState.result);
  }
  public void AddScore() { _scoreCtrl.Execute(false); }
  private void Update()
  {
    if (Instant != null && _state == PlayState.play)
    {
      _resCtrl.Execute();
      _moleCtrl.Execute(Time.deltaTime);
      _handCtrl.Execute();
      _timePlay -= Time.deltaTime;
      if (_timePlay < 0f) { SetState(PlayState.result); }
    }
  }
  public GameObject CreateFollowobject() { return Instantiate(followObject.gameObject, followObject.parent); }
  public GameObject CreateBuffparticle() { return Instantiate(buffParticle, buffParticle.transform.parent); }
  public bool IsPlaying() { return _state == PlayState.play; }
}

public enum PlayState { title = 0, play = 1, result = 2 }
