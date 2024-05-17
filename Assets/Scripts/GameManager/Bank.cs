public class Bank
{
    public static Bank instance
    {
        get
        {
            if (_instance == null)
                _instance = new Bank();

            return _instance;
        }
    }

    private static Bank _instance;

    public int coins { get; set; }
}
