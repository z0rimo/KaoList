// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System.Text.Json;

namespace CodeRabbits.KaoList.Web.Services.Mananas;

public class MananaService
{
    public MananaService(HttpClient backChannel)
    {
        BackChannel = backChannel;
    }

    public HttpClient BackChannel { get; }

    public async Task<IEnumerable<MananaSong>> GetSongsByNoAsync(int no, MananaBrand brand)
    {
        var options = new JsonSerializerOptions
        {
            Converters = { new DateTimeJsonConverter() },
            PropertyNameCaseInsensitive = true,
        };

        var brandName = brand.ToString();
        var url = $"https://api.manana.kr/karaoke/no/{no}/{brandName.ToLower()}.json";

        var response = await BackChannel.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error fetching data: {response.StatusCode}");
            return Enumerable.Empty<MananaSong>();
        }

        var songs = await response.Content.ReadFromJsonAsync<IEnumerable<MananaSong>>(options);
        return songs ?? Enumerable.Empty<MananaSong>();
    }
}
