using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestExamples.Entities
{
	public class IPLocation
	{
		public long Id { get; set; }
		[Ip]
		public string IPAddressFrom { get; set; }
		[Ip]
		public string IPAddressTo { get; set; }
		public string CountryCode { get; set; }
		public string State { get; set; }
		public string City { get; set; }
		public float? Latitude { get; set; }
		public float? Longitude { get; set; }
		public string Timezone { get; set; }
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
	}
}
