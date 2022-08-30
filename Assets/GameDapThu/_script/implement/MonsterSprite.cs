using UnityEngine;

public class MonsterSprite : IExecuteBooleanAble
{
  private readonly SpriteRenderer _displace;
  private SpriteType _id;
  public MonsterSprite(int id)
  {
    _displace = Control.Instant.holderRoot.GetChild(0).GetChild(id).GetChild(1).GetComponent<SpriteRenderer>();
  }
  public void Execute(bool param)
  {
    //param mean new session. False = check result after being touched
    if (param)
    {
      _id = (SpriteType)Random.Range(0, 4);
      if (_id != SpriteType.bomb)
      {
        _displace.transform.GetChild(0).gameObject.SetActive(Random.Range(0f, 100f) <= 33.33333f);
        if (_displace.transform.GetChild(1).gameObject.activeSelf) { _displace.transform.GetChild(1).gameObject.SetActive(false); }
      }
      else
      {
        _displace.transform.GetChild(1).gameObject.SetActive(true);
        _displace.transform.GetChild(0).gameObject.SetActive(false);
      }
    }
    else
    {
      if ((int)_id < 3)
      {
        Debug.Log("ADD SCORE!!!!!");
        Control.Instant.AddScore();
      }
    }
    switch (_id)
    {
      case SpriteType.monsterA:
        if (param) { _displace.sprite = Control.Instant.monsterSprites[0]; }
        else { _displace.sprite = Control.Instant.monsterSprites[1]; }
        break;
      case SpriteType.monsterB:
        if (param) { _displace.sprite = Control.Instant.monsterSprites[2]; }
        else { _displace.sprite = Control.Instant.monsterSprites[3]; }
        break;
      case SpriteType.monsterC:
        if (param) { _displace.sprite = Control.Instant.monsterSprites[4]; }
        else { _displace.sprite = Control.Instant.monsterSprites[5]; }
        break;
      case SpriteType.bomb:
        if (param) { _displace.sprite = Control.Instant.monsterSprites[6]; }
        else { _displace.transform.GetChild(1).gameObject.SetActive(false); }
        break;
      default:
        Debug.Log("This is impossible... wrong SpriteType: " + _id);
        break;
    }
  }
}
public enum SpriteType { monsterA = 0, monsterB = 1, monsterC = 2, bomb = 3 }

