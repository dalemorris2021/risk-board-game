namespace Risk;

public interface IPlayer
{
    void SendActions(IList<Action> actions);
    Action ReceiveAction();
}
