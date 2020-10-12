using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Parks.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;}

        [BsonRequired]
        public string Name {get; set;}

        [BsonRequired]
        public string Email {get; set;}

        [BsonRequired]
        public string Password {get; set;}
    }
}