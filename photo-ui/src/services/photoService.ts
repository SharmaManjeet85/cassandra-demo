import { apiClient } from '../api/axios';
import type {
    CreatePhotoRequest,
    Photo,
} from '../models/photo';

export const photoService = {
  async createPhoto(
    request: CreatePhotoRequest,
  ): Promise<void> {
    await apiClient.post('/photos', request);
  },

  async getPhotosByUser(
    userId: string,
  ): Promise<Photo[]> {
    const response = await apiClient.get<Photo[]>(
      `/photos/user/${userId}`,
    );

    return response.data;
  },
};