using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parks.Models;

namespace Parks.Repositories.IRepositories
{
    public interface INationalParkRepository
    {  
        public List<NationalPark> Get();

        public NationalPark Get(string id);

        public NationalPark Create(NationalPark nationalPark);

        public void Update(string id, NationalPark nationalParkIn);

        public void Remove(NationalPark nationalParkIn);

        public void Remove(string id);
    }
}