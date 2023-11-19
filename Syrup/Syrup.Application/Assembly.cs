using System.Reflection;

namespace Syrup.Application;

public static class ApplicationAssembly
{
    public static Assembly Get() => typeof(ApplicationAssembly).Assembly;
}
