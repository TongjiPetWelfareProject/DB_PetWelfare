<script>
import doctors from './doctors.vue'
import recoverystory from './recoverystory.vue'
import { useRouter } from 'vue-router';


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
     <el-button type="primary" @click="goToReservationPage" style="float: right">预约</el-button>
     </el-affix>
    <doctors/>
    <recoverystory/>
</template>

