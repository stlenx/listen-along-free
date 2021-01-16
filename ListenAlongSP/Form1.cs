using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using HtmlAgilityPack;
using RestSharp;

namespace ListenAlongSP
{
    public partial class Form1 : Form
    {
        private static string id = "";
        private static string progress = "";
        public Form1()
        {
            InitializeComponent();
        }

        private static async Task Call()
        {
            try
            {
                var client = new RestClient("https://api.spotify.com/v1/me/player?market=ES");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer BQBsSjvVOFwTLgiJ4E6OxoIBkYe0AsQ268blkrehsMCK2fjr7ug3d-Vpz6Qufej1G49U-m7TkekflQBnbz_PA2XP0UdSmdOmVDMXwBNgWDckCqsHOyGY5JIiRyjWLGJXs-WwXmoTEUUrUTvdcB3kIekxaNj7oOmvYWVmlB-imr2Z8RLOPaqutM8hRg0Orx5SPc653A0ruyEKXhtkGtvI4cmSBJuuXOpehXNRpa4ZOEWJBAZ5HSLj6b6cFtliMjOhj1wu1Ji9O-Je6NhnkAIt");
                
                var response = await client.ExecuteAsync(request);

                dynamic tmp = JsonConvert.DeserializeObject(response.Content);
                if (tmp != null)
                {
                    id = tmp.item.uri;
                    progress = tmp.progress_ms;
                    
                    await Queue();
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }
        }

        private static async Task Queue()
        {
            var client = new RestClient("https://api.spotify.com/v1/me/player/queue?uri=" + id);
            
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer BQAh6ADHMNE5Y2cMwadg2mwnWzuyn1mHUZfVsHMlI8FRfjyHCBr41mW8ddt3TsCOK37CS_WSPsQENiv88po_G7vj7Ybs5DqTyGHhD9W-CJlWJjlz3-XqyAwyiUclrQNo48UEG_16Sp1wu_I8Ozje652_iLDiql5I-242dCHDj2WBbMKTiOupE2s7_y7qZMdXo47l_Nl9rP1sRsFiBbY0YVakLDOakrODrYVFPpzRcZ_ih8V2SKsyZIVhXptxBtKw9txtLRvBmbXMSa6CZvD4-zBo88HC9Z9IdUAsCr4");
            var response = await client.ExecuteAsync(request);

            var client2 = new RestClient("https://api.spotify.com/v1/me/player/next");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("Accept", "application/json");
            request2.AddHeader("Content-Type", "application/json");
            request2.AddHeader("Authorization", "Bearer BQAh6ADHMNE5Y2cMwadg2mwnWzuyn1mHUZfVsHMlI8FRfjyHCBr41mW8ddt3TsCOK37CS_WSPsQENiv88po_G7vj7Ybs5DqTyGHhD9W-CJlWJjlz3-XqyAwyiUclrQNo48UEG_16Sp1wu_I8Ozje652_iLDiql5I-242dCHDj2WBbMKTiOupE2s7_y7qZMdXo47l_Nl9rP1sRsFiBbY0YVakLDOakrODrYVFPpzRcZ_ih8V2SKsyZIVhXptxBtKw9txtLRvBmbXMSa6CZvD4-zBo88HC9Z9IdUAsCr4");
            var response2 = await client2.ExecuteAsync(request2);

            var client3 = new RestClient("https://api.spotify.com/v1/me/player/seek?position_ms=" + progress);
            var request3 = new RestRequest(Method.PUT);
            request3.AddHeader("Accept", "application/json");
            request3.AddHeader("Content-Type", "application/json");
            request3.AddHeader("Authorization", "Bearer BQAh6ADHMNE5Y2cMwadg2mwnWzuyn1mHUZfVsHMlI8FRfjyHCBr41mW8ddt3TsCOK37CS_WSPsQENiv88po_G7vj7Ybs5DqTyGHhD9W-CJlWJjlz3-XqyAwyiUclrQNo48UEG_16Sp1wu_I8Ozje652_iLDiql5I-242dCHDj2WBbMKTiOupE2s7_y7qZMdXo47l_Nl9rP1sRsFiBbY0YVakLDOakrODrYVFPpzRcZ_ih8V2SKsyZIVhXptxBtKw9txtLRvBmbXMSa6CZvD4-zBo88HC9Z9IdUAsCr4");
            var response3 = await client3.ExecuteAsync(request3);
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