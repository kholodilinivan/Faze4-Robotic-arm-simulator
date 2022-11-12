using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipDragElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public RectTransform thisRect;
    public RectTransform pivotRect;

    [SerializeField]
    private RectTransform tooltipShower;

    private Canvas canvas;
    private RectTransform canvasRect;
    
    private Vector3 delta;
    public Vector3 stopPosition;
    public bool state = true;



    public void ChangeSize(float value)
    {
        transform.localScale = new Vector3(value,value,value);
    }

    internal void Init(Canvas canvas, RectTransform canvasRect, float size)
    {
        this.canvas = canvas;
        this.canvasRect = canvasRect;
        ChangeSize(size);
    }

    public void SetPosition(bool state)
    {
        this.state = state;
        if(!state)
        {
            stopPosition = thisRect.position;
        }
    }

    #region IBeginDragHandler implementation
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (state)
        {
            tooltipShower.transform.SetAsLastSibling();
            delta = transform.position - (Vector3)eventData.position;
        }
    }
    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData)
    {
        if (state)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out pos);
            transform.position = canvas.transform.TransformPoint(pos) + delta;
        }
    }

    #endregion

    #region IEndDragHandler implementation
    public void OnEndDrag(PointerEventData eventData)
    {
        if (state)
        {
            Vector3[] v = new Vector3[4];
            thisRect.GetWorldCorners(v);

            var deltaXY = new Vector3();

            if (v[1].x < 0)
            {
                deltaXY.x += -v[1].x;
            }
            else if (v[3].x > Screen.width)
            {
                deltaXY.x += Screen.width - v[3].x;
            }

            if (v[3].y < 0)
            {
                deltaXY.y += -v[3].y;
            }
            else if (v[1].y > Screen.height)
            {
                deltaXY.y += Screen.height - v[1].y;
            }

            transform.position += deltaXY;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        tooltipShower.transform.SetAsLastSibling();
    }

    #endregion
}
