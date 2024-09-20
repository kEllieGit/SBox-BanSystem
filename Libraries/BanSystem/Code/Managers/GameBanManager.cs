using Sandbox;

namespace BanSystem;

public sealed class GameBanManager
{
	public static GameBanManager Instance
	{
		get
		{
			_instance ??= FileSystem.Data.ReadJson( FileName, new GameBanManager() );
			return _instance;
		}
	}
	private static GameBanManager _instance;

	public const string FileName = "GameBans.json";

	/// <summary>
	/// Represents game bans.
	/// For server bans, use <seealso cref="BanManager.Bans"/>
	/// </summary>
	public BanList Bans { get; set; } = new();

	/// <summary>
	/// Game ban the given connection.
	/// </summary>
	/// <param name="channel">Connection to ban</param>
	/// <param name="reason">Reason for the ban</param>
	public static Ban Ban( Connection channel, string reason = "No Reason" )
	{
		var ban = Instance.Bans.Ban( channel, reason );
		FileSystem.Data.WriteJson( FileName, Instance );
		return ban;
	}
}
