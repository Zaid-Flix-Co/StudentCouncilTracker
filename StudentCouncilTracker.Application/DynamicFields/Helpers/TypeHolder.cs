using System.Collections.Concurrent;
using System.Reflection;

namespace StudentCouncilTracker.Application.DynamicFields.Helpers;

/// <summary>
/// Class TypeHolder.
/// </summary>
public static class TypeHolder
{
    /// <summary>
    /// The property holder
    /// </summary>
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> PropertyHolder = new();

    /// <summary>
    /// The method holder
    /// </summary>
    private static readonly ConcurrentDictionary<Type, MethodInfo[]> TypeMethodsHolder = new();

    /// <summary>
    /// The method holder
    /// </summary>
    private static readonly ConcurrentDictionary<string, MethodInfo> MethodHolder = new();

    /// <summary>
    /// The field holder
    /// </summary>
    private static readonly ConcurrentDictionary<Type, FieldInfo[]> FieldHolder = new();

    /// <summary>
    /// The attribute property holder
    /// </summary>
    private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<PropertyInfo, List<Attribute>>> AttributePropertyHolder = new();

    /// <summary>
    /// Получает сохраненные свойства для типа
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>PropertyInfo[].</returns>
    public static PropertyInfo[] GetProperties(Type t)
    {
        return PropertyHolder.GetOrAdd(t, type => type.GetProperties());
    }

    /// <summary>
    /// Получает сохраненные свойства для типа и для родительского интерфейса.
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>List&lt;PropertyInfo&gt;.</returns>
    public static List<PropertyInfo> GetRoleProperties(Type t)
    {
        var properties = new List<PropertyInfo>(GetProperties(t));

        //todo:
        //foreach (var interf in t.GetInterfaces().Where(f => f.Name is nameof(IHaveBaseRolesAndStatusesOrganizationStructure) or nameof(IHaveBaseRoles) or nameof(IHaveBaseStatuses)))
        //{
        //    foreach (var propertyInfo in interf.GetProperties())
        //        if (!properties.Contains(propertyInfo))
        //            properties.Add(propertyInfo);
        //}

        return properties;
    }

    /// <summary>
    /// Получает сохраненное свойство для типа по имени
    /// </summary>
    /// <param name="t">The t.</param>
    /// <param name="name">The name.</param>
    /// <returns>PropertyInfo.</returns>
    public static PropertyInfo GetRoleProperty(Type t, string name)
    {
        return GetRoleProperties(t).Find(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase))!;
    }

    /// <summary>
    /// Получает сохраненные методы для типа
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>MethodInfo[].</returns>
    public static MethodInfo[] GetMethods(Type t)
    {
        return TypeMethodsHolder.GetOrAdd(t, type => type.GetMethods());
    }

    /// <summary>
    /// Список методов для роли для типа
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>List&lt;MethodInfo&gt;.</returns>
    public static List<MethodInfo> GetRoleMethods(Type t)
    {
        var methods = new List<MethodInfo>(GetMethods(t));

        //todo: 
        //foreach (var interf in t.GetInterfaces().Where(f => f.Name is nameof(IHaveBaseRolesAndStatusesOrganizationStructure) or nameof(IHaveBaseRoles) or nameof(IHaveBaseStatuses)))
        //{
        //    foreach (var method in interf.GetMethods())
        //        if (!methods.Contains(method))
        //            methods.Add(method);
        //}

        return methods;
    }

    /// <summary>
    /// Получает сохраненный методы для типа по имени
    /// </summary>
    /// <param name="t">The t.</param>
    /// <param name="name">The name.</param>
    /// <returns>MethodInfo.</returns>
    public static MethodInfo GetRoleMethod(Type t, string name)
    {
        return MethodHolder.GetOrAdd($"{t.Name}_{name}", _ => GetRoleMethods(t).Find(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase))!);
    }

    /// <summary>
    /// Получает сохраненные поля для типа
    /// </summary>
    /// <param name="t">The t.</param>
    /// <returns>FieldInfo[].</returns>
    public static FieldInfo[] GetFields(Type t)
    {
        return FieldHolder.GetOrAdd(t, type => type.GetFields());
    }

    /// <summary>
    /// Получает сохраненный атрибут для типа и конкретного свойства
    /// </summary>
    /// <param name="t">Type of object</param>
    /// <param name="p">Property of object</param>
    /// <param name="a">Type of attribute to get from <paramref name="p" /> property of <paramref name="t" /> type. 'p' and 't' are inputs for this method</param>
    /// <returns>System.Object.</returns>
    public static object GetAttributes(Type t, PropertyInfo p, Type a)
    {
        var typePropsWithAttrs = AttributePropertyHolder.GetOrAdd(t, new ConcurrentDictionary<PropertyInfo, List<Attribute>>());
        var propAttrs = typePropsWithAttrs.GetOrAdd(p, propertyInfo => propertyInfo.GetCustomAttributes().ToList());
        return propAttrs.Find(f => f.GetType() == a)!;
    }
}