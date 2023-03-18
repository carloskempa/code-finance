using CodeFinance.Infra.Queue.Abstractions;
using Newtonsoft.Json;
using System.Text;

namespace CodeFinance.Infra.Queue.RabbitMQ
{
    public class MQMessage : IMQMessage
    {
        public string ModelName { get; set; }
        public string Body { get; set; }

        public byte[] ToBytes()
        {
            return Encoding.UTF8.GetBytes(ToJson());
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(new
            {
                this.ModelName,
                this.Body
            });
        }

        public static MQMessage FromBytes(byte[] body)
        {
            var jsonString = Encoding.UTF8.GetString(body);
            var resultado = JsonConvert.DeserializeObject<MQMessage>(jsonString);

            return resultado;
        }
    }
}
