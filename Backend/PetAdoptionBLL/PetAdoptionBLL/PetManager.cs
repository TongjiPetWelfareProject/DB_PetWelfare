using System;
using PetAdopt.DAL;
namespace PetAdopt.BLL
{
    public class PetManager
    {
        public int GetPet(Pet pet)
        {
            return new PetServer().GetPet(pet);
        }
    }
}
