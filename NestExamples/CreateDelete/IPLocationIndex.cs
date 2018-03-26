using Nest;
using NestExamples.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestExamples.CreateDelete
{
	public class IPLocationIndex : ElasticIndexBase
	{
		public IPLocationIndex(string indexName) : base(indexName)
		{
		}

		protected override Func<CreateIndexDescriptor, CreateIndexDescriptor> GetCreateIndexDescriptor()
		{
			return i => i.Settings(s => s
				.NumberOfReplicas(1)
				.NumberOfShards(4))
				.Mappings(mm => mm.Map<IPLocation>(m => m
					.Properties(p => p
						.Ip(s => s.Name(n => n.IPAddressFrom))
						.Ip(s => s.Name(n => n.IPAddressTo))
						.Keyword(s => s.Name(n => n.CountryCode).DocValues())
						.Keyword(s => s.Name(n => n.State).DocValues())
						.Keyword(s => s.Name(n => n.City).DocValues())
						.Number(s => s.Name(n => n.Latitude).DocValues())
						.Number(s => s.Name(n => n.Longitude).DocValues())
						.Keyword(s => s.Name(n => n.Timezone).DocValues())
						.GeoPoint(s => s.Name(n => n.Pin)))));
		}

		protected override void AddElementsToIndex(BulkDescriptor desc)
		{
			AddElementsToIndex(desc, GetData());
		}

		private List<IPLocation> GetData()
		{
			var list = new List<IPLocation>();
			for(int i=1; i<100; i++)
			{
				list.Add(GetIPLocation(i));
			}
			return list;
		}

		private IPLocation GetIPLocation(int id)
		{
			IPLocation ip = new IPLocation()
			{
				Id = id,
				IPAddressFrom = "192.168." + id + ".1",
				IPAddressTo = "192.168." + id + ".255",
				CountryCode = "US",
				State = "CA"
			};
			return ip;
		}
	}
}
