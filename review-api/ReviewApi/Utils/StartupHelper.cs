
using System.Net;
using System.Net.Sockets;

namespace ReviewApi.Utils
{

    public static class StartupHelper
    {
        const string localhost = "127.0.0.1";


        public static void WaitForDapr()
        {
            var ipadress = IPAddress.Parse(localhost);
            var server = new TcpListener(ipadress, 80);
            server.Start();
        }


    }

}