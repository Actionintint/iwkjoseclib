using System;

class ModBinom
{
    long[] fac;
    long[] finv;
    long[] inv;
    int mod;

    public ModBinom(int N, int p = (int)1e9 + 7)//N < p  O(N)
    {
        N++;

        fac = new long[N];
        finv = new long[N];
        inv = new long[N];

        fac[0] = fac[1] = 1;
        finv[0] = finv[1] = 1;
        inv[1] = 1;
        for (int i = 2; i < N; i++)
        {
            fac[i] = fac[i - 1] * i % p;
            inv[i] = p - inv[p % i] * (p / i) % p;
            finv[i] = finv[i - 1] * inv[i] % p;
        }

        mod = p;
    }

    public long Binom(int n, int k)// O(1)
    {
        if (n < k) return 0;
        if (n < 0 || k < 0) return 0;
        return fac[n] * finv[k] % mod * finv[n - k] % mod;
    }
}
