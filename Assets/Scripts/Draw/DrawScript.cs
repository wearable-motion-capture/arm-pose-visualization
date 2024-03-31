using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // Vector3[] positions = new Vector3[3] { new Vector3(0, 0, 0), new Vector3(-1, 1, 0), new Vector3(1, 1, 0) };
        // DrawTriangle(positions);
        DrawPolygon(20, 0.22f, transform.position, 0.02f, 0.02f);
    }

    private void DrawTriangle(Vector3[] vertexPositions)
    {
        lineRenderer.positionCount = 3;
        lineRenderer.SetPositions(vertexPositions);
    }

    private void DrawPolygon(int vertexNumber, float radius, Vector3 centerPos, float startWidth, float endWidth)
    {
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.loop = true;
        var angle = 2 * Mathf.PI / vertexNumber;
        lineRenderer.positionCount = vertexNumber;

        for (var i = 0; i < vertexNumber; i++)
        {
            var rotationMatrix = new Matrix4x4(
                new Vector4(Mathf.Cos(angle * i), 0, Mathf.Sin(angle * i)),
                new Vector4(0, 1, 0, 0),
                new Vector4(-1 * Mathf.Sin(angle * i), 0, Mathf.Cos(angle * i), 0),
                new Vector4(0, 0, 0, 1)
            );
            var initialRelativePosition = new Vector3(0, 0, radius);
            lineRenderer.SetPosition(i, centerPos + rotationMatrix.MultiplyPoint(initialRelativePosition));
        }
    }
}