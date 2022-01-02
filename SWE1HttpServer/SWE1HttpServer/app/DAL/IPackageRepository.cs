using SWE1HttpServer.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.app.DAL
{
    public interface IPackageRepository
    {
        void addPackageToDb(List<Card> package);
        List<Card> getPackageFromDb();

    }
}
