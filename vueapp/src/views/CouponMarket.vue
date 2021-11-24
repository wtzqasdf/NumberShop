<template>
    <div class="container-fluid mt-4">
        <div class="row justify-content-center">
            <div class="col-xl-4 col-lg-6 col-md-6 col-12 mt-2" v-for="(data,index) in coupons" :key="index">
                <div class="coupon" @click="addCouponToUser(index)">
                    <div class="d-flex justify-content-between">
                        <h4>{{ data.couponName }}</h4>
                        <small class="text-danger">{{ '剩' + data.remainCount + '張'}}</small>
                    </div>
                    <div v-if="data.couponType === 'percent'">{{ '折抵' + data.pp + '%' }}</div>
                    <div v-else-if="data.couponType === 'price'">{{ '折抵' + data.pp + '元' }}</div>
                    <div class="d-flex justify-content-end">
                        <span>{{ '有效日期: ' + data.expireDate }}</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <notification ref="notify"></notification>
</template>

<script lang="ts">
import Http from '@/commons/Http';
import { ref } from '@vue/reactivity';
import Axios from 'axios';
import { Vue, Options } from 'vue-class-component';
import Coupon from '../item/coupon';
import Notification from '../components/Notification.vue';

@Options({
    components: {
        Notification,
    },
})
export default class CouponMarket extends Vue {
    coupons: InstanceType<typeof Coupon>[] | null = ref<any>(null);
    notify: InstanceType<typeof Notification> | null = ref<any>(null);

    created() {
        this.coupons = [];
    }

    mounted() {
        document.title = '優惠券市場 - NumberShop';
        this.getCoupons();
    }

    getCoupons() {
        Axios.get(Http.createApiPath('coupon', ''))
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

    addCouponToUser(index: number) {
        let coupon = this.coupons![index];
        Axios.post(Http.createApiPath('coupon', coupon.couponID))
            .then((res) => {
                if (res.data.status) {
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
}
</script>

<style scoped>
.font-italic {
    font-style: italic;
}
.font-bold {
    font-weight: bold;
}
.coupon {
    border: 2px dotted black;
    padding: 0.5rem;
    cursor: pointer;
}
.coupon:hover {
    background-color: rgba(0, 255, 0, 0.2);
}
.coupon:active {
    background-color: rgba(0, 255, 0, 0.5);
}
</style>