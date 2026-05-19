using Cassandra;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Photo.Api.HealthChecks;

public sealed class CassandraHealthCheck
    : IHealthCheck
{
    private readonly Cassandra.ISession _session;

    public CassandraHealthCheck(Cassandra.ISession session)
    {
        _session = session;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _session.ExecuteAsync(
                new SimpleStatement(
                    "SELECT now() FROM system.local"))
                .WaitAsync(cancellationToken);

            return HealthCheckResult.Healthy(
                "Cassandra is healthy");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(
                "Cassandra is unavailable",
                ex);
        }
    }
}