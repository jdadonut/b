using System.Linq;
using Xunit;
using b.Extensions;

namespace b.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            byte[] arr = new byte[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16
            };
            int i = -1;
            foreach (var byteIterated in arr.Iterate(4))
            {
                byte[] testArr =
                {
                    arr[++i],
                    arr[++i],
                    arr[++i],
                    arr[++i]
                };
                Assert.Equal(byteIterated, testArr);
                
                Assert.True(byteIterated.Length == 4, "byteIterated.Length != 4");
                

            }


        }
    }
}