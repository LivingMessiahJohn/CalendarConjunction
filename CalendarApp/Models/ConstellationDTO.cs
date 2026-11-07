namespace CalendarApp.Models;

public sealed class ConstellationDTO
{
	public int? CircleOrder { get; init; }

	public int? MonthOrder { get; init; }

	public string Sign { get; init; } = "";

	/// <summary>
	/// Mazzaroth name. Gospel-in-the-Stars spelling for a sign, biblical Hebrew name for a constellation named in Scripture.
	/// </summary>
	public string Mazzarah { get; init; } = "";

	public string HebrewTransliteration { get; init; } = "";

	public string HebrewWord { get; init; } = "";

	public string Verses { get; init; } = "";

	public string Meaning { get; init; } = "";

	public string KeyIdea { get; init; } = "";

	public string MainStars { get; init; } = "";

	public static IReadOnlyList<ConstellationDTO> All { get; } =
	[
		new()
		{
			CircleOrder = 1,
			MonthOrder = 6,
			Sign = "Virgo",
			Mazzarah = "Bethulah",
			HebrewTransliteration = "Betulah",
			HebrewWord = "בתולה",
			Verses = "Gen 3:15",
			Meaning = "The promised Seed of the woman",
			KeyIdea = "Virgin holding the branch",
			MainStars = "Spica = \"the Branch\"",
		},
		new()
		{
			CircleOrder = 2,
			MonthOrder = 7,
			Sign = "Libra",
			Mazzarah = "Moznayim",
			HebrewTransliteration = "Moznayim",
			HebrewWord = "מאזנים",
			Meaning = "The price paid / the required sacrifice",
			KeyIdea = "The scales",
			MainStars = "Zubenelgenubi & Zubeneschamali (\"price which covers / is deficient\")",
		},
		new()
		{
			CircleOrder = 3,
			MonthOrder = 8,
			Sign = "Scorpio",
			Mazzarah = "Akrav",
			HebrewTransliteration = "Akrav",
			HebrewWord = "עקרב",
			Meaning = "The mortal conflict",
			KeyIdea = "The scorpion",
			MainStars = "Antares = \"the wounding\"",
		},
		new()
		{
			CircleOrder = 4,
			MonthOrder = 9,
			Sign = "Sagittarius",
			Mazzarah = "Keshet",
			HebrewTransliteration = "Keshet",
			HebrewWord = "קשת",
			Meaning = "The triumphant archer / the going forth",
			KeyIdea = "The archer aiming at the scorpion's heart",
		},
		new()
		{
			CircleOrder = 5,
			MonthOrder = 10,
			Sign = "Capricorn",
			Mazzarah = "Gedi",
			HebrewTransliteration = "Gedi",
			HebrewWord = "גדי",
			Meaning = "Life out of death / the sacrifice",
			KeyIdea = "The goat-fish (dying goat + living fish)",
			MainStars = "Algedi = \"the kid cut off\"",
		},
		new()
		{
			CircleOrder = 6,
			MonthOrder = 11,
			Sign = "Aquarius",
			Mazzarah = "Deli",
			HebrewTransliteration = "Deli / D'li",
			HebrewWord = "דלי",
			Meaning = "The outpoured blessing / the living water",
			KeyIdea = "The water-bearer pouring out",
			MainStars = "Sadalmelik, Sadalsuud",
		},
		new()
		{
			CircleOrder = 7,
			MonthOrder = 12,
			Sign = "Pisces",
			Mazzarah = "Dagim",
			HebrewTransliteration = "Dagim",
			HebrewWord = "דגים",
			Meaning = "The multiplied / the redeemed people",
			KeyIdea = "The two fish bound together",
		},
		new()
		{
			CircleOrder = 8,
			MonthOrder = 1,
			Sign = "Aries",
			Mazzarah = "Taleh",
			HebrewTransliteration = "Taleh / Ṭ'leh",
			HebrewWord = "טלה",
			Meaning = "The substitute Lamb / glory out of humiliation",
			KeyIdea = "The ram",
			MainStars = "Elnath, Sheratan (\"wounded, bruised\")",
		},
		new()
		{
			CircleOrder = 9,
			MonthOrder = 2,
			Sign = "Taurus",
			Mazzarah = "Shor",
			HebrewTransliteration = "Shor",
			HebrewWord = "שור",
			Meaning = "His glorious coming in judgment",
			KeyIdea = "The bull rushing forward",
			MainStars = "Aldebaran = \"the governor\"",
		},
		new()
		{
			CircleOrder = 10,
			MonthOrder = 3,
			Sign = "Gemini",
			Mazzarah = "Teomim",
			HebrewTransliteration = "Teomim",
			HebrewWord = "תאומים",
			Meaning = "The two natures united / His rule",
			KeyIdea = "The twins (one wounded, one ruling)",
		},
		new()
		{
			CircleOrder = 11,
			MonthOrder = 4,
			Sign = "Cancer",
			Mazzarah = "Sartan",
			HebrewTransliteration = "Sartan",
			HebrewWord = "סרטן",
			Meaning = "The sheepfold / His possessions held secure",
			KeyIdea = "The crab holding the multitude",
		},
		new()
		{
			CircleOrder = 12,
			MonthOrder = 5,
			Sign = "Leo",
			Mazzarah = "Aryeh",
			HebrewTransliteration = "Aryeh / Ari",
			HebrewWord = "אריה",
			Meaning = "The triumphant King / enemies destroyed",
			KeyIdea = "The lion",
			MainStars = "Regulus = \"the little king\"",
		},
		new()
		{
			Sign = "Pleiades",
			Mazzarah = "Kimah",
			HebrewTransliteration = "Kimah",
			HebrewWord = "כִּימָה",
			Verses = "Job 9:9; Job 38:31; Amos 5:8",
			Meaning = "H3598 — \"cluster / heap\"",
			KeyIdea = "Star cluster in Taurus",
		},
		new()
		{
			Sign = "Orion",
			Mazzarah = "Kesil",
			HebrewTransliteration = "Kesil",
			HebrewWord = "כְּסִיל",
			Verses = "Job 9:9; Job 38:31; Amos 5:8; Isa 13:10",
			Meaning = "H3685 — \"fool / insolent one\" (also used more generally for bright constellations in Isa 13:10)",
			KeyIdea = "The Coming of the Branch",
			MainStars = "Betelgeuse, Rigel (\"the foot that crushes\"), Bellatrix",
		},
		new()
		{
			Sign = "The Bear",
			Mazzarah = "Ash / Ayish",
			HebrewTransliteration = "Ash or Ayish",
			HebrewWord = "עָשׁ / עַיִשׁ",
			Verses = "Job 9:9; Job 38:32",
			Meaning = "H5906 — with its \"sons\" (the tail stars)",
			KeyIdea = "Ursa Major / Arcturus in older translations",
		},
		new()
		{
			Sign = "The Zodiac",
			Mazzarah = "Mazzaroth",
			HebrewTransliteration = "Mazzaroth",
			HebrewWord = "מַזָּרוֹת",
			Verses = "Job 38:32",
			Meaning = "H4216 — often understood as the 12 signs",
			KeyIdea = "Constellations in general",
		},
		new()
		{
			Sign = "Constellations / Planets",
			Mazzarah = "Mazzaloth",
			HebrewTransliteration = "Mazzaloth",
			HebrewWord = "מַזָּלוֹת",
			Verses = "2 Kings 23:5",
			Meaning = "H4208 — related term",
		},
		new()
		{
			Sign = "Southern constellations",
			Mazzarah = "Chambers of the south",
			HebrewTransliteration = "Chambers of the south",
			HebrewWord = "חַדְרֵי תֵמָן",
			Verses = "Job 9:9",
		},
		new()
		{
			Sign = "Draco",
			Mazzarah = "Nachash",
			HebrewTransliteration = "Nachash",
			HebrewWord = "נָחָשׁ",
			Verses = "Job 26:13; Genesis 3; Revelation 12",
			Meaning = "Often identified with the constellation Draco",
			KeyIdea = "The crooked serpent",
		},
	];
}
