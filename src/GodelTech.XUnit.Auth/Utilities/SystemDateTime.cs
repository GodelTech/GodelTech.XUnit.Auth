using System;

namespace GodelTech.XUnit.Auth.Utilities;

internal class SystemDateTime : IDateTime
{
    public DateTime GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}
