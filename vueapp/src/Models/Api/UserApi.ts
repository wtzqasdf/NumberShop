import Axios from 'axios';
import Http from '../../commons/Http';
import UserModel from '../UserModel';

export default class UserApi {
    protected static _lockStatus: boolean;
    protected static _userModel: UserModel;
    protected static _status: boolean;

    public static getUserData() {
        this._lockStatus = true;

        Axios.get(Http.createApiPath('user', 'member'))
            .then((res) => {
                if (res.data.status) {
                    const model = new UserModel(
                        res.data.user.account,
                        res.data.user.identity,
                        res.data.user.email,
                        res.data.user.permission,
                        res.data.user.regdate
                    );
                    this._status = true;
                    this._userModel = model;
                } else {
                    this._status = false;
                }

                this._lockStatus = false;
            })
            .catch(() => {
                this._status = false;

                this._lockStatus = false;
            });
    }

    constructor() {
        this._timer = -10000;
    }

    private _timer: number;
    public getUserModel(callback: (status: boolean, model: UserModel) => void) {
        if (this._timer !== -10000)
            return;

        this._timer = setInterval(() => {
            if (!UserApi._lockStatus) {
                callback(UserApi._status, UserApi._userModel);
                clearInterval(this._timer);
                this._timer = -10000;
            }
        }, 10);
    }
}