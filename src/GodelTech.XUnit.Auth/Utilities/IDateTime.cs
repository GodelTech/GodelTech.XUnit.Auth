using System;

namespace GodelTech.XUnit.Auth.Utilities;

/// <summary>
/// DateTime.
/// </summary>
public interface IDateTime
{
    /// <summary>
    /// Get current UTC DateTime.
    /// </summary>
    /// <returns>DateTime.</returns>
    DateTime GetUtcNow();
}
