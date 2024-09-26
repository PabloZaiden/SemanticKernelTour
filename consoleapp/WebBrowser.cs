using HtmlAgilityPack;
using Microsoft.SemanticKernel;

namespace SemanticKernelTour.Plugins
{
    public class WebBrowser
    {
        [KernelFunction]
        public string BrowseWebsite(string url)
        {
            using (var client = new HttpClient())
            {
                var html = client.GetStringAsync(url).Result;
                return ExtractTextWithHyperlinks(html);
            }
        }

        static string ExtractTextWithHyperlinks(string htmlContent)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            var nodes = doc.DocumentNode.SelectNodes("//a");
            var result = new List<string>();

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    string href = node.GetAttributeValue("href", string.Empty);
                    string text = node.InnerText;
                    result.Add($"{text} ({href})");
                }
            }

            return string.Join(Environment.NewLine, result);
        }
    }
}