using UnityEngine;

public class GeneratePlatform : IExecuteAble
{
  private readonly Transform[] _platformsLevel;
  private int _id;
  private float _floatTempt;
  public GeneratePlatform()
  {
    if (_platformsLevel == null)
    {
      _platformsLevel = new Transform[26];
    }
    _platformsLevel[0] = Control.Instant.platformOri.transform;
    for (var i = 1; i< _platformsLevel.Length; i++)
    {
      _platformsLevel[i] = Control.Instant.SpawnPlatform().transform;
      _platformsLevel[i].gameObject.name = "Platform_" + i.ToString("00");
      _platformsLevel[i].localPosition = (Control.Instant.topLeft.position.y - Control.Instant.bottomRight.position.y) / 5f * (i - 2) * Vector3.up;
    }
  }
  public void Execute()
  {
    _platformsLevel[0].parent.localPosition = Vector3.zero;
    for (var i = 1; i < _platformsLevel.Length; i++)
    {
      if (i % 10 != 0)
      {
        if (i == 1) { _id = Random.Range(1, _platformsLevel[i].childCount - 1); }
        else
        {
          switch (_id)
          {
            case 0: _floatTempt = Random.Range(45f, 90f); break;
            case 1: _floatTempt = Random.Range(27f, 90f); break;
            case 2: _floatTempt = Random.Range(0f, 90f); break;
            case 3: _floatTempt = Random.Range(0f, 67f); break;
            default: _floatTempt = Random.Range(0f, 45f); break;
          }
          if (_floatTempt < 30f) { _id -= 1; }
          else if (_floatTempt < 60f) { _id += 1; }
          _id = Mathf.Clamp(_id, 0, 4);
        }
        for (var p = 0; p < _platformsLevel[i].childCount; p++)
        {
          if (p == _id)
          {
            _platformsLevel[i].GetChild(p).gameObject.SetActive(true);
            _platformsLevel[i].GetChild(p).localPosition = (((p - 2f) * 5f) + Random.Range(p == 0 ? 0f : -2.5f, p == 4 ? 0f : 2.5f)) * 7.872f * Vector3.right;
            _floatTempt = Random.Range(0f, 100f);
            _platformsLevel[i].GetChild(p).GetChild(0).gameObject.SetActive(_floatTempt > 25f);
            _platformsLevel[i].GetChild(p).GetChild(1).gameObject.SetActive(_floatTempt <= 25f);
            _platformsLevel[i].GetChild(p).GetChild(2).gameObject.SetActive(Random.Range(0f, 100f) <= 50f);
          }
          else { _platformsLevel[i].GetChild(p).gameObject.SetActive(false); }
        }
      }
    }
  }
}
