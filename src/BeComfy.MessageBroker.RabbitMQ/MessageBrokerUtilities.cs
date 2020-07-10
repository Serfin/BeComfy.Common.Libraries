using System.Text;

namespace BeComfy.MessageBroker.RabbitMQ
{
    public static class MessageBrokerUtilities
    {
        public static string ToKebabCase(this string input)
        {
            string output = string.Empty;
            byte[] byteArr = Encoding.UTF8.GetBytes(input);

            for (var i = 0; i < byteArr.Length; i++)
            {
                if (byteArr[i] > 64 && byteArr[i] < 91)
                {
                    if (i == 0)
                    {
                        output += (char) (byteArr[i] + 32);
                    }
                    else 
                    {
                        output += string.Concat('-', (char) (byteArr[i] + 32));
                    }
                }
                else
                {
                    output += (char)byteArr[i];
                }
            }

            return output;
        }    

        public static string GetRoutingKey<TMessage>()
            => typeof(TMessage).Name.ToKebabCase();
    }
}