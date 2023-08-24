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
      Coin
    };
  },
};
</script>



<template>
    
    <doctors/>
    <recoverystory/>
</template>

<style>
  .fixedbuttonmedical{
    position: fixed;
    top: 30%; 
    right: 2vw; 
    border-radius: 10px;
    box-shadow:  0px 4px 4px rgba(116, 114, 114, 0.2);
  }
</style>
