<script>
import doctors from './doctors.vue'
import recoverystory from './recoverystory.vue'
import { useRouter } from 'vue-router';
import {Coin} from '@element-plus/icons-vue'


export default {
  components: {
    doctors,
    recoverystory,
  },
  setup() {
    const router = useRouter();

    const goToReservationPage = () => {
      router.push('/reservationdoctor');
    };

    return {
      goToReservationPage,
    };
  },
};
</script>



<template>
    <el-button  class="fixedbuttonmedical"  type="primary" :icon="Coin" circle @click="goToReservationPage" style="border-radius: 10px;float:right;box-shadow: 1px 1px 1px 1px rgba(116, 114, 114, 0.2))">立即预约</el-button>
    <doctors/>
    <recoverystory/>
</template>

<style>
  .fixedbuttonmedical{
    position: fixed;
    top: 30%; /* 距离页面底部的距离 */
    right: 2vw; /* 距离页面右侧的距离，使用 vh 单位可以根据视口高度自动调整 */
    border-radius: 10px;
    box-shadow:  0px 4px 4px rgba(116, 114, 114, 0.2);
  }
</style>
