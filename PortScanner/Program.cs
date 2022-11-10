using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PortScanner;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;



namespace COMDataExchanger
{
    class Program
    {
        public static StringBuilder sb = new StringBuilder();

        static SerialPort _serialPort = null;
        static bool session;
        static int[] jsonData;


        static void Main(string[] args)
        {

            AllPorts();

            try
            {
                _serialPort = new SerialPort
                {
                    PortName = Console.ReadLine() ?? throw new InvalidOperationException()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            if (_serialPort == null)
            {
                Console.WriteLine("The text format is not correct.");
                return;
            }
            _serialPort.ReadTimeout = 2000;
            _serialPort.WriteTimeout = 2000;
            _serialPort.BaudRate = 19200;

            _serialPort.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataRecieved);

            try
            {
                _serialPort.Open();
                session = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Write();
            Console.ReadKey();
        }

        private static void AllPorts()
        {
            var ports = SerialPort.GetPortNames();

            for (int i = 0; i < ports.Length; i++)
            {
                Console.WriteLine("[" + i.ToString() + "] " + ports[i].ToString());
            }
        }

        private static void Write()
        {
            while (session == true)
            {
                var message = Console.ReadLine();

                _serialPort.WriteLine($"<{_serialPort.PortName}>: {message}");
            }
        }

        private static void _serialPort_DataRecieved(object sender, SerialDataReceivedEventArgs e)
        {
            var selectedPeople = new List<string>();
            var recievedData = _serialPort.ReadExisting();
            Console.WriteLine(recievedData);



            Regex regex = new Regex(@"^\* \w\w\w\s\s\W\W...........", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection matches = regex.Matches(recievedData);
           
            Thread.Sleep(500);

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                    Console.WriteLine(match.Value);
                    Console.WriteLine("Найдено");
            }
            else
            {
                Console.WriteLine("Нету");
            }
            JObject o = JObject.Parse(matches.ToString());

            try
            {
                JsonObject jsonObject = new JsonObject()
                {
                    BLD = jsonData[0].ToString(),
                    UBG = jsonData[1].ToString(),
                    BIL = jsonData[2].ToString(),
                    PRO = jsonData[3].ToString(),
                    NIT = jsonData[4].ToString(),
                    KET = jsonData[5].ToString(),
                    GLU = jsonData[6].ToString(),
                    PH = jsonData[7].ToString(),
                    SG = jsonData[8].ToString(),
                    LEU = jsonData[9].ToString()
                };
                var jsonString = JsonConvert.SerializeObject(jsonObject);
                Console.WriteLine(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

         static void JsonCon(string[] jsonData)
        { 
            try
            {
                JsonObject jsonObject = new JsonObject()
                {
                    BLD = jsonData[0],
                    UBG = jsonData[1],
                    BIL = jsonData[2],
                    PRO = jsonData[3],
                    NIT = jsonData[4],
                    KET = jsonData[5],
                    GLU = jsonData[6],
                    PH = jsonData[7],
                    SG = jsonData[8],
                    LEU = jsonData[9]
                };
                var jsonString = JsonConvert.SerializeObject(jsonObject);
                Console.WriteLine(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}