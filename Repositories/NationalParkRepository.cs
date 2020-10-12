using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parks.Repositories.IRepositories;
using Parks.Models;
using MongoDB.Driver;
using Parks.Services.Configuration;

namespace Parks.Repositories
{
    public class NationalParkRepository : INationalParkRepository
    {

        private readonly IMongoCollection<NationalPark> _nationalParks;

        public NationalParkRepository(IParksDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _nationalParks = database.GetCollection<NationalPark>("NationalParks");
        }
        
        public List<NationalPark> Get() => 
            _nationalParks.Find(nationalPark => true).ToList();

        public NationalPark Get(string id) =>
            _nationalParks.Find<NationalPark>(nationalPark => nationalPark.Id == id).FirstOrDefault();

        public NationalPark Create(NationalPark nationalPark)
        {
            _nationalParks.InsertOne(nationalPark);
            return nationalPark;
        }
        public void Update(string id, NationalPark nationalParkIn) =>
            _nationalParks.ReplaceOne(nationalPark => nationalPark.Id == id, nationalParkIn);

        public void Remove(NationalPark nationalParkIn) =>
            _nationalParks.DeleteOne(nationalPark => nationalPark.Id == nationalParkIn.Id);

        public void Remove(string id) =>
            _nationalParks.DeleteOne(nationalPark => nationalPark.Id == id);
    }
}