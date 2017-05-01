using System;

namespace MultiPortScan
{
    /// <summary>
    /// A Console type Multi Port TCP Scanner
    /// Author : Philip Murray
    /// </summary>

    class Program
    {

        static void Main(string[] args)
        {
            string host;
            int portStart;
            int portStop;
            int Threads;
            int timeout;

            youGotItWrong: //goto: Start Again

            //this is for the user to select a host ip
            string ip;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Enter Host I.P or Domain example: (127.0.0.1) or (localhost) etc..");
            Console.ResetColor();
            Console.Write("Enter Host I.P or Domain : ");
            ip = Console.ReadLine();
            Console.WriteLine();
            host = ip;

            //this is for the user to select the start port
            string startPort;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Min Start Port : 0");
            Console.ResetColor();
            Console.Write("Enter A start Port : ");
            startPort = Console.ReadLine();
            Console.WriteLine();

            //THIS CHECKS TO SEE IF IT THE START PORT CAN BE PARSED OUT
            int number;
            bool resultStart = int.TryParse(startPort, out number);

            if (resultStart)
            {
                portStart = int.Parse(startPort);
            }

            else
            {
                Console.WriteLine("Try Again NOOOB!!");
                goto youGotItWrong;
               // return;
            }


            //this is for the end port
            string endPort;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Max End Port : 65535");
            Console.ResetColor();
            Console.Write("Enter A End Port : ");
            endPort = Console.ReadLine();
            Console.WriteLine();
          

            //THIS CHECKS TO SEE IF IT THE END PORT CAN BE PARSED OUT
            int number2;
            bool resultEnd = int.TryParse(endPort, out number2);

            if (resultEnd)
            {
                portStop = int.Parse(endPort);
            }

            else
            {
                Console.WriteLine("Try Again NOOOB!!");

                goto youGotItWrong;
               // return;
            }

            //this is how many threads will be started
            string threadsToRun;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Normal Thread amount is between 1 - 50 (less threads = higher accuracy)");
            Console.ResetColor();
            Console.Write("Enter How Many Threads To Run : ");
            threadsToRun = Console.ReadLine();
            Console.WriteLine();
          

            //THIS CHECKS TO SEE IF IT THE END PORT CAN BE PARSED OUT
            int number3;
            bool resultThreads = int.TryParse(threadsToRun, out number3);

            if (resultThreads)
            {
                Threads = int.Parse(threadsToRun);
            }

            else
            {
                Console.WriteLine("Try Again NOOOB!!");

                goto youGotItWrong;
                
                // return;
            }

            //this is how many threads will be started 
            string tcpTimeout;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Normal Timeout amount is between 1 - 10 secs ( 1 = 1 second)(higher timeout = higher accuracy)");
            Console.ResetColor();
            Console.Write("Enter Timeout : ");
            tcpTimeout = Console.ReadLine();
            Console.WriteLine();
          
            //THIS CHECKS TO SEE IF IT THE timeout CAN BE PARSED OUT
            int number4;
            bool resultTimeout = int.TryParse(tcpTimeout, out number4);

            if (resultTimeout)
            {
                timeout = int.Parse(tcpTimeout) * 1000;
             
            }

            else
            {
                Console.WriteLine("Try Again NOOOB!!");

                goto youGotItWrong;
                //  return;
            }

            try
            {

                host = ip;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            if (resultStart == true && resultEnd == true)
            {
                try
                {

                    portStart = int.Parse(startPort);
                    portStop = int.Parse(endPort);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

            }
            if (resultThreads == true)
            {
                try
                {

                    Threads = int.Parse(threadsToRun);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return;
                }
            }

            PortScanner ps = new PortScanner(host, portStart, portStop , timeout);
            ps.start(Threads);

        }
        
    }
}
