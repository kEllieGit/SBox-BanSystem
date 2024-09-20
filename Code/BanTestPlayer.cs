using Sandbox;
using BanSystem;

namespace Testing;

/// <summary>
/// A testing player to test kicking/banning.
/// </summary>
public sealed class BanTestPlayer : Component, IKickable
{
	protected override void OnStart()
	{
		BanManager.ConnectionAttempt( this );
		base.OnStart();
	}

	[Authority( NetPermission.HostOnly )]
	public void Kick( string reason = "No Reason" )
	{
		Game.Close();
		Log.Warning( $"You have been removed from the game! Reason: {reason}" );
	}

	[Authority( NetPermission.HostOnly )]
	public void Ban( string reason = "No Reason", BanType type = BanType.Server )
	{
		switch ( type )
		{
			case BanType.Server:
				BanManager.Ban( Network.Owner, reason );
				break;
			case BanType.Game:
				GameBanManager.Ban( Network.Owner, reason );
				break;
		}

		Kick( reason );
	}
}
