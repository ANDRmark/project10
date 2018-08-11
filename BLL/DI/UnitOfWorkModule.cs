using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DI
{
    public class UnitOfWorkModule : Ninject.Modules.NinjectModule
    {
        string connectionString;

        public UnitOfWorkModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public override void Load()
        {
            //Use Entity Framework as DAL
            this.Bind<DAL.Interfaces.IUnitOfWork>().To<DAL_EF.UnitOfWork>().WithConstructorArgument(connectionString);
            //Use ADO.NET as DAL
            //this.Bind<DAL.Interfaces.IUnitOfWork>().To<DAL_ADO.ADOUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
