class 関数ライブラリ
{
    int GCD(int a, int b) => a % b == 0 ? b : GCD(b, a % b);

    int GCD(IEnumerable<int> a)
    {
        var res = a.First();
        foreach (var item in a) res = GCD(item, res);
        return res;
    }

    int GCD(params int[] a)
    {
        var res = a[0];
        for (int i = 0; i < a.Length; i++)
        {
            res = GCD(res, a[i]);
        }
        return res;
    }

    bool IsPrime(int n)
    {
        if (n < 2) return false;
        var r = (int)Math.Sqrt(n);
        for (int i = 2; i <= r; i++) if (n % i == 0) return false;
        return true;
    }

    long Pow(long x, int n, int m = (int)1e9 + 7)
    {
        var res = 1L;
        while (n != 0)
        {
            if ((n & 1) != 0) res = res * x % m;
            x = x * x % m;
            n >>= 1;
        }
        return res;
    }

    int BitCount(int n)
    {
        n = (n & 0x55555555) + (n >> 1 & 0x55555555);
        n = (n & 0x33333333) + (n >> 2 & 0x33333333);
        n = (n & 0x0f0f0f0f) + (n >> 4 & 0x0f0f0f0f);
        n = (n & 0x00ff00ff) + (n >> 8 & 0x00ff00ff);
        return (n & 0x0000ffff) + (n >> 16 & 0x0000ffff);
    }

    int BitCount(long n)
    {
        n = (n & 0x5555555555555555) + (n >> 1 & 0x5555555555555555);
        n = (n & 0x3333333333333333) + (n >> 2 & 0x3333333333333333);
        n = (n & 0x0f0f0f0f0f0f0f0f) + (n >> 4 & 0x0f0f0f0f0f0f0f0f);
        n = (n & 0x00ff00ff00ff00ff) + (n >> 8 & 0x00ff00ff00ff00ff);
        n = (n & 0x0000ffff0000ffff) + (n >> 16 & 0x0000ffff0000ffff);
        return (int)((n & 0x000000000000003f) + (n >> 32 & 0x000000000000003f));
    }

    double Differential(Func<double, double> f, double x)
    {
        return (f(x + 1.0 / 1000000) - f(x - 1.0 / 1000000)) / (1.0 / 500000);
    }

    double Integral(Func<double, double> f, double a, double b, int n = 50000)
    {
        n *= 2;
        var w = (b - a) / n;

        var res = (f(a) + f(b)) * w / 3;
        for (int i = 1; i < n; i++)
        {
            res += f(a + w * i) * ((i & 1) == 0 ? 2 : 4) * w / 3;
        }

        return res;
    }
}
