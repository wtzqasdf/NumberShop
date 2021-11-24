<template>
    <div class="container-fluid mt-4">
        <div class="row justify-content-center">
            <div class="col-xl-4 col-lg-6 col-12">
                <div class="d-flex border-dashed">
                    <div>
                        <img src="/images/a.png" />
                    </div>
                    <div class="d-flex flex-column flex-fill justify-content-between">
                        <div>
                            <h4>{{ merchDetail.name }}</h4>
                            <small>{{ merchDetail.description }}</small>
                        </div>
                        <div class="d-flex justify-content-between">
                            <span class="text-danger price">{{ merchDetail.priceText }}</span>
                            <span class="font-bold">{{ '剩餘' + merchDetail.remain + '件' }}</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row justify-content-center mt-3">
            <div class="col-xl-4 col-lg-6 col-12">
                <div class="d-flex justify-content-end">
                    <!-- Check this merch have remain count -->
                    <div v-if="merchDetail.remain !== 0">
                        <select class="select-dd" v-model="inputMerchCount">
                            <option v-for="(num, index) in merchDetail.remain" :key="index" :value="num">{{ num }}</option>
                        </select>
                        <button class="btn btn-success" @click="addToCart()">加入購物車</button>
                    </div>
                    <div v-if="merchDetail.remain === 0">
                        <span class="text-danger font-bold">商品已售完</span>
                    </div>
                </div>
            </div>
        </div>
        <div class="row justify-content-center mt-3">
            <div class="col-xl-4 col-lg-6 col-12">
                <div class="review-board d-flex flex-column" v-for="(review, index) in reviews" :key="index">
                    <div class="d-flex justify-content-between">
                        <small class="font-bold font-italic">{{ review.account }}</small>
                        <small class="font-bold">
                            <span v-for="index in review.score" :key="index">★</span>
                        </small>
                    </div>
                    <div class="px-2">
                        <pre v-html="review.content"></pre>
                    </div>
                    <div class="text-end">
                        <small>{{ review.postdate }}</small>
                    </div>
                </div>
                <div class="mt-4">
                    <h6 class="text-center">撰寫評論</h6>
                    <select class="form-control" v-model="inputReviewScore">
                        <option value="1">很差</option>
                        <option value="2">比較差</option>
                        <option value="3">普通</option>
                        <option value="4">比較好</option>
                        <option value="5">很好</option>
                    </select>
                    <textarea class="form-control mt-2" rows="2" v-model="inputReviewContent"></textarea>
                    <div class="d-grid gap-2 mt-2">
                        <button type="button" class="btn btn-primary" @click="addReview()">送出</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <notification ref="notify"></notification>
</template>

<script lang="ts">
import { Vue, Options } from 'vue-class-component';
import { ref } from 'vue';
import Axios from 'axios';
import Http from '../commons/Http';
import MerchDetail from '../item/merch-detail';
import Notification from '../components/Notification.vue';

@Options({
    components: { Notification },
})
export default class MerchPage extends Vue {
    notify: InstanceType<typeof Notification> | null = ref<any>(null);
    merchDetail: InstanceType<typeof MerchDetail> | null = ref<any>(null);
    reviews: InstanceType<typeof Review>[] | null = ref<any>(null);

    inputMerchCount: InstanceType<typeof Number> | null = ref<any>(null);
    inputReviewScore: InstanceType<typeof Number> | null = ref<any>(null);
    inputReviewContent: InstanceType<typeof String> | null = ref<any>(null);

    created() {
        this.merchDetail = new MerchDetail('', '', 0, 0, '', '');
        this.reviews = [];
    }

    mounted() {
        document.title = '瀏覽商品 - NumberShop';
        this.inputMerchCount = 1;
        this.inputReviewScore = 3;
        this.inputReviewContent = '';
        this.getMerchDetail(this.$route.params.id as string);
        this.getReviews();
    }

    getMerchDetail(merchID: string) {
        Axios.get(Http.createApiPath('merch', 'merchdetail/single', merchID))
            .then((res) => {
                if (res.data.status) {
                    this.merchDetail!.name = res.data.detail.name;
                    this.merchDetail!.price = res.data.detail.price;
                    this.merchDetail!.description = res.data.detail.description;
                    this.merchDetail!.remain = res.data.detail.remain;
                    this.merchDetail!.merchID = res.data.detail.merchID;
                    this.merchDetail!.typeID = res.data.detail.typeID;
                    document.title = `${this.merchDetail!.name} - 瀏覽商品 - NumberShop`;
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法取得商品資料');
            });
    }

    addToCart() {
        Axios.post(Http.createApiPath('cart', 'merch'), { merchID: this.merchDetail?.merchID, count: this.inputMerchCount })
            .then((res) => {
                if (res.data.status) {
                    this.notify?.success(res.data.messages);
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法新增到購物車');
            });
    }

    getReviews() {
        Axios.get(Http.createApiPath('merch', 'review', this.$route.params.id as string))
            .then((res) => {
                if (res.data.status) {
                    this.reviews?.splice(0, this.reviews?.length);
                    for (let i = 0; i < res.data.reviews.length; i++) {
                        let review = res.data.reviews[i];
                        this.reviews?.push(new Review(review.account, review.content, review.score, review.postdate));
                    }
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法取得評論');
            });
    }

    addReview() {
        Axios.post(Http.createApiPath('merch', 'review'), { merchID: this.$route.params.id, content: this.inputReviewContent, score: this.inputReviewScore })
            .then((res) => {
                if (res.data.status) {
                    this.inputReviewScore = 3;
                    this.inputReviewContent = '';
                    this.getReviews();
                } else {
                    this.notify?.error(res.data.messages);
                }
            })
            .catch(() => {
                this.notify?.error('無法新增評論');
            });
    }
}
class Review {
    private _account: string;
    private _content: string;
    private _score: number;
    private _postdate: string;

    constructor(account: string, content: string, score: number, postdate: string) {
        this._account = account;
        this._content = content;
        this._score = score;
        this._postdate = postdate;
    }

    get account(): string {
        return this._account;
    }

    get content(): string {
        return this._content;
    }

    get score(): number {
        return this._score;
    }

    get postdate(): string {
        return this._postdate;
    }
}
</script>

<style scoped>
h4 {
    margin: 0.1rem 0;
    font-weight: bold;
}
img {
    width: auto;
    max-width: 128px;
}
.border-dashed {
    border: 2px dashed black;
    padding: 0.3rem;
}
.font-bold {
    font-weight: bold;
}
.font-italic {
    font-style: italic;
}

.price {
    font-weight: bold;
    font-style: italic;
}
.select-dd {
    margin: 0 0.5rem;
    padding: 0.2rem;
    height: 100%;
}

.review-board {
    width: 100%;
    background-color: rgb(217, 224, 192);
    margin: 0.3rem 0;
    padding: 0.3rem;
}
</style>