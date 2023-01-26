// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

namespace CodeRabbits.KaoList.Web;

public static class ConfigurationManagerExtension
{

    /// <summary>
    /// Extracts the value with the specified key and converts it to type T.
    /// </summary>
    /// <typeparam name="T">The type to convert the value to.</typeparam>
    /// <param name="manager">The configuration manager.</param>
    /// <param name="key">The key of the configuration section's value to convert.</param>
    /// <remarks>
    /// If no matching value is found with the specified key, an exception is raised.
    /// </remarks>
    /// <returns>The converted value.</returns>
    public static T GetRequiredValue<T>(this ConfigurationManager manager, string key) where T : notnull
    {
        return manager.GetValue(key, default(T)) ?? throw new InvalidOperationException(SRHelper.Format(SR.InvalidValueName, key));
    }

    /// <summary>
    /// Extracts the value with the specified key and converts it to type T.
    /// </summary>
    /// <typeparam name="T">The type to convert the value to.</typeparam>
    /// <param name="manager">The configuration manager.</param>
    /// <param name="key">The key of the configuration section's value to convert.</param>
    /// <param name="defaultValue">The default value to use if no value is found.</param>
    /// <remarks>
    /// If no matching value is found with the specified key, an exception is raised.
    /// </remarks>
    /// <returns>The converted value.</returns>
    public static T GetRequiredValue<T>(this ConfigurationManager manager, string key, T defaultValue)
    {
        return (T)(manager.GetValue(typeof(T), key, defaultValue) ?? throw new InvalidOperationException(SRHelper.Format(SR.InvalidValueName, key)));
    }

    /// <summary>
    /// Extracts the value with the specified key and converts it to the specified type.
    /// </summary>
    /// <param name="manager">The configuration manager.</param>
    /// <param name="type">The type to convert the value to.</param>
    /// <param name="key">The key of the configuration section's value to convert.</param>
    /// <remarks>
    /// If no matching value is found with the specified key, an exception is raised.
    /// </remarks>
    /// <returns>The converted value.</returns>
    public static object? GetValue(
        this ConfigurationManager manager,
        Type type,
        string key)
    {
        return manager.GetValue(type, key, defaultValue: null) ?? throw new InvalidOperationException(SRHelper.Format(SR.InvalidValueName, key));
    }

    /// <summary>
    /// Extracts the value with the specified key and converts it to the specified type.
    /// </summary>
    /// <param name="manager">The configuration manager.</param>
    /// <param name="type">The type to convert the value to.</param>
    /// <param name="key">The key of the configuration section's value to convert.</param>
    /// <param name="defaultValue">The default value to use if no value is found.</param>
    /// <remarks>
    /// If no matching value is found with the specified key, an exception is raised.
    /// </remarks>
    /// <returns>The converted value.</returns>
    public static object? GetValue(
        this ConfigurationManager manager,
        Type type,
        string key,
        object? defaultValue)
    {
        return manager.GetValue(type, key, defaultValue) ?? throw new InvalidOperationException(SRHelper.Format(SR.InvalidValueName, key));
    }
}
