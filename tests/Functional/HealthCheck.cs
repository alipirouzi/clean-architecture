using FluentAssertions;

namespace Functional;

public class HealthCheck
{
    [Fact]
    public void AlwaysPass() => true.Should().BeTrue();
}