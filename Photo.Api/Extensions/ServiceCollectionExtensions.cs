using Cassandra;
using Microsoft.Extensions.Options;
using Photo.Application.Interfaces;
using Photo.Infrastructure.Configurations;
using Photo.Infrastructure.Repositories;
namespace Photo.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<CassandraSettings>(
            configuration.GetSection("Cassandra"));

        services.AddSingleton<ICluster>(sp =>
        {
            var settings = sp
                .GetRequiredService<IOptions<CassandraSettings>>()
                .Value;

            var builder = Cluster.Builder()
                .AddContactPoints(settings.ContactPoints)
                .WithPort(settings.Port)
                .WithLoadBalancingPolicy(
                    Policies.DefaultLoadBalancingPolicy);

            if (!string.IsNullOrWhiteSpace(
                    settings.LocalDatacenter))
            {
                builder = builder.WithLoadBalancingPolicy(
                    new DefaultLoadBalancingPolicy(
                        settings.LocalDatacenter));
            }

            if (!string.IsNullOrWhiteSpace(
                    settings.Username))
            {
                builder = builder.WithCredentials(
                    settings.Username,
                    settings.Password);
            }

            return builder.Build();
        });

        services.AddSingleton<Cassandra.ISession>(sp =>
        {
            var cluster = sp
                .GetRequiredService<ICluster>();

            var settings = sp
                .GetRequiredService<IOptions<CassandraSettings>>()
                .Value;

            return cluster.Connect(settings.Keyspace);
        });

        services.AddScoped<IPhotoRepository, PhotoRepository>();

        return services;
    }
}