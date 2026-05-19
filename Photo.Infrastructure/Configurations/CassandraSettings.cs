namespace Photo.Infrastructure.Configurations;

public sealed class CassandraSettings
{
    public List<string> ContactPoints { get; set; } = [];

    public int Port { get; set; }

    public string Keyspace { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string LocalDatacenter { get; set; } = string.Empty;
}