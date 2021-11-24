<template>
    <div class="container-fluid mt-4">
        <!-- 新增商品 -->
        <div class="row justify-content-center">
            <div class="col-xl-6 col-lg-6 col-12">
                <div class="box-form">
                    <h4>新增商品</h4>
                    <div class="mb-3">
                        <label class="form-label">商品類型</label>
                        <select class="form-control" v-model="inputAddMerchTypeID">
                            <option v-for="(data,index) in merchTypeList" :key="index" :value="data.typeID">{{ data.name }}</option>
                        </select>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">商品名稱</label>
                        <input type="text" class="form-control" v-model="inputAddMerchName" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">商品描述</label>
                        <input type="text" class="form-control" v-model="inputAddMerchDesc" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">價格</label>
                        <input type="number" class="form-control" v-model="inputAddMerchPrice" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">商品數量</label>
                        <input type="number" class="form-control" v-model="inputAddMerchRemain" />
                    </div>
                    <div class="d-grid gap-2">
                        <button type="button" class="btn btn-primary" @click="addMerchDetail()">新增</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="row mt-2">
            <div class="col-12">
                <table class="table table-striped table-sm table-bordered table-hover table-responsive">
                    <thead class="thead-default">
                        <tr>
                            <th>商品類型</th>
                            <th>商品名稱</th>
                            <th>商品敘述</th>
                            <th>價格</th>
                            <th>剩餘數量</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(data,index) in merchDetailList" :key="index">
                            <td>
                                <select class="form-control" v-model="data.typeID">
                                    <option v-for="(data2,index2) in merchTypeList" :key="index2" :value="data2.typeID">{{ data2.name }}</option>
                                </select>
                            </td>
                            <td>
                                <input class="form-control" type="text" v-model="data.name" />
                            </td>
                            <td>
                                <input class="form-control" type="text" v-model="data.description" />
                            </td>
                            <td>
                                <input class="form-control" type="number" v-model="data.price" />
                            </td>
                            <td>
                                <input class="form-control" type="number" v-model="data.remain" />
                            </td>
                            <td>
                                <button type="button" class="btn btn-success btn-sm" @click="updateMerchDetail(index)">儲存</button>
                                <button type="button" class="btn btn-danger btn-sm" @click="deleteMerchDetail(index)">刪除</button>
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
import { Vue, Options } from 'vue-class-component';
import { ref } from 'vue';
import MerchType from '../item/merch-type';
import MerchDetail from '../item/merch-detail';
import Axios from 'axios';
import Http from '../commons/Http';
import Notification from '../components/Notification.vue';
import UserApi from '../Models/Api/UserApi';
import UserModel from '../Models/UserModel';

@Options({
    components: {
        Notification,
    },
})
export default class MerchManagement extends Vue {
    merchTypeList: InstanceType<typeof MerchType>[] | null = ref<any>(null);
    merchDetailList: InstanceType<typeof MerchDetail>[] | null = ref<any>(null);
    notify: InstanceType<typeof Notification> | null = ref<any>(null);
    userApi!: UserApi;

    inputAddMerchTypeID: InstanceType<typeof String> | null = ref<any>(null);
    inputAddMerchName: InstanceType<typeof String> | null = ref<any>(null);
    inputAddMerchDesc: InstanceType<typeof String> | null = ref<any>(null);
    inputAddMerchPrice: InstanceType<typeof Number> | null = ref<any>(null);
    inputAddMerchRemain: InstanceType<typeof Number> | null = ref<any>(null);

    created() {
        this.inputAddMerchTypeID = '';
        this.inputAddMerchName = '';
        this.inputAddMerchDesc = '';
        this.inputAddMerchPrice = 0;
        this.inputAddMerchRemain = 0;
        this.merchTypeList = [];
        this.merchDetailList = [];
        this.userApi = new UserApi();
    }

    mounted() {
        document.title = '商品管理 - NumberShop';
        this.userApi.getUserModel(this.getUserModel);
    }

    getUserModel(status: boolean, model: UserModel) {
        if (status && model.permission === 1) {
            this.getMerchTypes();
            this.getMerchDetails();
        } else {
            this.$router.replace('/');
        }
    }

    getMerchTypes() {
        Axios.get(Http.createApiPath('m/merch', 'merchtype'))
            .then((res) => {
                if (res.data.status) {
                    this.merchTypeList?.splice(0, this.merchTypeList?.length);
                    for (let i = 0; i < res.data.types.length; i++) {
                        this.merchTypeList?.push(res.data.types[i]);
                    }
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法取得資料');
            });
    }

    getMerchDetails() {
        Axios.get(Http.createApiPath('m/merch', 'merchdetail'))
            .then((res) => {
                if (res.data.status) {
                    this.merchDetailList?.splice(0, this.merchDetailList?.length);
                    for (let i = 0; i < res.data.details.length; i++) {
                        let data = res.data.details[i];
                        this.merchDetailList?.push(new MerchDetail(data.name, data.description, data.price, data.remain, data.merchID, data.typeID));
                    }
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法取得資料');
            });
    }

    addMerchDetail() {
        Axios.post(Http.createApiPath('m/merch', 'merchdetail'), {
            typeID: this.inputAddMerchTypeID,
            name: this.inputAddMerchName,
            description: this.inputAddMerchDesc,
            price: this.inputAddMerchPrice,
            remain: this.inputAddMerchRemain,
        })
            .then((res) => {
                if (res.data.status) {
                    this.inputAddMerchTypeID = '';
                    this.inputAddMerchName = '';
                    this.inputAddMerchDesc = '';
                    this.inputAddMerchPrice = 0;
                    this.getMerchDetails();
                    this.notify?.success(res.data.messages);
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法新增資料');
            });
    }

    updateMerchDetail(index: number) {
        let merch = this.merchDetailList !== null ? this.merchDetailList[index] : null;
        Axios.put(Http.createApiPath('m/merch', 'merchdetail'), {
            name: merch?.name,
            description: merch?.description,
            price: merch?.price,
            remain: merch?.remain,
            typeID: merch?.typeID,
            merchID: merch?.merchID,
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

    deleteMerchDetail(index: number) {
        let merch = this.merchDetailList !== null ? this.merchDetailList[index] : null;
        Axios.delete(Http.createApiPath('m/merch', 'merchdetail'), { data: { merchID: merch?.merchID } })
            .then((res) => {
                if (res.data.status) {
                    this.merchDetailList?.splice(index, 1);
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

<style scoped>
h4 {
    text-align: center;
}
.box-form {
    box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.4);
    border-radius: 10px;
    padding: 1rem;
}
</style>