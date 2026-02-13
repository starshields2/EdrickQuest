using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class MarkdownTagger
{
    // Wrap matches with <link="keyword"> and optional <color=...>/<b>
    public static string TagText(string input, IEnumerable<TooltipInfo> entries)
    {
        if (string.IsNullOrEmpty(input)) return input;
        var list = entries?.ToList() ?? new List<TooltipInfo>();
        if (list.Count == 0) return input;

        // map by lowercase keyword for lookup
        var map = list.ToDictionary(e => e.keyword.ToLowerInvariant(), e => e);

        // build regex from escaped keywords, longest-first to avoid partial matches
        var escaped = list.Select(e => Regex.Escape(e.keyword)).OrderByDescending(s => s.Length).ToArray();
        if (escaped.Length == 0) return input;

        string pattern = $"(?<!\\w)({string.Join("|", escaped)})(?!\\w)";
        var rx = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        string Evaluator(Match m)
        {
            var matched = m.Value;
            var keyLower = matched.ToLowerInvariant();

            if (!map.TryGetValue(keyLower, out var entry))
            {
                // fallback: try find by case-insensitive match (in case map keys differ slightly)
                entry = map.Values.FirstOrDefault(e => string.Equals(e.keyword, matched, StringComparison.OrdinalIgnoreCase));
                if (entry.keyword == null) return matched;
            }

            string content = matched; // keep original casing
            if (!string.IsNullOrEmpty(entry.color))
                content = $"<color={entry.color}>{content}</color>";
            if (entry.bold)
                content = $"<b>{content}</b>";
            if (entry.italic)
                content = $"<i>{content}</i>";

            // note: link id uses the exact keyword stored in entry.keyword
            return $"<link=\"{entry.keyword}\">{content}</link>";
        }

        return rx.Replace(input, new MatchEvaluator(Evaluator));
    }
}