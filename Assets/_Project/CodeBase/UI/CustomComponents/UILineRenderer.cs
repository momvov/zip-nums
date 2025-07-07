using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.CustomComponents
{
  [AddComponentMenu("UI/Extensions/UI Line Renderer")]
  [RequireComponent(typeof(CanvasRenderer))]
  public class UILineRenderer : MaskableGraphic
  {
    public List<Vector2> points = new();

    [Range(1, 100)] public float thickness = 10f;
    [Range(2, 32)] public int cornerResolution = 8;
    [Range(2, 32)] public int capResolution = 8;

    public override Texture mainTexture => s_WhiteTexture;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
      vh.Clear();

      if (points == null || points.Count < 2)
        return;

      DrawLineSegments(vh);
      DrawCorners(vh);
      DrawCaps(vh);
    }
    
    public void SetPoints(IEnumerable<Vector2> newPoints)
    {
      points.Clear();
      points.AddRange(newPoints);
      SetVerticesDirty();
    }

    private void DrawLineSegments(VertexHelper vh)
    {
      float halfThickness = thickness / 2.0f;

      for (int i = 0; i < points.Count - 1; i++)
      {
        Vector2 p1 = points[i];
        Vector2 p2 = points[i + 1];

        Vector2 direction = (p2 - p1).normalized;
        Vector2 normal = new Vector2(-direction.y, direction.x);

        Vector2 v1 = p1 - normal * halfThickness;
        Vector2 v2 = p1 + normal * halfThickness;
        Vector2 v3 = p2 + normal * halfThickness;
        Vector2 v4 = p2 - normal * halfThickness;

        AddQuad(vh, v1, v2, v3, v4);
      }
    }

    private void DrawCorners(VertexHelper vh)
    {
      float halfThickness = thickness / 2.0f;

      for (int i = 1; i < points.Count - 1; i++)
      {
        Vector2 pPrev = points[i - 1];
        Vector2 pCurr = points[i];
        Vector2 pNext = points[i + 1];

        Vector2 dirIn = (pCurr - pPrev).normalized;
        Vector2 dirOut = (pNext - pCurr).normalized;

        float cross = Vector3.Cross(dirIn, dirOut).z;

        if (Mathf.Abs(cross) < 0.001f) 
        {
          float dot = Vector2.Dot(dirIn, dirOut);

          if (dot <= 0) 
            DrawCap(vh, pCurr, dirIn, cornerResolution);

          continue;
        }

        Vector2 normalIn = new Vector2(-dirIn.y, dirIn.x);
        Vector2 normalOut = new Vector2(-dirOut.y, dirOut.x);

        Vector2 startRadius;
        Vector2 endRadius;

        if (cross > 0)
        {
          startRadius = -normalIn * halfThickness;
          endRadius = -normalOut * halfThickness;
        }
        else
        {
          startRadius = normalIn * halfThickness;
          endRadius = normalOut * halfThickness;
        }

        float totalAngle = Vector2.SignedAngle(startRadius, endRadius);

        DrawFan(vh, pCurr, startRadius, totalAngle, cornerResolution);
      }
    }

    private void DrawCaps(VertexHelper vh)
    {
      if (points.Count < 2) 
        return;
      
      Vector2 startCapCenter = points[0];
      Vector2 startCapDir = (startCapCenter - points[1]).normalized;
      DrawCap(vh, startCapCenter, startCapDir, capResolution);

      Vector2 endCapCenter = points[points.Count - 1];
      Vector2 endCapDir = (endCapCenter - points[points.Count - 2]).normalized;
      DrawCap(vh, endCapCenter, endCapDir, capResolution);
    }

    private void DrawCap(VertexHelper vh, Vector2 center, Vector2 direction, int resolution)
    {
      Vector2 normal = new Vector2(-direction.y, direction.x);
      Vector2 startRadius = normal * (thickness / 2.0f);
      DrawFan(vh, center, startRadius, -180f, resolution);
    }

    private void DrawFan(VertexHelper vh, Vector2 center, Vector2 startRadius, float totalAngle, int resolution)
    {
      if (resolution <= 0) 
        return;

      float angleStep = totalAngle / resolution;
      Vector2 prevVertex = center + startRadius;

      for (int i = 1; i <= resolution; i++)
      {
        Vector3 rotatedRadius = Quaternion.Euler(0, 0, angleStep * i) * startRadius;
        Vector2 nextVertex = center + (Vector2)rotatedRadius;
        
        AddTriangle(vh, center, prevVertex, nextVertex);
        
        prevVertex = nextVertex;
      }
    }

    private void AddTriangle(VertexHelper vh, Vector2 v1, Vector2 v2, Vector2 v3)
    {
      int startIndex = vh.currentVertCount;
      
      UIVertex vert1 = UIVertex.simpleVert;
      vert1.position = v1;
      vert1.color = color;
      vh.AddVert(vert1);
      
      UIVertex vert2 = UIVertex.simpleVert;
      vert2.position = v2;
      vert2.color = color;
      vh.AddVert(vert2);
      
      UIVertex vert3 = UIVertex.simpleVert;
      vert3.position = v3;
      vert3.color = color;
      vh.AddVert(vert3);
      
      vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
    }

    private void AddQuad(VertexHelper vh, Vector2 v1, Vector2 v2, Vector2 v3, Vector2 v4)
    {
      int startIndex = vh.currentVertCount;
      
      UIVertex vert1 = UIVertex.simpleVert;
      vert1.position = v1;
      vert1.color = color;
      vh.AddVert(vert1);
      
      UIVertex vert2 = UIVertex.simpleVert;
      vert2.position = v2;
      vert2.color = color;
      vh.AddVert(vert2);
      
      UIVertex vert3 = UIVertex.simpleVert;
      vert3.position = v3;
      vert3.color = color;
      vh.AddVert(vert3);
      
      UIVertex vert4 = UIVertex.simpleVert;
      vert4.position = v4;
      vert4.color = color;
      vh.AddVert(vert4);
      
      vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
      vh.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
    }
  }
}