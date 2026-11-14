namespace CalendarApp.Features.Constellations;

public sealed class DecanDTO
{
	public string Sign { get; init; } = "";
	public int DecanOrder { get; init; }
	public string Name { get; init; } = "";
	public string Note { get; init; } = "";

	public ConstellationDTO? Constellation =>
		ConstellationDTO.All.FirstOrDefault(item => item.Sign == Sign);

	public static IReadOnlyList<DecanDTO> All { get; } =
	[
		new() { Sign = "Virgo", DecanOrder = 1, Name = "Coma", Note = "Desired/Infant" },
		new() { Sign = "Virgo", DecanOrder = 2, Name = "Centaurus" },
		new() { Sign = "Virgo", DecanOrder = 3, Name = "Boötes", Note = "the coming shepherd" },
		new() { Sign = "Libra", DecanOrder = 1, Name = "Crux", Note = "Southern Cross" },
		new() { Sign = "Libra", DecanOrder = 2, Name = "Lupus / Victima" },
		new() { Sign = "Libra", DecanOrder = 3, Name = "Corona" },
		new() { Sign = "Scorpio", DecanOrder = 1, Name = "Serpens" },
		new() { Sign = "Scorpio", DecanOrder = 2, Name = "Ophiuchus" },
		new() { Sign = "Scorpio", DecanOrder = 3, Name = "Hercules" },
		new() { Sign = "Sagittarius", DecanOrder = 1, Name = "Lyra" },
		new() { Sign = "Sagittarius", DecanOrder = 2, Name = "Ara" },
		new() { Sign = "Sagittarius", DecanOrder = 3, Name = "Draco" },
		new() { Sign = "Capricorn", DecanOrder = 1, Name = "Sagitta" },
		new() { Sign = "Capricorn", DecanOrder = 2, Name = "Aquila" },
		new() { Sign = "Capricorn", DecanOrder = 3, Name = "Delphinus" },
		new() { Sign = "Aquarius", DecanOrder = 1, Name = "Piscis Australis" },
		new() { Sign = "Aquarius", DecanOrder = 2, Name = "Pegasus" },
		new() { Sign = "Aquarius", DecanOrder = 3, Name = "Cygnus" },
		new() { Sign = "Pisces", DecanOrder = 1, Name = "The Band" },
		new() { Sign = "Pisces", DecanOrder = 2, Name = "Cepheus" },
		new() { Sign = "Pisces", DecanOrder = 3, Name = "Andromeda" },
		new() { Sign = "Aries", DecanOrder = 1, Name = "Cassiopeia" },
		new() { Sign = "Aries", DecanOrder = 2, Name = "Cetus" },
		new() { Sign = "Aries", DecanOrder = 3, Name = "Perseus" },
		new() { Sign = "Taurus", DecanOrder = 1, Name = "Orion", Note = "the Coming of the Branch; Betelgeuse, Rigel (\"the foot that crushes\"), Bellatrix" },
		new() { Sign = "Taurus", DecanOrder = 2, Name = "Eridanus" },
		new() { Sign = "Taurus", DecanOrder = 3, Name = "Auriga" },
		new() { Sign = "Gemini", DecanOrder = 1, Name = "Lepus" },
		new() { Sign = "Gemini", DecanOrder = 2, Name = "Sirius", Note = "Canis Major" },
		new() { Sign = "Gemini", DecanOrder = 3, Name = "Procyon" },
		new() { Sign = "Cancer", DecanOrder = 1, Name = "Ursa Major" },
		new() { Sign = "Cancer", DecanOrder = 2, Name = "Ursa Minor" },
		new() { Sign = "Cancer", DecanOrder = 3, Name = "Argo" },
		new() { Sign = "Leo", DecanOrder = 1, Name = "Hydra" },
		new() { Sign = "Leo", DecanOrder = 2, Name = "Crater" },
		new() { Sign = "Leo", DecanOrder = 3, Name = "Corvus" },
	];
}
