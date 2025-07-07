using CodeBase.Services;
using CodeBase.StaticData;
using CodeBase.UI.Presenters;
using UnityEngine;

namespace CodeBase.Infrastructure.CompositionRoot
{
  public class Bootstrapper : MonoBehaviour
  {
    [SerializeField] private LevelSetupConfig _levelSetupConfig;
    [SerializeField] private MainColorsConfig _mainColorsConfig;
    
    [SerializeField] private Transform _windowsContainer;
    [SerializeField] private PuzzlePresenter _puzzlePresenter;

    private MainColorProvider _mainColorProvider;
    private ILevelLoader _levelLoader;
    private WindowsService _windowsService;
    private WinHandler _winHandler;

    private void Awake()
    {
      _mainColorProvider = ServiceLocator.Get<MainColorProvider>();
      _levelLoader = ServiceLocator.Get<ILevelLoader>();
      _windowsService = ServiceLocator.Get<WindowsService>();
      _winHandler = ServiceLocator.Get<WinHandler>();
    }

    private void Start()
    {
      _mainColorProvider.SetConfig(_mainColorsConfig);
      _levelLoader.SetLevelSetupConfig(_levelSetupConfig);
      _windowsService.Initialize(_windowsContainer);
      _winHandler.Initialize();
      _puzzlePresenter.Initialize(_levelSetupConfig.GridSize);
    }
  }
}