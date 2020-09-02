using PetShopApplication.Core.DomainService;
using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PetShopApplication.Core.ApplicationService.Impl
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepo;

        public PetService(IPetRepository petRepository)
        {
            _petRepo = petRepository;
        }
        public Pet Create(Pet pet)
        {
            return _petRepo.Create(pet);
        }

        public void Delete(Pet pet)
        {
            _petRepo.Delete(pet);
        }

        public List<Pet> FilterForPetType(string type)
        {
            Pet.Type2 thePetType;
            int typePet;
            if (int.TryParse(type, out typePet) && typePet <= 5)
            {
                switch (typePet)
                {
                    case 1:
                        thePetType = Pet.Type2.Dog;
                        break;
                    case 2:
                        thePetType = Pet.Type2.Cat;
                        break;
                    case 3:
                        thePetType = Pet.Type2.Hawk;
                        break;
                    case 4:
                        thePetType = Pet.Type2.Lion;
                        break;
                    default:
                        thePetType = Pet.Type2.Snake;
                        break;
                }

            }
            else
            {
                throw new InvalidDataException(message: "Error, you did not chose type of pet correctly. MOron.");
            }
            return _petRepo.FilterForPetType(thePetType);

        }

        public Pet FindPetById(int id)
        {
            return _petRepo.ReadByPetId(id);
        }

        public List<Pet> Get5CheapestPets()
        {
            return _petRepo.ReadPets().OrderBy(pet => pet.Price).Take(5).ToList();
        }

        public Pet NewPet(string name, string type, DateTime birthday, DateTime soldDate, string previousOwner, double price)
        {
            Pet.Type2 thePetType;
            int typePet;
            if (int.TryParse(type, out typePet) && typePet <= 5)
            {
                switch (typePet)
                {
                    case 1:
                        thePetType = Pet.Type2.Dog;
                        break;
                    case 2:
                        thePetType = Pet.Type2.Cat;
                        break;
                    case 3:
                        thePetType = Pet.Type2.Hawk;
                        break;
                    case 4:
                        thePetType = Pet.Type2.Lion;
                        break;
                     default:
                        thePetType = Pet.Type2.Snake;
                        break;
                }

            }
            else
            {
                throw new InvalidDataException(message: "Error, you did not chose type of pet correctly. MOron.");
            }
            var newPet = new Pet()
            {
                Name = name,
                Type = thePetType,             
                Birthdate = birthday,
                SoldDate = soldDate,
                previousOwner = previousOwner,
                Price = price
            };
            return newPet;
        }

        public List<Pet> ReadPets()
        {
            return _petRepo.ReadPets();
        }

        public List<Pet> sortedPetsByPrice()
        {
            return _petRepo.ReadPets().OrderBy(pet => pet.Price).ToList();

        }

        public Pet Update(Pet pet)
        {
            Pet pets = FindPetById(pet.Id);
            pet.Name = pet.Name;
            pet.Type = pet.Type;
            pet.Birthdate = pet.Birthdate;
            pet.SoldDate = pet.SoldDate;
            pet.previousOwner = pet.previousOwner;
            pet.Price = pet.Price;
            return _petRepo.UpdateDB(pet);
        }
    }
}
