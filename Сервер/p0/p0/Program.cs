using System;
using System.Text;
// Обратите внимание, данную библиотеку нужно будет подключить
using System.ServiceModel;

namespace Server
{
    public class lab5
    {

        [ServiceContract]
        public interface IMyService
        {
            [OperationContract]
            int GetText(string Str);

            [OperationContract]
            string GetStr();
        }
        // Реализация методов, которые описаны в интерфейсе
        public static string Mystr="";
        public static int n = 0;
        public class MyService : IMyService
        {
            
            public int GetText(string Str)
            {
                int k = 0;
                for (int i = 0; i < Str.Length; i++)
                {
                    if ((Str[i] == ' ') && (Str[i + 1] != ' '))
                        k++;
                    if (Str[i] == '\n')
                    {                        
                        n++;
                        k++;
                    }
                }
                Mystr += Str;
                Console.WriteLine(Str);
                Console.WriteLine(">>Количество слов в тексте: " + k);
                Console.WriteLine(">>Количество строк в тексте: " + n);
                //Console.WriteLine(n + " BBBBBBBBBb ");
                return k;
            }

            public string GetStr()
            {
               // Console.WriteLine(Mystr + " BBBBBBBBBb ");
                string Str = ">>Количество строк в тексте: " + n+"\n";
                int j = 0, k = 1;
                for (int i = 0; i < Mystr.Length; i++)
                {
                    if (Mystr[i] == '\n')
                    {
                        Str += Mystr.Substring(j, (i-j+1));
                        j=i+1;
                        Str += ">>Количество слов в тексте: " + k + '\n';
                        k = 1;
                    }
                    else                    
                        if ((Mystr[i] == ' ') && (Mystr[i+1] != ' '))
                            k++;
                    
                }
                Console.WriteLine(Str);
                return Str;
            }


        }
        class Program
        {
            static void Main(string[] args)
            {
                ServiceHost host = new ServiceHost(typeof(MyService), new Uri("http://localhost:8000/MyService"));
                host.AddServiceEndpoint(typeof(IMyService), new BasicHttpBinding(), "");
                host.Open();
                Console.WriteLine("Сервер запущен");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}