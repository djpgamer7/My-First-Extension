using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Maca134.Arma.DllExport;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace A3_test {

	public class A3_test
	{

		WebRequest wRUrl;

		[ArmaDllExport]
		public static string Invoke(string input, int size)
		{
			string[] args = input.Split('|');
			string url = args[0];
			string requestType = args[1];

			if (url == " " || url == null)
			{
				return "Error in url!";
			}

			if (requestType == " " || requestType == null)
			{
				return "Error in request type!";
			}

			if (requestType == "GET" || requestType == "get")
			{
				return getRequest(url);

			}

			if (requestType == "POST" || requestType == "post")
			{
				if (args[2] == "" || args[2] == null)
				{
					return "Error in post request";
				}

				return postRequest(url, args[2].Split(','));

			}


			//string requestResponse = getRequest(url);

			//return requestResponse;

			return "Error";
		}

		public static string getRequest(string url)
		{

			var client = new RestClient(url);

			var request = new RestRequest();

			var response = client.Get(request);

			return response.Content.ToString();
		}

		public static string postRequest(string url, string[] requestArray)
		{
			var client = new RestClient(url);
			var request = new RestRequest();

			//request.AddJsonBody(requestBody);

			//var response = client.Post(request);

			//|embedauthor,embedname,embedtitle,embeddescription,embedcolor,embedfootertext

			Author embedAuthor1 = new Author
			{
				name = requestArray[0],
				url = "",
				icon_url = ""
			};
			
			Footer embedFooter1 = new Footer
			{
				text = requestArray[4],
				icon_url = ""
			};

			List<Embed> myEmbeds = new List<Embed>
			{
				new Embed
                {
					author = embedAuthor1,
					title = requestArray[1],
					url = "",
					description = requestArray[2],
					color = 2199199,
					footer = embedFooter1
				}
				
			};

			DiscordEmbed compiledEmbedMessage = new DiscordEmbed
			{
				username = "AST Bot",
				avatar_url = "",
				content = "",
				embeds = myEmbeds
			};
			request.AddJsonBody(compiledEmbedMessage);

			var response = client.Post(request);

			return compiledEmbedMessage.ToString();

			//return response.StatusCode.ToString();
			//return response.StatusCode.ToString();
		}

	}
	public class Author
	{
		public string name { get; set; }
		public string url { get; set; }
		public string icon_url { get; set; }
	}

	public class Footer
	{
		public string text { get; set; }
		public string icon_url { get; set; }
	}

	public class Embed
	{
		public Author author { get; set; }
		public string title { get; set; }
		public string url { get; set; }
		public string description { get; set; }
		public int color { get; set; }
		public Footer footer { get; set; }
	}

	public class DiscordEmbed
	{
		public string username { get; set; }
		public string avatar_url { get; set; }
		public string content { get; set; }
		public List<Embed> embeds { get; set; }
	}

}