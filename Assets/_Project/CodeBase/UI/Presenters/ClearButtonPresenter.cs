using CodeBase.Infrastructure;
using CodeBase.Services;

namespace CodeBase.UI.Presenters
{
  public class ClearButtonPresenter : ButtonPresenter
  {
    private IPuzzleService _puzzleService;
    
    protected override void Awake()
    {
      base.Awake();

      _puzzleService = ServiceLocator.Get<IPuzzleService>();
    }

    protected override void OnClicked() => 
      _puzzleService.ClearPath();
  }
}