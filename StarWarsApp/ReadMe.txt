To use web application, install .Net v8. 
sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-8.0


To use Funfact page, install OpenAI:
dotnet add package OpenAI --prerelease
and add openaiKey in Controllers/FunfactsController.cshtml, line 21


To start app, open terminal, go to project path (.../StarWarsApp) and run the following command: 
dotnet run
