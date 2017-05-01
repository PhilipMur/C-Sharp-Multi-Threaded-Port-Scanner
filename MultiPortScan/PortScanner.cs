using System;
using System.Net.Sockets;
using System.Threading;

namespace MultiPortScan
{
    class PortScanner
    {

        private string host;
        private PortList portList;
        private bool turnOff = true;
        private int count = 0;
        public int tcpTimeout ;

        private class isTcpPortOpen
        {
            public TcpClient MainClient { get; set; }
            public bool tcpOpen { get; set; }
        }


        public PortScanner(string host, int portStart, int portStop , int timeout)
        {
            this.host = host;
            portList = new PortList(portStart, portStop);
            tcpTimeout = timeout;
           
        }

        public void start(int threadCounter)
        {
            for (int i = 0; i < threadCounter; i++)
            {

                Thread thread1 = new Thread(new ThreadStart(RunScanTcp));
                thread1.Start();
               
            }

        }

        public void RunScanTcp()
        {
          
            int port;

            //while there are more ports to scan 
            while ((port = portList.NextPort()) != -1)
            {
                count = port;

                Thread.Sleep(1); //lets be a good citizen to the cpu
                
                Console.Title = "Current Port Count : " + count.ToString();
                
                try
                {

                    Connect(host, port, tcpTimeout);

                }
                catch
                {        
                    continue;
                }
              
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("TCP Port {0} is open ", port);
                Console.ResetColor();

            }
            
              
                if (turnOff == true )
                {

                    turnOff = false;
                    Console.WriteLine();
                    Console.WriteLine("Scan Complete !!!");

                    Console.ReadKey();

                }

        }
    //method for returning tcp client connected or not connected
        public TcpClient Connect(string hostName, int port, int timeout)
        {
            var newClient = new TcpClient();

            var state = new isTcpPortOpen
            {
                MainClient = newClient, tcpOpen = true
            };

            IAsyncResult ar = newClient.BeginConnect(hostName, port, AsyncCallback, state);
            state.tcpOpen = ar.AsyncWaitHandle.WaitOne(timeout, false);

            if (state.tcpOpen == false || newClient.Connected == false)
            {
                throw new Exception();

            }
            return newClient;
        }
        //async callback for tcp clients
        void AsyncCallback(IAsyncResult asyncResult)
        {
            var state = (isTcpPortOpen)asyncResult.AsyncState;
            TcpClient client = state.MainClient;

            try
            {
                client.EndConnect(asyncResult);
            }
            catch
            {
                return;
            }

            if (client.Connected && state.tcpOpen)
            {
                return;
            }
               
            client.Close();
        }
        
    }
}
