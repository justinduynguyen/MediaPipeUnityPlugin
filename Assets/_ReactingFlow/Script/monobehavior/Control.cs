using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
  public static Control Instant;
  public delegate void CallBack();
  public static event CallBack OnCall;

  public Transform uiRoot, holder, annotation, particleRoot, topleft, bottomRight;
  public Vector3 handA, handB;
  public Text time, playScore, resultScore;
  public Sprite[] targets, rings;
  public SpriteRenderer[] spriteTargets, spriteRings;
  public GameObject heart, broken, star;
  public float scale;
  private int[] _ids;
  private int level, attemp, currentID, score;
  private IExecuteVector3Able _heart, _broken, _star;
  private bool _isPlayable;

  private IExecuteAble _flow;
  private void Awake()
  {
    if (Instant == null) { Instant = this; }
    else { Destroy(gameObject); }
  }
  private void Start()
  {
    _flow = Factory.CreateFlow();
    _ids = new int[holder.GetChild(1).childCount];
    _heart = Factory.CreateParticle(ParticleType.heart);
    _broken = Factory.CreateParticle(ParticleType.broken);
    _star = Factory.CreateParticle(ParticleType.star);
  }
  public void SetTime(int second) { time.text = (second / 60).ToString("00") + ":" + (second % 60).ToString("00"); }
  public void DisplaceScore()
  {
    playScore.text = score.ToString();
    resultScore.text = score.ToString();
  }
  public void ChangeState() { _flow.Execute(); }
  public void SetLevel(int lvl) { level = lvl; }
  public void SetIDs(int[] ids)
  {
    for (var i = 0; i < ids.Length; i++)
    {
      _ids[i] = ids[i];
      if (_ids[i] <= level)
      {
        spriteTargets[i].sprite = targets[_ids[i]];
        spriteRings[i].sprite = rings[_ids[i]];
      }
      spriteTargets[i].gameObject.SetActive(false);
    }
    currentID = 0;
    _isPlayable = true;
    Invoke(nameof(DisplayTarget), 1f);
  }
  public void ResetSession()
  {
    level = 1;
    score = 0;
    attemp = 0;
    DisplaceScore();
    for (var i = 0; i < particleRoot.childCount; i++) { particleRoot.GetChild(i).gameObject.SetActive(false); }
  }
  private void DisplayTarget()
  {
    for (var i = 0; i < spriteTargets.Length; i++)
    {
      if (_ids[i] <= level) { spriteTargets[i].gameObject.SetActive(true); }
    }
  }
  public void Touchtarget(int id)
  {
    _isPlayable = _ids[id] == currentID;
    if (_isPlayable)
    {
      Debug.Log("Correct target");
      currentID += 1;
      score += Mathf.RoundToInt(Mathf.Pow(2, _ids[id]));
      if (currentID > level)
      {
        _isPlayable = false;
        attemp += 1;
        if (attemp > level)
        {
          attemp = 0;
          level = Mathf.Min(4, level + 1);
        }
        _star.Execute(spriteTargets[id].transform.position);
      }
      else
      {
        _heart.Execute(spriteTargets[id].transform.position);
      }
    }
    else
    {
      attemp = 0;
      level = Mathf.Max(1, level - 1);
      _broken.Execute(spriteTargets[id].transform.position);
      Debug.Log("Wrong target");
    }
    DisplaceScore();
  }
  public bool GetPlayable() { return _isPlayable; }
  public GameObject SpawnHeart() { return Instantiate(heart, particleRoot); }
  public GameObject SpawnBrokenHeart() { return Instantiate(broken, particleRoot); }
  public GameObject SpawnStar() { return Instantiate(star, particleRoot); }

  private void Update() { OnCall?.Invoke(); }
}
