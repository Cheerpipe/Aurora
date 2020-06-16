using Aurora.Utils;
using RestSharp;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Devices.HassioLightDevice
{
    public class HassioClient
    {

        public HassioClient()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }



        public async Task SetColor(Color color)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
            string service = "turn_on";
            string postData = "{ \"entity_id\": \"light.pasillo_techo\", \"rgb_color\": [" + color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString() + "], \"brightness\": 1  }";

            if (color.R == 0 && color.G == 0 && color.B == 0)
            {
                service = "turn_off";
                postData = "{ \"entity_id\": \"light.pasillo_techo\"}";
            }

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://192.168.1.16:8123/api/services/light/{0}", service));
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers["Authorization"] = "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiI2YmIwNTM3M2I3MTY0MGQyOTc3YWQwMzNlNmVmYTk2MiIsImlhdCI6MTU5MjI1OTg2MSwiZXhwIjoxOTA3NjE5ODYxfQ.-VcGCT-aztII480YwTErOEKk1fmEpajvizLlyalbSL0";
            httpWebRequest.Method = "POST";
            //httpWebRequest.KeepAlive = false;
            httpWebRequest.Timeout = 1000;


            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            httpWebRequest.ContentLength = (long)bytes.Length;

            Stream requestStream = httpWebRequest.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            requestStream.Dispose();
            using (httpWebRequest.GetResponse())
            {
                //nada
            }
        }
    }
}