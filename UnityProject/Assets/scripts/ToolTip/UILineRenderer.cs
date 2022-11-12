using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UILineRenderer : Graphic
{
    public List<Vector2> points = new List<Vector2>();

    float width;
    float height;

    public float thickness = 10;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        width = Screen.width;
        height = Screen.height;

        if (points.Count < 2)
            return;

        if (points[0] == points[1])
        {
            points[1] = points[1] + Vector2.one;
        }

        for (int i = 0; i < points.Count; i++)
        {
            Vector2 point = points[i];
            Vector2 heading;
            if (i < points.Count - 1)
            {
                heading = points[i + 1] - points[i];
            }
            else
            {
                heading = points[i] - points[i - 1];
            }
            var distance = heading.magnitude;
            var direction = heading / distance;
            //Debug.Log(direction);
            DrawVerticesForPoint(point, Vector2.Perpendicular(direction), vh);
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            int index = i * 2;
            vh.AddTriangle(index + 0, index + 1, index + 2);
            vh.AddTriangle(index + 3, index + 2, index + 1);
        }
    }

    protected void FixedUpdate()
    {
        SetVerticesDirty();
    }

    private void DrawVerticesForPoint(Vector2 point, Vector2 normal, VertexHelper vh)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;
        vertex.position = -thickness / 2 * normal;
        vertex.position += new Vector3(point.x - width/2, point.y-height/2);
        vh.AddVert(vertex);

        vertex.position = thickness / 2 * normal;
        vertex.position += new Vector3(point.x - width / 2, point.y - height / 2);
        vh.AddVert(vertex);
    }

    //protected override void OnPopulateMesh(VertexHelper vh)
    //{
    //    Vector2 corner1 = Vector2.zero;
    //    Vector2 corner2 = Vector2.zero;

    //    corner1.x = 0f;
    //    corner1.y = 0f;
    //    corner2.x = 1f;
    //    corner2.y = 1f;

    //    corner1.x -= rectTransform.pivot.x;
    //    corner1.y -= rectTransform.pivot.y;
    //    corner2.x -= rectTransform.pivot.x;
    //    corner2.y -= rectTransform.pivot.y;

    //    corner1.x *= rectTransform.rect.width;
    //    corner1.y *= rectTransform.rect.height;
    //    corner2.x *= rectTransform.rect.width;
    //    corner2.y *= rectTransform.rect.height;

    //    vh.Clear();

    //    UIVertex vert = UIVertex.simpleVert;

    //    vert.position = new Vector2(corner1.x, corner1.y);
    //    vert.color = color;
    //    vh.AddVert(vert);

    //    vert.position = new Vector2(corner1.x, corner2.y);
    //    vert.color = color;
    //    vh.AddVert(vert);

    //    vert.position = new Vector2(corner2.x, corner2.y);
    //    vert.color = color;
    //    vh.AddVert(vert);

    //    vert.position = new Vector2(corner2.x, corner1.y);
    //    vert.color = color;
    //    vh.AddVert(vert);

    //    vh.AddTriangle(0, 1, 2);
    //    vh.AddTriangle(2, 3, 0);
    //}
}
