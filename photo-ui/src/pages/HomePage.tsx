import MainLayout from '../layouts/MainLayout';

export default function HomePage() {
  return (
    <MainLayout>
      <h1 className="text-3xl font-bold">
        Welcome to Photo App
      </h1>

      <p className="mt-4 text-gray-600">
        Distributed photo platform using
        Cassandra and .NET 10
      </p>
    </MainLayout>
  );
}