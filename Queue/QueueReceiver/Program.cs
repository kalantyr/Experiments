using System;
using System.Messaging;

namespace QueueReceiver
{
    internal class Program
    {
        private static void Main()
        {
            var msgQ = new MessageQueue(@".\Private$\test")
            {
                Formatter = new XmlMessageFormatter(new[] {typeof(string)})
            };

            while (true)
            {
                var message = msgQ.Receive();
                Console.WriteLine(message.Body);
            }
        }
    }
}
