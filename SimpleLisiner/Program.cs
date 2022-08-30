using System.Net;
using System.Text;

var isWork = true;

using var listener = new HttpListener
{
    Prefixes = { "http://192.168.1.61:1111/" }
};

listener.Start();

while (isWork)
{
    var request = listener.GetContext().Request;
    var text = request.Url!.Query;
   
    isWork = Check(text);
    
    Console.WriteLine($"Received - {text}");

    LogToFile(text);
}

static void LogToFile(string text)
{
    using var streamWriter = new StreamWriter("/mnt/sda1/Logs/Text.txt", true, Encoding.UTF8);
    streamWriter.WriteLine(text);
}

static bool Check(string text)
{
    return text != "?stop";
}