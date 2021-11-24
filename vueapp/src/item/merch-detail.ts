export default class MerchDetail {
    private _name: string;
    private _description: string;
    private _price: number;
    private _merchID: string;
    private _typeID: string;
    private _remain: number;

    constructor(name: string, description: string, price: number, remain: number, merchID: string, typeID: string) {
        this._name = name;
        this._description = description;
        this._price = price;
        this._remain = remain;
        this._merchID = merchID;
        this._typeID = typeID;
    }

    get name(): string {
        return this._name;
    }

    set name(n: string) {
        this._name = n;
    }

    get description(): string {
        return this._description;
    }

    set description(d: string) {
        this._description = d;
    }

    get price(): number {
        return this._price;
    }

    set price(p: number) {
        this._price = p;
    }

    get remain(): number {
        return this._remain;
    }

    set remain(r: number) {
        this._remain = r;
    }

    get merchID(): string {
        return this._merchID;
    }

    set merchID(m: string) {
        this._merchID = m;
    }

    get typeID(): string {
        return this._typeID;
    }

    set typeID(t: string) {
        this._typeID = t;
    }

    get priceText(): string {
        return '$' + this._price;
    }
}