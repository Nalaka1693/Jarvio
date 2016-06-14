using System;
using System.IO;
using System.IO.Ports;

namespace blueCsharp1
{
    class Program
    {
        private static int i = 0, count = 1;
        private static int recValFlex = 50;
        private static Int16 recValGyro = 0;
        private static Int16[] gyroVals = new Int16[4];
        private static float[] angleVals = new float[3];
        private static int f = 0;
        
        static void Main(string[] args)
        {
            byte[] sendData = { (byte)'b' };

            SerialPort blueToothConn = new SerialPort();
            blueToothConn.BaudRate = 9600;
            blueToothConn.PortName = "COM14";  

            try
            {
                if (!blueToothConn.IsOpen)
                {
                    blueToothConn.Open();
                    Console.WriteLine("Opened");
                    blueToothConn.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
					
                    while (true)
                    {
                        if (recValFlex < 50)
                        {
                            blueToothConn.Write(sendData, 0, sendData.Length);
                            Console.WriteLine("Send Data: " + sendData[0]);
                        }
                        System.Threading.Thread.Sleep(500);
                    }                        
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }

        private static void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            try
            {

                Console.WriteLine("ookpokpo");
                SerialPort sp = (SerialPort)sender;
                Console.WriteLine(sp.ReadLine());
                recValGyro = Int16.Parse(sp.ReadLine());
                               
                Console.WriteLine("Data count = " + count + "\t Data: " + recValGyro);                    
                count++;

                if (count > 99)
                {
                    count = 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }       
    }
}
