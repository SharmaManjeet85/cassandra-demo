import { useState } from 'react';

import MainLayout from '../layouts/MainLayout';

import PhotoCard from '../components/PhotoCard';

import { usePhotos }
  from '../hooks/usePhotos';

export default function HomePage() {
  const [userId, setUserId] =
    useState('');

  const {
    data,
    isLoading,
  } = usePhotos(userId);

  return (
    <MainLayout>
      <div className="mb-6">
        <input
          type="text"
          value={userId}
          onChange={(e) =>
            setUserId(e.target.value)
          }
          placeholder="Enter user id"
          className="
            border
            rounded-lg
            px-4
            py-2
            w-full
            max-w-md
          "
        />
      </div>

      {isLoading && (
        <p>Loading photos...</p>
      )}

      <div className="
        grid
        grid-cols-1
        md:grid-cols-2
        lg:grid-cols-3
        gap-6
      ">
        {data?.map((photo: any) => (
          <PhotoCard
            key={photo.id}
            photo={photo}
          />
        ))}
      </div>
    </MainLayout>
  );
}