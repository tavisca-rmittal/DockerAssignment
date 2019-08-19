using Xunit;

namespace HiHelloApi.Tests
{

    public class HiHelloFixture
    {
        [Fact]
        public void Test_hi()
        {
            HiHelloController hihelloapi = new HiHelloController();
            var result = hihelloapi.Get("hi");
            Assert.Equal("hello", result);
        }

        [Fact]
        public void Test_hello()
        {
            HiHelloController hihelloapi = new HiHelloController();
            var result = hihelloapi.Get("hello");
            Assert.Equal("hi", result);
        }

        [Fact]
        public void Test_error()
        {
            HiHelloController hihelloapi = new HiHelloController();
            var result = hihelloapi.Get("xyz");
            Assert.Equal("error", result);
        }

    }
}
