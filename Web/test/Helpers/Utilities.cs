// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeRabbits.KaoList.Data;
using CodeRabbits.KaoList.Identity;
using CodeRabbits.KaoList.Song;

namespace CodeRabbits.KaoList.Web.Test
{
    public static class Utilities
    {
        public static void InitializeDbForTests(KaoListDataContext db)
        {
            db.Instrumental.AddRange(GetInstrumentals());
            db.Users.AddRange(GetKaoListUsers());
            db.Sings.AddRange(GetSings());
            db.SingUsers.AddRange(GetSingUsers());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(KaoListDataContext db)
        {
            db.Instrumental.RemoveRange(db.Instrumental);
            db.Users.RemoveRange(db.Users);
            db.Sings.RemoveRange(db.Sings);
            db.SingUsers.RemoveRange(db.SingUsers);
            InitializeDbForTests(db);
        }

        public static List<KaoListUser> GetKaoListUsers() => new()
        {
            new KaoListUser
            {
                Id = "00004e53-a27c-423e-892c-e5a54f665cee",
                NickName = "木村世治(hurdy gurdy / Pale Green)",
                NormalizedNickName = "木村世治(HURDY GURDY / PALE GREEN)",
                Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 31, second: 27),
                UserName = "97b39e0e-a114-4482-bdf3-15ce7c959c96",
                NormalizedUserName = "97B39E0E-A114-4482-BDF3-15CE7C959C96",
                EmailConfirmed = false,
                SecurityStamp = "25592c5e-fff7-4194-93f9-ecb48fabe02a",
                ConcurrencyStamp = "7ec6487d-e2be-43ed-9a28-fadeb90c48aa",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
            },
            new KaoListUser
            {
                Id = "42239b25-df67-49c2-a028-d93016bff8e3",
                NickName = "木村世治",
                NormalizedNickName = "木村世治",
                Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 31, second: 26, millisecond: 298),
                UserName = "44f1867d-bc20-48db-b7a7-627f33471531",
                NormalizedUserName = "44F1867D-BC20-48DB-B7A7-627F33471531",
                EmailConfirmed = false,
                SecurityStamp = "8761a834-4fa1-4fed-88cf-273dc3cedbd9",
                ConcurrencyStamp = "6f62f178-e5a1-4cef-951a-5f0c872192e0",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
            },
            new KaoListUser
            {
                Id="ca4dd1f0-36cb-4fe7-b4a1-4947659a8129",
                NickName="",
                NormalizedNickName="",
                Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 31, second: 25, millisecond: 404),
                UserName = "0fc30f21-73bd-436a-9cd2-61d94bc31606",
                NormalizedUserName = "0FC30F21-73BD-436A-9CD2-61D94BC31606",
                EmailConfirmed = false,
                SecurityStamp = "92dab372-ef8b-4a14-849b-8d1b4709f14e",
                ConcurrencyStamp = "8d9c727c-35c1-44e9-9313-3c0f6f2044d7",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
            },
        };

        public static List<Sing> GetSings() => new()
        {
            new Sing
            {
                Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 32, second: 32, millisecond: 283),
                Id="4df36b28-31ba-4e41-87aa-6d9309e1fc55",
                InstrumentalId="cd973782-c032-4e14-be1e-72bb32026eea"
            },
            new Sing
            {
                Created = new DateTime(year: 2022, month: 8, day: 29, hour: 5, minute: 32, second: 31, millisecond: 919),
                Id="24f266f2-d34b-488c-a974-63f0a1671c3d",
                InstrumentalId="d950653d-847a-4707-9e57-ca51914c7c02"
            },
        };

        public static List<SingUser> GetSingUsers() => new()
        {
             new SingUser
            {
                SingId = "4df36b28-31ba-4e41-87aa-6d9309e1fc55",
                UserId = "00004e53-a27c-423e-892c-e5a54f665cee",
            },
            new SingUser
            {
                SingId = "24f266f2-d34b-488c-a974-63f0a1671c3d",
                UserId = "42239b25-df67-49c2-a028-d93016bff8e3",
            },
        };

        public static List<Instrumental> GetInstrumentals() => new()
        {
            new Instrumental
            {
                Id="cd973782-c032-4e14-be1e-72bb32026eea",
                Composer="42239b25-df67-49c2-a028-d93016bff8e3",
                ConcurrencyStamp="61eee3da-3b9f-423b-a466-874d2a085c78",
                Created=new DateTime(year: 2012, month: 11, day: 24),
                NormalizedTitle="bright lights",
                Title="bright lights",
            },
            new Instrumental
            {
                Id="d950653d-847a-4707-9e57-ca51914c7c02",
                Composer="ca4dd1f0-36cb-4fe7-b4a1-4947659a8129",
                ConcurrencyStamp="e3eb3a53-4a70-4501-bfac-99b292873495",
                Created=new DateTime(year: 2015, month: 2, day: 1),
                NormalizedTitle="felt",
                Title="felt",
            },
        };
    }
}
