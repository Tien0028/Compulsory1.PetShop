using PetShopApplication.Core.ApplicationService;
using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApplication.Infrastructure.Static.Data
{
    public class FakeDB
    {
        readonly IPetService _petService;
        public FakeDB(IPetService petService)
        {
            _petService = petService;
        }
        public void InitData()
        {
            Pet pet1 = new Pet
            {
                
                Name = "Dante",
                Type = "Dog",
                Birthdate = new DateTime(2013, 7, 8),
                SoldDate = new DateTime(2013, 7, 8),
                previousOwner = "Uriel Sorensson",
                Price = 250
            };
            _petService.Create(pet1);
            Pet pet2 = new Pet
            {
                
                Name = "Vergil",
                Type = "Cat",
                Birthdate = new DateTime(2013, 7, 8),
                SoldDate = new DateTime(2013, 7, 8),
                //Color = "Blue",
                previousOwner = "Bruce Wayne",
                Price = 550
            };
            _petService.Create(pet2);
            Pet pet3 = new Pet
            {

                Name = "Lady",
                Type = "Snake",
                Birthdate = new DateTime(2013, 7, 8),
                SoldDate = new DateTime(2013, 7, 8),
                //Color = "Brown",
                previousOwner = "Loki Odinson",
                Price = 360
            };
            _petService.Create(pet3);
            Pet pet4 = new Pet
            {
                
                Name = "Trish",
                Type = "Hawk",
                Birthdate = new DateTime(2012, 6, 4),
                SoldDate = new DateTime(2012, 6, 4),
                //Color = "White",
                previousOwner = "Urizen Sparkles",
                Price = 70
            };
            _petService.Create(pet4);
            Pet pet5 = new Pet
            {
                
                Name = "Iris",
                Type = "Lioness",
                Birthdate = new DateTime(2012, 6, 4),
                SoldDate = new DateTime(2012, 6, 4),
                //Color = "Orange",
                previousOwner = "Simba",
                Price = 120
            };
            _petService.Create(pet5);
        }
    }
}
