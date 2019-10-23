using System;
using System.ServiceModel;

namespace Client
{
    // Тоже сюда закинул интерфейс, можно выделить в отдельную библиотеку
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        int GetText(string Str);

        [OperationContract]
        string GetStr();
    }
    class Program
    {
        static void Main(string[] args)
        {
            Uri tcpUri = new Uri("http://localhost:8000/MyService");
            EndpointAddress address = new EndpointAddress(tcpUri);
            BasicHttpBinding binding = new BasicHttpBinding();
            ChannelFactory<IMyService> factory = new ChannelFactory<IMyService>(binding, address);
            IMyService service = factory.CreateChannel();
            String Str="", Str2="", StrForInter="";
            while (StrForInter != "3")
            {
                Console.WriteLine("\n>>>Нажмите 1, чтобы ввести текст.\n>>>Нажмите 2, чтобы получить множество всех строки.\n>>>Нажмите 3, чтобы выйти");
                StrForInter = Console.ReadLine();
                switch (StrForInter)
                {
                    case "1":
                        Console.WriteLine(">>>Введите '<<', чтобы отправить сообщение");
                        do
                        {
                            Str += Str2;
                            Str2 = Console.ReadLine() + '\n';
                        }
                        while (Str2 != "<<\n");
                        Console.WriteLine(">>Количество слов в тексте: " + service.GetText(Str));
                        break;
                    case "2":
                        Console.Write(service.GetStr());
                        break;
                }
            }
            Console.ReadLine();
        }
    }
}
