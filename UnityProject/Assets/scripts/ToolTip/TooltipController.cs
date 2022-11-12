using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TooltipController : MonoBehaviour
{
    private static TooltipController instance;

    private List<TooltipShower> tooltips = new List<TooltipShower>();
    private static List<Action> lazyCreate = new List<Action>();

    public TooltipShower tooltipPanel;
    public Canvas canvas;
    public Camera cam;
    private RectTransform canvasRect;
    private float size = 1;

    private void Awake()
    {
        canvasRect = canvas.GetComponent<RectTransform>();
        instance = this;
        foreach (var l in lazyCreate)
        {
            l?.Invoke();
        }
        lazyCreate = null;
    }

    internal static void RemoveTooltip(TooltipInformation tooltip)
    {
        var a = instance.tooltips.FirstOrDefault(x => x.tooltip == tooltip);
        if (a == null)
            return;

        instance.tooltips.Remove(a);
        Destroy(a.gameObject);
    }

    internal static void CreateTooltip(TooltipInformation tooltip)
    {
        var a = Instantiate(instance.tooltipPanel, instance.canvas.transform);
        a.Init(tooltip, instance.cam, instance.canvas, instance.canvasRect, instance.size);
        instance.tooltips.Add(a);

        if (instance.tooltips.Count > 0)
        {
            if (instance.tooltips[0].gameObject.activeSelf)
            {
                a.gameObject.SetActive(true);
            }
        }
    }

    public static void AddTooltip(TooltipInformation tooltip)
    {
        if (instance != null)
        {
            CreateTooltip(tooltip);
        }
        else
        {
            lazyCreate.Add(() => AddTooltip(tooltip));
        }
    }

    public void ShowTooltips()
    {
        foreach (var a in tooltips)
        {
            a.gameObject.SetActive(true);
        }
    }

    public void HideTooltips()
    {
        foreach (var a in tooltips)
        {
            a.gameObject.SetActive(false);
        }
    }

    public void Toogle()
    {
        if (tooltips.Count > 0)
        {
            if (tooltips[0].gameObject.activeSelf)
            {
                HideTooltips();
            }
            else
            {
                ShowTooltips();
            }
        }
    }

    public void ChangeSize(float value)
    {
        size = value;
        foreach (var a in tooltips)
        {
            a.ChangeSize(size);
        }
    }

}
