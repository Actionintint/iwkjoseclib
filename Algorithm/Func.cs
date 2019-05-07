class Func
{
    public static int GCD(int a, int b)
    {
        while (b != 0)
        {
            var c = a;
            a = b;
            b = c % b;
        }
        return a;
    }

    public static bool IsPrime(int n)
    {
        if (n < 2) return false;
        if (n == 2) return true;
        if ((n & 1) == 0) return false;
        var r = (int)Math.Sqrt(n);
        for (int i = 3; i <= r; i += 2) if (n % i == 0) return false;
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

    int[,] MatrixMul(int[,] x, int[,] y)
    {
        var r = x.GetLength(0);
        var c = y.GetLength(1);
        var l = x.GetLength(1);
        var m = new int[r, c];
        for (int i = 0; i < r; i++)
        {
            for (int j = 0; j < c; j++)
            {
                var a = 0;
                for (int k = 0; k < l; k++)
                {
                    a += x[i, k] * y[k, j];
                }
                m[i, j] = a;
            }
        }
        return m;
    }

    long[] fac;
    long[] finv;
    long[] inv;
    const int mod = (int)1e9 + 7;
    const int nMax = 5 * (int)1e5;

    void InitC()
    {
        fac = new long[nMax];
        finv = new long[nMax];
        inv = new long[nMax];

        fac[0] = fac[1] = 1;
        finv[0] = finv[1] = 1;
        inv[1] = 1;
        for (int i = 2; i < nMax; i++)
        {
            fac[i] = fac[i - 1] * i % mod;
            inv[i] = mod - inv[mod % i] * (mod / i) % mod;
            finv[i] = finv[i - 1] * inv[i] % mod;
        }
    }

    long C(int n, int r)
    {
        if (n < r) return 0;
        if (n < 0 || r < 0) return 0;
        return fac[n] * (finv[r] * finv[n - r] % mod) % mod;
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

    int[] orgSet;
    int[] comSet;
    int cp;
    int varCom;
    int[] max;
    bool finished;

    public void InitCombination(int n, int k)
    {
        orgSet = Enumerable.Range(0, n).ToArray();
        comSet = Enumerable.Range(0, k).ToArray();
        varCom = cp = k - 1;
        max = Enumerable.Range(n - k, k).ToArray();
        finished = false;
    }

    public void NextCombination()
    {
        if (finished) return;

        if (comSet[0] >= max[0])
        {
            finished = true;
            return;
        }

        if (comSet[cp] == max[cp])
        {
            while (comSet[cp] == max[cp]) cp--;
            int Loc = comSet[cp];
            if (comSet[cp] + 1 == max[cp]) comSet[cp] = ++Loc;
            else for (int t = cp; t <= varCom; t++) comSet[t] = ++Loc;
            cp = varCom;
            return;
        }
        else comSet[cp]++;
    }
}
