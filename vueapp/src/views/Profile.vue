<template>
    <div class="container-fluid mt-4">
        <div class="row justify-content-center">
            <div class="col-xl-4 col-lg-6 col-12">
                <div class="box-form">
                    <h4>資料維護</h4>
                    <div class="mb-3">
                        <label for class="form-label">帳號</label>
                        <input type="text" class="form-control" v-model="account" disabled />
                    </div>
                    <div class="mb-3">
                        <label for class="form-label">舊密碼</label>
                        <input type="password" class="form-control" v-model="oldPassword" placeholder="未變更無需輸入" />
                    </div>
                    <div class="mb-3">
                        <label for class="form-label">新密碼</label>
                        <input type="password" class="form-control" v-model="newPassword" placeholder="未變更無需輸入" />
                    </div>
                    <div class="mb-3">
                        <label for class="form-label">Email</label>
                        <input type="email" class="form-control" v-model="email" />
                    </div>
                    <div class="mb-3">
                        <label for class="form-label">暱稱</label>
                        <input type="text" class="form-control" v-model="userIdentity" />
                    </div>
                    <div class="mb-3">
                        <label for class="form-label">註冊日期</label>
                        <input type="text" class="form-control" v-model="regDate" disabled />
                    </div>
                    <div class="d-grid gap-2">
                        <button type="button" class="btn btn-success" @click="onUpdateProfile()">更新</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <notification ref="notify"></notification>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import { ref } from '@vue/reactivity';
import Axios from 'axios';
import Http from '../commons/Http';
import Notification from '../components/Notification.vue';
import UserApi from '../Models/Api/UserApi';
import UserModel from '../Models/UserModel';

@Options({
    components: { Notification },
})
export default class Profile extends Vue {
    account: InstanceType<typeof String> | null = ref<any>(null);
    oldPassword: InstanceType<typeof String> | null = ref<any>(null);
    newPassword: InstanceType<typeof String> | null = ref<any>(null);
    email: InstanceType<typeof String> | null = ref<any>(null);
    userIdentity: InstanceType<typeof String> | null = ref<any>(null);
    regDate: InstanceType<typeof String> | null = ref<any>(null);

    notify: InstanceType<typeof Notification> | null = ref<any>(null);
    userApi!: UserApi;

    created() {
        this.userApi = new UserApi();
    }

    mounted() {
        document.title = '個人維護 - NumberShop';
        this.userApi.getUserModel(this.getUserModel);
    }

    getUserModel(status: boolean, model: UserModel) {
        if (status) {
            this.account = model.account;
            this.email = model.email;
            this.userIdentity = model.identity;
            this.regDate = model.regdate;
        } else {
            this.$router.replace('/');
        }
    }

    onUpdateProfile() {
        Axios.put(Http.createApiPath('user', 'member'), {
            oldPassword: this.oldPassword,
            newPassword: this.newPassword,
            email: this.email,
            identity: this.userIdentity,
        })
            .then((res) => {
                if (res.data.status) {
                    this.oldPassword = '';
                    this.newPassword = '';
                    this.notify?.success(res.data.messages);
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('更新資料時發生錯誤');
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