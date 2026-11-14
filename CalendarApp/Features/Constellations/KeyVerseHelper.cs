using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;

namespace CalendarApp.Features.Constellations;

public static class KeyVerseHelper
{
	private static readonly Regex ReferencePattern = new(
		@"^(?<book>.+?)\s+(?<chapter>\d+)(?::(?<verse>\d+))?$",
		RegexOptions.Compiled | RegexOptions.CultureInvariant);

	private static readonly Dictionary<string, string> Books = new(StringComparer.OrdinalIgnoreCase)
	{
		["Genesis"] = "gen",
		["Gen"] = "gen",
		["Revelation"] = "rev",
		["Rev"] = "rev",
		["Job"] = "job",
		["Amos"] = "amo",
		["Isaiah"] = "isa",
		["Isa"] = "isa",
		["2 Kings"] = "2ki",
	};

	public static MarkupString GetAnchors(BibleNamedConstellationsDTO item) =>
		GetAnchors(item.KeyVerses);

	public static MarkupString GetAnchors(string? keyVerses)
	{
		if (string.IsNullOrWhiteSpace(keyVerses))
		{
			return (MarkupString)"";
		}

		var anchors = keyVerses
			.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			.Select(ToAnchor);

		return (MarkupString)string.Join("; ", anchors);
	}

	private static string ToAnchor(string reference)
	{
		var text = reference.Trim();
		var match = ReferencePattern.Match(text);
		if (!match.Success)
		{
			return WebUtility.HtmlEncode(text);
		}

		var book = Regex.Replace(match.Groups["book"].Value.Trim(), @"\s+", " ");
		if (!Books.TryGetValue(book, out var code))
		{
			return WebUtility.HtmlEncode(text);
		}

		var chapter = match.Groups["chapter"].Value;
		var verse = match.Groups["verse"].Success ? match.Groups["verse"].Value : "1";
		var href = $"https://www.blueletterbible.org/kjv/{code}/{chapter}/{verse}/";
		return $"""<a href="{href}" target="_blank">{WebUtility.HtmlEncode(text)}</a>""";
	}
}
