using UnityEngine;
using UnityEngine.UI;

public class ControlMono : MonoBehaviour
{
  public static ControlMono Instant;

  public int winScore = 10, score;
  public GameObject headAnnotation, follower;
  public Transform topLeft, bottomRight, topPlatform, bottomPlatform, uiRoot, holder, particleRoot;
  public BallBehavior _ball;
  public Slider speedSlider;

  private IExecuteAble _faceCtrl, _scenceCtrl, _soccerLogic;
  private IExecuteIntegerAble _scoreDisplace, _soccerFlow;
  private IExecuteVector3Able _particleScore, _particleBorder, _particleReset;
  private PlayState _state;
  private void Awake()
  {
    if (Instant == null) { Instant = this; }
    else { Destroy(gameObject); }
  }
  private void Start()
  {
    _faceCtrl = Factory.CreateFaceControl();
    _scenceCtrl = Factory.CreateScenceSetup();
    _soccerLogic = Factory.CreateSoccerLogic();
    _soccerFlow = Factory.CreateSoccerFlow();
    _scoreDisplace = Factory.CreateUiSoccerScore();
    _particleScore = Factory.CreateUiSoccerParticle(0);
    _particleBorder = Factory.CreateUiSoccerParticle(1);
    _particleReset = Factory.CreateUiSoccerParticle(2);
    ChangeState(0);
  }
  private void Update()
  {
    if (Instant != null)
    {
      if (_state == PlayState.play)
      {
        _faceCtrl.Execute();
        _soccerLogic.Execute();
      }
      _scenceCtrl.Execute();
    }
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      speedSlider.gameObject.SetActive(!speedSlider.gameObject.activeSelf);
    }
  }
  public void ChangeState(int state)
  {
    _state = (PlayState)state;
    _soccerFlow.Execute(state);
  }
  public void Score(int add)
  {
    score += add;
    _scoreDisplace.Execute(score);
  }
  public GameObject CreateParticle(int id) { return Instantiate(particleRoot.GetChild(id).gameObject, particleRoot); }
  public void ShowParticle(ParticleType type, Vector3 collisionPos)
  {
    switch (type)
    {
      case ParticleType.score: _particleScore.Execute(collisionPos); break;
      case ParticleType.border: _particleBorder.Execute(collisionPos); break;
      case ParticleType.reset: _particleReset.Execute(collisionPos); break;
      default: Debug.Log("How the F*** can this be possible. Wrong particle enum: " + type); break;
    }
  }
  public void SetSpeed()
  {
    _ball.SetSpeed((int)speedSlider.value);
  }
}
public enum PlayState { title = 0, play = 1, result = 2 }
public enum ParticleType { score = 0, border = 1, reset = 2 }
