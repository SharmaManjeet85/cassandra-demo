import { useMutation } from '@tanstack/react-query';

import { photoService } from '../services/photoService';

export function useCreatePhoto() {
  return useMutation({
    mutationFn: photoService.createPhoto,
  });
}