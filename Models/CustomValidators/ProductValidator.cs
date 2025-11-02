using System.ComponentModel.DataAnnotations;

namespace RoutingDemo.Models.CustomValidators
{
	public class ProductValidator
	{
		public static ValidationResult ValidateExpiryDate(DateTime? expiryDate, ValidationContext c)
		{
			//c.ObjectInstance gives you the model you are validating
			Product? p = c.ObjectInstance as Product;
			if(p != null)
			{
				if(expiryDate.Value < p.MDate)
				{
					return new ValidationResult("Expiry date must be after Manufacturing date");
				}
			}
			return ValidationResult.Success;
		}
	}
}
/* Entity Framework is an ORM: Object Relational Mapping tool.
 * Maps classes to database objects
 * Ex: Product -> Id {Key] Database generated
 *	mapped to
 *	Create table products
 *	(
 *		id int primary key identity(1, 1)
 *		
 *		Database First approach: Your database wil be generated from code
 */ 