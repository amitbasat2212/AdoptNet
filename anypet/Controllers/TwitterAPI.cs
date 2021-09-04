using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace anypet.Controllers
{
	public class TwitterAPI : Controller
	{
		const string TwitterApiBaseUrl = "https://api.twitter.com/1.1/";
		string consumerKey, consumerKeySecret, accessToken, accessTokenSecret;
		// class "HMACSHA1" hash-based message authentication code =>  is a specific type of
		// message authentication code mixes a secret key with the message data
		HMACSHA1 sigHasher;
		DateTime epochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Creates an object for sending tweets to Twitter using Single-user OAuth.
		// OAuth - protocol in order to connect to Twitter
		// Constructor
		public TwitterAPI(string consumerKey, string consumerKeySecret, string accessToken, string accessTokenSecret)
		{
			this.consumerKey = consumerKey;
			this.consumerKeySecret = consumerKeySecret;
			this.accessToken = accessToken;
			this.accessTokenSecret = accessTokenSecret;

			// creating the hash code
			sigHasher = new HMACSHA1(new ASCIIEncoding().GetBytes(string.Format("{0}&{1}", consumerKeySecret, accessTokenSecret)));
		}

		// Sends a tweet with the supplied text and returns the response from the
		// Twitter API.
		public Task<string> Tweet(string text)
		{
			// Dictionary is built on key and value like map
			var data = new Dictionary<string, string> {
			{ "status", text },
			{ "trim_user", "1" }
        };

            return SendRequest("statuses/update.json", data); // data is the dictionary 
			// the satatuses is the url string
        }

		// send to the server of twitter anything we need
        Task<string> SendRequest(string url, Dictionary<string, string> data)
		{
			var fullUrl = TwitterApiBaseUrl + url; // creating new hash url with my own 
			// key code generated

			// Timestamps are in seconds since 1/1/1970.
			var timestamp = (int)((DateTime.UtcNow - epochUtc).TotalSeconds);

			// Add all the OAuth headers we'll need to use when constructing the hash.
			data.Add("oauth_consumer_key", consumerKey);
			data.Add("oauth_signature_method", "HMAC-SHA1");
			data.Add("oauth_timestamp", timestamp.ToString());
			data.Add("oauth_nonce", "a"); // Required, but Twitter doesn't appear to
										  // use it, so "a" will do.
			data.Add("oauth_token", accessToken);
			data.Add("oauth_version", "1.0");

			// Generate the OAuth signature and add it to our payload.
			data.Add("oauth_signature", GenerateSignature(fullUrl, data));

			// Build the OAuth HTTP Header from the data.
			string oAuthHeader = GenerateOAuthHeader(data);

			// Build the form data (exclude OAuth stuff that's already in the header).
			// A container for name/value tuples encoded using application/x-www-form-urlencoded MIME type.
			var formData = new FormUrlEncodedContent(data.Where(kvp => !kvp.Key.StartsWith("oauth_")));

			return SendRequest(fullUrl, oAuthHeader, formData);
		}

		// Generate an OAuth signature from OAuth header values.
		string GenerateSignature(string url, Dictionary<string, string> data)
		{
			var sigString = string.Join(
				"&",
				data
					.Union(data)
					.Select(kvp => string.Format("{0}={1}", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value)))
					.OrderBy(s => s)
			);

			var fullSigData = string.Format(
				"{0}&{1}&{2}",
				"POST",
				Uri.EscapeDataString(url),
				Uri.EscapeDataString(sigString.ToString())
			);

			return Convert.ToBase64String(sigHasher.ComputeHash(new ASCIIEncoding().GetBytes(fullSigData.ToString())));
		}

		// Generate the raw OAuth HTML header from the values (including signature).
		string GenerateOAuthHeader(Dictionary<string, string> data)
		{
			return "OAuth " + string.Join(
				", ",
				data
					.Where(kvp => kvp.Key.StartsWith("oauth_"))
					.Select(kvp => string.Format("{0}=\"{1}\"", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value)))
					.OrderBy(s => s)
			);
		}

		// Send HTTP Request and return the response.
		async Task<string> SendRequest(string fullUrl, string oAuthHeader, FormUrlEncodedContent formData)
		{
			using (var http = new HttpClient())
			{
				http.DefaultRequestHeaders.Add("Authorization", oAuthHeader);

				var httpResp = await http.PostAsync(fullUrl, formData);
				var respBody = await httpResp.Content.ReadAsStringAsync();

				return respBody;
			}
		}
	}
}