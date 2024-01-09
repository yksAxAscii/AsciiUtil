using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class intExtensions
{
    /// <summary>
    /// m,nの最大公約数を返します
    /// </summary>
    /// <param name="m"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int Gcd(this int self, int m, int n)
    {
        self = n == 0 ? Mathf.Abs(m) : Gcd(self, n, m % n);
        return self;
    }
}
