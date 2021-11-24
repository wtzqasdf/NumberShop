<template>
    <div class="container-fluid mt-4">
        <!-- 新增商品類型 -->
        <div class="row justify-content-center">
            <div class="col-xl-6 col-lg-6 col-12">
                <div class="box-form">
                    <h4>新增商品類型</h4>
                    <div class="mb-3">
                        <label class="form-label">名稱</label>
                        <input type="text" class="form-control" v-model="inputAddTypeName" />
                    </div>
                    <div class="d-grid gap-2">
                        <button type="button" class="btn btn-primary" @click="addMerchType()">新增</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="row mt-2">
            <div class="col-12">
                <table class="table table-striped table-sm table-bordered table-hover table-responsive">
                    <thead class="thead-default">
                        <tr>
                            <th>名稱</th>
                            <th>動作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(data,index) in merchTypeList" :key="index">
                            <td>
                                <input class="form-control" type="text" v-model="data.name" />
                            </td>
                            <td>
                                <button type="button" class="btn btn-success btn-sm" @click="updateMerchType(index)">儲存</button>
                                <button type="button" class="btn btn-danger btn-sm" @click="deleteMerchType(index)">刪除</button>
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
import Axios from 'axios';
import Http from '../commons/Http';
import Notification from '../components/Notification.vue';
import UserApi from '../Models/Api/UserApi';
import UserModel from '../Models/UserModel';

@Options({
    components: { Notification },
})
export default class MerchManagement extends Vue {
    inputAddTypeName: InstanceType<typeof String> | null = ref<any>(null);
    merchTypeList: InstanceType<typeof MerchType>[] | null = ref<any>(null);
    notify: InstanceType<typeof Notification> | null = ref<any>(null);
    userApi!: UserApi;

    created() {
        this.merchTypeList = [];
        this.userApi = new UserApi();
    }

    mounted() {
        document.title = '商品類型管理 - NumberShop';
        this.userApi.getUserModel(this.getUserModel);
    }

    getUserModel(status: boolean, model: UserModel) {
        if (status && model.permission === 1) {
            this.getMerchTypes();
        } else {
            this.$router.replace('/');
        }
    }

    addMerchType() {
        Axios.post(Http.createApiPath('m/merch', 'merchtype'), { name: this.inputAddTypeName })
            .then((res) => {
                if (res.data.status) {
                    this.inputAddTypeName = '';
                    this.getMerchTypes();
                    this.notify?.success(res.data.messages);
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法新增資料');
            });
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

    updateMerchType(index: number) {
        let merch = this.merchTypeList !== null ? this.merchTypeList[index] : null;
        Axios.put(Http.createApiPath('m/merch', 'merchtype'), { name: merch?.name, typeID: merch?.typeID })
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

    deleteMerchType(index: number) {
        let merch = this.merchTypeList !== null ? this.merchTypeList[index] : null;
        Axios.delete(Http.createApiPath('m/merch', 'merchtype'), { data: { typeID: merch?.typeID } })
            .then((res) => {
                if (res.data.status) {
                    this.merchTypeList?.splice(index, 1);
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
.table {
    table-layout: fixed;
}
.box-form {
    box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.4);
    border-radius: 10px;
    padding: 1rem;
}
</style>