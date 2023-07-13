using WalkersServer.Scripts;

namespace WalkersServer
{
    public class Program {  
    
        public static void Main(string[] args) {  
            
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR();
            
            var app = builder.Build();
            
            app.MapHub<WalkersHub>("/WalkersHub");
            app.MapGet("/", () => "Walkers server");
            
            app.Run();
        }  
  
    }
    
}

