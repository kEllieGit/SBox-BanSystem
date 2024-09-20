using Sandbox;
using Sandbox.Diagnostics;

namespace BanSystem;

[Title( "Ban Manager" )]
[Category( "Ban System" )]
public sealed class BanManager : Component
{
	public static BanManager Instance { get; private set; }

	/// <summary>
	/// Represents server bans.
	/// For game bans, use <seealso cref="GameBanManager.Bans"/>
	/// </summary>
	public BanList Bans { get; set; } = new();

	internal static Logger Log { get; } = new( "Ban System" );

	protected override void OnStart()
	{
		Instance = this;
		base.OnStart();
	}

	/// <summary>
	/// A player tries to play the game. Check if they're allowed to.
	/// </summary>
	public static ConnectionAttemptResult ConnectionAttempt( Component kickableComponent )
	{
		if ( kickableComponent is not IKickable )
		{
			Log.Warning( "Provided component is not kickable!" );
			return ConnectionAttemptResult.Success;
		}

		var result = ConnectionAttemptResult.Success;
		var owner = kickableComponent.Network.Owner;
		var kickable = kickableComponent as IKickable;

		bool isServerBanned = Instance.Bans.IsBanned( owner );
		bool isGameBanned = GameBanManager.Instance.Bans.IsBanned( owner );

		if ( isGameBanned )
		{
			var banReason = GameBanManager.Instance.Bans.GetBanReason( owner );
			kickable?.Kick( banReason );

			Log.Info( $"Game banned player '{owner.DisplayName}' tried connecting! Ban reason: {banReason}" );
			return ConnectionAttemptResult.Banned;
		}

		if ( isServerBanned )
		{
			var banReason = Instance.Bans.GetBanReason( owner );
			kickable?.Kick( banReason );

			Log.Info( $"Server banned player '{owner.DisplayName}' tried connecting! Ban reason: {banReason}" );
			return ConnectionAttemptResult.Banned;
		}

		return result;
	}

	public static Ban Ban( Connection channel, string reason )
	{
		return Instance.Bans.Ban( channel, reason );
	}

	public enum ConnectionAttemptResult
	{
		Success,
		Failure,
		Banned
	}
}

public enum BanType
{
	Server,
	Game,
}
