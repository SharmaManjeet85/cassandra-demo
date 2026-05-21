import { useState } from 'react';

import MainLayout from '../layouts/MainLayout';
import Button from '../components/Button';

import { useCreatePhoto } from '../hooks/usePhotos';

export default function CreatePhotoPage() {
  const mutation = useCreatePhoto();

  const [form, setForm] = useState({
    userId: '',
    title: '',
    url: '',
  });

  async function handleSubmit(
    e: React.FormEvent,
  ) {
    e.preventDefault();

    try {
      await mutation.mutateAsync(form);

      alert('Photo created successfully');

      setForm({
        userId: '',
        title: '',
        url: '',
      });
    } catch (error) {
      console.error(error);

      alert('Failed to create photo');
    }
  }

  return (
    <MainLayout>
      <div className="max-w-xl bg-white p-8 rounded-xl shadow">
        <h1 className="text-3xl font-bold">
          Create Photo
        </h1>

        <form
          onSubmit={handleSubmit}
          className="mt-6 space-y-4"
        >
          <div>
            <label className="block mb-2 font-medium">
              User Id
            </label>

            <input
              type="text"
              value={form.userId}
              onChange={(e) =>
                setForm({
                  ...form,
                  userId: e.target.value,
                })
              }
              className="
                w-full
                border
                rounded-lg
                px-4
                py-2
              "
              required
            />
          </div>

          <div>
            <label className="block mb-2 font-medium">
              Title
            </label>

            <input
              type="text"
              value={form.title}
              onChange={(e) =>
                setForm({
                  ...form,
                  title: e.target.value,
                })
              }
              className="
                w-full
                border
                rounded-lg
                px-4
                py-2
              "
              required
            />
          </div>

          <div>
            <label className="block mb-2 font-medium">
              Image URL
            </label>

            <input
              type="text"
              value={form.url}
              onChange={(e) =>
                setForm({
                  ...form,
                  url: e.target.value,
                })
              }
              className="
                w-full
                border
                rounded-lg
                px-4
                py-2
              "
              required
            />
          </div>

          <Button
            type="submit"
            isLoading={mutation.isPending}
          >
            Create Photo
          </Button>
        </form>
      </div>
    </MainLayout>
  );
}