using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Models;
using System.Net;
using System.Net.Sockets;


var ip = IPAddress.Parse("127.0.0.1");
var port = 45678;

var listener = new TcpListener(ip, port);

listener.Start(1);

var dirInfo = new DirectoryInfo(@"..\..\..\Data");
string jsonData;
JArray jsonArray = new();
foreach (var file in dirInfo.GetFiles())
{
    if (file.Extension == ".json")
    {
        jsonData = File.ReadAllText(file.FullName);
        jsonArray = JArray.Parse(jsonData);
    }
}

while (true)
{
    var client = await listener.AcceptTcpClientAsync();
    var clientStream = client.GetStream();
    var br = new BinaryReader(clientStream);
    var bw = new BinaryWriter(clientStream);
    Console.WriteLine($"{client.Client.RemoteEndPoint} are connected...");
    await Task.Run(async () =>
    {
        while (true)
        {
            var text = br.ReadString();
            var text2 = br.ReadString();
            var car = ConvertToCar(text2);
            int id = car.Id;
            await Console.Out.WriteLineAsync(id.ToString());
            switch (text)
            {
                case "Get":
                    if (id != 0)
                        GetByIdAsync(id);
                    else if (id == 0)
                        GetAll();
                    break;
                case "Remove":
                    Delete(id);
                    break;
                case "Put":
                    Update(car);
                    break;
                case "Post":
                    Add(car);
                    break;
                default:
                    break;
            }
        }
    });

}

Car ConvertToCar(string text)
{
    Car car = new();
    car.Id = int.Parse(text.Split(' ')[0]);
    car.Make = text.Split(' ')[1];
    car.Model = text.Split(' ')[2];
    car.Year = ushort.Parse(text.Split(' ')[3]);
    car.VIN = text.Split(' ')[4];
    car.Color = text.Split(' ')[5];
    return car;
}

async Task<Car?> GetByIdAsync(int Id)
{
    Car? car = new();
    string jsonData = await File.ReadAllTextAsync("CarData.json");

    JObject jsonObject = JObject.Parse(jsonData);

    if (Id != 0)
    {
        foreach (JObject item in jsonArray)
        {
            if (Id == (int)item["Id"])
            {
                car = item.ToObject<Car>();
            }
        }
    }
    else
        Console.WriteLine("Something get wrong id is not found!");
    return car;
}

async Task<List<Car>> GetAll()
{
    List<Car> Cars = new();
    Car? car = new();
    string jsonData = await File.ReadAllTextAsync("CarData.json");

    JObject jsonObject = JObject.Parse(jsonData);

    foreach (JObject item in jsonArray)
    {
        car = item.ToObject<Car>();
        Cars.Add(car);
    }
    return Cars;
}

async Task<bool> Delete(int Id)
{
    bool result;
    string jsonData = await File.ReadAllTextAsync("CarData.json");

    List<Car?> Cars = JsonConvert.DeserializeObject<List<Car?>>(jsonData);

    Car? objectToRemove = Cars.FirstOrDefault(c => c.Id == Id);
    result = Cars.Remove(objectToRemove);


    string updatedJson = JsonConvert.SerializeObject(Cars, Formatting.Indented);

    File.WriteAllText("CarData.json", updatedJson);
    return result;
}

async Task<bool> Add(Car car)
{
    try
    {
        string jsonData = await File.ReadAllTextAsync("CarData.json");
        List<Car?> Cars = JsonConvert.DeserializeObject<List<Car?>>(jsonData);
        Cars.Add(car);
        string updatedJson = JsonConvert.SerializeObject(Cars, Formatting.Indented);
        File.WriteAllText("CarData.json", updatedJson);
    }
    catch (Exception ex)
    {
        return false;
    }
    return true;
}


async Task<bool> Update(Car car)
{
    try
    {
        string jsonData = await File.ReadAllTextAsync("CarData.json");
        List<Car?> Cars = JsonConvert.DeserializeObject<List<Car?>>(jsonData);
        Car? objectToUpdate = Cars.FirstOrDefault(c => c.Id == car.Id);
        if (objectToUpdate != null)
        {
            objectToUpdate.Make = car.Make;
            objectToUpdate.Model = car.Model;
            objectToUpdate.VIN = car.VIN;
            objectToUpdate.Year = car.Year;
            objectToUpdate.Color = car.Color;
        }

        string updatedJson = JsonConvert.SerializeObject(Cars, Formatting.Indented);

        File.WriteAllText("CarData.json", updatedJson);
    }
    catch (Exception ex)
    {
        return false;
    }
    return true;
}