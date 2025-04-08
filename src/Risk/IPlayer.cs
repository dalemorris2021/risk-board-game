namespace Risk;

public interface IPlayer
{
    Task SendActions(IList<Action> actions);
    Task<Action?> ReceiveAction();
}
