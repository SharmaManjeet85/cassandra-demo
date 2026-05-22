import { useMutation, useQuery } from '@tanstack/react-query';

import { photoService } from '../services/photoService';

export function useCreatePhoto() {
  return useMutation({
    mutationFn: photoService.createPhoto,
  });

}
export function usePhotos(userId: string) {
  return useQuery({
    queryKey: ['photos', userId],

    queryFn: () =>
      photoService.getPhotosByUser(userId),

    enabled: !!userId,
  });
}