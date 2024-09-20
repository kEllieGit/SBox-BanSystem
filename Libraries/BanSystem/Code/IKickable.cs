namespace BanSystem;

public interface IKickable
{
	/// <summary>
	/// Kick the owning connection from the game.
	/// </summary>
	/// <param name="reason">Reason for the kick</param>
	void Kick( string reason = "No Reason" );

	/// <summary>
	/// Ban the owning connection from the game.
	/// </summary>
	/// <param name="reason">Reason for the ban</param>
	/// <param name="type">Type of the ban</param>
	void Ban( string reason = "No Reason", BanType type = BanType.Server );
}
