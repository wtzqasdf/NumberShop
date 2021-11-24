export default class Coupon {
    private _couponID: string;
    private _couponName: string;
    private _couponType: string;
    private _pp: number;
    private _remainCount: number;
    private _expireDate: string;

    constructor(couponID: string, couponName: string, couponType: string, percent: number | null, price: number | null, remainCount: number, expireDate: string) {
        this._couponID = couponID;
        this._couponName = couponName;
        this._couponType = couponType;
        this._remainCount = remainCount;
        if (percent !== null) {
            this._pp = percent as number;
        } else {
            this._pp = price as number;
        }
        this._expireDate = expireDate;
    }

    public get couponID(): string {
        return this._couponID;
    }

    public get couponName(): string {
        return this._couponName;
    }
    public set couponName(n: string) {
        this._couponName = n;
    }

    public get couponType(): string {
        return this._couponType;
    }
    public set couponType(t: string) {
        this._couponType = t;
    }

    public get pp(): number {
        return this._pp;
    }
    public set pp(p: number) {
        this._pp = p;
    }

    public get remainCount(): number {
        return this._remainCount;
    }
    public set remainCount(c: number) {
        this._remainCount = c;
    }

    public get expireDate(): string {
        return this._expireDate;
    }
    public set expireDate(d: string) {
        this._expireDate = d;
    }
}