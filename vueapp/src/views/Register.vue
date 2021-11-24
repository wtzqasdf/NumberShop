<template>
    <div class="container-fluid mt-4">
        <div class="row justify-content-center">
            <div class="col-xl-4 col-lg-6 col-12">
                <div class="box-form">
                    <h4>會員註冊</h4>
                    <div class="mb-3">
                        <label for class="form-label">帳號</label>
                        <input type="text" class="form-control" v-model="account" />
                    </div>
                    <div class="mb-3">
                        <label for class="form-label">密碼</label>
                        <input type="password" class="form-control" v-model="password" />
                    </div>
                    <div class="mb-3">
                        <label for class="form-label">Email</label>
                        <input type="email" class="form-control" v-model="email" />
                    </div>
                    <div class="mb-3">
                        <label for class="form-label">暱稱</label>
                        <input type="text" class="form-control" v-model="userIdentity" />
                    </div>
                    <div class="d-grid gap-2">
                        <button type="button" class="btn btn-success" @click="onRegister()">註冊</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <notification ref="notify"></notification>
</template>

<script lang="ts">
import Http from '../commons/Http';
import { Options, Vue } from 'vue-class-component';
import { ref } from 'vue';
import Axios from 'axios';
import Notification from '../components/Notification.vue';
import UserApi from '../Models/Api/UserApi';
import UserModel from '../Models/UserModel';

@Options({
    components: { Notification },
})
export default class Register extends Vue {
    account: InstanceType<typeof String> | null = ref<any>(null);
    password: InstanceType<typeof String> | null = ref<any>(null);
    email: InstanceType<typeof String> | null = ref<any>(null);
    userIdentity: InstanceType<typeof String> | null = ref<any>(null);
    notify: InstanceType<typeof Notification> | null = ref<any>(null);
    userApi!: UserApi;

    created() {
        this.userApi = new UserApi();
    }

    mounted() {
        document.title = '會員註冊 - NumberShop';
        this.userApi.getUserModel(this.getUserModel);
    }

    getUserModel(status: boolean, model: UserModel) {
        if (status) {
            this.$router.replace('/');
        }
    }
    
    onRegister() {
        Axios.post(Http.createApiPath('user', 'register'), {
            account: this.account,
            password: this.password,
            email: this.email,
            identity: this.userIdentity,
        })
            .then((res) => {
                if (res.data.status) {
                    this.$router.push('/');
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('註冊時發生錯誤');
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