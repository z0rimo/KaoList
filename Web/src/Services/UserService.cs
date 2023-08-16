// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Song;
using Newtonsoft.Json;

namespace CodeRabbits.KaoList.Web.Services
{
    public class UserService
    {
        private readonly KaoListDataContext _context;

        public UserService(KaoListDataContext context)
        {
            _context = context;
        }

        public void ConvertAndSaveJsonToDb()
        {
            var jsonFilePath = "users.json";
            var jsonContent = File.ReadAllText(jsonFilePath);
            var users = JsonConvert.DeserializeObject<List<User>>(jsonContent);

            foreach (var user in users!)
            {
                var userinfo = new KaoListUser
                {
                    NickName = user.Name,
                    NormalizedNickName = user.Name.ToUpper(),
                    NickNameEditedDatetime = DateTime.UtcNow
                };

                _context.Users.Add(userinfo);
            }

            _context.SaveChanges();
        }
    }
}

public class User
{
    public required string Name { get; set; }
}
