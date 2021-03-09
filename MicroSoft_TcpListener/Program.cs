using CasListener;
using Microsoft.Azure.Documents;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace MicroSoft_TcpListener
{
    class Program
    {

        static public CasScalePackInfo ScaleData = new CasScalePackInfo();
        static NetworkStream stream;
        static int Counter;
        static uint TempInt;
        static int SepCounter;
        static byte Checksum;
        static string TempString;
        static public byte[] bytesFrom = new byte[1024];
        static public byte[] bytesTo = new byte[1024];
        static public bool GetPacketDone { get; set; }
        static int StateMachine;
        static public int RecivedBytes { get; set; }
        static public int ErrorPackets { get; set; }
        static string ScaleName;

        static MainContext context = new MainContext();
        static CasScalePackInfo WriteData = new CasScalePackInfo();
        static PropertyInfo[] infos = typeof(CasScalePackInfo).GetProperties();


        public void Initialize()
        {

            GetPacketDone = false;
            StateMachine = 0;
            RecivedBytes = 0;
            Counter = 0;
            SepCounter = 0;
            Checksum = 0;
            ScaleName = null;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static void SendAct()
        {
             byte Checksum;
            string Tempstring;

            bytesTo[0] = (byte)'i';
            bytesTo[1] = (byte)'0';
            bytesTo[2] = (byte)'0';
            bytesTo[3] = (byte)'F';
            bytesTo[4] = (byte)'0';
            bytesTo[5] = (byte)'7';
            bytesTo[6] = (byte)'0';
            bytesTo[7] = (byte)',';
            bytesTo[8] = (byte)'0';
            bytesTo[9] = (byte)'2';
            bytesTo[10] = (byte)'L';
            bytesTo[11] = (byte)'0';
            bytesTo[12] = (byte)'2';
            bytesTo[13] = (byte)'B';
            bytesTo[14] = (byte)':';
            bytesTo[15] = (byte)'^';
            bytesTo[16] = (byte)'=';
            Tempstring = ScaleData.ScaleID.ToString("X2");
            ASCIIEncoding.ASCII.GetBytes(Tempstring, 0, 2, bytesTo, 17);
            bytesTo[19] = (byte)'.';
            bytesTo[20] = (byte)'*';
            bytesTo[21] = (byte)'=';
            Tempstring = ScaleData.DeparmentNumber.ToString("X2");
            ASCIIEncoding.ASCII.GetBytes(Tempstring, 0, 2, bytesTo, 22);
            bytesTo[24] = (byte)'.';
            bytesTo[25] = (byte)'$';
            bytesTo[26] = (byte)'=';
            if (ScaleData.ScaleLocked == 0)
                bytesTo[27] = (byte)'0';
            else
                bytesTo[27] = (byte)'1';
            bytesTo[28] = (byte)'.';
            bytesTo[29] = (byte)'&';
            bytesTo[30] = (byte)'=';
            Tempstring = GetLocalIPAddress();
            string[] ipBytes = Tempstring.Split('.');
            byte TempByte = 0, i = 0;
            foreach (var bytes in ipBytes)
            {
                TempByte = (byte)Int32.Parse(bytes);
                Tempstring = TempByte.ToString("X2");
                ASCIIEncoding.ASCII.GetBytes(Tempstring, 0, 2, bytesTo, 31 + i);
                i += 2;
            }
            bytesTo[39] = (byte)'.';
            bytesTo[40] = (byte)'@';
            bytesTo[41] = (byte)'=';
            Tempstring = ScaleData.ScalePort.ToString("X4");
            ASCIIEncoding.ASCII.GetBytes(Tempstring, 0, 4, bytesTo, 42);
            bytesTo[46] = (byte)'.';
            bytesTo[47] = (byte)'?';
            bytesTo[48] = (byte)'=';
            Tempstring = ScaleData.ScaleServiceType.ToString("X1");
            ASCIIEncoding.ASCII.GetBytes(Tempstring, 0, 1, bytesTo, 49);
            bytesTo[50] = (byte)'.';
            bytesTo[51] = (byte)'T';
            bytesTo[52] = (byte)'=';
            Tempstring = ScaleData.ScaleTail.ToString("X4");
            ASCIIEncoding.ASCII.GetBytes(Tempstring, 0, 4, bytesTo, 53);
            bytesTo[57] = (byte)'.';
            Checksum = bytesTo[15];
            for (i = 16; i < 58; i++)
                Checksum ^= bytesTo[i];
            bytesTo[58] = Checksum;
            stream.Write(bytesTo, 0, 59);
            Console.WriteLine("packet acknoledged ... ");

        }

        static bool GetHexValues(byte Number)
        {
            uint Temp;
            if (SepCounter < Number)
            {
                Temp = bytesFrom[Counter];
                Temp <<= (SepCounter * 8);
                TempInt += Temp;
                SepCounter++;
                if (SepCounter == Number)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        public static void GetSaleInfo()
        {
            for (Counter = 0; Counter < RecivedBytes; Counter++)
            {
                if (StateMachine > 15 && StateMachine <= 203)
                {
                    Checksum ^= bytesFrom[Counter];
                }
                if (bytesFrom[Counter] == 'i' && StateMachine == 0)
                    StateMachine = 1;
                else
                if (bytesFrom[Counter] == '0' && StateMachine == 1)
                    StateMachine = 2;
                else
                if (bytesFrom[Counter] == '0' && StateMachine == 2)
                    StateMachine = 3;
                else
                if (bytesFrom[Counter] == 'F' && StateMachine == 3)
                    StateMachine = 4;
                else
                if (bytesFrom[Counter] == '0' && StateMachine == 4)
                    StateMachine = 5;
                else
                if (bytesFrom[Counter] == '7' && StateMachine == 5)
                    StateMachine = 6;
                else
                if (bytesFrom[Counter] == '0' && StateMachine == 6)
                {
                    StateMachine = 7;
                    ScaleData.CasProtocolIdentifier = "i00F070";
                }
                else
                if (bytesFrom[Counter] == ',' && StateMachine == 7)
                    StateMachine = 8;
                else
                if (StateMachine == 8)
                {
                    StateMachine = 9;
                    TempString = null;
                    TempString += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);// bytesFrom[Counter];
                }
                else
                if (StateMachine == 9)
                {
                    StateMachine = 10;
                    TempString += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                    ScaleData.FunctionCode = (byte)Int32.Parse(TempString, System.Globalization.NumberStyles.HexNumber);
                }
                else
                if (bytesFrom[Counter] == 'L' && StateMachine == 10)
                    StateMachine = 11;
                else
                if (StateMachine == 11)
                {
                    TempString = null;
                    TempString += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                    StateMachine = 12;
                }
                else
                if (StateMachine == 12)
                {
                    TempString += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                    StateMachine = 13;
                }
                else
                if (StateMachine == 13)
                {
                    TempString += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                    StateMachine = 14;
                    ScaleData.PackLenght = (uint)Int32.Parse(TempString, System.Globalization.NumberStyles.HexNumber);
                }
                else
                if (bytesFrom[Counter] == ':' && StateMachine == 14)
                    StateMachine = 15;
                else
                if (bytesFrom[Counter] == '^' && StateMachine == 15)
                {
                    StateMachine = 16;
                    Checksum = (byte)'^';
                }
                else
                if (bytesFrom[Counter] == '=' && StateMachine == 16)
                    StateMachine = 17;
                else
                if (StateMachine == 17)
                {
                    StateMachine = 18;
                    TempString = null;
                    TempString += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                }
                else
                if (StateMachine == 18)
                {
                    StateMachine = 19;
                    TempString += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                    ScaleData.ScaleID = (byte)Int32.Parse(TempString, System.Globalization.NumberStyles.HexNumber);
                }
                else
                if (bytesFrom[Counter] == '.' && StateMachine == 19)
                    StateMachine = 20;
                else
                if (bytesFrom[Counter] == '*' && StateMachine == 20)
                    StateMachine = 21;
                else
                if (bytesFrom[Counter] == '=' && StateMachine == 21)
                    StateMachine = 22;
                else
                if (StateMachine == 22)
                {
                    StateMachine = 23;
                    TempString = null;
                    TempString += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                }
                else
                if (StateMachine == 23)
                {
                    StateMachine = 24;
                    TempString += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                    ScaleData.DeparmentNumber = (byte)Int32.Parse(TempString);
                }
                else
                if (bytesFrom[Counter] == '.' && StateMachine == 24)
                    StateMachine = 25;
                else
                if (bytesFrom[Counter] == '$' && StateMachine == 25)
                    StateMachine = 26;
                else
                if (bytesFrom[Counter] == '=' && StateMachine == 26)
                    StateMachine = 27;
                else
                if (StateMachine == 27)
                {
                    StateMachine = 28;
                    ScaleData.ScaleLocked = bytesFrom[Counter];
                    if (ScaleData.ScaleLocked != '1')
                        ScaleData.ScaleLocked = 0;
                    else
                        ScaleData.ScaleLocked = 0;
                }
                else
                if (bytesFrom[Counter] == '.' && StateMachine == 28)
                    StateMachine = 29;
                else
                if (bytesFrom[Counter] == '&' && StateMachine == 29)
                    StateMachine = 30;
                else
                if (bytesFrom[Counter] == '=' && StateMachine == 30)
                {
                    StateMachine = 31;
                    TempString = null;
                    SepCounter = 0;
                    ScaleData.ScaleIP = null;
                }
                else
                if (StateMachine >= 31 && StateMachine <= 39)
                {
                    StateMachine++;
                    if (SepCounter++ == 2)
                    {

                        SepCounter = 1;
                        ScaleData.ScaleIP += (Int64.Parse(TempString, System.Globalization.NumberStyles.HexNumber)).ToString();
                        if (StateMachine < 39)
                            ScaleData.ScaleIP += '.';
                        else
                        {
                            if (bytesFrom[Counter] == '.' && StateMachine == 40)
                                StateMachine = 41;
                        }
                        TempString = System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1); ;
                    }
                    else
                        TempString += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                }
                else
                if (bytesFrom[Counter] == '@' && StateMachine == 41)
                    StateMachine = 42;
                else
                if (bytesFrom[Counter] == '=' && StateMachine == 42)
                {
                    StateMachine = 43;
                    TempString = null;
                }
                else
                if (StateMachine >= 43 && StateMachine <= 46)
                {
                    StateMachine++;
                    TempString += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                    if (StateMachine == 47)
                        ScaleData.ScalePort = (uint)Int32.Parse(TempString, System.Globalization.NumberStyles.HexNumber);
                }
                else
                if (bytesFrom[Counter] == '.' && StateMachine == 47)
                    StateMachine = 48;
                else
                if (bytesFrom[Counter] == '?' && StateMachine == 48)
                    StateMachine = 49;
                else
                if (bytesFrom[Counter] == '=' && StateMachine == 49)
                    StateMachine = 50;
                else
                if (StateMachine == 50)
                {
                    StateMachine = 51;
                    TempString = null;
                    TempString += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                    ScaleData.ScaleServiceType = (byte)Int32.Parse(TempString);
                }
                else
                if (bytesFrom[Counter] == '.' && StateMachine == 51)
                    StateMachine = 52;
                else
                if (bytesFrom[Counter] == 'T' && StateMachine == 52)
                    StateMachine = 53;
                else
                if (bytesFrom[Counter] == '=' && StateMachine == 53)
                {
                    StateMachine = 54;
                    TempString = null;
                }
                else
                if (StateMachine >= 54 && StateMachine <= 57)
                {
                    StateMachine++;
                    TempString += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                    if (StateMachine == 58)
                    {
                        ScaleData.ScaleTail = (uint)Int64.Parse(TempString, System.Globalization.NumberStyles.HexNumber);

                    }
                }
                else
                if (bytesFrom[Counter] == '.' && StateMachine == 58)
                    StateMachine = 59;
                else
                if (StateMachine == 59)
                {
                    StateMachine = 60;

                    ScaleData.Normal = false;
                    ScaleData.Prepack = false;
                    ScaleData.SelfService = false;
                    ScaleData.PluData = false;
                    ScaleData.TicketData = false;

                    switch ((int)bytesFrom[Counter])
                    {
                        case 0x00:
                            ScaleData.Normal = true;
                            break;
                        case 0x01:
                            ScaleData.PrepackData = true;
                            break;
                        case 0x02:
                            ScaleData.SelfService = true;
                            break;
                        case 0x20:
                            ScaleData.PluData = true;
                            break;
                        case 0x40:
                            ScaleData.TicketData = true;
                            break;

                    }
                    TempInt = 0;
                    SepCounter = 0;
                }
                else
                if (StateMachine == 60)
                {
                    if (GetHexValues(1))
                    {
                        StateMachine += 1;
                        ScaleData.ScaleID = (byte)TempInt;
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }
                else
                if (StateMachine == 61)
                {
                    if (GetHexValues(1))
                    {
                        StateMachine += 1;
                        ScaleData.PluType = (byte)TempInt;
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }
                else
                if (StateMachine == 62)
                {
                    if (GetHexValues(1))
                    {
                        StateMachine += 1;
                        ScaleData.DeparmentNumber = (byte)TempInt;
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }
                else
                if (StateMachine == 63)
                {
                    if (GetHexValues(4))
                    {
                        StateMachine += 4;
                        ScaleData.PLUNumber = TempInt;
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }
                else
                if (StateMachine == 67)
                {
                    if (GetHexValues(4))
                    {
                        StateMachine += 4;
                        ScaleData.Itemcode = TempInt;
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }

                else
                if (StateMachine == 71)
                {
                    if (GetHexValues(4))
                    {
                        StateMachine += 4;
                        ScaleData.Weight = TempInt;
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }
                else
                if (StateMachine == 75)
                {
                    if (GetHexValues(2))
                    {
                        StateMachine += 2;
                        ScaleData.Qty = (short)TempInt;
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }
                else
                if (StateMachine == 77)
                {
                    if (GetHexValues(2))
                    {
                        StateMachine += 2;
                        ScaleData.Pcs = (short)TempInt;
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }
                else
                if (StateMachine == 79)
                {
                    if (GetHexValues(4))
                    {
                        StateMachine += 4;
                        ScaleData.UnitPrice = TempInt;
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }
                else
                if (StateMachine == 83)
                {
                    if (GetHexValues(4))
                    {
                        StateMachine += 4;
                        ScaleData.TotalPrice = TempInt;
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }
                else
                if (StateMachine == 87)
                {
                    if (GetHexValues(4))
                    {
                        StateMachine += 4;
                        ScaleData.DiscountPrice = TempInt;
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }
                else
                if (StateMachine == 91)
                {
                    if (GetHexValues(4))
                    {
                        StateMachine += 4;
                        ScaleData.ScaleTransactioncounter = TempInt;
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }
                else
                if (StateMachine == 95)
                {
                    if (GetHexValues(2))
                    {
                        StateMachine += 2;
                        ScaleData.TicketNumber = (short)TempInt;
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }
                else
                if (StateMachine == 97)
                {
                    if (GetHexValues(1))
                    {
                        StateMachine += 1;
                        ScaleData.NegativeSale = false;
                        ScaleData.Return = false;
                        ScaleData.Void = false;
                        ScaleData.Prepack = false;
                        ScaleData.Label = false;
                        ScaleData.Override = false;
                        ScaleData.Add = false;
                        ScaleData.NoVoid = false;
                        switch (TempInt)
                        {

                            case 0x01:
                                ScaleData.NegativeSale = true;
                                break;
                            case 0x02:
                                ScaleData.Return = true;
                                break;
                            case 0x04:
                                ScaleData.Void = true;
                                break;
                            case 0x08:
                                ScaleData.Prepack = true;
                                break;
                            case 0x10:
                                ScaleData.Label = true;
                                break;
                            case 0x20:
                                ScaleData.Override = true;
                                break;
                            case 0x40:
                                ScaleData.Add = true;
                                break;
                            case 0x80:
                                ScaleData.NoVoid = true;
                                break;

                        }
                        TempInt = 0;
                        SepCounter = 0;
                    }
                }
                else
                if (StateMachine == 98)
                {
                    StateMachine = 99;
                    ScaleData.CurrentDate_year = bytesFrom[Counter];
                }
                else
                if (StateMachine == 99)
                {
                    StateMachine = 100;
                    ScaleData.CurrentDate_month = bytesFrom[Counter];
                }
                else
                if (StateMachine == 100)
                {
                    StateMachine = 101;
                    ScaleData.CurrentDate_day = bytesFrom[Counter];
                }
                else
                if (StateMachine == 101)
                {
                    StateMachine = 102;
                    ScaleData.CurrentTime_hour = bytesFrom[Counter];
                }
                else
                if (StateMachine == 102)
                {
                    StateMachine = 103;
                    ScaleData.CurrentTime_min = bytesFrom[Counter];
                }
                else
                if (StateMachine == 103)
                {
                    StateMachine = 104;
                    ScaleData.CurrentTime_second = bytesFrom[Counter];
                }
                else
                if (StateMachine == 104)
                {
                    StateMachine = 105;
                    ScaleData.SaleDate_year = bytesFrom[Counter];
                }
                else
                if (StateMachine == 105)
                {
                    StateMachine = 106;
                    ScaleData.SaleDate_month = bytesFrom[Counter];
                }
                else
                if (StateMachine == 106)
                {
                    StateMachine = 107;
                    ScaleData.SaleDate_day = bytesFrom[Counter];
                    ScaleData.Barcode = null;
                }
                else
                if (StateMachine >= 107 && StateMachine <= 126)
                {
                    ScaleData.Barcode += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                    StateMachine++;
                }
                else
                if (StateMachine >= 127 && StateMachine <= 146)
                {
                    ScaleData.TraceCode += System.Text.Encoding.UTF8.GetString(bytesFrom, Counter, 1);
                    StateMachine++;
                }
                else
                if (StateMachine == 147)
                {
                    StateMachine = 148;
                    ScaleData.CurrentTicketSaleorder = bytesFrom[Counter];
                }
                else
                if (StateMachine == 148)
                {
                    StateMachine = 149;
                    ScaleData.reserved = bytesFrom[Counter];
                    SepCounter = 0;
                }
                else
                if (StateMachine >= 149 && StateMachine <= 203)
                {
                    ScaleData.PluName[SepCounter] = bytesFrom[Counter];
                    StateMachine++;
                    SepCounter++;
                }
                else
                if (StateMachine == 204)
                {
                    if (Checksum != bytesFrom[Counter])
                    {
                        ErrorPackets++;
                    }
                    else
                    {
                        Console.WriteLine("packet recieved ... ");

                        foreach (PropertyInfo info in infos)
                        {
                            info.SetValue(WriteData, info.GetValue(ScaleData, null), null);
                        }
                        context.CasSalesData.Add(WriteData);
                        context.SaveChanges();
                        SendAct();
                    }
                    StateMachine = 0;
                }
                else
                {
                    StateMachine = 0;
                }
            }
        }
        static void Main(string[] args)
        {

            TcpListener server = null;
            try
            {

                GetPacketDone = false;
                StateMachine = 0;
                RecivedBytes = 0;
                Counter = 0;
                SepCounter = 0;
                Checksum = 0;
                ScaleName = null;

                // Set the TcpListener on port 13000.
                Int32 port = 20304;
                IPAddress localAddr = IPAddress.Parse("192.168.1.53");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                String data = null;
                int i = 0;



                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();


                    data = null;

                    // Get a stream object for reading and writing
                    stream = client.GetStream();

                    // Loop to receive all the data sent by the client.
                    while ((RecivedBytes = stream.Read(bytesFrom, 0, bytesFrom.Length)) != 0)
                    {
                        i++;
                        Console.WriteLine("\r\nConnected number = {0}", i);
                        GetSaleInfo();
                    }

                    // Shutdown and end connection
                    Console.WriteLine("Connection closed !!!");
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

    }
}
