using GenerateTestData.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateTestData.Entities
{
	public class User
	{
		public string Address { get; set; }
		public string City { get; set; }
		public DateTimeOffset CreatedDate { get; set; }
		public string Email { get; set; }
		public DateTimeOffset ModifiedDate { get; set; }
		public string Name { get; set; }
		public long Id { get; set; }
		public bool IsActive { get; set; }
		public string PrimaryPhone { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }

		public User()
		{
			Name = RandomData.Get(RandomData.Names);
			
			var randomAddress = RandomData.Get(RandomData.Addresses);
			Address = randomAddress.Address;
			City = randomAddress.City;
			State = randomAddress.State;
			Zip = randomAddress.Zip;

			IsActive = RandomData.GetBoolean();
			Email = Name + "@" + RandomData.Get(RandomData.EmailDomains);

			PrimaryPhone = "9";
			for (int i = 0; i < 9; i++)
				PrimaryPhone += RandomData.Next(10);

			ModifiedDate = DateTime.Now.AddDays(-1 * RandomData.Next(100));
			CreatedDate = DateTime.Now.AddDays(-1 * RandomData.Next(100));
		}
	}
}
