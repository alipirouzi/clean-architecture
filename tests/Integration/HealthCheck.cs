using FluentAssertions;

namespace Integration;

public class HealthCheck
{
    [Fact]
    public void AlwaysPass()
    {
        true.Should().BeTrue();
    }
}