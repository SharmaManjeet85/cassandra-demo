var builder = DistributedApplication.CreateBuilder(args);


builder.AddContainer(
    name: "cassandra",
    image: "cassandra")
    .WithEndpoint(
        port: 9042,
        targetPort: 9042,
        name: "cql");


builder.AddProject<Projects.Photo_Api>(
    "photo-api");


builder.Build().Run();