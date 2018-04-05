using Elasticsearch.Net;
using Nest;
using NestExamples.CreateDelete;
using NestExamples.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NestExamples
{
	public class ElasticSearch
	{
		private readonly ElasticClient _client;

		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		private string _elasticServer;
		private string _indexName;
		private string _percolateIndexName;
		private IndexFromFile<User> idx;
		private IndexFromFile<UserPercolate> pidx;

		public ElasticSearch()
		{
			var nodes = ConfigurationManager.AppSettings["ElasticSearchNodes"];
			_indexName = ConfigurationManager.AppSettings["Index"];
			_percolateIndexName = ConfigurationManager.AppSettings["IndexPercolate"];
			if (string.IsNullOrEmpty(_indexName))
			{
				_indexName = "users" + DateTime.Now.Ticks;
			}
			if(string.IsNullOrEmpty(_percolateIndexName))
			{
				_percolateIndexName = "userpercolate";
			}

			if (!string.IsNullOrWhiteSpace(nodes))
			{
				var connectionPool = new StaticConnectionPool(nodes.Split(',').Select(uri => new Uri(uri)));
				_elasticServer = nodes.Split(',')[0];
				var config = new ConnectionSettings(connectionPool);
				config.DefaultIndex(this._indexName);

				var strMaxRetries = ConfigurationManager.AppSettings["MaxRetries"];
				int maxRetries;
				if (int.TryParse(strMaxRetries, out maxRetries) && maxRetries > 0)
				{
					config.MaximumRetries(maxRetries);
				}
				else
				{
					config.MaximumRetries(2);
				}
				config.DisableDirectStreaming();
				_client = new ElasticClient(config);
			}
			else
			{
				throw new ArgumentException("Elastic Search Nodes Configuration not available");
			}
			idx = new IndexFromFile<User>(_client, _indexName, "Schema/User.json");
			pidx = new IndexFromFile<UserPercolate>(_client, _percolateIndexName, "Schema/UserPercolate.json");
		}

		public ElasticClient GetClient()
		{
			return _client;
		}

		public void CreateIndex()
		{
			idx.DeleteIndexIfExists();
			idx.CreateIndex();
			pidx.DeleteIndexIfExists();
			pidx.CreateIndex();
		}

		public void DeleteIndex()
		{
			DeleteIndex(_indexName);
			DeleteIndex(_percolateIndexName);
		}

		public void DeleteIndex(string index)
		{
			var idx = new ElasticIndexBase<User>(_client, index);
			idx.DeleteIndex();
		}

		public void PopulateUsers()
		{
			idx.PopulateData(GetUsers());
			PopulatePercolateQueries();
		}

		public void ElasticQuery(SearchDescriptor<User> searchDescriptor)
		{
			idx.ExecuteQuery(searchDescriptor);
		}

		public SearchDescriptor<User> QueryUsersByState(string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			searchDescriptor.Query(q => q.Term(r => r.State, state));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryMoreUsersByState(string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			searchDescriptor.Query(q => q.Term(r => r.State, state));
			searchDescriptor.From(5);
			searchDescriptor.Size(100);
			return searchDescriptor;
		}

		public SearchDescriptor<User> QuerySortByState()
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			searchDescriptor.Sort(q => q.Field("state", SortOrder.Descending));
			searchDescriptor.From(0);
			searchDescriptor.Size(100);
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryIdBetween(int start, int end)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			searchDescriptor.Query(q => q.Range(r => r.Field("id").GreaterThanOrEquals(start)) && q.Range(r => r.Field("id").LessThanOrEquals(end)));
			searchDescriptor.Sort(q => q.Field("id", SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query5(int start, int end, string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			searchDescriptor.Query(q => (q.Range(r => r.Field("id").GreaterThanOrEquals(start)) && q.Range(r => r.Field("id").LessThanOrEquals(end))) || q.Term(r => r.State, state));
			searchDescriptor.Sort(q => q.Field("id", SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query6(int start, int end, string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Range(r => r.Field("id").GreaterThanOrEquals(start));
			QueryContainer query2 = Query<User>.Range(r => r.Field("id").LessThanOrEquals(end));
			QueryContainer query3 = Query<User>.Term(r => r.State, state);
			searchDescriptor.Query(q => q.Bool(r => r.Must(query1, query2, query3)));
			searchDescriptor.Sort(q => q.Field("id", SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query7(int start, int end, string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Range(r => r.Field(s => s.Id).GreaterThanOrEquals(start));
			QueryContainer query2 = Query<User>.Range(r => r.Field(s => s.Id).LessThanOrEquals(end));
			QueryContainer query3 = Query<User>.Term(r => r.State, state);
			searchDescriptor.Query(q => q.Bool(r => r.Must(query1, query2, query3)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query8(int start, int end, string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Range(r => r.Field(s => s.Id).GreaterThanOrEquals(start));
			QueryContainer query2 = Query<User>.Range(r => r.Field(s => s.Id).LessThanOrEquals(end));
			QueryContainer query12 = Query<User>.Bool(r => r.Must(query1, query2));
			QueryContainer query3 = Query<User>.Term(r => r.State, state);
			searchDescriptor.Query(q => q.Bool(r => r.Should(query12, query3)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query9(int start, int end, string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Range(r => r.Field(s => s.Id).GreaterThanOrEquals(start).LessThanOrEquals(end));
			QueryContainer query2 = Query<User>.Term(r => r.State, state);
			searchDescriptor.Query(q => q.Bool(r => r.Should(query1, query2)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query10(int start, int end, params string[] states)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Range(r => r.Field(s => s.Id).GreaterThanOrEquals(start).LessThanOrEquals(end));
			QueryContainer query2 = Query<User>.Terms(r => r.Field(s => s.State).Terms(states));
			searchDescriptor.Query(q => q.Bool(r => r.Should(query1, query2)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryName(string name)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Match(r => r.Field(f => f.Name).Query(name));
			searchDescriptor.Query(q => q.Bool(r => r.Must(query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryAutoComplete(string name)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Match(r => r.Field(f => f.Name.Suffix("autocomplete")).Query(name));
			searchDescriptor.Query(q => q.Bool(r => r.Must(query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query12()
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			searchDescriptor.Sort(q => q.Field(s => s.State, SortOrder.Ascending).Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query13()
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			SortDescriptor<User> sort = new SortDescriptor<User>();
			sort.Field("state", SortOrder.Descending);
			sort.Field("id", SortOrder.Descending);
			searchDescriptor.Sort(q => sort);
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query14()
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			SortDescriptor<User> sort = new SortDescriptor<User>();
			sort.Field(q => q.State, SortOrder.Descending);
			sort.Field(q => q.Id, SortOrder.Descending);
			searchDescriptor.Sort(q => sort);
			return searchDescriptor;
		}

		public SearchDescriptor<User> Query15()
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			SortDescriptor<User> sort = new SortDescriptor<User>();
			// Missing last is the default sort. So, no need to explicitly define this. 
			sort.Field(f => f.Field(q => q.State).Order(SortOrder.Descending).MissingLast());
			sort.Field(q => q.Id, SortOrder.Descending);
			searchDescriptor.Sort(q => sort);
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryNotState1(string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Match(r => r.Field(f => f.State).Query(state));
			searchDescriptor.Query(q => q.Bool(r => r.MustNot(query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryNotState2(string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Match(r => r.Field(f => f.State).Query(state));
			searchDescriptor.Query(q => q.Bool(r => r.Must(!query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryNotState3(string state)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.Match(r => r.Field(f => f.State).Query(state));
			searchDescriptor.Query(q => !query1);
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryByCreatedDate(DateTime date)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			QueryContainer query1 = Query<User>.DateRange(r => r.Field(f => f.CreatedDate).GreaterThanOrEquals(date));
			searchDescriptor.Query(q => q.Bool(r => r.Must(query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public SearchDescriptor<User> QueryByNestedField(string value)
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(_indexName);
			//QueryContainer query1 = Query<User>.Nested(r => r.Path("customData").Query(q => q.Bool(bq => bq.Must(
			//								m => m.Match(m1 => m1.Field(l => l.CustomData.Suffix("name")).Query("Name1")) 
			//								&& m.Match(m2 => m2.Field(l => l.CustomData.Suffix("value")).Query(value))))));

			// The following is better than above.
			QueryContainer query1 = Query<User>.Nested(r => r.Path("customData").Query(q => q.Bool(bq => bq.Must(
								m => m.Match(m1 => m1.Field(l => l.CustomData.Suffix("name")).Query("Name1")),
								m => m.Match(m2 => m2.Field(l => l.CustomData.Suffix("value")).Query(value))))));

			searchDescriptor.Query(q => q.Bool(r => r.Must(query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));
			return searchDescriptor;
		}

		public void SearchDescriptorToJson()
		{
			var searchDescriptor = new SearchDescriptor<User>().Index(new string[] { _indexName, "otherindex2" });
			QueryContainer query1 = Query<User>.DateRange(r => r.Field(f => f.CreatedDate).GreaterThanOrEquals(new DateTime(2010, 1, 1)));
			searchDescriptor.Query(q => q.Bool(r => r.Must(query1)));
			searchDescriptor.Sort(q => q.Field(s => s.Id, SortOrder.Ascending));

			Log.Info(((IUrlParameter)((ISearchRequest)searchDescriptor).Index).GetString(_client.ConnectionSettings));
			Log.Info(((IUrlParameter)((ISearchRequest)searchDescriptor).Index).GetString(new ElasticClient().ConnectionSettings));

			var memoryStream = new System.IO.MemoryStream();
			new ElasticClient().RequestResponseSerializer.Serialize(searchDescriptor, memoryStream);
			var json = Encoding.UTF8.GetString(memoryStream.ToArray());
			Log.Info(json);
		}

		public void QueryAggregator(Func<SearchDescriptor<User>, SearchDescriptor<User>> descriptor)
		{
			var response = _client.Search(descriptor);
			if (response != null)
			{
				Log.Debug(response.DebugInformation);
				Log.Debug("Result Count: " + response.Total);
				foreach (var user in response.Hits)
				{
					Log.Debug(user.Source.ToString());
				}
				var aggregations = response.Aggregations.Terms("state").Buckets.OfType<KeyedBucket<string>>();
				foreach (var aggregation in aggregations)
				{
					Log.Debug(aggregation.Key + " - " + aggregation.DocCount);
				}
			}
		}

		public Func<SearchDescriptor<User>, SearchDescriptor<User>> QueryAggergator1()
		{
			 return s => s
							.From(0)
							.Size(0)
							.Index(_indexName)
							.Query(q => q.Range(r => r.Field("id").GreaterThanOrEquals(5)) && q.Range(r => r.Field("id").LessThanOrEquals(60)))
							.Aggregations(a => a.Terms("state",
									c => c.Field(p => p.State).Size(30).Aggregations(g => g.ValueCount("stateCount", d => d.Field(e => e.State)))));
		}

		public Func<SearchDescriptor<User>, SearchDescriptor<User>> QueryAggergator2()
		{
			return s => s
					.From(0)
					.Size(0)
					.Index(_indexName)
					.Query(q => q.Range(r => r.Field("id").GreaterThanOrEquals(5)) && q.Range(r => r.Field("id").LessThanOrEquals(60)))
					.Aggregations(a => a.Terms("state", st => st
											.Field(p => p.State)
											.MinimumDocumentCount(2)
											.Size(20)
											.ShardSize(100)
											.ExecutionHint(TermsAggregationExecutionHint.Map)
											.Missing("n/a")
											.Script(ss => ss.Source("'State of Being: '+_value"))
											.Order(o => o
												.KeyAscending()
												.CountDescending()
											)
											.Meta(m => m
												.Add("foo", "bar")
											)));
		}

		public Func<SearchDescriptor<User>, SearchDescriptor<User>> QueryAggergator3()
		{
			return s => s
						   .From(0)
						   .Size(0)
						   .Index(_indexName)
						   .Query(q => q.Range(r => r.Field(f => f.Id).GreaterThanOrEquals(5)) && q.Range(r => r.Field(f => f.Id).LessThanOrEquals(60)))
						   .Aggregations(a => a.Terms("state",
								   c => c.Field(p => p.State).Size(30).Order(o => o.CountDescending())));
		}

		public void QueryAggergator4()
		{
			Func<SearchDescriptor<User>, SearchDescriptor<User>> descriptor = s => s
						   .From(0)
						   .Size(0)
						   .Index(_indexName)
						   .Aggregations(t => t.Filter("states", f1 => f1.Filter(f2 => f2
								.Range(r => r.Field(f => f.Id).GreaterThanOrEquals(5)) && f2.Range(r => r.Field(f => f.Id).LessThanOrEquals(60)))
								.Aggregations(a => a.Terms("state",
								   c => c.Field(p => p.State).Size(30).Order(o => o.CountDescending())))));

			var response = _client.Search(descriptor);
			if (response != null)
			{
				Log.Debug(response.DebugInformation);
				Log.Debug("Result Count: " + response.Total);
				foreach (var user in response.Hits)
				{
					Log.Debug(user.Source.ToString());
				}
				var aggregations = ((BucketAggregate)(((SingleBucketAggregate)response.Aggregations["states"])["state"])).Items.OfType<KeyedBucket<object>>();
				foreach (var aggregation in aggregations)
				{
					Log.Debug(aggregation.Key + " - " + aggregation.DocCount);
				}
			}
		}

		public QueryContainer GetQueryForPercolate1(string state)
		{
			QueryContainer query = Query<User>.Term(r => r.State, state);
			return query;
		}

		public void PopulatePercolateQueries()
		{
			List<string> emailIds = new RandomGenerator().GetEmailIds();
			int count = 1;
			List<UserPercolate> percolators = new List<UserPercolate>();
			string[] states = new string[] { "AP", "TN", "KA", "KL", "DL", "UP", "TS" };
			for(int i=0; i<20; i++)
			{
				UserPercolate percolator = new UserPercolate();
				percolator.SearchId = count++;
				percolator.SearchEmail = emailIds[ i % emailIds.Count];
				percolator.Query = GetQueryForPercolate1(states[i % states.Length]);
				percolators.Add(percolator);
			}
			pidx.PopulateData(percolators);
		}

		private void PercolateFromDocumentByState(string state)
		{
			User user = new User();
			user.State = state;
			var desc = new QueryContainerDescriptor<UserPercolate>()
								.Percolate(p => p.Document<User>(user).Field(q => q.Query));
			var searchDescriptor = new SearchDescriptor<UserPercolate>().Index(_percolateIndexName).Query(q => desc);
			pidx.ExecuteQuery(searchDescriptor);
		}

		private void PercolateFromDocumentByStates()
		{
			foreach(string state in new string[] { "AP", "TN", "KA", "KL", "DL", "UP", "TS" })
			{
				PercolateFromDocumentByState(state);
			}
		}

		private void PercolateFromESDoc()
		{
			for(int i=1; i<10; i++)
			{
				var desc = new QueryContainerDescriptor<UserPercolate>()
									.Percolate(p => p.Field(q => q.Query).Index(_indexName).Type("user").Id(i.ToString()));
				var searchDescriptor = new SearchDescriptor<UserPercolate>().Index(_percolateIndexName).Query(q => desc);
				pidx.ExecuteQuery(searchDescriptor);
			}
		}

		public void Query()
		{
			ElasticQuery(QueryUsersByState("TN"));
			ElasticQuery(QueryUsersByState("KL"));
			ElasticQuery(QueryUsersByState("KA"));
			ElasticQuery(QueryUsersByState("TS"));
			ElasticQuery(QueryUsersByState("MP"));
			ElasticQuery(QueryUsersByState("DL"));
			ElasticQuery(QueryUsersByState("UP"));

			ElasticQuery(QueryUsersByState("AP"));
			ElasticQuery(QueryMoreUsersByState("AP"));

			ElasticQuery(QuerySortByState());

			ElasticQuery(QueryIdBetween(25, 35));
			ElasticQuery(Query5(25, 35, "TN"));
			ElasticQuery(Query6(25, 35, "TN"));
			ElasticQuery(Query7(25, 35, "TN"));
			ElasticQuery(Query8(25, 35, "TN"));
			ElasticQuery(Query9(25, 35, "TN"));
			ElasticQuery(Query10(25, 35, "TN", "AP"));
			ElasticQuery(QueryAutoComplete("NA"));
			ElasticQuery(QueryAutoComplete("na"));
			ElasticQuery(QueryName("nagasatish"));
			ElasticQuery(QueryName("nagasatish tiruveedula"));
			ElasticQuery(Query12());
			ElasticQuery(Query13());
			ElasticQuery(Query14());
			ElasticQuery(Query15());
			ElasticQuery(QueryNotState1("AP"));
			ElasticQuery(QueryNotState2("AP"));
			ElasticQuery(QueryNotState3("AP"));
			ElasticQuery(QueryUsersByState("ap"));
			ElasticQuery(QueryByCreatedDate(new DateTime(2011, 1, 1)));
			ElasticQuery(QueryByNestedField("Value0"));
			SearchDescriptorToJson();
			QueryAggregator(QueryAggergator1());
			QueryAggregator(QueryAggergator2());
			QueryAggregator(QueryAggergator3());
			QueryAggergator4();
			PercolateFromDocumentByStates();
			PercolateFromESDoc();
		}

		private List<User> GetUsers()
		{
			List<User> users = new List<User>();
			RandomGenerator random = new RandomGenerator();
			for (int i=0; i<88; i++)
			{
				User user = random.GetUser(i+1);
				users.Add(user);
			}
			return users;
		}
	}
}
