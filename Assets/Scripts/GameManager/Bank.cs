public class Bank
{
    private static Bank _instance;

    // Implementation of the Singleton pattern
    public static Bank instance
    {
        get
        {
            if (_instance == null)
                _instance = new Bank();

            return _instance;
        }
    }

    public int coins { get; set; } = 100000;
}
