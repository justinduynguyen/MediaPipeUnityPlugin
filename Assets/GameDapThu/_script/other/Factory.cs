public static class Factory
{
  public static IExecutable CreateFlow() { return new Flow(); }
  public static IExecutable CreateLogic() { return new Logic(); }
  public static IExecutable CreateTracker() { return new HandTracking(); }
  public static IExecutable CreatMonster(int id) { return new Monster(id); }
  public static IExecutable CreatSceneSetting() { return new SceneSetting(); }
  public static IExecuteBooleanAble CreateMonsterSprite(int id) { return new MonsterSprite(id); }
  public static IParticleAble CreateParticleControl() { return new ParticleControl(); }
  public static IExecuteIntegerAble CreateScoreUi() { return new UiScore(); }
  public static IExecuteIntegerAble CreateLifeUi() { return new UiLife(); }
}
