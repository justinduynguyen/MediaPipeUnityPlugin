using UnityEngine;

public class Control : MonoBehaviour
{
  public static Control Instant;
  public delegate void UpdateCallBack();
  public static event UpdateCallBack OnUpdate;

  public Transform topLeft, bottomRight, uiRoot, holderRoot, annotation, bg, effectRoot;
  public GameObject platformOri, waterDrop, collectEffect, ladingEffect, winEffect;
  public Vector3 trackPos;
  public Vector2 sceneSize;
  public Sprite borderLeft, borderRight;
  public int currentPlatform;
  public bool isPower, isTop, isOnground;
  public AudioSource blopSFX;

  private IExecuteAble _dimension, _follower, _flow, _progress;

  private void Awake()
  {
    if (Instant == null) { Instant = this; }
    else { Destroy(gameObject); }
  }
  private void Start()
  {
    _dimension = Factory.CreateDimensionCtrl();
    _follower = Factory.CreateFollower();
    _flow = Factory.CreateFlow();
    _progress = Factory.CreateProgress();
    OnUpdate += _dimension.Execute;
    OnUpdate += _follower.Execute;
  }
  public void ChangeFlow() { _flow.Execute(); }
  public void ProgressDisplace() { _progress.Execute(); }
  public GameObject SpawnPlatform() { return Instantiate(platformOri, platformOri.transform.parent); }
  public GameObject SpawnLadingEffect() { return Instantiate(ladingEffect, effectRoot); }
  public GameObject SpawnCollectEffect() { return Instantiate(collectEffect, effectRoot); }
  public void PlayJumpSound() { blopSFX.Play(); }
  public void Finish()
  {
    winEffect.transform.position = holderRoot.GetChild(1).GetChild(1).position;
    winEffect.SetActive(true);
  }
  private void Update() { OnUpdate?.Invoke(); }
}
