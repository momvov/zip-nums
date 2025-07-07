using CodeBase.Data;
using CodeBase.StaticData;

namespace CodeBase.Services
{
  public class LevelLoader : ILevelLoader
  {
    private readonly ILevelGenerator _levelGenerator;
    private readonly IPuzzleService _puzzleService;
    private readonly MainColorProvider _mainColorProvider;
    private readonly TimerService _timerService;

    private LevelSetupConfig _levelSetupConfig;

    public LevelLoader(
      ILevelGenerator levelGenerator, 
      IPuzzleService puzzleService,
      MainColorProvider mainColorProvider,
      TimerService timerService)
    {
      _levelGenerator = levelGenerator;
      _puzzleService = puzzleService;
      _mainColorProvider = mainColorProvider;
      _timerService = timerService;
    }

    public void SetLevelSetupConfig(LevelSetupConfig levelSetupConfig) => 
      _levelSetupConfig = levelSetupConfig;

    public void LoadNewLevel()
    {
      _timerService.Reset();
      _timerService.Start();
      
      _mainColorProvider.DetermineNewMainColor();
      
      LevelData levelData = _levelGenerator.GenerateLevel(_levelSetupConfig);
      
      _puzzleService.InitializeWithLevel(levelData);
    }
  }
}