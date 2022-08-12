public static class Factory
{
  public static IExecuteAble CreateFaceControl() { return new FaceControl(); }
  public static IExecuteAble CreateScenceSetup() { return new SceneSetup(); }
  public static IExecuteAble CreateSoccerLogic() { return new SoccerLogic(); }
  public static IExecuteFloatAble CreateUiSoccerTime() { return new SoccerTimeDisplace(); }
  public static IExecuteIntegerAble CreateUiSoccerScore() { return new SoccerScoreDisplace(); }
  public static IExecuteVector3Able CreateUiSoccerParticle(int id) { return new SoccerParticlePlatform(id); }
  public static IExecuteIntegerAble CreateSoccerFlow() { return new SoccerFlow(); }
}
