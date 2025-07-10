using System;
using System.Collections.Generic;
using CodeBase.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Views
{
  public class GridView : MonoBehaviour
  {
    private const int InitialNumPointsPoolSize = 8;
    
    [SerializeField] private RectTransform _cellsContainer;
    [SerializeField] private RectTransform _numPointsContainer;
    
    [SerializeField] private CellView _cellPrefab;
    [SerializeField] private NumPointView _numPointPrefab;
    
    [SerializeField] private Sprite[] _gridFillSprites;
    [SerializeField] private Sprite[] _gridBorderSprites;
    
    public event Action<Vector2Int, PointerEventData> PointerDownOnCell;
    public event Action<Vector2Int, PointerEventData> PointerEnteredOnCell;

    private CellView[,] _cells;
    
    private List<NumPointView> _numPointsInUse = new();
    private GameObjectPool<NumPointView> _numPointPool;
    
    private int _size;

    public void DrawGrid(int size)
    {
      _size = size;
      
      _cells = new CellView[_size, _size];

      _numPointPool = new GameObjectPool<NumPointView>(_numPointPrefab, InitialNumPointsPoolSize, _numPointsContainer);
      
      float step = 1f / _size;

      for (int y = 0; y < _size; y++)
      {
        for (int x = 0; x < _size; x++)
        {
          CellView newCell = Instantiate(_cellPrefab, _cellsContainer);
          newCell.gameObject.name = $"Cell_{x}_{y}";
          
          newCell.RectTransform.anchorMin = new Vector2(x * step, 1f - (y + 1) * step);
          newCell.RectTransform.anchorMax = new Vector2((x + 1) * step, 1f - y * step);
          newCell.RectTransform.offsetMin = Vector2.zero;
          newCell.RectTransform.offsetMax = Vector2.zero;

          int spriteIndex = GetSpriteIndexForCell(x, y);
          
          newCell.SetFillSprite(_gridFillSprites[spriteIndex]);
          newCell.SetBorderSprite(_gridBorderSprites[spriteIndex]);

          var vectorPosition = new Vector2Int(x, y);

          newCell.PointerEntered += pointerEventData => PointerEnteredOnCell?.Invoke(vectorPosition, pointerEventData);
          newCell.PointerDown += pointerEventData => PointerDownOnCell?.Invoke(vectorPosition, pointerEventData);

          _cells[x, y] = newCell;
        }
      }
    }

    public Vector2 GetCellCenterPosition(Vector2Int position)
    {
      if (_cells == null || _cells[position.x, position.y] == null)
        return Vector2.zero;
        
      RectTransform cellRect = _cells[position.x, position.y].RectTransform;
      Rect containerRect = _cellsContainer.rect;

      Vector2 anchorCenter = (cellRect.anchorMin + cellRect.anchorMax) * 0.5f;

      float posX = (anchorCenter.x - _cellsContainer.pivot.x) * containerRect.width;
      float posY = (anchorCenter.y - _cellsContainer.pivot.y) * containerRect.height;
        
      return new Vector2(posX, posY);
    }

    public void SetNumPoint(Vector2Int position, int num)
    {
      CellView cell = _cells[position.x, position.y];
      if (cell == null) return;

      NumPointView numPoint = _numPointPool.Get();
      numPoint.gameObject.SetActive(true);

      numPoint.RectTransform.SetParent(_numPointsContainer, false);

      numPoint.RectTransform.anchorMin = cell.RectTransform.anchorMin;
      numPoint.RectTransform.anchorMax = cell.RectTransform.anchorMax;
      numPoint.RectTransform.offsetMin = Vector2.zero;
      numPoint.RectTransform.offsetMax = Vector2.zero;
      
      numPoint.SetNum(num);
      _numPointsInUse.Add(numPoint);
    }

    public void SetCellColor(Vector2Int position, Color color) => 
      _cells[position.x, position.y].SetColor(color);

    public void FullClear()
    {
      ClearNums();

      ClearColors();
    }
    
    public void ClearNums()
    {
      foreach (NumPointView numPoint in _numPointsInUse)
      {
        numPoint.gameObject.SetActive(false);
        _numPointPool.Release(numPoint);
      }
      
      _numPointsInUse.Clear();
    }
    
    public void ClearColors()
    {
      for (int y = 0; y < _size; y++)
        for (int x = 0; x < _size; x++) 
          _cells[x, y].SetColor(Color.clear);
    }

    private int GetSpriteIndexForCell(int x, int y)
    {
      int xIndex = x == 0 
        ? 0 
        : x == _size - 1 
          ? 2 
          : 1;
      
      int yIndex = y == 0 
        ? 0 
        : y == _size - 1 
          ? 2 
          : 1;

      return yIndex * 3 + xIndex;
    }
  }
}