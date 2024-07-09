using System.Net;
using System.Text;

public abstract class ListenerBase
{
    private readonly string listeningEndPoint;
    protected HttpListener httpListener;
    public bool Started { get; private set; }

    private HttpListenerContext Context { get; set; }

    public ListenerBase(string listeningEndPoint)
    {
        this.listeningEndPoint = listeningEndPoint;
    }

    protected abstract void WriteLog(string msg);

    public void Start()
    {
        httpListener = new HttpListener();
        httpListener.Prefixes.Add(listeningEndPoint);
        Console.WriteLine($"{this.GetType().Name} start at ip: {listeningEndPoint}");
        httpListener.Start();
        Started = true;
        while (httpListener.IsListening)
        {
            try
            {
                Context = httpListener.GetContext();
                var query = Context.Request.RawUrl!.Trim('/');

                var domain = Context.Request.Headers.Get("toll-proxy-domain");
                
                //todo: make request to domain
                
                //todo: write response back

                WriteLog($"{Context.Request.Url} url was called.");
            }
            catch (Exception ex)
            {
                Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                WriteToResponse($"{ex.Message}\r\n{ex.StackTrace}");
            }
        }
    }

    protected void WriteToResponse(byte[] data)
    {
        Context.Response.ContentLength64 = data.Length;
        Context.Response.OutputStream.Write(data, 0, data.Length);
    }

    protected void WriteToResponse(string str)
    {
        var data = Encoding.UTF8.GetBytes(str);
        Context.Response.ContentLength64 = data.Length;
        Context.Response.OutputStream.Write(data, 0, data.Length);
    }

    public void Stop()
    {
        httpListener.Stop();
    }

    protected string GetParam(string paramName)
    {
        return Context.Request.GetParam(paramName);
    }
}