using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;
using System.Xml;
using System.Xml.Linq;

namespace CodeRabbits.KaoList.Web.TagHelpers
{
    [HtmlTargetElement("svg", Attributes = nameof(Src))]
    public class SvgTagHelper : TagHelper
    {
        private readonly Uri _baseUri;
        public string? Src { get; set; }

        public SvgTagHelper(IConfiguration configuration)
        {
            var svgPath = configuration["SvgFilePath"] ?? throw new NullReferenceException("SvgFilePath not set.");
            if (!Path.EndsInDirectorySeparator(svgPath))
            {
                svgPath += Path.DirectorySeparatorChar;
            }
            _baseUri = new Uri(Path.GetFullPath(svgPath));            
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!(Src?.StartsWith("~/") ?? false))
            {
                output.SuppressOutput();
                return;
            }

            var filePath = Src[2..];
            if (!Uri.TryCreate(_baseUri, filePath, out Uri? svgPathUri))
            {
                output.SuppressOutput();
                return;
            }

            if (!_baseUri.IsBaseOf(svgPathUri))
            {
                output.SuppressOutput();
                return;
            }

            var path = svgPathUri.LocalPath;
            if (!File.Exists(path))
            {
                return;
            }

            var svgFIleStream = File.OpenRead(path);
            XDocument svg = await XDocument.LoadAsync(svgFIleStream, LoadOptions.None, CancellationToken.None);
            svgFIleStream.Close();

            XElement svgElement = svg.Root ?? throw new FormatException("Could not find the root of the svg.");
            foreach (var attribute in context.AllAttributes.Where(item => item.Name != "src"))
            {
                svgElement.SetAttributeValue(attribute.Name, attribute.Value);
            }

            XmlWriterSettings settings = new()
            {
                Async = true
            };

            output.Reinitialize("svg", TagMode.StartTagAndEndTag);

            output.Attributes.Clear();
            foreach (var attribute in svgElement.Attributes())
            {
                output.Attributes.Add(attribute.Name.ToString(), attribute.Value);
            }

            output.Content.Clear();
            foreach (var item in svgElement.Elements())
            {
                output.Content.AppendHtml(new XElement(item.Name.LocalName, item.Attributes(), item.Elements()).ToString());
            }
        }
    }
}