using UnityEngine;

public class Monster : IExecutable
{
  private readonly GameObject _monster;
  private float _dormant, _active, _floatTmp;
  private bool _isDormant, _isTouched;
  private readonly Transform _fire;
  private readonly IExecuteBooleanAble _spriteCtrl;
  public Monster(int id)
  {
    _monster = Control.Instant.holderRoot.GetChild(0).GetChild(id).GetChild(1).gameObject;
    _fire = Control.Instant.holderRoot.GetChild(0).GetChild(id).GetChild(1).GetChild(1);
    _spriteCtrl = Factory.CreateMonsterSprite(id);
    Debug.Log(_monster.name);
  }
  public void Execute()
  {
    if (_monster.activeSelf == false)
    {
      if (_isDormant == false) { DeactivateMonster(); }
      _dormant -= Time.deltaTime;
      if (_dormant <= 0f) { ActivateMonster(); }
    }
    else
    {
      _active -= Time.deltaTime;
      if (_active <= 0f) { DeactivateMonster(); }
      else if (_active >= 1f)
      {
        LerpPosition((1.25f - _active) * 4f);
        if (_fire.gameObject.activeSelf)
        {
          if (_fire.GetChild(2).localPosition != _fire.GetChild(0).localPosition)
          {
            _fire.GetChild(2).localPosition = _fire.GetChild(0).localPosition;
            _fire.GetChild(2).localScale = _fire.GetChild(0).localScale;
          }
        }
      }
      else if (_active <= 0.25f)
      {
        LerpPosition(_active * 4f);
        /*
        if (_fire.gameObject.activeSelf)
        {
          Debug.Log("Kaboom baby!!!");
          Control.Instant.ShowParticle(ParticleType.explosion, _monster.transform.position);
          Control.Instant.LoseLife();
          DeactivateMonster();
        }
        else { LerpPosition(_active * 4f); }
        */
      }
      else
      {
        if (_monster.transform.localPosition != Vector3.up * 1f) { _monster.transform.localPosition = Vector3.up * 1f; }
        if (Vector3.Distance(_monster.transform.position, Control.Instant.handPosA) <= 17f || Vector3.Distance(_monster.transform.position, Control.Instant.handPosB) <= 15f)
        {
          if (_isTouched == false)
          {
            _isTouched = true;
            Touched();
          }
        }
        else
        {
          if (_isTouched) { _isTouched = false; }
        }
        if (_fire.gameObject.activeSelf)
        {
          _floatTmp = Mathf.Clamp01(1.25f - _active);
          _fire.GetChild(2).localPosition = Vector3.Lerp(_fire.GetChild(0).localPosition, _fire.GetChild(1).localPosition, _floatTmp);
          _fire.GetChild(2).localScale = Vector3.Lerp(_fire.GetChild(0).localScale, _fire.GetChild(1).localScale, _floatTmp);
        }
      }
    }
  }
  private void ActivateMonster()
  {
    _isTouched = false;
    _isDormant = false;
    _active = 1.5f;
    if (_monster.activeSelf == false) { _monster.SetActive(true); }
    if (_monster.transform.localPosition != Vector3.down * 2f) { _monster.transform.localPosition = Vector3.down * 2f; }
    _spriteCtrl.Execute(true);
  }
  private void DeactivateMonster()
  {
    _isDormant = true;
    _dormant = Random.Range(1f, 3f);
    if (_monster.transform.localPosition != Vector3.down * 2f) { _monster.transform.localPosition = Vector3.down * 2f; }
    if (_monster.activeSelf) { _monster.SetActive(false); }
  }
  private void Touched()
  {
    if (_monster.transform.GetChild(0).gameObject.activeSelf)
    {
      _monster.transform.GetChild(0).gameObject.SetActive(false);
      Control.Instant.ShowParticle(ParticleType.helm, _monster.transform.GetChild(0).position);
    }
    else
    {
      if (_active > 0.5f) { _active = 0.5f; }
      if (_fire.gameObject.activeSelf)
      {
        /*
        _fire.gameObject.SetActive(false);
        Control.Instant.ShowParticle(ParticleType.bomb, _fire.transform.position);
        */
        Debug.Log("Kaboom baby!!!");
        Control.Instant.ShowParticle(ParticleType.explosion, _monster.transform.position);
        Control.Instant.LoseLife();
        DeactivateMonster();
      }
      else { Control.Instant.ShowParticle(ParticleType.monster, _monster.transform.position); }
      _spriteCtrl.Execute(false);
    }
  }
  private void LerpPosition(float lerp) { _monster.transform.localPosition = Vector3.Lerp(Vector3.down * 2f, Vector3.up * 1f, lerp); }
}
