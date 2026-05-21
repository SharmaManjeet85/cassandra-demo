import type { ReactNode } from 'react';
import { Link } from 'react-router-dom';

interface Props {
  children: ReactNode;
}

export default function MainLayout({
  children,
}: Props) {
  return (
    <div className="min-h-screen bg-gray-100">
      <header className="bg-white shadow-sm">
        <div className="max-w-6xl mx-auto px-6 py-4 flex justify-between">
          <Link
            to="/"
            className="text-2xl font-bold text-blue-600"
          >
            Photo App
          </Link>

          <Link
            to="/create"
            className="bg-blue-600 text-white px-4 py-2 rounded-lg"
          >
            Add Photo
          </Link>
        </div>
      </header>

      <main className="max-w-6xl mx-auto p-6">
        {children}
      </main>
    </div>
  );
}