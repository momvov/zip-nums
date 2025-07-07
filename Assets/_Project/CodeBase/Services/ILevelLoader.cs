using CodeBase.StaticData;

namespace CodeBase.Services
{
  public interface ILevelLoader
  {
    void SetLevelSetupConfig(LevelSetupConfig levelSetupConfig);
    void LoadNewLevel();
  }
}