export interface IUser {
    id: number;
    username: String;
    password: String;
    firstName: String;
    lastName: String;
    token: String;
    role: String;
    isBlocked: boolean;
}