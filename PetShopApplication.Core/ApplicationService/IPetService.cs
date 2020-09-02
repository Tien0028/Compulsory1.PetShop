using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApplication.Core.ApplicationService
{
    public interface IPetService
    {
        List<Pet> ReadPets();
        Pet FindPetById(int id);
        Pet NewPet(string name, string type, DateTime birthday, DateTime soldDate, string previousOwner, double price);
        Pet Create(Pet pet);
        Pet Update(Pet pet);
        void Delete(Pet pet);
        List<Pet> FilterForPetType(string type);
        List<Pet> Get5CheapestPets();
        List<Pet> sortedPetsByPrice();

    }
}
