using Cassandra;

namespace Photo.Api.HostedServices;

public sealed class CassandraStartupService
    : IHostedService
{
    private readonly Cassandra.ISession _session;

    private readonly ILogger<
        CassandraStartupService> _logger;

    public CassandraStartupService(
        Cassandra.ISession session,
        ILogger<CassandraStartupService> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task StartAsync(
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Starting Cassandra validation");

        await ValidateConnectionAsync(
            cancellationToken);

        await EnsureTablesExistAsync(
            cancellationToken);

        _logger.LogInformation(
            "Cassandra startup validation completed");
    }

    public Task StopAsync(
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task ValidateConnectionAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            await _session.ExecuteAsync(
                new SimpleStatement(
                    "SELECT now() FROM system.local"))
                .WaitAsync(cancellationToken);

            _logger.LogInformation(
                "Cassandra connection validated");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(
                ex,
                "Failed to connect to Cassandra");

            throw;
        }
    }

    private async Task EnsureTablesExistAsync(
        CancellationToken cancellationToken)
    {
        var createPhotosByIdTable = @"
CREATE TABLE IF NOT EXISTS photos_by_id
(
    photo_id UUID PRIMARY KEY,
    user_id TEXT,
    title TEXT,
    url TEXT,
    uploaded_at TIMESTAMP
);";

        var createPhotosByUserTable = @"
CREATE TABLE IF NOT EXISTS photos_by_user
(
    user_id TEXT,
    uploaded_at TIMESTAMP,
    photo_id UUID,
    title TEXT,
    url TEXT,
    PRIMARY KEY (user_id, uploaded_at)
)
WITH CLUSTERING ORDER BY (uploaded_at DESC);";

        await _session.ExecuteAsync(
            new SimpleStatement(
                createPhotosByIdTable))
            .WaitAsync(cancellationToken);

        await _session.ExecuteAsync(
            new SimpleStatement(
                createPhotosByUserTable))
            .WaitAsync(cancellationToken);

        _logger.LogInformation(
            "Cassandra tables verified");
    }
}