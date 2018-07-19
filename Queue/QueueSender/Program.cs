using System;
using System.Messaging;

namespace QueueSender
{
    internal class Program
    {
        private static void Main()
        {
            var msgQ = new MessageQueue(@".\Private$\test");

            while (true)
            {
                var msgBody = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(msgBody))
                    break;

                Message msg = new Message
                {
                    Body = msgBody
                };
                msgQ.Send(msg);
            }
        }
    }
}
