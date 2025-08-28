using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PublicRequestApp.Models;


namespace PublicRequestApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
                try
                {
                    bool canConnect = this.Database.CanConnect();
                    if (canConnect)
                    {
                        Console.WriteLine("החיבור ל-DB הצליח!");
                    }
                    else
                    {
                        Console.WriteLine("החיבור ל-DB נכשל.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("שגיאה בחיבור: " + ex.Message);
                }
            

        }



        public DbSet<Request> Request { get; set; }
    }
}
