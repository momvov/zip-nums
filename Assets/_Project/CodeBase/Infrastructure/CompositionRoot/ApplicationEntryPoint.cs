using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.CompositionRoot
{
  public static class ApplicationEntryPoint
  {
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Main()
    {
      RegisterServices();
    }

    private static void RegisterServices()
    {
      WindowsService windowsService = new WindowsService();
      ServiceLocator.Register(windowsService);
      
      MainColorProvider mainColorProvider = new MainColorProvider();
      ServiceLocator.Register(mainColorProvider);
      
      IHamiltonianPathGenerator hamiltonianPathGen = new OptimizedHamiltonianPathGenerator();
      ServiceLocator.Register(hamiltonianPathGen);
      
      ILevelGenerator levelGenerator = new LevelGenerator(hamiltonianPathGen);
      ServiceLocator.Register(levelGenerator);
      
      IPuzzleService puzzleService = new PuzzleService();
      ServiceLocator.Register(puzzleService);
      
      TimerService timerService = new TimerService();
      ServiceLocator.Register(timerService);
      
      ILevelLoader levelLoader = new LevelLoader(levelGenerator, puzzleService, mainColorProvider, timerService);
      ServiceLocator.Register(levelLoader);
      
      WinHandler winHandler = new WinHandler(puzzleService, windowsService, timerService);
      ServiceLocator.Register(winHandler);
    }
  }
}