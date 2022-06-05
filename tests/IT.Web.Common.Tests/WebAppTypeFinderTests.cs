using FluentAssertions;
using IT.Web.Common.Abstractions;

namespace IT.Web.Common.Tests;

public class UnitTest1
{
    [Fact]
    public void Return()
    {
        ITypeFinder finder = new WebAppTypeFinder();

        List<Type> findableInterfaceType = finder.FindClassesOfType<IMyInterface>().ToList();
        findableInterfaceType.Count.Should().Be(1);
        typeof(IMyInterface).IsAssignableFrom(findableInterfaceType.FirstOrDefault()).Should().BeTrue();

        List<Type> findableClassType = finder.FindClassesOfType<MyFindableClass>().ToList();
        findableClassType.Count.Should().Be(1);
        typeof(MyFindableClass).IsAssignableFrom(findableClassType.FirstOrDefault()).Should().BeTrue();

        List<Type> ignoredType = finder.FindClassesOfType<MyIgnoredClass>().ToList();
        ignoredType.Count.Should().Be(0);
    }
}

public interface IMyInterface
{

}

public class MyFindableClass : IMyInterface
{

}

[ReflectionIgnore]
public class MyIgnoredClass : IMyInterface
{

}
