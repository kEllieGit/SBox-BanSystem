using Sandbox;
using System.Linq;
using System.Collections.Generic;

namespace BanSystem;

/// <summary>
/// Represents a list of bans.
/// </summary>
public class BanList : List<Ban>
{
	public Ban Ban( Connection channel, string reason = "No Reason" )
	{
		Ban ban = new()
		{
			SteamId = (long)channel.SteamId,
			Reason = reason
		};

		Add( ban );
		return ban;
	}

	public Ban Ban( ulong steamId, string reason = "No Reason" )
	{
		Ban ban = new()
		{
			SteamId = (long)steamId,
			Reason = reason
		};

		Add( ban );
		return ban;
	}

	public Ban GetBan( Connection connection )
	{
		return this.FirstOrDefault( x => x.SteamId == (long)connection.SteamId );
	}

	public Ban GetBan( long steamId )
	{
		return this.FirstOrDefault( x => x.SteamId == steamId );
	}

	public string GetBanReason( Connection connection )
	{
		return GetBan( connection ).Reason;
	}

	public string GetBanReason( long steamId )
	{
		return GetBan( steamId ).Reason;
	}

	public bool IsBanned( Connection channel )
	{
		return this.Any( x => x.SteamId == (long)channel.SteamId );
	}
}
