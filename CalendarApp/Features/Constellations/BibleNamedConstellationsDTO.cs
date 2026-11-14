namespace CalendarApp.Features.Constellations;

public sealed class BibleNamedConstellationsDTO
{
	public string CommonName { get; init; } = "";
	public string BiblicalName { get; init; } = "";
	public string Note { get; init; } = "";
	public string KeyVerses { get; init; } = "";

	public static IReadOnlyList<BibleNamedConstellationsDTO> All { get; } =
	[
		new()
		{
			CommonName = "Orion",
			BiblicalName = "Kesil (כְּסִיל)",
			Note = "“Fool / insolent one”",
			KeyVerses = "Job 9:9; Job 38:31; Amos 5:8; Isa 13:10",
		},
		new()
		{
			CommonName = "The Bear (Ursa Major)",
			BiblicalName = "Ash / Ayish (עָשׁ / עַיִשׁ)",
			Note = "With its “sons” (tail stars)",
			KeyVerses = "Job 9:9; Job 38:32",
		},
		new()
		{
			CommonName = "Zodiac / Constellations",
			BiblicalName = "Mazzaroth (מַזָּרוֹת)",
			Note = "The 12 signs",
			KeyVerses = "Job 38:32",
		},
		new()
		{
			CommonName = "Constellations / Planets",
			BiblicalName = "Mazzaloth (מַזָּלוֹת)",
			Note = "Related term",
			KeyVerses = "2 Kings 23:5",
		},
		new()
		{
			CommonName = "Southern constellations",
			BiblicalName = "Chambers of the south",
			Note = "—",
			KeyVerses = "Job 9:9",
		},
		new()
		{
			CommonName = "Draco (“crooked serpent”)",
			BiblicalName = "Nachash (נָחָשׁ)",
			Note = "Often linked to the serpent",
			KeyVerses = "Job 26:13",
		},
	];
}