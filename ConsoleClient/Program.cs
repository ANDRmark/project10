using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Ninject;
using BLL.Interfaces;
using System.Reflection;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            IKernel kernel = new StandardKernel(
                new BLL.DI.MapperModule(),
                new BLL.DI.UnitOfWorkModule(connectionString),
                new BLL.DI.ServicesModule()
                );

            IMessageService messageService = kernel.Get<IMessageService>();
            List<BLL.DTO.MessageDTO> messages =  messageService.GetAll().ToList();
            foreach(var m in messages)
            {
                Console.WriteLine($"ID = {m.Id}, body = {m.MessageBody}");
            }
            Console.WriteLine("END");
            messageService.Insert(new BLL.DTO.MessageDTO() { MessageBody="This is new message", CreateDate=DateTime.Now, ThemeId=1});
            Console.ReadKey();
        }
    }
}
