using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace ListenAlongSP
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient Client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }

        private static async Task Call()
        {
            var clientHandler = new HttpClientHandler
            {
                UseCookies = false,
            };
            var client = new HttpClient(clientHandler);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://accounts.spotify.com/api/token"),
                Headers =
                {
                    { "Authorization", "Basic " + Base64Encode("4723033661d44c2a901ac89ac2e2b3b5") + ":" + Base64Encode("e849f86060864789ada7be019d9747a3") }
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" }
                })
            };
            Console.WriteLine("Cum");
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("see");
            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine(body);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Call();
        }
        
        private static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}