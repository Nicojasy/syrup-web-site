using System.Reflection;

namespace Syrup.Identity.Application;

public static class ApplicationAssembly
{
    public static Assembly Get() => typeof(ApplicationAssembly).Assembly;
}
