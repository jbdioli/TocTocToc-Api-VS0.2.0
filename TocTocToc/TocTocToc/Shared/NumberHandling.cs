using System;

namespace TocTocToc.Shared;

public class NumberHandling
{

    public static bool IsMiniGreaterThan<T>(T valueMini, T valueMaxi)
    {
        if (typeof(T) == typeof(bool))
            throw new Exception("[ Error : You can't compare bool values ]");

        var mini = 0;
        var maxi = 0;
        var isGreater = false;

        // if (valueMini == null && valueMaxi == null) return false;

        if (valueMini != null && !String.IsNullOrEmpty(valueMini.ToString()))
            mini = int.Parse(valueMini.ToString());

        if (valueMaxi != null && !String.IsNullOrEmpty(valueMaxi.ToString()))
            maxi = int.Parse(valueMaxi.ToString());

        if (mini > maxi)
            isGreater = true;

        return isGreater;
    }


}