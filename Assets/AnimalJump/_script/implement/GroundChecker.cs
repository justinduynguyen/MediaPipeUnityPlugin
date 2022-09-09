using UnityEngine;

public class GroundChecker : IBooleanAble
{//23f
  private readonly Transform _frog, _platformRoot;
  private Transform[] _platforms;
  private Vector2 _border;
  private Vector3 _pos;
  private int _intTempt, _selected;
  private bool _isFoundPlatform;
  private float _floatTmpt;
  private readonly IExecuteTransformAble _ladingEffect;
  public GroundChecker()
  {
    _frog = Control.Instant.holderRoot.GetChild(1).GetChild(1);
    _platformRoot = Control.Instant.platformOri.transform.parent;
    _ladingEffect = Factory.CreateParticleCtrl(true);
  }
  public bool Check()
  {
    if (_platforms == null)
    {
      if (_platformRoot.childCount > 1)
      {
        _platforms = new Transform[_platformRoot.childCount];
        for (var i = 0; i < _platforms.Length; i++) { _platforms[i] = _platformRoot.GetChild(i); }
        CalculatePlaformBorder(_platforms[Control.Instant.currentPlatform]);
      }
      _isFoundPlatform = true;
    }
    else
    {
      if (_frog.position.x >= _border.x && _frog.position.x <= _border.y)
      {//within the border check if airBorne
        if (_frog.position.y - 0.001f > _platforms[Control.Instant.currentPlatform].position.y)
        {
          _isFoundPlatform = false;
        }
        else
        {
          if (_frog.position.y < _platforms[Control.Instant.currentPlatform].position.y)
          {
            _pos = _frog.position;
            _pos.y = _platforms[Control.Instant.currentPlatform].position.y;
            _frog.position = _pos;
          }
          if (_isFoundPlatform == false)
          {
            _selected = 0;
            if (Control.Instant.currentPlatform % 10 == 0)
            {
              _floatTmpt = Vector3.Distance(_platforms[Control.Instant.currentPlatform].GetChild(_selected).position, _frog.position);
              for (var i = 1; i < _platforms[Control.Instant.currentPlatform].childCount; i++)
              {
                if (Vector3.Distance(_platforms[Control.Instant.currentPlatform].GetChild(i).position, _frog.position) < _floatTmpt)
                {
                  _selected = i;
                  _floatTmpt = Vector3.Distance(_platforms[Control.Instant.currentPlatform].GetChild(_selected).position, _frog.position);
                }
              }
            }
            else
            {
              for (var i = 1; i < _platforms[Control.Instant.currentPlatform].childCount; i++)
              {
                if (_platforms[Control.Instant.currentPlatform].GetChild(i).gameObject.activeSelf)
                {
                  _selected = i;
                  break;
                }
              }
            }
            _ladingEffect.Execute(_platforms[Control.Instant.currentPlatform].GetChild(_selected));
          }
          _isFoundPlatform = true;
        }
      }
      else { _isFoundPlatform = false; }
    }
    if (_isFoundPlatform == false) { SeekPlatform(); }        //airborn check from platform above
    if (Control.Instant.isOnground != _isFoundPlatform) { Control.Instant.isOnground = !Control.Instant.isOnground; }
    return _isFoundPlatform;
  }
  private void SeekPlatform()
  {
    for (var i = _platforms.Length - 1; i >= 0; i--)
    {
      if (_platforms[i].position.y < _frog.position.y)
      {
        CalculatePlaformBorder(_platforms[i]);
        if (_frog.position.x >= _border.x && _frog.position.x <= _border.y)
        {
          Control.Instant.currentPlatform = i;              //platform is found regist it to control
          break;
        }
      }
    }
    if (Control.Instant.isTop != (Control.Instant.currentPlatform == _platforms.Length - 1)) { Control.Instant.isTop = !Control.Instant.isTop; }
  }
  private void CalculatePlaformBorder(Transform platform)
  {
    _intTempt = -1;
    for (var i = 0; i < platform.childCount; i++)
    {
      if (platform.GetChild(i).gameObject.activeSelf)
      {
        if (_intTempt != -1)
        {//more than 1 plaform active => all activated          
          _intTempt = -1;
          break;
        }
        else { _intTempt = i; }//get child id of active platform
      }
      else
      {
        if (_intTempt != -1) { break; }//only 1 active platform, break from loop and calculate border
      }
    }
    if (_intTempt == -1)
    {
      _border.x = platform.GetChild(0).position.x - 25f;
      _border.y = platform.GetChild(platform.childCount - 1).position.x + 25f;
      if (Control.Instant.waterDrop != null) { Control.Instant.waterDrop = null; }
    }
    else
    {
      _border.x = platform.GetChild(_intTempt).position.x - 25f;
      _border.y = platform.GetChild(_intTempt).position.x + 25f;
      if (Control.Instant.isPower != platform.GetChild(_intTempt).GetChild(1).gameObject.activeSelf) { Control.Instant.isPower = !Control.Instant.isPower; }
      if (platform.GetChild(_intTempt).GetChild(2).gameObject.activeSelf)
      {
        if (Control.Instant.waterDrop != platform.GetChild(_intTempt).GetChild(2).gameObject) { Control.Instant.waterDrop = platform.GetChild(_intTempt).GetChild(2).gameObject; }
      }
      else
      {
        if (Control.Instant.waterDrop != null) { Control.Instant.waterDrop = null; }
      }
    }
  }
}
