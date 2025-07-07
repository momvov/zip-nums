using UnityEngine;

namespace CodeBase.Services
{
  public interface IHamiltonianPathGenerator
  {
    Vector2Int[] GenerateHamiltonianPath(int size);
  }
}