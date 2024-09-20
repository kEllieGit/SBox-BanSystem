namespace BanSystem;

public record Ban
{
	public long SteamId { get; set; }

	public string Reason { get; set; }
}
