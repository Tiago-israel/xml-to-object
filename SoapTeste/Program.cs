using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SoapTeste
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("cep:");
            var cep = Console.ReadLine() ;
            var client = new HttpClient();
            var response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/xml/");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();

                var serializer = new XmlSerializer(typeof(Endereco));
                using (TextReader reader = new StringReader(result))
                {
                    var endereco = (Endereco)serializer.Deserialize(reader);
                    Console.WriteLine(endereco);
                }
            }
            
        }
    }

    [XmlRoot("xmlcep")]
    public class Endereco
    {
        [XmlElement("cep")]
        public string Cep { get; set; }
        [XmlElement("logradouro")]
        public string Logradouro { get; set; }
        [XmlElement("complemento")]
        public string Complemento { get; set; }
        [XmlElement("bairro")]
        public string Bairro { get; set; }
        [XmlElement("localidade")]
        public string Localidade { get; set; }
        [XmlElement("uf")]
        public string Uf { get; set; }

        public override string ToString()
        {
            return $"{Cep} - {Bairro} - {Uf}";
        }
    }
}
