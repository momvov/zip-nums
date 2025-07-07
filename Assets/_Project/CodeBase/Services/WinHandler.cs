using CodeBase.StaticData;

namespace CodeBase.Services
{
  public class WinHandler
  {
    private readonly IPuzzleService _puzzleService;
    private readonly WindowsService _windowsService;
    private readonly TimerService _timerService;

    public WinHandler(
      IPuzzleService puzzleService,
      WindowsService windowsService,
      TimerService timerService)
    {
      _puzzleService = puzzleService;
      _windowsService = windowsService;
      _timerService = timerService;
    }

    public void Initialize() => 
      _puzzleService.Solved += OnPuzzleSolved;

    private void OnPuzzleSolved()
    {
      _timerService.Stop();
      _windowsService.ShowWindow(WindowId.Win);
    }
  }
}