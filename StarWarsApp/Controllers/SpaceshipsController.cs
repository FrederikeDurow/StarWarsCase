using Microsoft.AspNetCore.Mvc;
using StarWarsApp.Models;
using System.Text.Json;
using StarWarsApp.Controllers;



namespace StarWarsApp.Controllers
{
    public class SpaceshipsController : Controller
    {
        static List<Spaceship> spaceships = new List<Spaceship>();
        static int nextId = 0;
        static int allmembers= 0;
        static bool sortByCrew = true;
        private static readonly HttpClient httpClient = new HttpClient();


        public IActionResult Index()
        // Show Spaceships

        {
            return View(spaceships);
        }

        public IActionResult Create()
        // Add spaceship to list
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Spaceship spaceship)
        {
            // Asks SWAPI for Starship with name given by user, outputs given data.

            string apiUrl = $"https://swapi.dev/api/starships/?search={spaceship.Name}";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<ApiResponse>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });


                int crewmembers = 0;
                string name = "";
                if (data != null && data.Results != null && data.Results.Count > 0)
                {

                    name = data.Results[0].Name;
                    string crewString = data.Results[0].Crew;
                    if (int.TryParse(crewString.Replace(",", ""), out int crewCount))
                    {
                        crewmembers = crewCount;
                    }
                }

            if (name != "" && !spaceships.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    spaceship.Name = name;
                    spaceship.Crewmembers = crewmembers;
                    spaceship.Id = nextId;
                    nextId++;
                    spaceships.Add(spaceship);

                    allmembers+= crewmembers;
                    for (int i = 0; i < spaceships.Count; i++)
                    {
                        spaceships[i].AllCrewMembers = allmembers;
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Sort(string sortBy)
        // Lets user sort by Name or Crew size
        {
            if(sortBy == "Crew")
            {
                spaceships = spaceships.OrderBy(s => s.Crewmembers).ToList();
            }
            else
            {
                spaceships = spaceships.OrderBy(s => s.Name).ToList();
            }
            sortByCrew = !sortByCrew;
            return RedirectToAction("Index");
        }
        
       
    
        public IActionResult Delete(int id)
        // Lets user delete starship from list
        
        {
            Spaceship ship = spaceships.Find(p => p.Id == id);
            
            if (ship != null)
            {
                allmembers -= ship.Crewmembers; 
                spaceships.Remove(ship); 
                
                for (int i = 0; i < spaceships.Count; i++)
                {
                    spaceships[i].AllCrewMembers = allmembers;
                }
            } 
            return RedirectToAction("Index");
        }
    }
}

    public class ApiResponse
    {
        public int Count { get; set; }
        public List<Starship> Results { get; set; }
    }

    public class Starship
    {
        public string Name { get; set; }
        public string Crew { get; set; }
    }

   