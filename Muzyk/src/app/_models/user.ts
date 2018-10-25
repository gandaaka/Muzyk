import { Photo } from './photo';
import { Video } from './video';

export interface User {
    id: number;
    username: string;
    firstName: string;
    lastName: string;
    age: number;
    genre: string;
    gender: string;
    created: Date;
    lastActive: Date;
    photoUrl: string;
    coverPhotoUrl: string;
    city: string;
    country: string;
    interests?: string;
    introduction?: string;
    yearsOfExperience?: number;
    photos?: Photo[];
    videos?: Video[];
}
