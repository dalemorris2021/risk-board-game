namespace Risk;

public class NeutralBot : IPlayer
{
    public Action ReceiveAction()
    {
        return Action.END;
    }

    public void SendActions(IList<Action> actions)
    {

    }
}
