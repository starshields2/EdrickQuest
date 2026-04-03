using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LinkHandler : MonoBehaviour
{
    private TMP_Text _tmpTextBox;
    private Canvas _canvasToCheck;
    private Camera _cameraToUse;
    private RectTransform _textBoxRectTransform;
    private int _currentLinkIndex = -1;

    public delegate void HoverOnLinkEvent(string keyword, Vector3 mousePosition);
    public static event HoverOnLinkEvent OnHoverOnLinkEvent;
    public delegate void CloseTooltipEvent();
    public static event CloseTooltipEvent OnCloseTooltipEvent;

    void Awake()
    {
        _tmpTextBox = GetComponent<TMP_Text>();
        _canvasToCheck = GetComponentInParent<Canvas>();
        _textBoxRectTransform = GetComponent<RectTransform>();
        
        if(_canvasToCheck.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            _cameraToUse = null;
        }
        else
        {
            _cameraToUse = _canvasToCheck.worldCamera;
        }
    }

    void Update()
    {
        CheckForLinkAtMousePosition();
    }

    private void CheckForLinkAtMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;

        bool isIntersectingRectTransform = TMP_TextUtilities.IsIntersectingRectTransform(_textBoxRectTransform, mousePos, _cameraToUse);
        
        int intersectingLink = isIntersectingRectTransform 
            ? TMP_TextUtilities.FindIntersectingLink(_tmpTextBox, mousePos, _cameraToUse) 
            : -1;

        if (intersectingLink != _currentLinkIndex)
        {
            if (_currentLinkIndex != -1)
            {
                OnCloseTooltipEvent?.Invoke();
            }

            if (intersectingLink != -1)
            {
                TMP_LinkInfo linkInfo = _tmpTextBox.textInfo.linkInfo[intersectingLink];
                OnHoverOnLinkEvent?.Invoke(linkInfo.GetLinkID(), mousePos);
            }

            _currentLinkIndex = intersectingLink;
        }
    }
}