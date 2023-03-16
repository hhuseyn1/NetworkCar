using Server.Enums;

namespace Server.Models;

class Command
{
    public HttpMethods Method { get; set; }
    public Car? Car { get; set; }
}
