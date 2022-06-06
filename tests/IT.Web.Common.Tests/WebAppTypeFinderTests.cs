using FluentAssertions;
using IT.Web.Common.Abstractions;

namespace IT.Web.Common.Tests;

public class WebAppTypeFinderTests
{
    [Fact]
    public void FindClassesOfType_ReturnsTheAppropriateTypes()
    {
        ITypeFinder finder = new WebAppTypeFinder();

        Type[] findableInterfaceType = finder.FindClassesOfType<IMyInterface>().ToArray();
        findableInterfaceType.Length.Should().Be(1);
        typeof(IMyInterface).IsAssignableFrom(findableInterfaceType.FirstOrDefault()).Should().BeTrue();

        Type[] findableClassType = finder.FindClassesOfType<MyFindableClass>().ToArray();
        findableClassType.Length.Should().Be(1);
        typeof(MyFindableClass).IsAssignableFrom(findableClassType.FirstOrDefault()).Should().BeTrue();

        Type[] ignoredType = finder.FindClassesOfType<MyIgnoredClass>().ToArray();
        ignoredType.Length.Should().Be(0);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public interface IMyInterface
    {

    }

    // ReSharper disable once MemberCanBePrivate.Global
    public class MyFindableClass : IMyInterface
    {

    }

    [ReflectionIgnore]
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MyIgnoredClass : IMyInterface
    {

    }
}
