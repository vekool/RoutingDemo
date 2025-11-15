using Microsoft.EntityFrameworkCore;

namespace RoutingDemo.Models
{
	//Model of the database - database structure

	public class SampleContext:DbContext
	{
		//provided by framework
		public SampleContext(DbContextOptions options):base(options)
		{
			
		}
		//this models the database table named Products
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }
	}

	/* Data Source=(localdb)\\MSSQLLocalDB;
	 *	The server address (where to find the server)
	 *	
	 * Initial Catalog=SampleContext;
			The name of the database

		Integrated Security=True;
			Login with windows credentials

		Connect Timeout=30
			Disconnect if server is unresponsive for 30 seconds	 
	 add-migration InitialDB
	update-database

	you can write update-database -v (it will show all commands run)
	 */
}
