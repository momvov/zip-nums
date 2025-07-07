using System.Linq;
using CodeBase.Infrastructure;
using CodeBase.Services;
using CodeBase.UI.CustomComponents;
using CodeBase.UI.Views;
using CodeBase.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Presenters
{
  public class PuzzlePresenter : MonoBehaviour
  {
    [SerializeField] private GridView _gridView;
    [SerializeField] private UILineRenderer _lineRenderer;

    private IPuzzleService _puzzleService;
    private MainColorProvider _mainColorProvider;

    private Color _pathCellColor;
    private Color _pathEndsCellColor;

    public void Initialize(int gridSize)
    {
      _puzzleService = ServiceLocator.Get<IPuzzleService>();
      _mainColorProvider = ServiceLocator.Get<MainColorProvider>();
      
      _gridView.DrawGrid(gridSize);
      
      _gridView.PointerDownOnCell += OnPointerDownOnCell;
      _gridView.PointerEnteredOnCell += OnPointerEnteredOnCell;
      
      _puzzleService.LevelDataSet += OnLevelDataSet;
      _puzzleService.PathPointAdded += OnPathPointAdded;
      _puzzleService.PathPointRemoved += OnPathPointRemoved;

      _mainColorProvider.ColorChanged += OnMainColorChanged;
    }

    private void OnPointerDownOnCell(Vector2Int cellPosition, PointerEventData pointerEventData) => 
      _puzzleService.SetNewPathPoint(cellPosition);

    private void OnPointerEnteredOnCell(Vector2Int cellPosition, PointerEventData pointerEventData) => 
      _puzzleService.SetNewPathPoint(cellPosition);

    private void OnLevelDataSet()
    {
      _gridView.Clear();
      UpdateLineRenderer();
      
      SetNumPoints();
    }

    private void OnPathPointAdded(Vector2Int addedPathPoint)
    {
      if (_puzzleService.PathPoints.Count > 2)
      {
        _gridView.SetCellColor(addedPathPoint, _pathEndsCellColor);
        _gridView.SetCellColor(_puzzleService.GetPenultimatePathPoint(), _pathCellColor);
      }
      else
      {
        _gridView.SetCellColor(addedPathPoint, _pathEndsCellColor);
        _gridView.SetCellColor(_puzzleService.GetPenultimatePathPoint(), _pathEndsCellColor);
      }
      
      UpdateLineRenderer();
    }

    private void OnPathPointRemoved(Vector2Int removedPointPosition)
    {
      if (_puzzleService.PathPoints.Count > 1)
      {
        _gridView.SetCellColor(removedPointPosition, Color.clear);
        _gridView.SetCellColor(_puzzleService.GetLastPathPoint(), _pathEndsCellColor);
      }
      else
      {
        _gridView.SetCellColor(removedPointPosition, Color.clear);
        _gridView.SetCellColor(_puzzleService.GetLastPathPoint(), Color.clear);
      }
      
      UpdateLineRenderer();
    }

    private void UpdateLineRenderer() => 
      _lineRenderer.SetPoints(
        _puzzleService.PathPoints.Select(vector2int => _gridView.GetCellCenterPosition(vector2int)));

    private void SetNumPoints()
    {
      for (var i = 0; i < _puzzleService.LevelData.NumberPositions.Length; i++)
      {
        Vector2Int numPosition = _puzzleService.LevelData.NumberPositions[i];
        _gridView.SetNumPoint(numPosition, i + 1);
      }
    }

    private void OnMainColorChanged(Color mainColor)
    {
      _lineRenderer.color = mainColor;
      
      _pathCellColor = new Color(mainColor.r, mainColor.g, mainColor.b, mainColor.a * 0.5f);
      _pathEndsCellColor = new Color(mainColor.r, mainColor.g, mainColor.b, mainColor.a * 0.2f);

      UpdateCellsColorOnPath();
    }

    private void UpdateCellsColorOnPath()
    {
      if (_puzzleService.PathPoints.Count <= 1) 
        return;
      
      _gridView.SetCellColor(_puzzleService.GetPathPointByIndex(0), _pathEndsCellColor);
      _gridView.SetCellColor(_puzzleService.GetLastPathPoint(), _pathEndsCellColor);

      for (int i = 1; i < _puzzleService.PathPoints.Count - 1; i++) 
        _gridView.SetCellColor(_puzzleService.GetPathPointByIndex(i), _pathCellColor);
    }
  }
}