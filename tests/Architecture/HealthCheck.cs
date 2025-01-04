using FluentAssertions;

namespace Architecture;

public class HealthCheck
{
    [Fact]
    public void AlwaysPass()
    {
        true.Should().BeTrue();
    }
}