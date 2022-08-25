using UnityEngine;
using UnityEngine.UI;

public class MonoControl : MonoBehaviour
{
  public static MonoControl Instant;

  public GameObject footFollower, block, fireWork;
  public Transform uiRoot, holderRoot, pointerAnnotation, topLeft, bottomRight;
  public Text playScore, resultScore, playTime;
  public SpriteRenderer[] effects;
  public Sprite correct, wrong;

  public delegate void CallBack();
  public static event CallBack OnCalled;

  private int _footCode, _nodeCode, _score, _second;
  private float _time;
  private IExecuteAble _flow;
  private int _i;

  private void Awake()
  {
    if (Instant == null) { Instant = this; }
    else { Destroy(gameObject); }
  }
  private void Start()
  {
    _flow = Factory.CreateFlow();
  }
  private void Update() { OnCalled?.Invoke(); }
  public GameObject SpawnFireWork() { return Instantiate(fireWork, holderRoot.GetChild(1)); }
  public GameObject SpawnFootFollower() { return Instantiate(footFollower, holderRoot.GetChild(1)); }
  public GameObject SpawnBlock() { return Instantiate(block, holderRoot.GetChild(0)); }
  public void SetFootCode(int foot)
  {
    _footCode = foot;
  }
  public void SetNodeCode(int node)
  {
    _nodeCode = node;
    effects[0].sprite = (node == 3 || node == 5 || node == 8) ? correct : wrong;
    effects[1].sprite = (node == 3 || node == 6 || node == 9) ? correct : wrong;
    effects[2].sprite = (node == 5 || node == 6 || node == 11) ? correct : wrong;
    effects[3].sprite = (node == 8 || node == 9 || node == 11) ? correct : wrong;
  }
  public bool CheckCode()
  {
    return _footCode == _nodeCode;
  }
  public void ResetSession()
  {
    _score = 0;
    _time = 30f;
    _second = 30;
    UpdateUi();
  }
  public void AddScore()
  {
    _score += 1;
    UpdateUi();
  }
  public void SpentTime()
  {
    _time -= Time.deltaTime;
    if (_time <= 0f) { NextFlow(); }
    else
    {
      if (_second != Mathf.RoundToInt(_time))
      {
        _second = Mathf.RoundToInt(_time);
        UpdateUi();
      }
    }
  }
  public void UpdateUi()
  {
    playScore.text = _score.ToString();
    resultScore.text = _score.ToString();
    playTime.text = "00:"+_second.ToString();
  }
  public void NextFlow() { _flow.Execute(); }
}
