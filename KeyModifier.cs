using System;

namespace AtTheFront
{
    [Flags]
    public enum KeyModifier
    {
        NONE = 0,
        MOD_ALT = 1,
        MOD_CONTROL = 2,
        MOD_SHIFT = 4,
        MOD_WIN = 8
    }
}
