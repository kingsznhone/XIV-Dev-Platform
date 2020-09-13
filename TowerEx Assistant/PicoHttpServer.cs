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

        public async Task<Coordinate> StartListen()
        {
            HttpListenerContext context = await server.GetContextAsync();
            return RequestData(context.Request);
        }

        public Coordinate RequestData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody) return null;

            StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding);

            // Convert the data to a string and display it on the console.
            string s = reader.ReadToEnd();
            reader.Close();
            //return s;

            return JsonSerializer.Deserialize<Coordinate>(s);
            // If you are finished with the request, it should be closed also.
        }
    }
}
