using UnityEngine;

public class DelayInactive : MonoBehaviour
{
  public float life;
  private void OnEnable()
  {
    Invoke(nameof(Deactivate), life);
  }
  private void Deactivate()
  {
    gameObject.SetActive(false);
  }
}
