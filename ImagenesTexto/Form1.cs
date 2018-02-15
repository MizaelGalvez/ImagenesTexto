using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImagenesTexto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();



        }



        // **********************************************
        // *** Update or verify the following values. ***
        // **********************************************

        // Replace the subscriptionKey string value with your valid subscription key.
        const string subscriptionKey = "";

        // Replace or verify the region.
        //
        // You must use the same region in your REST API call as you used to obtain your subscription keys.
        // For example, if you obtained your subscription keys from the westus region, replace 
        // "westcentralus" in the URI below with "westus".
        //
        // NOTE: Free trial subscription keys are generated in the westcentralus region, so if you are using
        // a free trial subscription key, you should not need to change this region.
        const string uriBase = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0/recognizeText";
        string imagen = "";
        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"‪C:\Users\Usuario\Desktop\";
            openFileDialog1.Filter = " JPG files (*.JPG)|*. JPG|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                        imagen = openFileDialog1.FileName;
                        pictureBox1.BackgroundImage = Image.FromFile(imagen);
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("El archivo seleccionado no es un tipo de imagen válido");
                }
            }

            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

            // Get the path and filename to process from the user.
            Console.WriteLine("Handwriting Recognition:");
            Console.Write("Enter the path to an image with handwritten text you wish to read: ");
            string imageFilePath = imagen;

            // Execute the REST API call.
            ReadHandwrittenText(imageFilePath);

            Console.WriteLine("\nPlease wait a moment for the results to appear. Then, press Enter to exit...\n");
            Console.ReadLine();
        }

        /// <summary>
        /// Gets the handwritten text from the specified image file by using the Computer Vision REST API.
        /// </summary>
        /// <param name="imageFilePath">The image file with handwritten text.</param>
        public async void ReadHandwrittenText(string imageFilePath)
        {
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameter. Set "handwriting" to false for printed text.
            string requestParameters = "handwriting=true";

            // Assemble the URI for the REST API Call.
            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response = null;

            // This operation requrires two REST API calls. One to submit the image for processing,
            // the other to retrieve the text found in the image. This value stores the REST API
            // location to call to retrieve the text.
            string operationLocation = null;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);
            ByteArrayContent content = new ByteArrayContent(byteData);

            // This example uses content type "application/octet-stream".
            // You can also use "application/json" and specify an image URL.
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            // The first REST call starts the async process to analyze the written text in the image.
            response = await client.PostAsync(uri, content);

            // The response contains the URI to retrieve the result of the process.
            if (response.IsSuccessStatusCode)
                operationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault();
            else
            {
                // Display the JSON error data.
                textBox1.Text = "\nError: en Dimenciones de la imagen o en peso del archivo, maximo 4 megas y una resolucion no mayor a 1080x900\n";
                Console.WriteLine("\nError:\n");
                Console.WriteLine(JsonPrettyPrint(await response.Content.ReadAsStringAsync()));
                return;
            }

            // The second REST call retrieves the text written in the image.
            //
            // Note: The response may not be immediately available. Handwriting recognition is an
            // async operation that can take a variable amount of time depending on the length
            // of the handwritten text. You may need to wait or retry this operation.
            //
            // This example checks once per second for ten seconds.
            string contentString;
            int i = 0;
            do
            {
                System.Threading.Thread.Sleep(1000);
                response = await client.GetAsync(operationLocation);
                contentString = await response.Content.ReadAsStringAsync();
                ++i;
            }
            while (i < 10 && contentString.IndexOf("\"status\":\"Succeeded\"") == -1);

            if (i == 10 && contentString.IndexOf("\"status\":\"Succeeded\"") == -1)
            {
                textBox1.Text = "\nError en Comunicacion con el Servidor\n";
                Console.WriteLine("\nTimeout error.\n");
                return;
            }

            // Display the JSON response.
            Console.WriteLine("\nResponse:\n");
            Console.WriteLine(JsonPrettyPrint(contentString));






                string json = (JsonPrettyPrint(contentString));

                JObject rss = JObject.Parse(json);
                string rssTitle = (string)rss["recognitionResult"]["text"];
            // James Newton-King

            string itemTitle1 = "";
            string itemTitle2 = "";
            string itemTitle3 = "";
            string itemTitle4 = "";
            string itemTitle5 = "";
            string itemTitle6 = "";
            string itemTitle7 = "";
            string itemTitle8 = "";

            try
            {
                 itemTitle1 = (string)rss["recognitionResult"]["lines"][0]["text"];
            }
            catch (Exception)
            {
                
            }
            try
            {
                 itemTitle2 = (string)rss["recognitionResult"]["lines"][1]["text"];
            }
            catch (Exception)
            {
                
            }
            try
            {
                 itemTitle3 = (string)rss["recognitionResult"]["lines"][2]["text"];
            }
            catch (Exception)
            {
                
            }
            try
            {
                 itemTitle4 = (string)rss["recognitionResult"]["lines"][3]["text"];
            }
            catch (Exception)
            {

            }
            try
            {
                itemTitle5 = (string)rss["recognitionResult"]["lines"][4]["text"];
            }
            catch (Exception)
            {

            }
            try
            {
                itemTitle6 = (string)rss["recognitionResult"]["lines"][5]["text"];
            }
            catch (Exception)
            {

            }
            try
            {
                itemTitle7 = (string)rss["recognitionResult"]["lines"][6]["text"];
            }
            catch (Exception)
            {

            }
            try
            {
                itemTitle8 = (string)rss["recognitionResult"]["lines"][7]["text"];
            }
            catch (Exception)
            {

            }
            // Json.NET 1.3 + New license + Now on CodePlex
            // ["Json.NET", "CodePlex"]

            // Json.NET
            // CodePlex
            textBox1.Text = textBox1.Text + "\n" + itemTitle1.ToString() +" ";
            textBox1.Text = textBox1.Text + "\n" + itemTitle2.ToString() + " ";
            textBox1.Text = textBox1.Text + "\n" + itemTitle3.ToString() + " ";
            textBox1.Text = textBox1.Text + "\n" + itemTitle4.ToString() + " ";
            textBox1.Text = textBox1.Text + "\n" + itemTitle5.ToString() + " ";
            textBox1.Text = textBox1.Text + "\n" + itemTitle6.ToString() + " ";
            textBox1.Text = textBox1.Text + "\n" + itemTitle7.ToString() + " ";
            textBox1.Text = textBox1.Text + "\n" + itemTitle8.ToString() + " ";
        }
        
    /// <summary>
    /// Returns the contents of the specified file as a byte array.
    /// </summary>
    /// <param name="imageFilePath">The image file to read.</param>
    /// <returns>The byte array of the image data.</returns>
    static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }


        /// <summary>
        /// Formats the given JSON string by adding line breaks and indents.
        /// </summary>
        /// <param name="json">The raw JSON string to format.</param>
        /// <returns>789e225204a046a0be0cf04e51278d5f</returns>
        public string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }

                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            try
                            {
                            sb.Append(new string(' ', ++offset * indentLength));

                            }
                            catch (Exception)
                            {
                                
                            }
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            try
                            {
                            sb.Append(new string(' ', --offset * indentLength));

                            }
                            catch (Exception)
                            {
                                
                            }
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            try
                            {
                            sb.Append(new string(' ', offset * indentLength));

                            }
                            catch (Exception)
                            {
                                
                            }
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }

            return sb.ToString().Trim();




        }


    }
}
