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
     <el-affix :offset="300">
     <!-- <router-link to="/reservationdoctor" target="_blank" style="float:right">
     </router-link> -->
     <el-button type="primary" :icon="Coin" circle @click="goToReservationPage" style="border-radius: 10px;float:right;box-shadow: 1px 1px 1px 1px rgba(116, 114, 114, 0.2))">立即预约</el-button>
     </el-affix>
    <doctors/>
    <recoverystory/>
</template>

