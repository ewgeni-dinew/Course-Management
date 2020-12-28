namespace CourseManagement.Tests
{
    using Xunit;

    public class Class1
    {
        [Fact]
        public void IsTestPositive()
        {
            var str = "dsaljkdalkkdJFK";
            Assert.EndsWith("JFK", str);
        }

        [Fact]
        public void IsTestPositive_2()
        {
            Assert.Equal(5, 3);
        }
    }
}
