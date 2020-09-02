using PetShopApplication.Core.DomainService;
using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetShopApplication.Infrastructure.Static.Data
{
    public class PetRepository : IPetRepository
    {
        static int id = 1;
        private List<Pet> _pets = new List<Pet>();
        public Pet Create(Pet createPet)
        {
            createPet.Id = id++;
            _pets.Add(createPet);
            return createPet;
        }

        public void Delete(Pet pet)
        {
            _pets.Remove(pet);
        }

        public List<Pet> FilterForPetType(Pet.Type2 type)
        {
            return _pets.Where(pet => pet.Type == type).ToList();
        }

        public Pet ReadByPetId(int id)
        {
            foreach (var pet in _pets)
            {
                if (pet.Id == id)
                {
                    return pet;
                }
            }
            return null;
        }

        public List<Pet> ReadPets()
        {
            return _pets;
        }

        public Pet UpdateDB(Pet pet)
        {
            var dbPet = this.ReadByPetId(pet.Id);
            if (dbPet != null)
            {
                dbPet.Name = pet.Name;
                dbPet.Type = pet.Type;
                return dbPet;
            }
            return null;
        }
    }
}
