using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InkLinkHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text textMeshPro;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMeshPro, eventData.position, null);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo[linkIndex];
            string linkID = linkInfo.GetLinkID();
            string linkText = linkInfo.GetLinkText();

            Debug.Log($"Clicked link: {linkID} ({linkText})");

            if (linkID == "whetstone")
            {
                Debug.Log("click whetstone");
            }
        }
    }
}
