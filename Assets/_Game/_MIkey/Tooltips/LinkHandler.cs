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
        _textBoxRectTransform = _tmpTextBox.GetComponent<RectTransform>();
        
        if(_canvasToCheck.renderMode == RenderMode.ScreenSpaceOverlay)
            _cameraToUse = null;
        else
        _cameraToUse = _canvasToCheck.worldCamera;
    }

    void Update()
    {
        CheckForLinkAtMousePosition();
    }

    private void CheckForLinkAtMousePosition()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        bool isIntersectingRectTransform = TMP_TextUtilities.IsIntersectingRectTransform(_textBoxRectTransform, mousePos, _cameraToUse);

        if (!isIntersectingRectTransform)
        {
            if(_currentLinkIndex != -1)
            {
                OnCloseTooltipEvent?.Invoke();
                _currentLinkIndex = -1;
            }
            return;
        }

        int intersectingLink = TMP_TextUtilities.FindIntersectingLink(_tmpTextBox, mousePos, _cameraToUse);

        if(_currentLinkIndex != intersectingLink)
        {
            OnCloseTooltipEvent?.Invoke();
        }

        if(intersectingLink == -1)
        {
            return;
        }

        TMP_LinkInfo linkInfo = _tmpTextBox.textInfo.linkInfo[intersectingLink];
        string linkId = linkInfo.GetLinkID();

        OnHoverOnLinkEvent?.Invoke(linkId, mousePos);
        _currentLinkIndex = intersectingLink;
    }
}
