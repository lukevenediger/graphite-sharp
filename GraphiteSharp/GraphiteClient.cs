using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Graphite
{
    public interface IGraphiteClient
    {
        GraphiteResponse GetData(string url);
        string GrabGraphImage(string url, int width, int height, bool graphOnly, int horizontalLine);
    }

    public class GraphiteClient : IGraphiteClient
    {
        public GraphiteResponse GetData(string url)
        {
            var client = new RestClient();
            client.AddHandler("application/json", new DynamicJsonDeserialiser());
            var request = new RestRequest(url + "&format=json", Method.GET);
            var response = client.Execute<dynamic>(request);

            var targets = new List<Series>();
            foreach (var rawTarget in response.Data)
            {
                var target = CreateTarget(rawTarget);
                targets.Add(target);
            }
            var graphiteResponse = new GraphiteResponse(url, targets);
            return graphiteResponse;
        }

        public string GrabGraphImage(string url, int width, int height, bool graphOnly, int horizontalLine)
        {
            var outputPath = Path.GetTempFileName();
            outputPath = Path.ChangeExtension(outputPath, "png");
            GrabGraphImage(url, width, height, graphOnly, horizontalLine, outputPath);
            return outputPath;
        }

        public void GrabGraphImage(string url, int width, int height, bool graphOnly, int horizontalLine, string outputPath)
        {
            var uri = new Uri(url);
            var parameters = HttpUtility.ParseQueryString(uri.Query);
            parameters.Set("width", width.ToString());
            parameters.Set("height", height.ToString());
            parameters.Set("yMin", "0");
            parameters.Set("format", "png");
            if (graphOnly)
            {
                parameters.Set("graphOnly", "true");
            }
            var targetUrl = url.Split('?')[0] + "?" + parameters.ToString();
            if (horizontalLine != 0)
            {
                targetUrl += "&target=constantLine(" + horizontalLine + ")";
            }

            var webClient = new WebClient();
            webClient.DownloadFile(targetUrl, outputPath);
        }

        private Series CreateTarget(dynamic rawTarget)
        {
            var datapoints = new List<Datapoint>();
            foreach (var rawDatapoint in rawTarget.datapoints)
            {
                var epoch = rawDatapoint.Last.Value;
                var value = rawDatapoint.First.Value == null ? 0 : Convert.ToInt32(rawDatapoint.First.Value);
                var datapoint = new Datapoint(epoch, value);
                datapoints.Add(datapoint);
            }
            return new Series(rawTarget.target.Value, datapoints);
        }
    }
}
