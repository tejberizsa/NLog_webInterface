using System;
using System.Collections.Generic;
using System.Linq;
using LogiAppMonitor.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LogiAppMonitor.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedUser()
        {
            var obsolateDatas = _context.LogMessages.Where(x => x.CreatedAt < DateTime.Now.AddMonths(-3));
            _context.RemoveRange(obsolateDatas);
            _context.SaveChanges();

            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        Error = HandleDeserializationError
                    };
            var users = JsonConvert.DeserializeObject<List<User>>(userData, settings);
            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();
                user.LastActive = DateTime.Now;
                if(!_context.Users.Any(x => x.Username == user.Username))
                {
                    _context.Users.Add(user);
                }
            }

            if(!_context.LogMessages.Any())
            {
                var logData = System.IO.File.ReadAllText("Data/LogSeedTestData.json");
                var logs = JsonConvert.DeserializeObject<List<LogMessage>>(logData, settings);
                foreach (var log in logs)
                {
                    log.CreatedAt = DateTime.Now;
                    _context.LogMessages.Add(log);
                }
            }

            _context.SaveChanges();
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }
    }
}