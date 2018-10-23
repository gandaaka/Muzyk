export interface Photo {
    id: number;
    mediaUrl: string;
    description: string;
    dateAdded: Date;
    isProfilePhoto: boolean;
    isCoverPhoto: boolean;
}
