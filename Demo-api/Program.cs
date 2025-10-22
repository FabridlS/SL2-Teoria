// See https://aka.ms/new-console-template for more information
Console.WriteLine("Vamos a hacer una consulta a la API");

string url = "https://dog.ceo/api/breed/mestizo/images/random";

HttpClient client = new HttpClient();

try
{
    HttpResponseMessage response = await client.GetAsync(url);
    if (response.IsSuccessStatusCode)
    {
        string responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseBody);
    }
    else
    {
        Console.WriteLine($"Error: {response.StatusCode}");
        // Console.WriteLine($"Error desc: {response}");
    }
    
}
catch (HttpRequestException e)
{
    Console.WriteLine($"Request error: {e.Message}");    
}