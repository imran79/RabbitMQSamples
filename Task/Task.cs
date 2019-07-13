using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Framing;


    class Task
    {
        static string getMessages(string[] args){
           return args.Length > 0 ? string.Join(string.Empty, args) : "Hello World"; 
        }
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory(){HostName = "localhost"};
            using(var conection = factory.CreateConnection())
            using(var channel = conection.CreateModel()){
                channel.QueueDeclare("task_queue", durable: true, exclusive:false, autoDelete:false, arguments: null);
                var message = getMessages(args);
                var body = Encoding.UTF8.GetBytes(message);
                var poperties = channel.CreateBasicProperties();
                poperties.Persistent = true;
                channel.BasicPublish(exchange: "",routingKey: "task_queue",
                basicProperties: poperties, body:body);

                

            }
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();

        }
    }

