using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;
using Ink.Parsed;
using Unity.VisualScripting;

public class TooltipHandler : MonoBehaviour
{
    [Header("Data (Markdown)")]
    [Tooltip("Markdown file containing tooltip entries. Each entry starts with a heading, e.g. '## Keyword' followed by description lines.")]
    [SerializeField] private TextAsset _tooltipMarkdownFile;
    [SerializeField] private bool _parseOnAwake = true;

    [Header("UI")]
    [SerializeField] private GameObject _tooltipPrefab;
    [SerializeField] private Vector2 _screenOffset = new Vector2(0, 150);

    private List<TooltipInfo> _tooltipContentList = new List<TooltipInfo>();
    private TMP_Text _tooltipDescriptionTMP;
    private RectTransform _tooltipRect;
    private Canvas _canvas;

    private void Awake()
    {
        if (_tooltipPrefab == null)
        {
            Debug.LogError("Tooltip prefab not assigned.");
            return;
        }

        _tooltipDescriptionTMP = _tooltipPrefab.GetComponentInChildren<TMP_Text>();
        _tooltipRect = _tooltipPrefab.GetComponent<RectTransform>();
        _canvas = _tooltipPrefab.GetComponentInParent<Canvas>();

        if (_canvas == null)
            Debug.LogWarning("Tooltip prefab is not inside a Canvas. Falling back to world positioning.");

        if (_parseOnAwake && _tooltipMarkdownFile != null)
        {
            _tooltipContentList = MarkdownParser.Parse(_tooltipMarkdownFile);
        }

        UpdateText(null); // initial tagging
    }

    private void OnEnable()
    {
        LinkHandler.OnHoverOnLinkEvent += GetToolTipInfo;
        LinkHandler.OnCloseTooltipEvent += CloseTooltip;
    }

    private void OnDisable()
    {
        LinkHandler.OnHoverOnLinkEvent -= GetToolTipInfo;
        LinkHandler.OnCloseTooltipEvent -= CloseTooltip;
    }

    // Call this if you change the markdown at runtime or want to re-parse.
    public void SetMarkdown(TextAsset md)
    {
        _tooltipMarkdownFile = md;
        _tooltipContentList = ParseMarkdown(md);
    }

    private List<TooltipInfo> ParseMarkdown(TextAsset md)
    {
        var list = new List<TooltipInfo>();
        if (md == null || string.IsNullOrWhiteSpace(md.text)) return list;

        var lines = md.text.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.None);
        var headingRx = new Regex(@"^\s*#{1,6}\s*(.+)$"); // matches headings like "# Title".."###### Title"

        string currentHeading = null;
        var sb = new StringBuilder();

        void EmitCurrent()
        {
            if (string.IsNullOrWhiteSpace(currentHeading)) return;
            var desc = sb.ToString().Trim();
            if (string.IsNullOrEmpty(desc)) desc = "(no description)";
            // Support multiple keywords in a heading separated by '|' or ','
            var keywords = currentHeading.Split(new[] { '|', ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var k in keywords)
            {
                var entry = new TooltipInfo { keyword = k.Trim(), description = desc };
                list.Add(entry);
            }
            sb.Clear();
        }

        foreach (var raw in lines)
        {
            var m = headingRx.Match(raw);
            if (m.Success)
            {
                // new heading -> emit previous
                EmitCurrent();
                currentHeading = m.Groups[1].Value.Trim();
            }
            else
            {
                sb.AppendLine(raw);
            }
        }
        EmitCurrent(); // flush last

        return list;
    }

    private void GetToolTipInfo(string keyword, Vector3 mousePosition)
    {
        foreach (var entry in _tooltipContentList)
        {
            if (entry.keyword == keyword)
            {
                if (!_tooltipPrefab.activeInHierarchy)
                {
                    if (_canvas != null && _tooltipRect != null)
                    {
                        Vector2 localPoint;
                        Camera cam = _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera;
                        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                            _canvas.transform as RectTransform,
                            mousePosition,
                            cam,
                            out localPoint
                        );
                        _tooltipRect.anchoredPosition = localPoint + _screenOffset;
                    }
                    else
                    {
                        _tooltipPrefab.transform.position = mousePosition + new Vector3(0, 400, 0);
                    }

                    _tooltipPrefab.SetActive(true);
                }

                if (_tooltipDescriptionTMP != null)
                    _tooltipDescriptionTMP.text = entry.description;

                return;
            }
        }

        Debug.Log("Keyword Not Found: " + keyword);
    }

    public void CloseTooltip()
    {
        if (_tooltipPrefab != null && _tooltipPrefab.activeInHierarchy)
        {
            _tooltipPrefab.SetActive(false);
        }
    }

    public void UpdateText(string newText)
    {
        // MarkdownTagger is a utility to wrap keywords in the text with <link> tags for interactivity.
        TMP_Text text = GetComponentInChildren<TMP_Text>();
    
        if(newText == null) newText = text.text; // if null, re-tag existing text (useful if tooltip content list changed)

        var tagged = MarkdownTagger.TagText(newText, _tooltipContentList);
        text.text = tagged;
        text.ForceMeshUpdate();
    }
}
