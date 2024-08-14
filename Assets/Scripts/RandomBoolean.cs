using System;
using System.Diagnostics.Contracts;


public static class RandomBoolean
{

    [Pure]
    public static bool GetRandomBoolean(int chanceOfTrue) => new Random().Next(100) < chanceOfTrue; 
    
}
