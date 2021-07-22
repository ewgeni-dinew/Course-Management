export interface IUser {
    id: number;
    username: String;
    password: String;
    firstName: String;
    lastName: String;
    accessToken: String;
    refreshToken: String;
    role: String;
    isBlocked: boolean;
}