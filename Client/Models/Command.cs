using Client.Enums;

namespace Client.Models;

class Command
{
    public HttpMethods Method { get; set; }
    public Car Car { get; set; }
}
