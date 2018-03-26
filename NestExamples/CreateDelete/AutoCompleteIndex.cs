using Nest;
using NestExamples.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NestExamples.CreateDelete
{
	public class AutoCompleteIndex : ElasticIndexBase
	{
		public AutoCompleteIndex(string indexName) : base(indexName)
		{
		}

		protected override Func<CreateIndexDescriptor, CreateIndexDescriptor> GetCreateIndexDescriptor()
		{
			Func<CreateIndexDescriptor, CreateIndexDescriptor> CreateIndexFunc =
				i => i.Settings(s => s
						.NumberOfReplicas(2)
						.NumberOfShards(1) // Since we separated out these into different types they are very small
						.Analysis(a => a
							.TokenFilters(f =>
								f.PatternReplace("punctuation_replace", p => p.Pattern(@"[^\w\s]|_").Replacement(""))
								.PatternReplace("multispace_replace", p => p.Pattern(@"\s+").Replacement(" ")))
							.CharFilters(cf => cf.Mapping("lowercase_replace", p => p.Mappings(
										new[] {
													"A => a",
													"B => b",
													"C => c",
													"D => d",
													"E => e",
													"F => f",
													"G => g",
													"H => h",
													"I => i",
													"J => j",
													"K => k",
													"L => l",
													"M => m",
													"N => n",
													"O => o",
													"P => p",
													"Q => q",
													"R => r",
													"S => s",
													"T => t",
													"U => u",
													"V => v",
													"W => w",
													"X => x",
													"Y => y",
													"Z => z"
									}
								)))
							.Tokenizers(t => t.EdgeNGram("edge_tokenizer", p => p.MinGram(1).MaxGram(20).TokenChars(new TokenChar[] { })))
							.Analyzers(an => an.Custom("edge_analyzer",
											p => p.Filters(
													new[] {
														"lowercase",
														"asciifolding",
														"punctuation_replace",
														"multispace_replace"
													}
												).CharFilters(new[] {
																"lowercase_replace",
												}).Tokenizer("edge_tokenizer"))
										.Custom("keyword_analyzer",
											p => p.Filters(new[] {
													"lowercase",
													"asciifolding",
													"punctuation_replace",
													"multispace_replace"
											}).CharFilters(new[] {
													"lowercase_replace"
											}).Tokenizer("keyword")))))
					.Mappings(m => m.Map<AutoCompleteItem>(mm => mm
									.Properties(p => p
										.Text(s => s.Name(a => a.Name).Analyzer("keyword_analyzer"))
										.Text(s => s.Name(a => a.NameForSearch)
													.Analyzer("keyword_analyzer")
													.Fields(f => f.Text(s1 => s1.Name("autocomplete")
																				.Analyzer("edge_analyzer")
																				.SearchAnalyzer("keyword_analyzer"))))
										.Number(s => s.Name(a => a.AutocompleteType).DocValues())
										.Keyword(s => s.Name(a => a.Type).Index(false))
										.Keyword(s => s.Name(a => a.BID).Index(false))
										.Text(s => s.Name(a => a.City).Analyzer("keyword_analyzer"))
										.Text(s => s.Name(a => a.State).Analyzer("keyword_analyzer"))
										.Number(n => n.Name(a => a.RecoIds).DocValues())
										.Number(n => n.Name(a => a.ListingSourceIds).DocValues())
										.Number(n => n.Name(a => a.ListingCount).DocValues())
										.GeoPoint(g => g.Name(a => a.Pin))
										.Object<LandingPageInfo>(o => o.Name(a => a.LandingPageInfo)
																			.Properties(p2 => p2
																					.Boolean(b => b.Name(a => a.IsCustomized).DocValues())
																					.Keyword(s => s.Name(a => a.ThumbnailUrl))
												)
										))));

			return CreateIndexFunc;
		}
	}
}
