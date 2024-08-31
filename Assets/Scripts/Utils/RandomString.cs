using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Utils
{
    public static class RandomString
    {
        
        private static readonly Random Random = new();
        
        
        [Pure]
        public static string GetRandomString(uint length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_=";
            return new string(Enumerable.Repeat(chars, (int)length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}