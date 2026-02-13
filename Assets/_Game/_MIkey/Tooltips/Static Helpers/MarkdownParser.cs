using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public static class MarkdownParser
{
    // Parse headings into TooltipInfo list.
    // Heading syntax: any level #..###### followed by text, optional metadata in braces:
    // ## keyword {color:#ff0000 bold}
    private static readonly Regex HeadingRx = new Regex(@"^\s*#{1,6}\s*(.+?)(?:\s*\{\s*(.+?)\s*\}\s*)?$", RegexOptions.Compiled);

    public static List<TooltipInfo> Parse(TextAsset md)
    {
        var list = new List<TooltipInfo>();
        if (md == null || string.IsNullOrWhiteSpace(md.text)) return list;

        var lines = md.text.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.None);
        string currentHeading = null;
        string currentMeta = null;
        var sb = new StringBuilder();

        void EmitCurrent()
        {
            if (string.IsNullOrWhiteSpace(currentHeading)) return;
            var desc = sb.ToString().Trim();
            if (string.IsNullOrEmpty(desc)) desc = "(no description)";

            // support multiple keywords separated by '|' or ','
            var keywords = currentHeading.Split(new[] { '|', ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var rawKey in keywords)
            {
                var key = rawKey.Trim();
                var item = new TooltipInfo
                {
                    keyword = key,
                    description = desc,
                    color = null,
                    bold = false
                };

                // parse metadata if present (currentMeta)
                if (!string.IsNullOrWhiteSpace(currentMeta))
                {
                    var parts = currentMeta.Split(new[] { ',', ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                    foreach (var p in parts)
                    {
                        var kv = p.Split(new[] { ':' }, 2);
                        var k = kv[0].Trim().ToLowerInvariant();
                        var v = kv.Length > 1 ? kv[1].Trim() : null;
                        if (k == "color" && !string.IsNullOrEmpty(v)) item.color = v;
                        if (k == "bold") item.bold = true;
                        if (k == "italic") item.italic = true;
                    }
                }

                list.Add(item);
            }

            sb.Clear();
            currentMeta = null;
        }

        foreach (var raw in lines)
        {
            var m = HeadingRx.Match(raw);
            if (m.Success)
            {
                EmitCurrent();
                currentHeading = m.Groups[1].Value.Trim();
                currentMeta = m.Groups.Count >= 3 ? m.Groups[2].Value?.Trim() : null;
            }
            else
            {
                sb.AppendLine(raw);
            }
        }
        EmitCurrent();
        return list;
    }
}