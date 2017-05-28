using System;
using AuthGuard.BLL.Domain.Entities.Identity;
using DddCore.Contracts.BLL.Domain.Entities.BusinessRules;
using DddCore.Contracts.BLL.Domain.Entities.Graph;
using DddCore.Crosscutting.DependencyInjection;
using DddCore.SL.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AuthGurd.Tests.Unit
{
    public class BusinessRulesValidatorTests
    {
        [Theory]
        [InlineData(typeof(IdentityResource))]
        public void DomainEntitiesHaveValidators(Type domainType)
        {
            var identityResource = new IdentityResource();

            var module = new DddCoreDiModuleInstaller();

            var container = new DiBootstrapper()
                .AddConfig(new ServiceCollection())
                .Bootstrap(module);

            var factory = container.GetService<IBusinessRulesValidatorFactory>();
            var validationResult = identityResource.ValidateGraph(factory, GraphDepth.AggregateRoot);
        }
    }
}