using System.IO;
using System.Net.Http;

namespace b.Objects
{
    public abstract class IImagePost
    {
        public string PostUri;
        public string ImageUri;
        public string PostTitle;
        public abstract Stream GetImage(HttpClient client);
        
        
    }
}