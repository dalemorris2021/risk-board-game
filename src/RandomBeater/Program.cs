namespace Risk;

public class RandomBeater : IPlayer
{
    public Action ReceiveAction()
    {
        return Action.END;
    }

    public void SendActions(IList<Action> actions)
    {

    }
}
