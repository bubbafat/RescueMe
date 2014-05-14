using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using System;

namespace RescueMe.Domain
{
    public class OutboundSmsMessageQueue
    {
        private const string SmsQueueName = "rescuemeoutboundsms";
        private const string SmsQueueConnectionStringKey = "RescueMe.Outbound.Queue.ConnectionString";

        private static volatile QueueClient _queue;
        private static readonly object _lock = new object();

        private static void InitializeQueue()
        {
            if (_queue == null)
            {
                lock (_lock)
                {
                    if (_queue == null)
                    {
                        string conn = CloudConfigurationManager.GetSetting(SmsQueueConnectionStringKey);

                        var namespaceManager = NamespaceManager.CreateFromConnectionString(conn);

                        if (!namespaceManager.QueueExists(SmsQueueName))
                        {
                            var desc = namespaceManager.CreateQueue(SmsQueueName);
                        }

                        _queue = QueueClient.CreateFromConnectionString(conn, SmsQueueName, ReceiveMode.PeekLock);
                    }
                }
            }
        }

        private static QueueClient Queue
        {
            get
            {
                InitializeQueue();
                return _queue;
            }
        }

        public static void Add(OutboundSmsMessage message)
        {
            BrokeredMessage queueMsg = new BrokeredMessage(message)
            {
                ScheduledEnqueueTimeUtc = message.ScheduledTime.UtcDateTime
            };

            Queue.Send(queueMsg);
        }

        public static void ProcessNext(Action<OutboundSmsMessage> callback)
        {
            try
            {
                // Wait an hour for the message.
                // Why an hour?  Totally arbitrary.
                var message = Queue.Receive(TimeSpan.FromHours(1));
                if (message != null)
                {
                    var sms = message.GetBody<OutboundSmsMessage>();
                    try
                    {
                        callback(sms);
                        message.Complete();
                    }
                    catch(Exception ex)
                    {
                        message.DeadLetter("Exception", ex.Message);
                        throw;
                    }
                }
            }
            catch (TimeoutException)
            {
                // Receive might throw on timeout
            }
        }
    }
}
