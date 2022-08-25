using UnityEngine;

public interface INodeAble
{
  void Move(Vector3 delta);
  void SetPosition(Vector3 pos);
  int GetCode();
  void SetScale(Vector3 scale);
  bool IsBottom();
  Vector3 GetPosition();
  Transform GetTransform();
}
