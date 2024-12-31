using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture;

public class StructureTests
{
    private const string DomainNameSpace = "Domain";
    private const string ApplicationNameSpace = "Application";
    private const string PersistenceNameSpace = "Persistence";
    private const string InfrastructureNameSpace = "Infrastructure";
    private const string SharedKernelNameSpace = "SharedKernel";
    private const string PresentationNameSpace = "Presentation";
    private const string WebApiNameSpace = "WebApi";
    private const string ConsoleUiNameSpace = "ConsoleUI";

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        var assembly = typeof(Domain.AssemblyInfo).Assembly;
        var otherProjects = new[]
        {
            ApplicationNameSpace,
            PersistenceNameSpace,
            InfrastructureNameSpace,
            PresentationNameSpace,
            ConsoleUiNameSpace,
            WebApiNameSpace
        };
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        var assembly = typeof(Application.AssemblyInfo).Assembly;
        var otherProjects = new[]
        {
            PersistenceNameSpace,
            InfrastructureNameSpace,
            PresentationNameSpace,
            ConsoleUiNameSpace,
            WebApiNameSpace
        };
        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }
    
}