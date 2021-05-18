using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
namespace TinyBrowser
{
	public static class ServerBrowser
	{
        static string host = "www.acme.com";
        static TcpClient tcpClient;
        static int port = 80;
        static StreamReader streamReader;
        static StreamWriter streamWriter;
        static NetworkStream networkStream;
        static string uri = "/";
        static string request;
        public static void SBrowser()
        {
            Console.WriteLine("Insert Web Adress here");
            host = Console.ReadLine();
            if (host.Length < 6)
            {
                host = "www.acme.com";
                Console.WriteLine($"Invalid Adress revert back to default {host}");
            }
            Console.WriteLine("Is this the Adress you want to use?");
            Console.WriteLine($"{host}");
            Console.WriteLine("[1] Yes, [0] No");
            var input1 = int.Parse(Console.ReadLine());
            while (input1 == 0)
            {
                Console.WriteLine("Please enter the Web adress here");
                host = Console.ReadLine();
                Console.WriteLine($"Is this the Adress you want to use?");
                Console.WriteLine($"{host}");
                Console.WriteLine("[1] Yes, [0] No");
                var input2 = int.Parse(Console.ReadLine());
                if (input2 == 1)
                {
                    break;
                }
            }
            Console.WriteLine($"{host} have been selected as Adress");
            TcpConnect();
            WebRequest();
            var webReader = WebReader();
            Console.WriteLine($"{WebTitle(webReader)}");
            Console.ReadKey();
        }
        static void TcpConnect()
        {
            tcpClient = new TcpClient(host, port);
            networkStream = tcpClient.GetStream();
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream, Encoding.ASCII);
            Console.WriteLine(networkStream.Socket.RemoteEndPoint);
        }
        static void WebRequest()
        {
            request = $"GET {uri} HTTP/1.1\r\nHost: {host}\r\n\r\n";
            streamWriter.AutoFlush = true;
            streamWriter.Write(request);
            Console.WriteLine($"{request}");
        }
        static string WebReader()
        {
            if (networkStream.CanRead)
            {
                var answer = streamReader.ReadToEnd();
                return answer;
            }
            Console.WriteLine("Error: 404");
            return string.Empty;
        }
        static string WebTitle(string answer)
        {
            const string first = "<Title>";
            const string last = "</Title>";
            var firstIndex = answer.IndexOf(first, StringComparison.OrdinalIgnoreCase);
            firstIndex += first.Length;
            var lastIndex = answer.IndexOf(last, StringComparison.OrdinalIgnoreCase);

            return answer[firstIndex..lastIndex];
        }
	}
}
            /*
            streamWriter.Write(request);
            streamWriter.Flush();
            var streamReader = new StreamReader(networkStream);
            var result = streamReader.ReadToEnd();
            //Console.WriteLine(result); 

            var uriBuilder = new UriBuilder(null, host);
            uriBuilder.Path = uri;
            Console.WriteLine($"Opened {uriBuilder}");
            // ======= FindTextBetweenTags Function START ========
            var titleTag = "<title>";
            // Find the start of the <title>-Tag
            var titleIndex = result.IndexOf(titleTag);
            string title = string.Empty;
            if (titleIndex != -1)
            {
                // Offset the index by the length of the <title>-Tag, to ommit it
                titleIndex += titleTag.Length;
                // Find the start of the </title>-End-Tag
                var titleEndIndex = result.IndexOf("</title>");
                if (titleEndIndex > titleIndex)
                {
                    // Get the string in between both
                    title = result[titleIndex..titleEndIndex];
                }
            }
            // ======= FindTextBetweenTags Function END ========
            Console.WriteLine("Title: " + title);

            var titleText = FindTextBetweenTags(result, "<title>", "</title>");
            Console.WriteLine(result);
            Console.ReadKey();
            */
