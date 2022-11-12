using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipInformation : MonoBehaviour
{
    private void OnEnable()
    {
        TooltipController.AddTooltip(this);    
    }

    private void OnDisable()
    {
        TooltipController.RemoveTooltip(this);
    }
}
