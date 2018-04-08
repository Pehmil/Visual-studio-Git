﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AirTrafficLibrary
{
    class Program
    {

        static void Main()
        {

            ITransponderReceiver TransponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            TransponderReceiver.TransponderDataReady += TransponderReceiverOnTransponderDataReady;

            string timetest = "20151006213456789";



            DateTime time = new DateTime(2015,10,06,21,34,56,789);
            //string timestamp = DateTime.Parse("20151006213456789", null).ToString("yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture);
            string format = "MMMM MM, yyyy, HH:mm:ss fff";
            DateTime myDate = DateTime.ParseExact(timetest, "yyyyMMddHHmmssfff", new CultureInfo("en-US",true));
            //string time1 = time.ToString(format, CultureInfo.CurrentUICulture);
            string dateformat = String.Format(new CultureInfo("en-US"),"{0:MMMM MM'th', yyyy, 'at' HH:mm:ss 'and' fff 'miliseconds'}", myDate);
            Console.WriteLine(dateformat);

            while (true)
            {
            }
        }

        private static void TransponderReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs rawTransponderDataEventArgs)
        {
            string Hans;
            //Jeg vil gerne se igennem denne liste TransponderData der ligger i eventargen
            foreach (var HANS in rawTransponderDataEventArgs.TransponderData)
            {
                Hans = ParseFlightInfo(HANS);
                Console.WriteLine(Hans);
            }
        }
        static public string ParseFlightInfo(string info)
        {
            string[] splitInfo = info.Split(';');

            string format = "yyyyMMddHHmmssfff";
            string tag = splitInfo[0];
            string xCoord = splitInfo[1];
            string yCoord = splitInfo[2];
            string altitude = splitInfo[3];
            string timestamp = splitInfo[4];
            DateTime date = DateTime.ParseExact(timestamp, format, CultureInfo.CreateSpecificCulture("en-US"));
            string dateformat = String.Format(new CultureInfo("en-US"),"{0:MMMM dd'th', yyyy, 'at' HH:mm:ss 'and' fff 'miliseconds'}", date);

            string text = $"\nTag: {tag}\nXCoord: {xCoord} Meters\nYCoord: {yCoord} Meters\nAltitude: {altitude} Meters\nTimestamp: {dateformat}";
            return text;

        }

    }
}