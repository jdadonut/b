using System.IO;
using System.Net.Http;
using b.Objects;

namespace b.Browser.Test
{
    public class TestImagePost : IImagePost
    {
        public string? PostUri;
        public string? ImageUri;

        public override Stream GetImage(HttpClient client)
        {
            return File.Open(ImageUri, new FileStreamOptions());
        }
        
    }
}