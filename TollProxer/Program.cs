
using TollProxer;

Console.WriteLine("Hello");

var proxyListener = new ProxyListener("http://localhost:8888/");

proxyListener.Start();

Console.WriteLine("Bye");