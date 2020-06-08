using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolForTheSummerApi.Models;
using MongoDB.Driver;

namespace CoolForTheSummerApi.Services
{
    public class CoolForTheSummerService
    {
        private readonly IMongoCollection<CoolForTheSummerUser> _coolForTheSummerUser;

        public CoolForTheSummerService(ICoolForTheSummerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _coolForTheSummerUser = database.GetCollection<CoolForTheSummerUser>(settings.CollectionName);
        }

        public List<CoolForTheSummerUser> Get() =>
            _coolForTheSummerUser.Find(coolForTheSummerUser => true).ToList();

        public CoolForTheSummerUser Get(string id) =>
            _coolForTheSummerUser.Find<CoolForTheSummerUser>(coolForTheSummerUser => coolForTheSummerUser.Id == id).FirstOrDefault();

        public CoolForTheSummerUser Create(CoolForTheSummerUser coolForTheSummerUser)
        {
            _coolForTheSummerUser.InsertOne(coolForTheSummerUser);
            return coolForTheSummerUser;
        }

        public void Update(string id, CoolForTheSummerUser coolForTheSummerUserIn) =>
            _coolForTheSummerUser.ReplaceOne(coolForTheSummerUser => coolForTheSummerUser.Id == id, coolForTheSummerUserIn);

        public void Remove(CoolForTheSummerUser coolForTheSummerUserIn) =>
            _coolForTheSummerUser.DeleteOne(coolForTheSummerUser => coolForTheSummerUser.Id == coolForTheSummerUserIn.Id);

        public void Remove(string id) =>
            _coolForTheSummerUser.DeleteOne(coolForTheSummerUser => coolForTheSummerUser.Id == id);
    }
}
