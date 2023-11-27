// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web.Services.YouTubes
{
    public class YouTubeService
    {
        private readonly HttpClient _client;

        public YouTubeService(HttpClient client)
        {
            _client = client;
        }

        public async Task UpdateInstSoundIdAsync()
        {

        }

        /*private async Task<IEnumerable<string>> GetPopularSingIdListAsync()
        {
       

        }*/
    }
}
