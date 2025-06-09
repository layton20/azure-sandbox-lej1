using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sandbox.API;

namespace Sandbox.IntegrationTest;

public abstract class BaseTest
{
    protected static WebApplicationFactory<Program> __Factory;

    [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void ClassInit(TestContext context)
    {
        __Factory = new WebApplicationFactory<Program>();
    }

    [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void ClassCleanup()
    {
        __Factory.Dispose();
    }

    protected static T GetClient<T>() where T : class
    {
        return __Factory.Services.GetRequiredService<T>();
    }
}