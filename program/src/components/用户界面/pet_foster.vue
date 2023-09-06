<template>
    <el-row>
      <el-col :span="24">
        <div class="foster_container">
          <p class="foster_title">宠物寄养服务</p>
          <p class="foster_sub_title">为您的宠物提供家一样的感觉</p>
          <buttion class="foster_top_button" @click="handleApplyForFoster">现在申请宠物寄宿</buttion>
        </div>
      </el-col>
    </el-row>
    <el-row :gutter="20">
    <el-col :span="16">
      <div class="foster_center">
          <p class="foster_third_title">尽享关怀，让你的宝贝获得完美寄养</p>
          <span style="display: block; text-indent: 2em;"> 在我们的寄养中心，你的宝贝将享受到舒适的睡眠环境、有趣的玩耍活动和美味的饮食。
            <br><br>
            我们为宠物提供专属的玩具和舒适的床铺，让他们感到宾至如归。</span>
        </div>

    </el-col>
    <el-col :span="8"> 
      <!-- <div class="foster_1"> -->
      <p class="foster_third_title" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;价格</p>
      <div class="foster_card">
      <div class="foster_center">
         
          <p>狗寄养</p>
          <p>· 大型犬：30元/天</p>
          <p>· 中型犬：25元/天</p>
          <p>· 小型犬：20元/天</p>
        </div>
        <div class="foster_center">
          <p>猫寄养</p>
          <p>· 20元/天</p>
        </div>
      </div>
      <!-- </div> -->
    </el-col>
  </el-row>
</template>

<script>
import { ref } from 'vue'; // 导入 ref
import { ElMessage, ElMessageBox } from 'element-plus'
import { useUserStore } from '@/store/user';
import { useRouter } from 'vue-router'

export default {
  setup() {
    const userStore = useUserStore();
    const router = useRouter();

    const handleApplyForFoster = () => {
      if (userStore.userInfo.User_ID) {
        // 用户已登录，跳转到 /pet_foster_table
        router.push('/pet_foster_table');
      } else {
        // 用户未登录，跳转到 /login
         ElMessage({
          message: '请先登录',
          type: 'warning',
        });
        router.push('/login');
      }
    }

    return {
      handleApplyForFoster
    }
  }
}
</script>

<style>
.foster_1{

 display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column; /* 将子元素垂直排列 */
}
.foster_container{
  height:300px;
  background-image: url("../../../public/fosterbg4.jpg");
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  display: flex;
  flex-direction: column; /* 将子元素垂直排列 */
  align-items: center;
  justify-content: center;
  
}
.foster_card{
  display: flex;
  align-items: center;
  justify-content: center;
}
.foster_center{
  width:80%;
  display: flex;
  flex-direction: column; /* 将子元素垂直排列 */
  align-items: center;
  justify-content: center;
}
.foster_title{
  font-size: 40px;
  color:#ADD8E6;
}
.foster_sub_title{
  font-size: 18px;
  color:rgb(255, 255, 255)
}
.foster_third_title{
  font-size: 24px;
  color:#ADD8E6;
  font-weight: bold;
}
.foster_top_button{
  /* display: inline-block;
  outline: none;
  cursor: pointer;
  border-radius: 50%; */
  background-color: #ADD8E6;
  color: rgb(255, 255, 255);
  border: 2px solid white;
  padding: 10px 20px;
  font-size: 16px;
  border-radius: 20px;
  cursor: pointer; /* 将鼠标指针设置为手指指向键 */
}
</style>
