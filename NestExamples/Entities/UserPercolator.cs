using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestExamples.Entities
{
	[ElasticsearchType(IdProperty = nameof(SearchId))]
	public class UserPercolate
	{
		public int SearchId { get; set; }
		public string SearchEmail { get; set; }
		public QueryContainer Query { get; set; }

		public UserPercolate() { }
		public UserPercolate(int id, string email, QueryContainer query)
		{
			SearchId = id;
			SearchEmail = email;
			Query = query;
		}

		public override string ToString()
		{
			return SearchId + " " + SearchEmail + " " + Query.ToString();
		}
	}
}
