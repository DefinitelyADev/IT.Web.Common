namespace IT.Web.Common;

/// <summary>
/// Used to make type finder ignore a class or struct
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class ReflectionIgnoreAttribute : Attribute
{
}
