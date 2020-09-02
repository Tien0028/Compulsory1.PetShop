using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShopApplication.Core.DomainService
{
    public interface IPetRepository
    {
        List<Pet> ReadPets();
        Pet Create(Pet createPet);
        Pet UpdateDB(Pet pet);
        Pet ReadByPetId(int id);
        void Delete(Pet pet);
        List<Pet> FilterForPetType(Pet.Type2 type);
    }
}
