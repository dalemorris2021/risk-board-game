namespace Risk;

public class NeutralBot : IPlayer
{
    public async Task<Action?> ReceiveAction()
    {
        return await Task.FromResult(Action.END);
    }

    public Task SendActions(IList<Action> actions)
    {
        return Task.FromResult(0);
    }
}
