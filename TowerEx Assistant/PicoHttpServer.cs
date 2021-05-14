using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowerEx_Assistant
{
    public enum NetworkMode
    {
        Relative = 0,
        Polar =1,

    }

    public class NetworkCoordBuffer
    {
        public string Mode { get; set; }
        public float X { get; set; }
        public float Z { get; set; }
        public float Y { get; set; }
        public float Rho { get; set; }
        public float Theta { get; set; }

        public NetworkCoordBuffer()
        {
            Mode = "Relative";
            X = 0;
            Y = 0;
            Z = 0;
            Rho = 0;
            Theta = 0;
        }

        public Coordinate GetCoordinate()
        {
            Coordinate C = new Coordinate();
            C.X = X;
            C.Y = Y;
            C.Z = Z;
            return C;
        }
    }

    public class PicoHttpServer
    {
        string[] prefixes;
        HttpListener server;

        public PicoHttpServer(int port)
        {
            server = new HttpListener();
            prefixes = new[] { string.Format("http://127.0.0.1:{0}/",port) };
            foreach (string s in prefixes)
            {
                server.Prefixes.Add(s);
            }
            server.Start();
        }

        public async Task<NetworkCoordBuffer> StartListen()
        {
            HttpListenerContext context = await server.GetContextAsync();
            return RequestData(context.Request);
        }

        public NetworkCoordBuffer RequestData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody) return null;

            StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding);
            string s = reader.ReadToEnd();
            reader.Close();
            //return s;
            return JsonSerializer.Deserialize<NetworkCoordBuffer>(s);
            // If you are finished with the request, it should be closed also.
        }
    }
}
