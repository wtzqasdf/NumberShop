<template>
    <div class="container-fluid mt-4">
        <div class="row justify-content-center">
            <div class="col-xl-6 col-lg-8 col-12">
                <div v-if="orderContainers.length === 0" class="text-center">
                    <span class="text-danger font-bold">沒有任何訂單歷史</span>
                </div>
                <div v-for="(container, index1) in orderContainers" :key="index1">
                    <div class="order-board" @click="expandOrder(container)">
                        <span>訂單ID:&nbsp;</span>
                        <span class="font-bold">{{container.orderID }}</span>
                    </div>
                    <div v-if="container.visible">
                        <table class="table table-striped table-sm table-bordered table-hover table-responsive">
                            <thead class="thead-default">
                                <tr>
                                    <th>商品名稱</th>
                                    <th>數量</th>
                                    <th>價格</th>
                                    <th>訂單時間</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(detail, index2) in container.details" :key="index2">
                                    <td>{{ detail.merchName }}</td>
                                    <td>{{ detail.count }}</td>
                                    <td>{{ detail.originPrice }}</td>
                                    <td>{{ detail.orderTime }}</td>
                                </tr>
                            </tbody>
                        </table>
                        <div class="text-end">
                            <span class="text-danger font-bold">{{ '總計:' + container.totalPrice }}</span>
                        </div>
                        <div class="text-end" v-if="container.hasUseCoupon">
                            <span>已使用優惠券</span>
                        </div>
                    </div>
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

@Options({
    components: { Notification },
})
export default class Orders extends Vue {
    notify: InstanceType<typeof Notification> | null = ref<any>(null);
    orderContainers: InstanceType<typeof OrderContainer>[] | null = ref<any>(null);
    userApi!: UserApi;

    created() {
        this.orderContainers = [];
        this.userApi = new UserApi();
    }

    mounted() {
        document.title = '訂單管理 - NumberShop';
        this.userApi.getUserModel(this.getUserModel);
    }

    getUserModel(status: boolean, model: UserModel) {
        if (status) {
            this.getOrders();
        } else {
            this.$router.replace('/login');
        }
    }

    getOrders() {
        Axios.get(Http.createApiPath('order', 'merch'))
            .then((res) => {
                if (res.data.status) {
                    for (let i = 0; i < res.data.containers.length; i++) {
                        let container = res.data.containers[i];
                        this.orderContainers?.push(new OrderContainer(container.orderID, container.orders, container.totalPrice, container.hasUseCoupon));
                    }
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法取得訂單資料');
            });
    }

    expandOrder(container: OrderContainer) {
        container.visible = container.visible ? false : true;
    }
}
class OrderContainer {
    private _orderID: string;
    private _visible: boolean;
    private _details: OrderDetail[];
    private _totalPrice: number;
    private _hasUseCoupon: boolean;

    constructor(orderID: string, orders: any, totalPrice: number, hasUseCoupon: boolean) {
        this._orderID = orderID;
        this._visible = false;
        this._totalPrice = totalPrice;
        this._hasUseCoupon = hasUseCoupon;
        this._details = [];
        for (let i = 0; i < orders.length; i++) {
            let o = orders[i];
            this._details.push(new OrderDetail(o.merchName, o.count, o.orderTimeText, o.originPrice));
        }
    }

    get orderID() {
        return this._orderID;
    }

    get details() {
        return this._details;
    }

    get totalPrice(): number {
        return this._totalPrice;
    }

    get hasUseCoupon(): boolean {
        return this._hasUseCoupon;
    }

    get visible() {
        return this._visible;
    }

    set visible(v: boolean) {
        this._visible = v;
    }

    addDetail(detail: OrderDetail) {
        this._details.push(detail);
    }
}
class OrderDetail {
    private _merchName: string;
    private _count: number;
    private _orderTime: string;
    private _originPrice: number;

    constructor(merchName: string, count: number, orderTime: string, originPrice: number) {
        this._merchName = merchName;
        this._count = count;
        this._orderTime = orderTime;
        this._originPrice = originPrice;
    }

    get merchName() {
        return this._merchName;
    }

    get count() {
        return this._count;
    }

    get orderTime() {
        return this._orderTime;
    }

    get originPrice() {
        return this._originPrice;
    }
}
</script>

<style scoped>
table {
    margin: 0;
}
.font-bold {
    font-weight: bold;
}
.order-board {
    width: 100%;
    background-color: rgb(228, 228, 228);
    padding: 0.2rem;
    margin: 0.3rem 0;
    cursor: pointer;
}
.order-board:hover {
    background-color: rgb(194, 194, 194);
}
</style>