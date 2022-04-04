using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace CodeRabbits.KaoList.Web.Controllers.Api
{
    [Route("[controller]/{id}")]
    [ApiController]
    public class SvgController : ControllerBase
    {
        static HashSet<string> colorTagSet = new HashSet<string>()
        {
            "color",
            "fill",
            "stroke"
        };

        private readonly Uri _baseUri;

        public SvgController(IConfiguration configuration)
        {
            var svgPath = configuration["SvgFilePath"] ?? throw new NullReferenceException("SvgFilePath not set.");
            if (!Path.EndsInDirectorySeparator(svgPath))
            {
                svgPath += Path.DirectorySeparatorChar;
            }
            _baseUri = new Uri(Path.GetFullPath(svgPath));
        }

        public async Task<IActionResult> Index(string id)
        {
            if (!Uri.TryCreate(_baseUri, id, out Uri? svgPathUri))
            {
                return NotFound();
            }

            if (!_baseUri.IsBaseOf(svgPathUri))
            {
                return NotFound();
            }

            var path = svgPathUri.LocalPath;
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            XDocument svg = XDocument.Load(path);
            XElement? svgElement = svg.Root;
            if (svgElement != null)
            {
                foreach (var query in Request.Query)
                {
                    if (colorTagSet.Contains(query.Key) && query.Value.SingleOrDefault() is not null && Regex.IsMatch(query.Value.Single(), "[a-f|A-F|0-9]{1,8}"))
                    {
                        svgElement.SetAttributeValue(query.Key, '#' + query.Value);
                    }
                    else
                    {
                        svgElement.SetAttributeValue(query.Key, query.Value);
                    }
                }
            }

            XmlWriterSettings settings = new()
            {
                Async = true
            };

            Response.StatusCode = 200;
            Response.Headers.ContentType = "image/svg+xml";
            Response.Headers.ETag = (CalculateMD5(path) ^ Request.QueryString.ToString().GetHashCode()).ToString("x4");
            using XmlWriter writer = XmlWriter.Create(Response.BodyWriter.AsStream(), settings);
            await svg.WriteToAsync(writer, CancellationToken.None);

            return new EmptyResult();
        }

        static int CalculateMD5(string filename)
        {
            using var md5 = MD5.Create();
            using var stream = System.IO.File.OpenRead(filename);
            return BitConverter.ToInt32(md5.ComputeHash(stream));

        }
    }
}
