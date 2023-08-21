<template>
  <div class="doctors">
    <p style="font-size: 24px;font-weight: bold;color:#729cd4;">医生介绍</p>
    <div width=50%>
      <el-carousel :interval="4000" type="card" height="250px" max-width="200" >
        <div>
          <el-carousel-item v-for="doctor in doctors" :key="doctor.id">
          <img class="imgdoctor" :src="doctor.photoUrl" style="height: 250px; width: 500px" />
          </el-carousel-item>
        </div> 
      </el-carousel>
      <br>
     </div>
     <div style="display:flex;align-items: center;justify-content: center">
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
      </div>
   <br>
  </div>
  </template>
  
  <style scoped>
.el-carousel{
  max-width:90%
}

.doctors{
  margin-left:20px;
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
import{defineComponent, ref, onMounted }from 'vue';

export default defineComponent({
  setup() {
    const doctors = ref([]);

    // 在组件挂载后获取医生数据
    onMounted(async () => {
      try {
        doctors.value = await medical_donate.doctorsAPI();
      } catch (error) {
        console.error('获取医生数据时出错：', error);
      }
    });

    return {
      doctors,
    };
  },
});

</script>