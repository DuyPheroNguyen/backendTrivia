using Supabase;
using Supabase.Interfaces;
using System;
using System.Threading.Tasks;

public class SupabaseService
{
    private static Client _client;

    public static Client Client
    {
        get
        {
            if (_client == null)
                Initialize();

            return _client;
        }
    }

    private static void Initialize()
    {
        var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
        var key = Environment.GetEnvironmentVariable("SUPABASE_ANON_KEY");

        var options = new SupabaseOptions
        {
            AutoRefreshToken = true,
            AutoConnectRealtime = true
        };

        _client = new Client(url, key, options);
    }

    public static async Task InitializeAsync()
    {
        Initialize();
        await _client.InitializeAsync();
    }
}
