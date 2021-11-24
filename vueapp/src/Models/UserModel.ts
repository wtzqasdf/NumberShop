export default class UserModel {
    private _account: string;
    private _identity: string;
    private _email: string;
    private _permission: number;
    private _regdate: string;

    constructor(account: string, identity: string, email: string, permission: number, regdate: string) {
        this._account = account;
        this._identity = identity;
        this._email = email;
        this._permission = permission;
        this._regdate = regdate;
    }

    public get account(): string{
        return this._account;
    }

    public get identity(): string {
        return this._identity;
    }

    public get email(): string{
        return this._email;
    }

    public get permission(): number {
        return this._permission;
    }

    public get regdate(): string{
        return this._regdate;
    }
}