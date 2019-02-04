export interface User {
    id: number;
    username: string;
    gender: string;
    age: number;
    registrationDate: Date;
    lastActive: Date;
    introduction?: string;
    photoUrl?: string;
}
