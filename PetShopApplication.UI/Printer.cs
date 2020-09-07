using PetShopApplication.Core.ApplicationService;
using PetShopApplication.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PetShopApplication.ConsoleApp
{
    public class Printer : IPrinter
    {
        IPetService _petService;
        int idOfPet;
        

        public Printer(IPetService petService)
        {
            _petService = petService;
        }

        public void StartUI()
        {
            string[] menuItems =            {
                "List All Pets",
                "Search By Type of Pet",
                "Breed A new Pet(Create)",
                "Release pet(Delete)",
                "Groom Pet(Update)",
                "Sort Pets by Price",
                "Get 5 cheapest pets",
                "Exit"

            };

            var selection = ShowMenu(menuItems);
            while (selection != 8)
            {
                switch (selection)
                {
                    case 1:
                        ListPets();
                        break;
                    case 2:
                        Console.WriteLine("Input Number of Pet Type (1-5): ");
                        SearchByPetType();
                        break;
                    case 3:
                        object pet = CreatePet();
                        _petService.Create((Pet)pet);
                        break;
                    case 4:
                        DeletePet();
                        break;
                    case 5:
                        idOfPet = GetPetId();
                        var petForUpdate = _petService.FindPetById(idOfPet);
                        Console.WriteLine("Grooming" + petForUpdate.Name);
                        var newPetName = NewPetData("New Name for " + petForUpdate.Id);
                        var newPetType = NewPetData("New Type for " + petForUpdate.Id);
                        var newPetOwner = NewPetData("New Owner for " + petForUpdate.Id);

                        petForUpdate.Name = newPetName;
                        //petForUpdate.Type = newPetType;
                        Pet.Type2 thePetType;
                        int typePet;
                        if (int.TryParse(newPetType, out typePet) && typePet <= 5)
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
                        petForUpdate.Type = thePetType;
                        petForUpdate.previousOwner = newPetOwner;

                        _petService.Update(petForUpdate);
                        break;
                    case 6:
                        sortPetsByPrice();
                        break;
                    case 7:
                        showCheapestPets();
                        break;

                    case 8:
                        break;
                }
                selection = ShowMenu(menuItems);
            }
            Console.WriteLine("Later, pal. And remember, there is no refund policy!");
            Console.ReadLine();
        }

        private void sortPetsByPrice()
        {
            var list = _petService.sortedPetsByPrice();
            foreach(var pet in list)
            {
                Console.WriteLine("Pet Id: {0} Price: {1:N}", pet.Id, pet.Price);
            }
        }

        private void showCheapestPets()
        {
            var list = _petService.Get5CheapestPets();
            foreach (var pet in list)
            {
                Console.WriteLine("Name: {0} Price: {1}", pet.Name, pet.Price);
            }
        }

        private string NewPetData(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        private int ShowMenu(string[] menuItems)
        {
            Console.WriteLine("What do you want to do today, pal?");
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine($"{(i + 1)}: {menuItems[i]}");
            }
            Console.WriteLine("");
            Console.WriteLine("");

            int selection;
            while(!int.TryParse(Console.ReadLine(), out selection)
                || selection < 1
                || selection > 8)
            {
                Console.WriteLine("Please select a number between 1-8");
            }
            Console.WriteLine();
            return selection;

        }

        private int GetPetId()
        {
            Console.WriteLine("Write Pet Id: ");
            int idOfPet;
            while (!int.TryParse(Console.ReadLine(), out idOfPet))
            {
                Console.WriteLine("Please insert a number: ");
            }
            return idOfPet;
        }

        private void DeletePet()
        {
            idOfPet = GetPetId();
            var petForDeletion = _petService.FindPetById(idOfPet);
            Console.WriteLine("Releasing " + petForDeletion.Name);
            _petService.Delete(petForDeletion);
        }

        private object CreatePet()
        {
            
            Console.WriteLine("let's make a new Pet! YAY!");

            var name = NewPetData("Name of New Pet");
            Console.WriteLine("Number and Pet Type: 1 = Dog, 2 = Cat, 3 = Hawk, 4 = Snake, 5 = Lion");
            string type = NewPetData("Insert number 1-5 for Type of New Pet");

            Console.WriteLine("Birthdate of New Pet:");
            DateTime birthday = GetDate();
            Console.WriteLine("Solddate of New Pet:");
            DateTime soldDate = GetDate();
            
            var previousOwner = NewPetData("Who's the previous owner of new Pet?");
            double price = GetPrice("Price of your Pet");
            return _petService.NewPet(name, type, birthday, soldDate, previousOwner, price);
        }

        private double GetPrice(string v)
        {
            double price;
            while (!Double.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Estimated price of the new Pet");
            }
            return price;
        }

        private DateTime GetDate()
        {
            DateTime birthday;
            while (!DateTime.TryParse(Console.ReadLine(), out birthday))
            {
                Console.WriteLine("Please input the date i.e 02/03/2005");
            }
            return birthday;
        }

        private void SearchByPetType()
        {
            Console.WriteLine("1 = Dog, 2 = Cat, 3 = Hawk, 4 = Lion, 5 = Snake");
            string type = Console.ReadLine();
            List<Pet> filteredList = _petService.FilterForPetType(type);
            foreach (var pet in filteredList)
            {
                Console.WriteLine(pet.Type);
                Console.WriteLine(pet.Name);
                Console.WriteLine("");
            }
        }

        private void ListPets()
        {
            var pets = _petService.ReadPets();
            foreach (var pet in pets)
            {
                Console.WriteLine("id: " + pet.Id + "\n"
                                + "name: " + pet.Name + "\n"
                                + "type: " + pet.Type + "\n"
                                + "birthday : " + pet.Birthdate + "\n"
                                + "sold date : " + pet.SoldDate + "\n"
                                + "previous owner : " + pet.previousOwner + "\n"
                                + "price : " + pet.Price + "\n"
                                );
            }
        }
    }
}
