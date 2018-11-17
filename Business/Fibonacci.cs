namespace Business
{
    /// <summary>
    /// Method related to the Fibonacci sequence
    /// </summary>
    public static class Fibonacci
    {
        /// <summary>
        /// Return the n th Fibonacci number in the sequence
        /// </summary>
        public static int GetFibNumber(int n)
        {
            if (1 <= n && n <= 100)
            {
                if (n == 1 || n == 2)
                {
                    return 1;
                }

                return GetFibNumber(n - 1) + GetFibNumber(n - 2);
            }

            return -1;
        }
    }
}
