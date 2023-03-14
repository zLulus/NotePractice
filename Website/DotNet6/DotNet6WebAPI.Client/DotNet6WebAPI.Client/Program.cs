using Newtonsoft.Json;
using TestClientNamespace;

HttpClient httpClient = new HttpClient();
TestClient client = new TestClient("http://localhost:5137/", httpClient);
var enums = await client.GetEnumAsync();
Console.WriteLine(JsonConvert.SerializeObject(enums));

Console.ReadLine();