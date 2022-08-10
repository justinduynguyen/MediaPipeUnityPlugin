using UnityEngine;

public class MoleBehavior : MonoBehaviour, IBooleanAble
{
  public GameObject child;
  private bool _isActive;
  private IExecuteVector3Able _buffParticle;

  private void Start()
  {
    _buffParticle = Factory.GetBuffGenerator();
    child = transform.GetChild(0).gameObject;
    if (child == null) { Destroy(gameObject); }
    else
    {
      child.SetActive(false);
      Invoke(nameof(Activate), 0.1f);
    }
  }
  private void Activate() { child.SetActive(true); }
  private void OnTriggerStay(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      if (_isActive)
      {
        if (child.activeSelf) { _buffParticle.Execute(child.transform.position); }
        Toggle();
        WackAMoleControl.Instant.AddScore();
      }
    }
  }
  public bool GetValue() { return _isActive; }
  public void Toggle()
  {
    _isActive = !_isActive;
    child.SetActive(_isActive);
  }
}
