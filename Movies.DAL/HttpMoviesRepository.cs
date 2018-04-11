using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Movies.DomainModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Movies.DAL
{
    public class HttpMoviesRepository : IMoviesRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// A static HttpClient to prevent excessive socket use.
        /// </summary>
        private static readonly HttpClient HttpClient = new HttpClient();

        public HttpMoviesRepository(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        private string EndpointUrl
        {
            get
            {
                var endpointUrl = _configuration["EndpointURL"];
                if (string.IsNullOrEmpty(endpointUrl))
                {
                    throw new Exception("Missing configuration: EndpointURL");
                }

                return endpointUrl;
            }
        }

        public DomainModel.Movies GetAllMovies()
        {
            var movies = new DomainModel.Movies();
            if (!_memoryCache.TryGetValue(EndpointUrl, out List<Movie> listOfMovies))
            {
                var task = HttpClient.GetAsync(EndpointUrl)
                    .ContinueWith(taskwithresponse =>
                    {
                        var response = taskwithresponse.Result;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var jsonString = response.Content.ReadAsStringAsync();
                            jsonString.Wait();
                            listOfMovies = ValidateAndParseResponse(jsonString.Result);
                        }
                    });
                task.Wait();
                _memoryCache.Set(EndpointUrl, listOfMovies, TimeSpan.FromMinutes(60));
            }

            movies.Initialise(listOfMovies);
            return movies;
        }

        /// <summary>
        /// Validate the received JSON string against the our object structure.  If it's valid
        /// then create and populate the <see cref="Movie"/> and <see cref="Role"/> objects.
        /// </summary>
        /// <param name="jsonString">Received JSON string.</param>
        /// <exception cref="System.Exception">Thrown if the response is invalid JSON or mandatory data is missing.</exception>
        /// <returns>List of <see cref="Movie"/> objects.</returns>
        private List<Movie> ValidateAndParseResponse(string jsonString)
        {
            var generator = new JSchemaGenerator();
            var parsedSchema = generator.Generate(typeof(List<Movie>));
            var reader = new JsonTextReader(new StringReader(jsonString));

            var validatingReader = new JSchemaValidatingReader(reader)
            {
                Schema = parsedSchema
            };

            var messages = new List<string>();
            validatingReader.ValidationEventHandler += (o, a) => messages.Add(a.Message);

            try
            {
                var serializer = new JsonSerializer();
                var movies = serializer.Deserialize<List<Movie>>(validatingReader);

                if (messages.Count > 0)
                {
                    var exception = new Exception("Invalid Response : Missing Mandatory Data");
                    exception.Data.Add("ValidationErrors", messages);
                    throw exception;
                }

                return movies;
            }
            catch (JsonReaderException ex)
            {
                throw new Exception("Invalid Response : Invalid Json", ex);
            }
        }
    }
}
