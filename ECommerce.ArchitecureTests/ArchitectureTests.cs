using NetArchTest.Rules;
using Shouldly;
using System.Reflection;

namespace ECommerce.ArchitecureTests
{
    public class ArchitectureTests
    {
        private static readonly Assembly DomainAssembly = typeof(Domain.Entities.BaseEntity).Assembly;
        private static readonly Assembly ApplicationAssembly = typeof(UseCases.DependencyInjection).Assembly;
        private static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.DependencyInjection).Assembly;
        private static readonly Assembly ApiAssembly = typeof(API.DependencyInjection).Assembly;


        [Fact]
        public void Domain_Should_Not_Depend_On_Any_Other_Project()
        {
            var result = Types.InAssembly(DomainAssembly)
                .ShouldNot()
                .HaveDependencyOnAny(
                    "ECommerce.UseCases",
                    "ECommerce.Infrastructure",
                    "ECommerce.API"
                )
                .GetResult();

            result.IsSuccessful.ShouldBeTrue();
        }

        [Fact]
        public void Application_Should_Not_Depend_On_Infrastructure_Or_Api()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .ShouldNot()
                .HaveDependencyOnAny(
                    "ECommerce.Infrastructure",
                    "ECommerce.API"
                )
                .GetResult();

            result.IsSuccessful.ShouldBeTrue();
        }


        [Fact]
        public void Infrastructure_Should_Not_Depend_On_Api()
        {
            var result = Types.InAssembly(InfrastructureAssembly)
                .ShouldNot()
                .HaveDependencyOn("ECommerce.API")
                .GetResult();

            result.IsSuccessful.ShouldBeTrue(FormatFailingTypes(result));
        }

        private string FormatFailingTypes(NetArchTest.Rules.TestResult result)
        {
            if (result.FailingTypeNames == null) return string.Empty;
            return $"Failing types: {string.Join(", ", result.FailingTypeNames)}";
        }

    }
}
