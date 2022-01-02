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
        public void addPackageToDb(List<Card> package)
        {
            
            packages.Add(package);
        }

        public List<Card> getPackageFromDb()
        {
            if(packages.Count > 0){
            List<Card> package = packages[0];
            packages.RemoveAt(0);
            return package;
            }else{
                return null;
            }
           
        }
    }
}