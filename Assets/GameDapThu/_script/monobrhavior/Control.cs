using UnityEngine;

public class Control : MonoBehaviour
{
  public static Control Instant;
  public delegate void CallBackDElegate();
  public static event CallBackDElegate OnCall;

  public Transform uiRoot, holderRoot, topLeft, bottomRight, foreGround, annotation, effectRoot;
  public GameObject bombHitEffect, helmetHitEffect, monsterHitEffect, bombExplosion;
  public Sprite[] monsterSprites, hearts;
  public Vector3 handPosA, handPosB;

  private IExecutable _flow;
  private IExecutable _scenceCtrl;
  private IParticleAble _particle;
  private IExecuteIntegerAble _lifeUI, _scoreUI;
  public int life, point;
  private void Awake()
  {
    if (Instant == null) { Instant = this; }
    else { Destroy(gameObject); }
  }
  private void Start()
  {
    _particle = Factory.CreateParticleControl();
    _flow = Factory.CreateFlow();
    _scenceCtrl = Factory.CreatSceneSetting();
    _lifeUI = Factory.CreateLifeUi();
    _scoreUI = Factory.CreateScoreUi();
  }
  public void AddScore()
  {
    point += 1;
    _scoreUI.Execute(point);
  }
  private void MinusLife()
  {
    if (life > 0)
    {
      life -= 1;
      _lifeUI.Execute(life);
    }
  }
  public void LoseLife() { Invoke(nameof(MinusLife), 0.5f); }
  public void ResetUI()
  {
    point = 0;
    life = 3;
    _lifeUI.Execute(3);
    _scoreUI.Execute(0);
    for (var i = 0; i < effectRoot.childCount; i++) { effectRoot.GetChild(i).gameObject.SetActive(false); }
  }
  public void NextFlowState() { _flow.Execute(); }
  public void ShowParticle(ParticleType type, Vector3 pos) { _particle.SpawnParticle(type, pos); }
  public GameObject SpawnParticle(ParticleType type)
  {
    switch (type)
    {
      case ParticleType.explosion: return Instantiate(bombExplosion, effectRoot);
      case ParticleType.monster: return Instantiate(monsterHitEffect, effectRoot);
      case ParticleType.helm: return Instantiate(helmetHitEffect, effectRoot);
      case ParticleType.bomb: return Instantiate(bombHitEffect, effectRoot);
      default: return null;
    }
  }
  private void Update()
  {
    _scenceCtrl.Execute();
    OnCall?.Invoke();
  }
}
