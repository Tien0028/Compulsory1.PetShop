using PetShopApplication.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using PetShopApplication.Core.ApplicationService;
using PetShopApplication.Core.ApplicationService.Impl;
using PetShopApplication.Core.DomainService;
using PetShopApplication.Infrastructure.Static.Data;
using System;

namespace PetShopApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();    
            serviceCollection.AddScoped<IPetRepository, PetRepository>();
            serviceCollection.AddScoped<IPetService, PetService>();
            serviceCollection.AddScoped<IPrinter, Printer>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var petService = serviceProvider.GetRequiredService<IPetService>();
            var fakeDB = new FakeDB(petService);

            fakeDB.InitData();

            var printer = serviceProvider.GetRequiredService<IPrinter>();
            printer.StartUI();

            Console.ReadLine();

        }
    }
}
