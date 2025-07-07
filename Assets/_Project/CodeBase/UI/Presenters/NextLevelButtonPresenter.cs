using CodeBase.Infrastructure;
using CodeBase.Services;
using CodeBase.StaticData;

namespace CodeBase.UI.Presenters
{
  public class NextLevelButtonPresenter : ButtonPresenter
  {
    private WindowsService _windowsService;
    private ILevelLoader _levelLoader;
    
    protected override void Awake()
    {
      base.Awake();
      
      _windowsService = ServiceLocator.Get<WindowsService>();
      _levelLoader = ServiceLocator.Get<ILevelLoader>();
    }

    protected override void OnClicked()
    {
      _windowsService.HideWindow(WindowId.Win);
      _levelLoader.LoadNewLevel();
    }
  }
}