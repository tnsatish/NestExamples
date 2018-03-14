using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestExamples
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
		public List<NameValuePair> CustomData { get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(Id + " " + Name + " " + Email + " " + Address + " " + City + " " + State + " " + Zip + " " + PrimaryPhone + " " + CreatedDate + " " + ModifiedDate + " ");
			foreach (var pair in CustomData)
			{
				sb.Append(pair.Name).Append(": ").Append(pair.Value).Append(" ");
			}
			return sb.ToString();
		}
	}

	public class NameValuePair
	{
		public string Name { get; set; }
		public string Value { get; set; }

		public NameValuePair(string name, string value)
		{
			Name = name;
			Value = value;
		}
	}
}
