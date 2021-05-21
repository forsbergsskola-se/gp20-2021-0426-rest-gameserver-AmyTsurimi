using System;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;
using TinyBrowser;

namespace TinyBrowser
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting ServerBrower");
            ServerBrowser.SBrowser();
        }
    }
}