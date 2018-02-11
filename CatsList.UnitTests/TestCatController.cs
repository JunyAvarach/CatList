using Microsoft.VisualStudio.TestTools.UnitTesting;
using CatsList.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CatsList.Models;
using Newtonsoft.Json;

namespace CatsList.UnitTests
{
    [TestClass]
    public class TestSimplePetController
    {

        private List<Person> GetTestPersons()
        {
            var testPersonsJsonString = "[{\"name\":\"Bob\",\"gender\":\"Male\",\"age\":23,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"},{\"name\":\"Fido\",\"type\":\"Dog\"}]},{\"name\":\"Jennifer\",\"gender\":\"Female\",\"age\":18,\"pets\":[{\"name\":\"Garfield\",\"type\":\"Cat\"}]},{\"name\":\"Steve\",\"gender\":\"Male\",\"age\":45,\"pets\":null},{\"name\":\"Fred\",\"gender\":\"Male\",\"age\":40,\"pets\":[{\"name\":\"Tom\",\"type\":\"Cat\"},{\"name\":\"Max\",\"type\":\"Cat\"},{\"name\":\"Sam\",\"type\":\"Dog\"},{\"name\":\"Jim\",\"type\":\"Cat\"}]},{\"name\":\"Samantha\",\"gender\":\"Female\",\"age\":40,\"pets\":[{\"name\":\"Tabby\",\"type\":\"Cat\"}]},{\"name\":\"Alice\",\"gender\":\"Female\",\"age\":64,\"pets\":[{\"name\":\"Simba\",\"type\":\"Cat\"},{\"name\":\"Nemo\",\"type\":\"Fish\"}]}]";

            List<Person> testPersons = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Person>>(testPersonsJsonString);

            return testPersons;
        }

        [TestMethod]
        public void GetPets_CheckForNullPetsByType()
        {
            var controller = new CatController();
            List<Person> persons = GetTestPersons();
            List<OwnersPet> cats = controller.GetPets(persons, null);

            // Assert
            Assert.AreEqual(0, cats.Count);
        }

        [TestMethod]
        public void GetPets_CheckForInvalidPetType()
        {
            var controller = new CatController();
            List<Person> persons = GetTestPersons();
            List<OwnersPet> pets =  controller.GetPets(persons, "NotaPet");

            // Assert
            Assert.IsNotNull(pets);
            Assert.AreEqual(0, pets.Count);
        }

        [TestMethod]
        public void GetPets_CheckForPetsByType()
        {
            var controller = new CatController();
            List<Person> persons = GetTestPersons();
            List<OwnersPet> cats = controller.GetPets(persons, "Dog");

            // Assert
            Assert.AreEqual(2, cats.Count);
        }

        [TestMethod]
        public void GetPets_CheckForAllPets()
        {
            var controller = new CatController();
            List<Person> persons = GetTestPersons();
            List<OwnersPet> cats = controller.GetPets(persons, "");

            // Assert
            Assert.AreEqual(10, cats.Count);
        }

        [TestMethod]
        public void GetPets_CheckInEmptyList()
        {
            var controller = new CatController();
            List<Person> persons = null;
            List<OwnersPet> cats = controller.GetPets(persons, "Cat");

            // Assert
            Assert.IsNull(cats);
        }

        [TestMethod]
        public async Task GetPerson_ShouldNotFind()
        {
            var controller = new CatController();
            var persons = await controller.GetPersonsAsync();

            int count = (from person in persons
                            where person.Name == "juny"
                            select person).Count();
            //not found
            Assert.AreEqual(count, 0 );
        }

        [TestMethod]
        public async Task GetCats_ShouldNotFind()
        {
            var controller = new CatController();
            var cats = await controller.GetCatsAsync();

            //Assert
            Assert.IsNotNull(cats);
        }
    }
}
