using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Business.CrossCuttingConcerns.Translate.GoogleTranslate
{
    public class WordTranslateHelper : IWordTranslateService
    {
        private readonly HttpClient _client;
        private readonly TranslateModel _model;

        public WordTranslateHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _client = new HttpClient();

            _model = Configuration.GetSection("TranslateModel").Get<TranslateModel>();
        }

        public IConfiguration Configuration { get; set; }

        public string Detect(string word)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2/detect"),
                Headers =
                {
                    { "x-rapidapi-key", _model.XRapidapiKey },
                    { "x-rapidapi-host", _model.XRapidapiHost },
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "q", word },
                }),
            };

            using (var response = _client.SendAsync(request).Result)
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().Result;
                dynamic stuff = JsonConvert.DeserializeObject(body);
                var language = stuff.data.detections[0][0].language;
                return language;
            }
        }

        public List<string> GetLanguages()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2/languages"),
                Headers =
                {
                    { "x-rapidapi-key", _model.XRapidapiKey },
                    { "x-rapidapi-host", _model.XRapidapiHost },
                },
            };

            using (var response = _client.SendAsync(request).Result)
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().Result;
                dynamic stuff = JsonConvert.DeserializeObject(body);

                var languages = new List<string>();
                foreach (var item in stuff.data.languages)
                {
                    string x = item["language"];
                    if (x != null)
                        languages.Add(x);
                }

                return languages;
            }
        }

        public string Translate(string word, string target, string source)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2"),
                Headers =
                {
                    { "x-rapidapi-key", _model.XRapidapiKey },
                    { "x-rapidapi-host", _model.XRapidapiHost },
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "q", word },
                    { "target", target },
                    { "source", source },
                }),
            };

            using (var response = _client.SendAsync(request).Result)
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().Result;
                dynamic stuff = JsonConvert.DeserializeObject(body);
                var translatedText = stuff.data.translations[0].translatedText;

                return translatedText;
            }
        }
    }

    public class TranslateModel
    {
        public string XRapidapiKey { get; set; }
        public string XRapidapiHost { get; set; }
    }
}
