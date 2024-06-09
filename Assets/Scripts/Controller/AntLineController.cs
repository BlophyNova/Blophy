using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntLineController : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineRenderer;
    public Vector2[] lineRendererPoints;
    public Vector2 offset = Vector2.zero;
    private void Start()
    {
        const float is16_9 = 0.5625f;
        lineRenderer.positionCount = lineRendererPoints.Length;
        lineRenderer.startWidth = .05f;
        lineRenderer.endWidth = .05f;
        if (Screen.height / (float)Screen.width > is16_9)
        {
            //这里放平板的处理方法
            lineRendererPoints[0].x = -.1f;
            lineRendererPoints[1].x = 1.1f;
            lineRendererPoints[2].x = 1.1f;
            lineRendererPoints[3].x = -.1f;
            lineRendererPoints[4].x = -.1f;
            lineRendererPoints[0].y = 0;
            lineRendererPoints[1].y = 0;
            lineRendererPoints[2].y = 1;
            lineRendererPoints[3].y = 1;
            lineRendererPoints[4].y = 0;
        }
        else if (Screen.height / (float)Screen.width < is16_9)
        {
            lineRendererPoints[0].x = 0;
            lineRendererPoints[1].x = 0;
            lineRendererPoints[2].x = 1;
            lineRendererPoints[3].x = 1;
            lineRendererPoints[4].x = 0;
            lineRendererPoints[0].y = -.1f;
            lineRendererPoints[1].y = 1.1f;
            lineRendererPoints[2].y = 1.1f;
            lineRendererPoints[3].y = -.1f;
            lineRendererPoints[4].y = -.1f;
        }
        else
        {
            Destroy(lineRenderer);
            Destroy(this);
        }
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, (Vector2)mainCamera.ViewportToWorldPoint(lineRendererPoints[i]));
        }
    }
    private void Update()
    {
        offset += Vector2.right * Time.deltaTime;
        lineRenderer.material.SetTextureOffset("_MainTex", offset);
    }
}
