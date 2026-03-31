using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using System.Linq;

namespace ipranges
{
    class iprange
    {
        public IPNetwork network;
        public BigInteger ip;
        public BigInteger lenght;
    }

    class iprangeraw
    {
        public BigInteger ip;
        public BigInteger lenght;
    }

    class Program
    {
        static void Main(string[] args)
        {
            JsonDocument jsonconfig=null;
            List<iprange> ipranges = new List<iprange>();
            List<iprangeraw> iprangesraw = new List<iprangeraw>();

            //read config
            try
            {
                FileStream config = File.OpenRead("config.json");
                jsonconfig = JsonDocument.Parse(config);
                config.Close();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("E: {0}", ex.Message);
                System.Environment.Exit(ex.HResult);
            }

            JsonElement jelem = new JsonElement();

            if (jsonconfig.RootElement.TryGetProperty("ipv4local", out jelem))
            {
                foreach (JsonElement ipv4 in jelem.EnumerateArray())
                {
                    JsonElement range = new JsonElement();
                    if (ipv4.TryGetProperty("range", out range))
                    {
                        IPNetwork r = IPNetwork.Parse(range.GetString());
                        if (r.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            iprange ipr = new iprange();
                            ipr.network = r;

                            byte[] bytes = r.Network.GetAddressBytes();
                            ipr.ip = 16777216L * bytes[0] + 65536 * bytes[1] + 256 * bytes[2] + bytes[3];
                            ipr.lenght = r.Total;
                            ipranges.Add(ipr);
                        }
                    }
                    else
                        Console.Error.WriteLine("W: in ipv4local section range not found");
                }
            }
            else
                Console.Error.WriteLine("W: ipv4local section not found");


            if (jsonconfig.RootElement.TryGetProperty("ipv4json", out jelem))
            {
                foreach (JsonElement ipv4 in jelem.EnumerateArray())
                {
                    JsonElement url = new JsonElement();
                    JsonElement field = new JsonElement();
                    if (ipv4.TryGetProperty("url", out url) && ipv4.TryGetProperty("field", out field))
                    {
                        try
                        {
                            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url.GetString());
                            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                            jsonconfig = JsonDocument.Parse(myHttpWebResponse.GetResponseStream());
                            myHttpWebResponse.Close();

                            JsonElement root = new JsonElement();
                            if (jsonconfig.RootElement.ValueKind!=JsonValueKind.Array)
                            {
                                root = jsonconfig.RootElement.GetProperty(ipv4.GetProperty("root").GetString());
                            }
                            else
                                root = jsonconfig.RootElement;

                            foreach (JsonElement service in root.EnumerateArray())
                            {
                                JsonElement ip = new JsonElement();
                                if(service.TryGetProperty(field.GetString(), out ip))
                                {
                                    if (ip.ValueKind == JsonValueKind.Array)
                                    { 
                                        foreach (JsonElement value in ip.EnumerateArray())
                                        {
                                            IPNetwork r = IPNetwork.Parse(value.GetString());
                                            if (r.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                            {
                                                iprange ipr = new iprange();
                                                ipr.network = r;

                                                byte[] bytes = r.Network.GetAddressBytes();
                                                ipr.ip = 16777216L * bytes[0] + 65536 * bytes[1] + 256 * bytes[2] + bytes[3];
                                                ipr.lenght = r.Total;
                                                ipranges.Add(ipr);
                                            }
                                        }
                                    }
                                    else
                                        {
                                            IPNetwork r = IPNetwork.Parse(ip.GetString());
                                            if (r.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                            {
                                                iprange ipr = new iprange();
                                                ipr.network = r;

                                                byte[] bytes = r.Network.GetAddressBytes();
                                                ipr.ip = 16777216L * (UInt32)bytes[0] + 65536 * (UInt32)bytes[1] + 256 * (UInt32)bytes[2] + (UInt32)bytes[3];
                                                ipr.lenght = r.Total;
                                                ipranges.Add(ipr);
                                            }
                                        }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine("W: issue with url:{0}\n{1}", url, ex.Message);
                        }
                    }
                    else
                        Console.Error.WriteLine("W: in ipv4json section url or field not found");
                }
            }
            else
                Console.Error.WriteLine("W: ipv4json section not found");

            ipranges = ipranges.OrderBy(x => x.ip).ThenByDescending(x => x.lenght).ToList();

            if (ipranges.Count > 1)
            {
                iprangeraw iprr = new iprangeraw();
                iprr.ip = ipranges[0].ip;
                iprr.lenght = ipranges[0].lenght;
                iprangesraw.Add(iprr);

                for (int i = 1; i < ipranges.Count; i++)
                {
                    iprangeraw iprr1 = new iprangeraw();
                    if (ipranges[i].ip <= iprangesraw.Last().ip + iprangesraw.Last().lenght)
                    {
                        iprr1.ip = iprangesraw.Last().ip;
                        BigInteger last = BigInteger.Max(iprangesraw.Last().ip + iprangesraw.Last().lenght - 1,
                            ipranges[i].ip + ipranges[i].lenght - 1);

                        iprr1.lenght = last - iprr1.ip + 1;
                        iprangesraw.RemoveAt(iprangesraw.Count - 1);
                    }
                    else
                    {
                        iprr1.ip = ipranges[i].ip;
                        iprr1.lenght = ipranges[i].lenght;
                    }
                    iprangesraw.Add(iprr1);
                }
            }

            int c = 0; 
            Console.WriteLine("\n\nINCLUDED");
            foreach (iprangeraw x in iprangesraw)
            {   
                var s = BigItoIPAddress(x.ip);
                var e = BigItoIPAddress(x.ip + x.lenght - 1);

                Console.WriteLine("{2}: {0} - {1}", s, e, c++);
            }

            BigInteger start = 0;
            BigInteger end = BigInteger.Parse("4294967295");

            c = 0;
            Console.WriteLine("\n\nEXCLUDED");
            for (int i = 0; i < iprangesraw.Count(); i++)
            {
                if (start < iprangesraw[i].ip)
                {
                    var s = BigItoIPAddress(start);
                    var e = BigItoIPAddress(iprangesraw[i].ip-1);
                    Console.WriteLine("{2}: {0} - {1}", s, e, c++);
                }

                start = iprangesraw[i].ip + iprangesraw[i].lenght;

                if (i == iprangesraw.Count() - 1 && start < end)
                {
                    var s = BigItoIPAddress(start);
                    var e = BigItoIPAddress(end);
                    Console.WriteLine("{2}: {0} - {1}", s, e, c++);
                }
            }
        }

        public static IPAddress BigItoIPAddress(BigInteger x)
        {
            byte[] number = new byte[4];
            for (int i = 0; i < 4; i++)
                number[i] = 0;


            var temp = x.ToByteArray(true);
            for (int i = 0; i < temp.Count(); i++)
                number[i] = temp[i];
            number = number.Reverse().ToArray();

            return new IPAddress(number);
        }
    }
}
