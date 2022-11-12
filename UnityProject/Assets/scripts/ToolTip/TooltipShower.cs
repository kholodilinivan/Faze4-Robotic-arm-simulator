using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipShower : MonoBehaviour
{
    [HideInInspector]
    public TooltipInformation tooltip;
    public Text text;
    public TooltipDragElement dragElement;
    [SerializeField]
    UILineRenderer UILineRenderer;
    private Camera cam;

    public RectTransform point;

    public void OnEnable()
    {
        StartCoroutine(SetInMidle());
    }

    internal void Init(TooltipInformation tooltip, Camera cam, Canvas canvas, RectTransform canvasRect, float size)
    {
        this.tooltip = tooltip;
        this.cam = cam;
        dragElement.Init(canvas, canvasRect, size);
    }

    private void Update()
    {
        var position = tooltip.transform.localPosition;
        text.text = $"[{-position.x:0.000} : {position.z:0.000} : {position.y:0.000}]";
    }

    public void ChangeSize(float value)
    {
        dragElement.ChangeSize(value);
        point.localScale = new Vector3(value, value, value);
    }

    IEnumerator SetInMidle()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            dragElement.pivotRect.anchoredPosition = new Vector2(0, 0);
            var position = tooltip.transform.position;

            Vector2 viewportPoint = cam.WorldToViewportPoint(position);  //convert game object position to VievportPoint
            viewportPoint.x *= cam.rect.width;
            viewportPoint.y *= cam.rect.height;

            //set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
            dragElement.pivotRect.anchorMin = viewportPoint;
            dragElement.pivotRect.anchorMax = viewportPoint;

            if (!dragElement.state)
            {
                dragElement.thisRect.position = dragElement.stopPosition;
            }

            UILineRenderer.points[0] = dragElement.pivotRect.transform.position;
            UILineRenderer.points[1] = dragElement.transform.position;
        }
    }

}

