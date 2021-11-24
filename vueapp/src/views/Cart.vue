<template>
    <div class="container-fluid mt-4">
        <div class="row justify-content-center">
            <div class="col-xl-6 col-lg-8 col-12">
                <table class="table table-striped table-sm table-bordered table-hover table-responsive">
                    <thead class="thead-default">
                        <tr>
                            <th>商品名稱</th>
                            <th>價格</th>
                            <th>購買數量</th>
                            <th>動作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(merch, index) in cartMerches" :key="index">
                            <td>{{ merch.name }}</td>
                            <td>{{ merch.price }}</td>
                            <td>{{ merch.count }}</td>
                            <td class="text-center">
                                <button type="button" class="btn btn-danger btn-sm" @click="deleteCartMerch(merch.merchID)">刪除</button>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="mb-3">
                    <label class="form-label">優惠券:&nbsp;</label>
                    <select v-model="inputCouponID" @change="calcTotalPrice()">
                        <option v-for="(data,index) in userCoupons" :key="index" :value="data.couponID">{{ data.couponName }}</option>
                    </select>
                </div>

                <div class="d-flex flex-column justify-content-end align-items-end mt-2">
                    <span>{{ '總數: ' + cartBill.merchCount }}</span>
                    <span class="font-bold text-danger" style="font-size:18px;">{{ '總計: ' + cartBill.totalPrice }}</span>
                </div>
                <div class="d-grid gap-2 mt-2">
                    <button type="button" class="btn btn-success" @click="addNewOrder()">完成下單</button>
                </div>
            </div>
        </div>
    </div>
    <notification ref="notify"></notification>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import { ref } from 'vue';
import Axios from 'axios';
import Http from '../commons/Http';
import Notification from '../components/Notification.vue';
import UserApi from '../Models/Api/UserApi';
import UserModel from '../Models/UserModel';
import Coupon from '../item/coupon';

@Options({
    components: { Notification },
})
export default class ShoppingCart extends Vue {
    notify: InstanceType<typeof Notification> | null = ref<any>(null);
    cartBill: InstanceType<typeof CartBill> | null = ref<any>(null);
    cartMerches: InstanceType<typeof CartMerch>[] | null = ref<any>(null);
    userCoupons: InstanceType<typeof Coupon>[] | null = ref<any>(null);
    userApi!: UserApi;

    inputCouponID: InstanceType<typeof String> | null = ref<any>(null);

    created() {
        this.cartBill = new CartBill(0, 0);
        this.cartMerches = [];
        this.userCoupons = [];
        this.userApi = new UserApi();
    }

    mounted() {
        document.title = '購物車 - NumberShop';
        this.userApi.getUserModel(this.getUserModel);
    }

    getUserModel(status: boolean, model: UserModel) {
        if (status) {
            this.getCartMerches();
            this.getUserCoupons();
            this.inputCouponID = '';
        } else {
            this.$router.replace('/login');
        }
    }

    getCartMerches() {
        Axios.get(Http.createApiPath('cart', 'merch'))
            .then((res) => {
                if (res.data.status) {
                    this.cartBill!.totalPrice = res.data.bill.totalPrice;
                    this.cartBill!.originPrice = res.data.bill.totalPrice;
                    this.cartBill!.merchCount = res.data.bill.merchCount;

                    this.cartMerches?.splice(0, this.cartMerches?.length);
                    for (let i = 0; i < res.data.merches.length; i++) {
                        let merch = res.data.merches[i];
                        this.cartMerches?.push(new CartMerch(merch.merchID, merch.name, merch.price, merch.count));
                    }
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法取得購物車資料');
            });
    }

    getUserCoupons() {
        Axios.get(Http.createApiPath('coupon', 'user'))
            .then((res) => {
                if (res.data.status) {
                    this.userCoupons?.splice(0, this.userCoupons?.length);
                    this.userCoupons?.push(new Coupon('', '(不使用)', '', 0, 0, 0, ''));
                    for (let i = 0; i < res.data.coupons.length; i++) {
                        let u = res.data.coupons[i];
                        this.userCoupons?.push(new Coupon(u.couponID, u.couponName, u.couponType, u.percent, u.price, u.remainCount, u.expireDateText));
                    }
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法取得優惠券資料');
            });
    }

    deleteCartMerch(merchID: string) {
        Axios.delete(Http.createApiPath('cart', 'merch'), { data: { merchID: merchID } })
            .then((res) => {
                if (res.data.status) {
                    this.getCartMerches();
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法刪除購物車資料');
            });
    }

    calcTotalPrice() {
        let coupon = this.userCoupons?.find((f) => {
            return f.couponID === this.inputCouponID;
        }) as Coupon;
        if (coupon?.couponType === 'percent') {
            this.cartBill!.totalPrice = this.cartBill!.originPrice * (coupon.pp / 100);
        } else {
            this.cartBill!.totalPrice = this.cartBill!.originPrice - coupon.pp;
        }
    }

    addNewOrder() {
        Axios.post(Http.createApiPath('order', 'merch'), { couponID: this.inputCouponID })
            .then((res) => {
                if (res.data.status) {
                    this.$router.push('/orders');
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法建立訂單');
            });
    }
}

class CartBill {
    private _originPrice: number;
    private _totalPrice: number;
    private _merchCount: number;

    constructor(totalPrice: number, merchCount: number) {
        this._originPrice = totalPrice;
        this._totalPrice = totalPrice;
        this._merchCount = merchCount;
    }

    get originPrice(): number {
        return this._originPrice;
    }

    set originPrice(p: number) {
        this._originPrice = p;
    }

    get totalPrice(): number {
        return this._totalPrice;
    }

    set totalPrice(p: number) {
        this._totalPrice = p;
    }

    get merchCount(): number {
        return this._merchCount;
    }

    set merchCount(c: number) {
        this._merchCount = c;
    }
}

class CartMerch {
    private _merchID: string;
    private _name: string;
    private _price: number;
    private _count: number;

    constructor(merchID: string, name: string, price: number, count: number) {
        this._merchID = merchID;
        this._name = name;
        this._price = price;
        this._count = count;
    }

    get merchID(): string {
        return this._merchID;
    }

    get name(): string {
        return this._name;
    }

    get price(): number {
        return this._price;
    }

    get count(): number {
        return this._count;
    }
}
</script>

<style scoped>
.font-bold {
    font-weight: bold;
}
.merch-board {
    padding: 0.3rem;
    background-color: rgb(238, 238, 238);
    border: 2px solid gray;
}
</style>