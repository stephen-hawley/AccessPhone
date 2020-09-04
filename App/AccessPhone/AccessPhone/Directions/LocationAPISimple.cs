using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;

namespace AccessPhone.Directions {
	public class LocationAPISimple {
		// base url: https://maps.googleapis.com/maps/api/place/findplacefromtext/json
		// arguments:
		// input=escaped search string
		// inputtype=textquery
		// fields=formatted_address,name,geometry
		// locationbias=point:latitude,longitude
		// key=API Key
		// json return:


		// base url: https://maps.googleapis.com/maps/api/place/nearbysearch/json
		// key=API Key
		// location=point:latitude,longitude
		// radius=meters
		// keyword=textquery
		// rankby=prominence   OR rankby=distance  CAN'T be used with radius

		// candidates  [
		//    {
		//       formatted_address string
		//       name string
		//       geometry {
		//           location {
		//              lat double
		//              lng double
		//           }
		//       }
		//    }
		//  ]
		//  status "OK"

		// formatted address - string
		// name - string
		// geometry {
		//    location {
		//       lat
		//       lng
		//    }
		// }

		const string locationurl = "https://maps.googleapis.com/maps/api/place/findplacefromtext/json";
		const string nearbyurl  = "https://maps.googleapis.com/maps/api/place/nearbysearch/json";

		string apiKey;
		HttpClient client;
		float radiusInMeters;

		public LocationAPISimple (string apiKey, HttpClient client, float radiusInMeters)
		{
			this.apiKey = apiKey;
			this.client = client;
			this.radiusInMeters = radiusInMeters;
		}

		public async Task<List<Destination>> FindNearestAsync (string query, Location sourceLocation, CancellationToken token)
		{
			InProgress = true;
			var result = new List<Destination> ();
			var uri = BuildNearbyQuery (query, sourceLocation);
			if (token.IsCancellationRequested)
				token.ThrowIfCancellationRequested ();
			try {
				var response = await client.GetAsync (uri, token);
				if (response.IsSuccessStatusCode) {
					var content = await response.Content.ReadAsStringAsync ();
					if (token.IsCancellationRequested)
						token.ThrowIfCancellationRequested ();
					var erroMessage = GetCandidates (content, result);
				}
			} catch (OperationCanceledException ex) {
				throw ex;
			}
			return result;
		}

		Uri BuildNearbyQuery (string query, Location sourceLocation)
		{
			var queryString = System.Web.HttpUtility.ParseQueryString (String.Empty);
			queryString.Add ("keyword", query);
			queryString.Add ("fields", "formatted_address,name,geometry");
			queryString.Add ("radius", radiusInMeters.ToString ());
			queryString.Add ("location", $"{sourceLocation.Latitude},{sourceLocation.Longitude}");
			queryString.Add ("key", apiKey);

			var builder = new UriBuilder (nearbyurl);
			builder.Query = queryString.ToString ();
			return builder.Uri;
		}

		Uri BuildLocationQuery (string query, Location sourceLocation)
		{
			var queryString = System.Web.HttpUtility.ParseQueryString (String.Empty);
			queryString.Add ("input", query);
			queryString.Add ("inputtype", "textquery");
			queryString.Add ("fields", "formatted_address,name,geometry");
			queryString.Add ("locationbias", $"point:{sourceLocation.Latitude},{sourceLocation.Longitude}");
			queryString.Add ("key", apiKey);

			var builder = new UriBuilder (locationurl);
			builder.Query = queryString.ToString ();
			return builder.Uri;
		}

		string GetCandidates (string jsonString, List<Destination> result)
		{
			var json = JObject.Parse (jsonString);
			var status = (string)json ["status"];
			// expected:
			// OK
			// ZERO_RESULTS
			// REQUEST_DENIED
			// INVALID_REQUEST
			// 
			if (status != "OK") {
				switch (status) {
				case "ZERO_RESULTS":
					return null;
				case "REQUEST_DENIED":
					return (string)json ["error_message"];
				case "INVALID_REQUEST":
					return ErrorMessageInJson (json) ?? "invalid request";
				default:
					return ErrorMessageInJson (json) ?? $"error - {status}";
				}
			}
			foreach (var cand in json ["results"]) {
// for location query
//			foreach (var cand in json ["candidates"]) {
					var dest = new Destination {
					Name = (string)cand ["name"],
// for location query
//					Address = (string)cand ["formatted_address"],
					Address = (string)cand ["vicinity"],
					GeoLocation = new Location ((double)cand ["geometry"] ["location"] ["lat"],
						(double)cand ["geometry"] ["location"] ["lng"])
				};
				result.Add (dest);
			}
			return null;
		}

		public bool CancelRequested { get; set; }
		public bool InProgress { get; private set; }

		static string ErrorMessageInJson (JObject json)
		{
			return json.ContainsKey ("error_message") ?
				(string)json ["error_message"] : null;
		}
	}
}
