using FluentAssertions;

namespace Unit;

public class HealthCheck
{
    [Fact]
    public void AlwaysPass() => true.Should().BeTrue();
}