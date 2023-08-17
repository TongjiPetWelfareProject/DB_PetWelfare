<template>
  <el-text size="large" tag="b">医生团队</el-text>
  <div width=50%>
    <el-carousel :interval="4000" type="card" height="250px" max-width="200" >
      <div>
        <el-carousel-item v-for="doctor in doctors" :key="doctor.id">
        <img class="imgdoctor" :src="doctor.photoUrl" style="height: 250px; width: 500px" />
   </el-carousel-item>
      </div> 
    </el-carousel>
  </div>

  </template>
  
  <style scoped>
.el-carousel{
  max-width:90%
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