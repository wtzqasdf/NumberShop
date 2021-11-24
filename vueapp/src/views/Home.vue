<template>
    <div class="container-fluid mt-2">
        <div class="row">
            <div class="col-12">
                <ul class="item-types">
                    <li v-for="(item,index) in merchTypes" :key="index">
                        <span class="badge bg-dark" @click="getMerchDetails(item.typeID)">{{ item.name }}</span>
                    </li>
                </ul>
            </div>
        </div>
        <div class="row">
            <div class="col-xl-2 col-lg-2 col-md-3 col-12" v-for="(item,index) in merchDetails" :key="index">
                <router-link :to="'/merch/' + item.merchID">
                    <div class="item-detail">
                        <span class="title">{{ item.name }}</span>
                        <span class="price">{{ item.priceText }}</span>
                    </div>
                </router-link>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component';
import { ref } from 'vue';
import Axios from 'axios';
import Http from '../commons/Http';
import MerchType from '../item/merch-type';
import MerchDetail from '../item/merch-detail';

export default class Home extends Vue {
    merchDetails: InstanceType<typeof MerchDetail>[] | null = ref<any>(null);
    merchTypes: InstanceType<typeof MerchType>[] | null = ref<any>(null);

    created() {
        this.merchDetails = [];
        this.merchTypes = [];
    }

    mounted() {
        document.title = 'NumberShop';
        this.getMerchTypes();
    }

    private getMerchTypes(): void {
        Axios.get(Http.createApiPath('merch', 'merchtype')).then((res) => {
            this.merchTypes?.splice(0, this.merchTypes?.length);
            for (let i = 0; i < res.data.types.length; i++) {
                this.merchTypes?.push(new MerchType(res.data.types[i].name, res.data.types[i].typeID));
            }
        });
    }

    private getMerchDetails(typeID: string): void {
        Axios.get(Http.createApiPath('merch', 'merchdetail', typeID)).then((res) => {
            this.merchDetails?.splice(0, this.merchDetails?.length);
            for (let i = 0; i < res.data.details.length; i++) {
                let detail = res.data.details[i];
                this.merchDetails?.push(new MerchDetail(detail.name, detail.description, detail.price, detail.remain, detail.merchID, detail.typeID));
            }
        });
    }
}
</script>

<style>
ul {
    list-style: none;
}
a {
    text-decoration: none;
}

.item-types {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    padding: 0;
}
.item-types li {
    padding: 0.25rem 0.25rem;
}
.item-types span {
    cursor: pointer;
    font-size: 20px;
}
.item-types span:hover {
    color: lightsteelblue;
}
.item-types span:active {
    color: lightgreen;
}

.item-detail {
    border: 2px solid black;
    border-radius: 20px;
    padding: 0.3rem;
    text-align: center;
    display: flex;
    flex-direction: column;
}
.item-detail .title {
    color: black;
    font-size: 18px;
    font-weight: bold;
}
.item-detail:hover .title {
    color: green;
}
.item-detail .price {
    color: darkred;
    font-style: italic;
    font-weight: bold;
}
</style>
