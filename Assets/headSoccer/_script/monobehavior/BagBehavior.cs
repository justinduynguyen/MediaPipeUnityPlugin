using UnityEngine;

public class BagBehavior : MonoBehaviour
{
  public SphereCollider coliderSphere;
  private Transform _topLeft, _bottomRight, _bottomPlatform;
  private Vector3 _pos;
  private IExecuteVector3Able _particleCtrl;
  public void Setup(Transform topLeft, Transform bottomRight, Transform bottomPlatform)
  {
    _topLeft = topLeft;
    _bottomRight = bottomRight;
    _bottomPlatform = bottomPlatform;
    _particleCtrl = new Stuffparticle();
  }
  public void UpdatePosition()
  {
    _pos.x = Mathf.Clamp(_bottomPlatform.position.x, _topLeft.transform.position.x + coliderSphere.radius, _bottomRight.transform.position.x - coliderSphere.radius);
    _pos.y = _bottomPlatform.position.y;
    _pos.z = _bottomPlatform.position.z;
    transform.position = _pos;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("platform"))
    {
      other.gameObject.SetActive(false);
      ControlMonoCatchStuff.Instant.Score(1);
      _particleCtrl.Execute(other.transform.position);
    }
  }
}
