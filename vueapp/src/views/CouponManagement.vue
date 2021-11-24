<template>
    <div class="container-fluid mt-4">
        <div class="row justify-content-center">
            <div class="col-xl-6 col-lg-6 col-12">
                <div class="box-form">
                    <h4>新增優惠券</h4>
                    <div class="mb-3">
                        <label class="form-label">優惠券名稱</label>
                        <input type="text" class="form-control" v-model="inputCouponName" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">折扣類型</label>
                        <select class="form-control" v-model="inputCouponType">
                            <option value="percent">百分比</option>
                            <option value="price">價格</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">折扣金額</label>
                        <input type="number" class="form-control" v-model="inputCouponPP" placeholder="價格或百分比" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">剩餘數量</label>
                        <input type="number" class="form-control" v-model="inputCouponRemainCount" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">有效日期</label>
                        <input type="date" class="form-control" v-model="inputCouponExpireDate" />
                    </div>
                    <div class="d-grid gap-2">
                        <button type="button" class="btn btn-primary" @click="addCoupon()">新增</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <table class="table table-striped table-sm table-bordered table-hover table-responsive">
                    <thead class="thead-default">
                        <tr>
                            <th>優惠券ID</th>
                            <th>優惠券名稱</th>
                            <th>優惠券類型</th>
                            <th>金額/百分比</th>
                            <th>有效日期</th>
                            <th>剩餘數量</th>
                            <th>動作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(data,index) in coupons" :key="index">
                            <td>
                                <span>{{ data.couponID }}</span>
                            </td>
                            <td>
                                <input class="form-control" type="text" v-model="data.couponName" />
                            </td>
                            <td>
                                <select class="form-control" v-model="data.couponType">
                                    <option value="percent">百分比</option>
                                    <option value="price">價格</option>
                                </select>
                            </td>
                            <td>
                                <input class="form-control" type="number" v-model="data.pp" />
                            </td>
                            <td>
                                <input class="form-control" type="date" v-model="data.expireDate" />
                            </td>
                            <td>
                                <input class="form-control" type="number" v-model="data.remainCount" />
                            </td>
                            <td>
                                <button type="button" class="btn btn-success btn-sm" @click="updateCoupon(index)">儲存</button>
                                <button type="button" class="btn btn-danger btn-sm" @click="deleteCoupon(index)">刪除</button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <notification ref="notify"></notification>
</template>

<script lang="ts">
import { ref } from '@vue/reactivity';
import { Vue, Options } from 'vue-class-component';
import Axios from 'axios';
import Http from '../commons/Http';
import Coupon from '../item/coupon';
import Notification from '../components/Notification.vue';

@Options({
    components: {
        Notification,
    },
})
export default class CouponManagement extends Vue {
    inputCouponName: InstanceType<typeof String> | null = ref<any>(null);
    inputCouponType: InstanceType<typeof String> | null = ref<any>(null);
    inputCouponPP: InstanceType<typeof Number> | null = ref<any>(null);
    inputCouponExpireDate: InstanceType<typeof Date> | null = ref<any>(null);
    inputCouponRemainCount: InstanceType<typeof Number> | null = ref<any>(null);
    coupons: InstanceType<typeof Coupon>[] | null = ref<any>(null);

    notify: InstanceType<typeof Notification> | null = ref<any>(null);

    created() {
        this.coupons = [];
    }

    mounted() {
        document.title = '優惠管理 - NumberShop';
        this.getCoupons();
    }

    addCoupon() {
        Axios.post(Http.createApiPath('m/coupon', ''), {
            couponName: this.inputCouponName,
            couponType: this.inputCouponType,
            couponPP: this.inputCouponPP,
            couponExpireDate: this.inputCouponExpireDate,
            remainCount: this.inputCouponRemainCount,
        })
            .then((res) => {
                if (res.data.status) {
                    this.inputCouponName = '';
                    this.inputCouponType = '';
                    this.inputCouponPP = 0;
                    this.inputCouponExpireDate = null;
                    this.getCoupons();
                    this.notify?.success(res.data.messages);
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法新增資料');
            });
    }

    getCoupons() {
        Axios.get(Http.createApiPath('m/coupon', ''))
            .then((res) => {
                if (res.data.status) {
                    this.coupons?.splice(0, this.coupons?.length);
                    for (let i = 0; i < res.data.coupons.length; i++) {
                        let c = res.data.coupons[i];
                        this.coupons?.push(new Coupon(c.couponID, c.couponName, c.couponType, c.percent, c.price, c.remainCount, c.expireDateText));
                    }
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法取得資料');
            });
    }

    updateCoupon(index: number) {
        let coupon = this.coupons![index];
        Axios.put(Http.createApiPath('m/coupon', ''), {
            couponID: coupon.couponID,
            couponName: coupon.couponName,
            couponType: coupon.couponType,
            couponPP: coupon.pp,
            couponExpireDate: coupon.expireDate,
            remainCount: coupon.remainCount,
        })
            .then((res) => {
                if (res.data.status) {
                    this.notify?.success(res.data.messages);
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法更新資料');
            });
    }

    deleteCoupon(index: number) {
        let coupon = this.coupons![index];
        Axios.delete(Http.createApiPath('m/coupon', '', coupon.couponID))
            .then((res) => {
                if (res.data.status) {
                    this.notify?.success(res.data.messages);
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法刪除資料');
            });
    }
}
</script>

<style>
h4 {
    text-align: center;
}
.box-form {
    box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.4);
    border-radius: 10px;
    padding: 1rem;
}
</style>