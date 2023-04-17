using System;
using System.Globalization;
using System.IO;

class Program 
{
    static void Main() 
    {
        CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

        Console.WriteLine("----------     INTERPRETADOR DE REQUISIÇÃO PROTOCOLO HTTP     ----------");
        
        bool flag = false;
        int typeHttp = 0;

        while(!flag) {
            Console.Write("\nDigite 1 se você for interpretar uma requisição ou 2 se for interpretar uma resposta: ");

            typeHttp = Convert.ToInt32(Console.ReadLine());

            if(typeHttp > 2 || typeHttp < 1) {
              Console.Write("\nOpção inválida");
            } else {
                flag = true;
            }
        }

        Console.Write("\nDigite a URL do arquivo: ");

        String? url = Console.ReadLine();

        Console.WriteLine("\nEsta é sua URL: " + url);
        Console.WriteLine("\nTentando acessar arquivo...\n");

        try {
            String fileContent = File.ReadAllText(url!);

            var http = fileContent.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            String head = http[0];

            Console.WriteLine("HEAD:");
            Console.WriteLine(head);

            var headInfo = head.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            String firstLine = headInfo[0];

            String httpMetodOrStatus = "";

            if(typeHttp == 1) {
                httpMetodOrStatus = firstLine.Split(' ')[0];
            } else {
                httpMetodOrStatus = firstLine.Split(' ')[1];
            }
           
            Console.WriteLine("\nMÉTODO HTTP OU STATUS CODE: \n" + httpMetodOrStatus + "\n");

            if (http.Length > 1) {
                var body = http[1];
                Console.WriteLine("CORPO DA MENSAGEM:");
                Console.WriteLine(body);
            } else {
                Console.WriteLine("Esta requisão não possui um corpo");
            }

        } catch (Exception ex) {
            Console.WriteLine("/////////////////////////////////////////");
            Console.WriteLine("ERRO NA LEITURA: " + ex.Message.ToString());
            Console.WriteLine("/////////////////////////////////////////");
        }
    }
}