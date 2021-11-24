import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Home from "../views/Home.vue";
import Login from '../views/Login.vue';
import Register from '../views/Register.vue';
import Profile from '../views/Profile.vue';
import Cart from '../views/Cart.vue';
import MerchPage from '../views/MerchPage.vue';
import Orders from '../views/Orders.vue';
import MerchManagement from '../views/MerchManagement.vue';
import MerchTypeManagement from '../views/MerchTypeManagement.vue';
import CouponManagement from '../views/CouponManagement.vue';
import CouponMarket from '../views/CouponMarket.vue';

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/register',
    name: 'Register',
    component: Register
  },
  {
    path: '/profile',
    name: 'Profile',
    component: Profile
  },
  {
    path: '/orders',
    name: 'Orders',
    component: Orders
  },
  {
    path: '/couponmarket',
    name: 'CouponMarket',
    component: CouponMarket
  },
  {
    path: '/cart',
    name: 'Cart',
    component: Cart
  },
  {
    path: '/merch/:id',
    name: 'MerchPage',
    component: MerchPage
  },
  {
    path: '/merchmanagement',
    name: 'MerchManagement',
    component: MerchManagement
  },
  {
    path: '/couponmanagement',
    name: 'CouponManagement',
    component: CouponManagement
  },
  {
    path: '/merchtype',
    name: 'MerchType',
    component: MerchTypeManagement
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
