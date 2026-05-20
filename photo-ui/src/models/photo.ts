export interface Photo {
  photoId: string;
  userId: string;
  title: string;
  url: string;
  uploadedAt: string;
}

export interface CreatePhotoRequest {
  userId: string;
  title: string;
  url: string;
}