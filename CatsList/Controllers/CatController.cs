using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using CatsList.Models;

namespace CatsList.Controllers
{
    [Produces("application/json")]
    [Route("api/Cat")]
    public class CatController : Controller
    {
        readonly string url = "http://agl-developer-test.azurewebsites.net/people.json";

        // GET: api/Cat
        [HttpGet]
        public async Task<IEnumerable<OwnersPet>> GetCatsAsync()
        {
            List<Person> persons = await GetPersonsAsync();

            string petType = "Cat";

            List<OwnersPet> catList = GetPets(persons, petType);

            return catList;
        }

        public async Task<List<Person>> GetPersonsAsync()
        {
            var result = new List<Person>();
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsAsync<List<Person>>();
                        
                    }
                }
                catch (HttpRequestException httpRequestException)
                {
                     BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
            return result;
        }

        public List<OwnersPet> GetPets(List<Person> persons, string petType)
        {
            List<OwnersPet> petList = null;

            if(persons != null)
            { 
                var pets =  from t in persons
                            .Where(l => l.Pets != null)
                            .SelectMany(l => l.Pets, (person, pet) => new { person, pet })
                           where t.pet.Type == petType ||  petType == string.Empty
                           orderby t.pet.Name
                           select new OwnersPet { Name = t.pet.Name, OwnerGender = t.person.Gender };

                petList = pets.ToList<OwnersPet>();
            }

            return petList;
        }
    }
}