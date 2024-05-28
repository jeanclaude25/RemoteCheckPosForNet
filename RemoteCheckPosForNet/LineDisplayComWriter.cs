using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace RemoteCheckPosForNet
{
    public static class LineDisplayComWriter
    {
        public static int DisplayLength = 20;
        public static int DisplayRows = 2;
        public static string DisplayClearCommand = "\x0C";
        //public static string setCursorToLine1Command = "\x1B[1;1H"; // Steuerbefehl zum Setzen des Cursors auf Zeile 1, Spalte 1
        public static string highlightOnCommand = "\x1B[4m"; // Beispielbefehl zum Einschalten der Hervorhebung
        //public static string highlightOffCommand = "\x1B[0m"; // Beispielbefehl zum Ausschalten der Hervorhebung

        private static SerialPort _serialPort;

        static LineDisplayComWriter()
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = "COM3"; // Change this to the appropriate COM port
            _serialPort.BaudRate = 9600; // Adjust baud rate as per your line display configuration
            _serialPort.Parity = Parity.None;
            _serialPort.DataBits = 8;
            _serialPort.StopBits = StopBits.One;
            //_serialPort.Encoding = Encoding.UTF8;

        }

        public static void WriteDisplayLine(string line1, string line2,string size, double price)
        {
            _serialPort.Open();
            _serialPort.Write(DisplayClearCommand);
            //_serialPort.Write(setCursorToLine1Command);
            var teststring = $"{FormatString(line1, 20),-20}" + $"{FormatString(line2, 14),-14}{price,6:##0.00}";
            _serialPort.Write(teststring);
            //_serialPort.Write($"{FormatString(line1, 20),-20}");
            ////Line1 = "1234567890123456789*";
            //_serialPort.Write($"{FormatString(line2, 14),-14}{price,6:##0.00}");
            _serialPort.Close();

            //try
            //{
            //    using (SerialPort serialPort = new SerialPort("COM3", 9600, Parity.None, 8, stopBits))
            //    {
            //        // Setzen der Kodierung auf UTF-8
            //        serialPort.Encoding = Encoding.UTF8;

            //        // Öffnen des seriellen Ports
            //        serialPort.Open();

            //        // Löschen des Displays
            //        serialPort.Write(clearDisplayCommand);

            //        // Cursor auf die erste Zeile setzen
            //        serialPort.Write(setCursorToLine1Command);

            //        // Hervorhebung einschalten
            //        serialPort.Write(highlightOnCommand);

            //        // Text auf die erste Zeile schreiben
            //        string text = "Grün";
            //        serialPort.Write(text);

            //        // Hervorhebung ausschalten
            //        serialPort.Write(highlightOffCommand);

            //        // Schließen des seriellen Ports
            //        serialPort.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Fehler: " + ex.Message);
            //}

        }
        static string ReplaceSpecialCharacters(string input)
        {
            // Ersetzen Sie die deutschen Umlaute durch ihre Basiszeichen
            return input.Replace("ä", "a")
                .Replace("ö", "o")
                .Replace("ü", "u")
                .Replace("Ä", "A")
                .Replace("Ö", "O")
                .Replace("Ü", "U")
                .Replace("ß", "ss");
        }
        private static string FormatString(string input, int maxLength)
        {
            input=ReplaceSpecialCharacters(input).Trim();
            
            if (input.Length > maxLength)
            {
                // Abschneiden des Strings auf die maximale Länge
                return input.Substring(0, maxLength);
            }
            else
            {
                //// Auffüllen des Strings mit Leerzeichen
                //return input.PadRight(maxLength);
                return input;
            }
        }

    }
}
