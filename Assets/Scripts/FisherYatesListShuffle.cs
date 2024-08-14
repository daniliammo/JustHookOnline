
using System;
using System.Collections.Generic;

public static class FisherYatesListShuffle
{
    private static readonly Random Rng = new();  

    public static void Shuffle<T>(this IList<T> list)  
    {  
        var n = list.Count;  
        while (n > 1) 
        {  
            n--;  
            var k = Rng.Next(n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }  
    }
}
