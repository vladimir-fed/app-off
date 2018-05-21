export class SignInModel {
    public userName: string;
    public password: string;
}

export class SignUpModel {
    public userName: string;
    public name: string;
    public password: string;
}

export class TokenModel {
    public token: string;
    public expirationDate: Date;
}

export class User {
    public id: number;
    public name: string;
    public userName: string;
}

export class Post {
    public id: number;
    public title: string;
    public content: string;
    public userId: number;
}

export class Comment {
    public id: number;
    public content: string;
    public dateTime: Date;
    public postId: number;
    public user: User;
}
