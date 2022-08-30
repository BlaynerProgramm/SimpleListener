using System.Net;
using System.Text;

const string STOPWORD = "stop";
const string URL = "https://192.168.1.61:1111/";

var isWork = true;

using var listener = new HttpListener
{
    Prefixes = { URL }
};

listener.Start();

Console.WriteLine($"Listener start work - {URL}");

while (isWork)
{
    var request = listener.GetContext().Request;
    var text = request.Url!.Query;
   
    isWork = !IsStopWord(text);
    
    Console.WriteLine($"Received - {text}");

    LogToFile(text);
}

static void LogToFile(string text)
{
    using var streamWriter = new StreamWriter("/mnt/sda1/Logs/Text.txt", true, Encoding.UTF8);
    streamWriter.WriteLine(text);
}

static bool IsStopWord(string text)
{
    return text == $"?{STOPWORD}";
}