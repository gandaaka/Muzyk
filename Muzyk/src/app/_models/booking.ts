import { User } from './user';

export interface Booking {
    recipientId: number;
    title: string;
    desc: string;
    date: Date;
    bookee: User;
}
