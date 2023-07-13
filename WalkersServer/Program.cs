using WalkersServer.Scripts;

namespace WalkersServer
{
    public class Program {  
    
        public static void Main(string[] args) {  
            
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR();
            
            Console.WriteLine("Start MSG");
            
            var app = builder.Build();

            app.MapGet("/", () => "Hello Ivan!");
            app.MapHub<WalkersHub>("/WalkersHub");
            
            app.Run();
            
        }  
  
    }
    
}

