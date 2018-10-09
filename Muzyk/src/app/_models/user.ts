import { Photo } from './photo';

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
    city: string;
    country: string;
    interests?: string;
    introduction?: string;
    yearsOfExperience?: number;
    photos?: Photo[];
}
