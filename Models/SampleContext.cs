using Microsoft.EntityFrameworkCore;

namespace RoutingDemo.Models
{
	//Model of the database - database structure
	public class SampleContext:DbContext
	{
		public DbSet<Product> Products { get; set; }
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
	 */
}
