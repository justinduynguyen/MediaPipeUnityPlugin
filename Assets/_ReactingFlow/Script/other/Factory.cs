public static class Factory
{
  public static IExecuteAble CreateFlow() { return new Flow(); }
  public static IExecuteAble CreateLogic() { return new Logic(); }
  public static IExecuteAble CreateTracker() { return new Tracker(); }
  public static IExecuteAble CreateIdSwap() { return new IdSwap(); }
  public static IExecuteFloatAble CreateTarget(int id) { return new Target(id); }
  public static IExecuteVector3Able CreateParticle(ParticleType type) { return new ShowParticle(type); }
  public static IExecuteAble CreateSceneCtrl() { return new SceneSetting(); }
}
