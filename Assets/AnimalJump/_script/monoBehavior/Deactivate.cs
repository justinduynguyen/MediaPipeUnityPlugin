using UnityEngine;

public class Deactivate : MonoBehaviour
{
  public float life;
  public Transform target;
  private void OnEnable() { Invoke(nameof(Hide), life); }
  private void OnDisable() { CancelInvoke(); }
  private void Update()
  {
    if (target != null)
    {
      if (transform.position != target.position) { transform.position = target.position; }
    }
  }
  private void Hide() { gameObject.SetActive(false); }
}
