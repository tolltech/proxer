namespace TollProxer;

public class ProxyListener : ListenerBase
{
    public ProxyListener(string listeningEndPoint) : base(listeningEndPoint)
    {
    }

    protected override void WriteLog(string msg)
    {
        Console.WriteLine(msg);
    }
}