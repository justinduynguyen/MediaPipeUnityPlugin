using UnityEngine;

public class BallBehavior : MonoBehaviour
{
  private Vector3 _direction, _v3Tmp, _v3CollisionPoint;
  private Transform _bottomPlatform;
  private float _waitTime, _ballRadius;
  private float _speed = 20f, _pauseTime = 2f;
  private int i;
  private Vector2 _ballAxisXRange;
  private Rigidbody _body;

  private void OnEnable() { PauseGame(); }
  private void Start()
  {
    _direction = Vector3.zero;
    _body = GetComponent<Rigidbody>();
    SetBottomPlatform();
    _ballRadius = GetComponent<SphereCollider>().radius;
  }
  private void SetBottomPlatform()
  {
    if (ControlMono.Instant != null)
    {
      _bottomPlatform = ControlMono.Instant.bottomPlatform;
      _ballAxisXRange.x = ControlMono.Instant.topLeft.position.x;
      _ballAxisXRange.y = ControlMono.Instant.bottomRight.position.x;
    }
    else { Invoke(nameof(SetBottomPlatform), Time.deltaTime); }
  }
  private void Update()
  {
    if (_direction.magnitude > 0f) { _body.MovePosition(transform.position + (_speed * Time.deltaTime * _direction)); }
    else { StickToBottom(); }
  }
  private void OnCollisionEnter(Collision collision)
  {
    _v3CollisionPoint = collision.contacts[0].point;
    if (collision.collider.CompareTag("SceneVerticalBorder"))
    {
      ControlMono.Instant.ShowParticle(ParticleType.reset, transform.position);
      PauseGame();
    }
    else
    {
      if (collision.collider.CompareTag("platform"))
      {
        ControlMono.Instant.Score(1);
        ControlMono.Instant.ShowParticle(ParticleType.score, _v3CollisionPoint);
      }
      else { ControlMono.Instant.ShowParticle(ParticleType.border, _v3CollisionPoint); }
      for (i = 0; i < collision.contacts.Length; i++) { _v3Tmp = i == 0 ? collision.contacts[i].normal : _v3Tmp + collision.contacts[i].normal; }
      _direction = Vector3.Reflect(_direction, _v3Tmp.normalized);
      _direction = Quaternion.Euler(0f, 0f, Random.Range(-15f, 15f)) * _direction;
    }
  }
  private void StickToBottom()
  {
    if (_bottomPlatform != null)
    {
      _v3Tmp = _bottomPlatform.position + (Vector3.up * 2.5f);
      _v3Tmp.x = Mathf.Clamp(_v3Tmp.x, _ballAxisXRange.x + _ballRadius, _ballAxisXRange.y - _ballRadius);
      transform.position = _v3Tmp;
    }
    _waitTime -= Time.deltaTime;
    if (_waitTime <= 0f) { RenewDirection(); }
  }
  private void RenewDirection()
  {
    _direction.x = Random.Range(-1f, 1f);
    _direction.y = Random.Range(1, 2f);
    _direction.z = 0f;
    if (_direction.magnitude == 0f) { _direction = Vector3.up; }
    else { _direction = _direction.normalized; }
  }
  private void PauseGame()
  {
    _direction = Vector3.zero;
    _waitTime = _pauseTime;
  }
  public void SetSpeed(int speed)
  {
    _speed = speed;
  }
}
