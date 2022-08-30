using UnityEngine;

public interface IParticleAble
{
  void SpawnParticle(ParticleType type, Vector3 position);
}
public enum ParticleType { explosion = 0, monster = 1, helm = 2, bomb = 3 }
