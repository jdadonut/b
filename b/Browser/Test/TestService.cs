using System.Collections.Generic;
using System.IO;
using b.Objects;

namespace b.Browser.Test
{
    public class TestService : IService
    {
        public IEnumerator<IImagePost> GetEnumerator(params string[] args)
        {
            foreach (var file in Directory.GetFiles("/barracks/images"))
            {
                yield return new TestImagePost() {ImageUri = file, PostUri = file, PostTitle = file};
            }
        }
    }
}