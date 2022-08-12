using UnityEngine;

public class ControlMonoCatchStuff : MonoBehaviour
{
  public static ControlMonoCatchStuff Instant;

  public int winScore = 10, score;
  public BagBehavior bag;
  public GameObject headAnnotation, follower;
  public Transform topLeft, bottomRight, topPlatform, bottomPlatform, uiRoot, holder, particleRoot;

  private IExecuteAble _faceCtrl, _scenceCtrl, _stuffCtrl;
  private IExecuteIntegerAble _flow, _ui;
  private PlayState _state;
  private const float _Session = 15f;
  private float _playTime;
  private void Awake()
  {
    if (Instant == null) { Instant = this; }
    else { Destroy(gameObject); }
  }
  private void Start()
  {
    _faceCtrl = new FaceControl(this);
    _scenceCtrl = new SceneSetup(this);
    _flow = new StuffFlow();
    _ui = new StuffScoreDisplace();
    _stuffCtrl = new StuffItemControl();
    ChangeState(0);
    bag.Setup(topLeft, bottomRight, bottomPlatform);
  }
  private void Update()
  {
    if (Instant != null)
    {
      _scenceCtrl.Execute();
      if (_state == PlayState.play)
      {
        _faceCtrl.Execute();
        bag.UpdatePosition();
        _playTime -= Time.deltaTime;
        _stuffCtrl.Execute();
        if (_playTime <= 0f) { ChangeState(2); }
      }
    }
  }
  public void ChangeState(int state)
  {
    _state = (PlayState)state;
    _flow.Execute(state);
    if (_state == PlayState.play)
    {
      _playTime = _Session;
      Score(score * -1);
    }
  }
  public void Score(int add)
  {
    score += add;
    _ui.Execute(score);
  }
  public GameObject CreateParticle() { return Instantiate(particleRoot.GetChild(1).gameObject, particleRoot); }
  public GameObject CreateItem() { return Instantiate(holder.GetChild(4).GetChild(0).gameObject, holder.GetChild(4)); }  
}
