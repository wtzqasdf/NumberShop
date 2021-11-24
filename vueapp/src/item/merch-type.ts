export default class MerchType {
    private _name: string;
    private _typeID: string;

    constructor(name: string, typeID: string) {
        this._name = name;
        this._typeID = typeID;
    }

    get name(): string {
        return this._name;
    }

    set name(n: string) {
        this._name = n;
    }

    get typeID(): string {
        return this._typeID;
    }

    set typeID(id: string) {
        this._typeID = id;
    }
}