using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestExamples.Entities
{
	public class AutoCompleteItem
	{
		public string Name { get; set; }
		public ItemType AutocompleteType { get; set; }
		public string Type { get; set; }
		public string BID { get; set; }
		public string Neighborhood { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public int[] RecoIds { get; set; }
		public int[] ListingSourceIds { get; set; }
		public int ListingCount { get; set; }
		[Number(Ignore = true)]
		public float? Latitude { get; set; }
		[Number(Ignore = true)]
		public float? Longitude { get; set; }

		public string NameForSearch { get; set; }

		public GeoCoordinates Pin
		{
			get
			{
				if (!(Latitude.HasValue && Longitude.HasValue))
				{
					return null;
				}
				var lat = Math.Round(Latitude.Value, 8);
				var lon = Math.Round(Longitude.Value, 8);
				return new GeoCoordinates
				{
					Lat = lat,
					Lon = lon
				};
			}
			set
			{
				if (value == null)
				{
					Latitude = null;
					Longitude = null;
				}
				else
				{
					Latitude = (float)value.Lat;
					Longitude = (float)value.Lon;
				}
			}
		}
		public LandingPageInfo LandingPageInfo { get; set; }

		public double? Distance { get; set; }
		public Office[] Offices { get; set; }
	}

	public enum ItemType
	{
		FullAddress = 0,
		StreetName = 1,
		ListingNumber = 2,
		UspsCity = 3,
		UspsCounty = 4,
		UspsZip = 5,
		Neighborhood = 6,
		School = 7,
		ElementarySchool = 8,
		MiddleSchool = 9,
		HighSchool = 10,
		SchoolDistrict = 11,
		UspsState = 12,
		ListingCity = 13,
		LandingCity = 14,
		State = 15,
		City = 16,
		County = 17,
		Zip = 18,
		CustomArea = 19,
		LakeName = 20,
		Subdivision = 21,
		MlsArea = 22,
		NeighborhoodNumber = 23,
		Communities = 24,
		ListingNumberWithAddress = 25,
		RegionCity = 26,
		RegionZip = 27,
		NarDesignation = 28,
		RecoDesignation = 29,
		SpokenLangauge = 30
	}

	public class GeoCoordinates
	{
		public double Lat { get; set; }
		public double Lon { get; set; }
	}

	public class LandingPageInfo
	{
		public bool IsCustomized { get; set; }
		public string ThumbnailUrl { get; set; }
		public string Metadata { get; set; }
	}

	public class Office
	{
		public int Id;
		public string Name;
		public string Url;
		public int RecoId;
	}
}
