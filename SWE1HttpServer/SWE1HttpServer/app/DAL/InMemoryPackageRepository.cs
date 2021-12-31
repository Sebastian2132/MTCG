using SWE1HttpServer.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.app.DAL
{
    public class InMemoryPackageRepository : IPackageRepository
    {
        List<List<Card>> packages = new();
        public void addPackageToDb(string package)
        {
            
            throw new NotImplementedException();
        }

        public IEnumerable<Card> getPackageFromDb()
        {
            throw new NotImplementedException();
        }
    }
}