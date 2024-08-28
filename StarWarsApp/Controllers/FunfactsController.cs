using Microsoft.AspNetCore.Mvc;
using StarWarsApp.Models;
using OpenAI.Chat;



namespace StarWarsApp.Controllers;

public class FunfactsController : Controller
{

    static Funfact funfact = new Funfact();
    public IActionResult Index()
    {
        return View(funfact);
    }

public async Task<IActionResult> Create() 
// Prompts to GPT, processes result. 
    {
        string openAIKey = "KEY";
        ChatClient client = new(model: "gpt-4o-mini", openAIKey);

        ChatCompletion completion = client.CompleteChat("Tell me a very short fun fact about Star Wars!");

        string responsetext = completion.Content.Last().Text;
        funfact.fact = responsetext;
        return RedirectToAction("Index");
    }
}


  
