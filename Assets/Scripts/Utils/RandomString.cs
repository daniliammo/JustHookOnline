using System;
using System.Linq;

namespace Utils
{
    public class RandomString
    {
        
        private static readonly Random Random = new Random();
        
        
        public static string GetRandomString(uint length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_=";
            return new string(Enumerable.Repeat(chars, (int)length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}