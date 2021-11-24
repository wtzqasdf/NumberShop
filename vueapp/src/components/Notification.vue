<template>
    <div class="notify-board mt-1">
        <div class="d-flex flex-column align-items-end">
            <div class="notify" v-for="(item, index) in itemContainer" :key="index" :class="[item.cssClass]" @click="close(item.index)">
                <span v-html="item.text"></span>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component';

export default class Notification extends Vue {
    protected itemContainer!: NotifyItem[];
    private currentIndex!: number;

    mounted() {
        this.itemContainer = [];
        this.currentIndex = 1;
    }

    public success(text: string | string[]) {
        if (Array.isArray(text)) {
            for (let i = 0; i < text.length; i++) {
                this.itemContainer.push(new NotifyItem('notify-success', text[i], this.currentIndex));
                this.currentIndex++;
            }
        } else {
            this.itemContainer.push(new NotifyItem('notify-success', text, this.currentIndex));
            this.currentIndex++;
        }
        this.$forceUpdate();
    }

    public error(text: string | string[]) {
        if (Array.isArray(text)) {
            for (let i = 0; i < text.length; i++) {
                this.itemContainer.push(new NotifyItem('notify-error', text[i], this.currentIndex));
                this.currentIndex++;
            }
        } else {
            this.itemContainer.push(new NotifyItem('notify-error', text, this.currentIndex));
            this.currentIndex++;
        }
        this.currentIndex++;
        this.$forceUpdate();
    }

    close(index: number) {
        for (let i = 0; i < this.itemContainer.length; i++) {
            let item = this.itemContainer[i];
            if (item.index === index) {
                this.itemContainer.splice(i, 1);
                this.$forceUpdate();
                break;
            }
        }
    }
}

class NotifyItem {
    private _cssClass!: string;
    private _text!: string;
    private _index!: number;

    constructor(cssClass: string, text: string, index: number) {
        this._cssClass = cssClass;
        this._text = text;
        this._index = index;
    }

    get cssClass() {
        return this._cssClass;
    }

    get text() {
        return this._text;
    }

    get index() {
        return this._index;
    }
}
</script>

<style scoped>
.notify-board {
    position: fixed;
    top: 0px;
    right: 5px;
    width: auto;
    background-color: transparent;
    z-index: 9999;
}
.notify {
    margin-top: 0.5rem;
    margin-left: 0.2rem;
    margin-right: 0.2rem;
    padding: 0.5rem 1rem;
    border-radius: 1rem;
    cursor: pointer;
}
.notify-success {
    background-color: rgba(175, 255, 159, 0.85);
}
.notify-success:hover {
    background-color: rgba(42, 230, 4, 0.85);
}
.notify-error {
    background-color: rgba(255, 179, 179, 0.85);
}
.notify-error:hover {
    background-color: rgba(255, 81, 81, 0.85);
}
</style>