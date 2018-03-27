using Nest;
using NestExamples.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestExamples.CreateDelete
{
	public class IPLocationIndex : ElasticIndexBase<IPLocation>
	{
		public IPLocationIndex(ElasticClient client, string indexName) : base(client, indexName)
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

		public override void ExecuteQueries()
		{
			ExecuteQuery(GetGreaterThan("192.168.90.255"));
			ExecuteQuery(GetGreaterThan("19.216.89.255"));
			ExecuteQuery(GetGreaterThan("99.216.89.255"));
			ExecuteQuery(GetGreaterThan("200.0.0.0"));
			ExecuteQuery(GetGreaterThan("1.0.0.0"));
			ExecuteQuery(GetGreaterThan("10.0.0.0"));
			ExecuteQuery(GetGreaterThan("100.0.0.0"));
			ExecuteQuery(GetGreaterThan("10.1.0.0"));
			ExecuteQuery(GetGreaterThan("10.8.0.0"));
			ExecuteQuery(GetGreaterThan("10.10.0.0"));
		}

		private SearchDescriptor<IPLocation> GetGreaterThan(string ip)
		{
			var searchDescriptor = new SearchDescriptor<IPLocation>().Index(_indexName);
			searchDescriptor.Query(q => q.TermRange(r => r.Field(f => f.IPAddressFrom).GreaterThanOrEquals(ip)));
			searchDescriptor.Sort(f => f.Ascending(r => r.IPAddressFrom));
			searchDescriptor.Size(20);
			return searchDescriptor;
		}

		private List<IPLocation> GetData()
		{
			var list = new List<IPLocation>();
			for(int i=1; i<99; i++)
			{
				list.Add(GetIPLocation(i));
			}
			return list;
		}

		private IPLocation GetIPLocation(int id)
		{
			int f = (id - 1) / 49 == 0 ? 10 : 192;
			int s = GetIndexNumber(((id-1)%49)/7);
			int t = GetIndexNumber((id - 1) % 7);
			IPLocation ip = new IPLocation()
			{
				Id = id,
				IPAddressFrom = f + "." + s + "." + t + ".1",
				IPAddressTo = f + "." + s + "." + t + ".255",
				CountryCode = "US",
				State = "CA"
			};
			return ip;
		}

		private int GetIndexNumber(int n)
		{
			switch(n%7)
			{
				case 0: return 1;
				case 1: return 9;
				case 2: return 10;
				case 3: return 90;
				case 4: return 100;
				case 5: return 200;
				case 6: return 255;
			}
			return 10;
		}
	}
}
