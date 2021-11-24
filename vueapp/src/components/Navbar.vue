<template>
    <nav class="navbar navbar-expand-md">
        <div class="container-fluid">
            <button class="navbar-toggler d-lg-none" type="button" data-bs-toggle="collapse" data-bs-target="#navbar-top" aria-controls="navbar-top" aria-expanded="false">▼</button>
            <div class="collapse navbar-collapse justify-content-between" id="navbar-top">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <router-link class="nav-link nav-direct" to="/">首頁</router-link>
                    </li>
                    <li class="nav-item" v-if="isLogin">
                        <router-link class="nav-link nav-direct" to="/cart">購物車</router-link>
                    </li>
                    <li class="nav-item" v-if="isLogin">
                        <router-link class="nav-link nav-direct" to="/couponmarket">優惠券市場</router-link>
                    </li>
                </ul>
                <ul class="navbar-nav" v-if="isLogin === false">
                    <li class="nav-item">
                        <router-link class="nav-link nav-direct" to="/login">登入</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link nav-direct" to="/register">註冊</router-link>
                    </li>
                </ul>
                <ul class="navbar-nav" v-if="isLogin">
                    <li class="nav-item">
                        <span class="nav-link font-italic text-primary">{{ identity }}</span>
                    </li>
                    <li class="nav-item" v-if="permission === 1">
                        <router-link class="nav-link nav-direct" to="/couponmanagement">優惠管理</router-link>
                    </li>
                    <li class="nav-item" v-if="permission === 1">
                        <router-link class="nav-link nav-direct" to="/merchtype">類型管理</router-link>
                    </li>
                    <li class="nav-item" v-if="permission === 1">
                        <router-link class="nav-link nav-direct" to="/merchmanagement">商品管理</router-link>
                    </li>
                    <li class="nav-item" v-if="isLogin">
                        <router-link class="nav-link nav-direct" to="/orders">訂單查詢</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link nav-direct" to="/profile">資料維護</router-link>
                    </li>
                    <li class="nav-item">
                        <span class="nav-link nav-direct" @click="onLogout()">登出</span>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component';
import Axios from 'axios';
import Http from '../commons/Http';
import { ref } from '@vue/reactivity';
import UserApi from '../Models/Api/UserApi';
import UserModel from '../Models/UserModel';

export default class Navbar extends Vue {
    isLogin: InstanceType<typeof Boolean> | null = ref<any>(null);
    identity: InstanceType<typeof String> | null = ref<any>(null);
    permission: InstanceType<typeof Number> | null = ref<any>(null);
    userApi!: UserApi;

    created() {
        this.userApi = new UserApi();
        this.$router.beforeEach(this.routeUpdated);
    }

    mounted() {
        this.isLogin = false;
        this.identity = '';
        this.permission = 0;
    }

    routeUpdated() {
        UserApi.getUserData();
        this.userApi.getUserModel(this.getUserModel);
    }

    getUserModel(status: boolean, model: UserModel) {
        if (status) {
            this.isLogin = true;
            this.identity = model.identity;
            this.permission = model.permission;
        }
    }

    onLogout() {
        Axios.post(Http.createApiPath('user', 'logout'))
            .then((res) => {
                if (res.data.status) {
                    this.isLogin = false;
                    this.identity = '';
                    this.permission = 0;
                    this.$router.push('/');
                } else {
                    console.log(res.data.messages);
                }
            })
            .catch(() => {
                console.log('登出時發生錯誤');
            });
    }
}
</script>

<style scoped>
.navbar {
    box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, 0.5);
}

.nav-link {
    color: black;
    text-align: center;
    padding: 0.5rem 0.5rem;
}
.nav-direct {
    cursor: pointer;
}
.nav-direct:hover {
    color: red;
}
.nav-direct:active {
    color: darkred;
}

.router-link-active {
    color: darkorange;
    font-weight: bold;
}
</style>