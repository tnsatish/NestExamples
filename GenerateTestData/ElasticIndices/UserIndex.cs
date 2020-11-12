using GenerateTestData.Entities;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateTestData.ElasticIndices
{
	public class UserIndex : ElasticIndexBase<User>
	{
		public UserIndex() : base("users")
		{
		}

		public UserIndex(string indexName) : base(indexName)
		{
		}

		protected override Func<CreateIndexDescriptor, CreateIndexDescriptor> GetCreateIndexDescriptor()
		{
			Func<CreateIndexDescriptor, CreateIndexDescriptor> CreateIndexFunc =
				i => i.Map<User>(m => m.Properties(p => p
										.Text(s => s.Name(a => a.Address))
										.Text(s => s.Name(a => a.City))
										.Text(s => s.Name(a => a.Email))
										.Text(s => s.Name(a => a.Name))
										.Text(s => s.Name(a => a.PrimaryPhone))
										.Text(s => s.Name(a => a.State))
										.Text(s => s.Name(a => a.Zip))
										.Number(s => s.Name(a => a.Id))
										.Boolean(s => s.Name(a => a.IsActive))
										.Date(s => s.Name(a => a.CreatedDate))
										.Date(s => s.Name(a => a.ModifiedDate))));
			return CreateIndexFunc;
		}

		protected override List<User> _GetRandomData()
		{
			List<User> users = new List<User>();
			for(int i=0; i<20000; i++)
			{
				User user = new User() { Id = i };
				users.Add(user);
			}
			return users;
		}
	}
}
