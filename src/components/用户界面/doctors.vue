<template>
  <div class="doctors">
    <p style="font-size: 24px;font-weight: bold;color:#729cd4;">医生介绍</p>
    <div width=50%>
      <el-carousel :interval="4000" type="card" height="300px" max-width="200" >   
          <el-carousel-item v-for="(doctor,index) in doctors" :key="index">  
            <div>
              <img class="imgdoctor" :src="doctor" style="height: 300px; width: 500px" />
            </div> 
          </el-carousel-item>
        
      </el-carousel>
      <br>
     </div>
     <div style="display:flex;align-items: center;justify-content: center;flex-direction: column;">
        <div style="display:block">
          <div>
          <p style="font-size: 24px;font-weight: bold;color:#9a89bb;">专业的医生团队，竭诚为您的宠物保驾护航。</p>
        </div>
        
        <div style="display:block;text-align: center;justify-content: center;">
          <p style="margin-bottom:1px;color: #828080;">这里，我们以医疗之爱，诠释着宠物健康的承诺</p>
            <p style="margin-bottom:1px;color: #828080;">成就宠物健康的守护者。</p>
        </div>
        <br><br>
        </div>
        <div style="display:block">
          <el-button type="primary" :icon="Select" circle @click="goToReservationPage" style="border-radius: 10px;float:right;box-shadow: 1px 1px 1px 1px rgba(116, 114, 114, 0.2))">立即预约</el-button>
        </div>
        
      </div>
   <br>
  </div>
  </template>
  
  <style scoped>
.el-carousel{
  max-width:100%
}

.doctors{
  margin-left:2vw;
}
  
  .el-carousel__item:nth-child(2n) {
    background-color: #99a9bf;
    border-radius: 10px;
    box-shadow: 0 0px 2px rgba(0, 0, 0, .2);
 
  }
  
  .el-carousel__item:nth-child(2n + 1) {
    background-color: #d3dce6;
    border-radius: 10px;
    box-shadow: 0 0px 2px rgba(0, 0, 0, .2);

  }

  </style>


<script>
import medical_donate from '@/api/medical_donate';
import { defineComponent, ref, onMounted }from 'vue';
import { useRouter } from 'vue-router';
import { Select } from '@element-plus/icons-vue'
import { useUserStore } from '@/store/user';
import { ElMessage, ElMessageBox } from 'element-plus'

export default defineComponent({
  setup() {
    const doctors = [
    '../../../public/tl.png',
    '../../../public/tl2.png',
    '../../../public/tl3.png', 
  ];

    // 在组件挂载后获取医生数据
    // onMounted(async () => {
    //   try {
    //     doctors.value = await medical_donate.doctorsAPI();
    //   } catch (error) {
    //     console.error('获取医生数据时出错：', error);
    //   }
    // });
    const router = useRouter();
    const userStore = useUserStore();
    const goToReservationPage = () => {
      if (userStore.userInfo.User_ID) {
        router.push('/reservationdoctor');
      } else {
        // 用户未登录，跳转到 /login
         ElMessage({
          message: '请先登录',
          type: 'warning',
        });
        router.push('/login');
      }
    };

    return {
      doctors,
      goToReservationPage,
      Select
    };
  },
});

</script>