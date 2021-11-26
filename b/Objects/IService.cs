using System.Collections.Generic;

namespace b.Objects
{
    public interface IService
    {
        public IEnumerator<IImagePost> GetEnumerator(params string[] args);
    }
}