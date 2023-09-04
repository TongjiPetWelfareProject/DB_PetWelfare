<script setup>
import { useUserStore } from '@/store/user';
import { ref } from 'vue'
import { useRouter } from 'vue-router'
const userStore = useUserStore();
const drawerOn = ref(false)
const router = useRouter();
const confirm = () => {
    console.log('用户退出登录了')
    userStore.clearUserInfo()
    router.push('/')
};

const mainpage = () => {
  router.replace({path:'/'})
}
</script>
<template>
    <nav class="app-topnav">
      <div class="titleall">
        <div class="titlehead" @click="mainpage">
          <img src="  ../../../public/animal-shelter.png" style="width:2.5vw;height:2.5vw;float:left">
          <p class="titlehead-text">同济宠物救助中心</p>
        </div>
               
        <div class="titlecontainer">
          <a href="https://github.com/shiguangbiyi/DataBase_Program_Pet-Rescue-Station" target="_blank" style="margin-top:4px;margin-right:4px" title="github">
            <img src="  ../../../public/github.png" alt="描述">
          </a>

          <el-button style="color:#5d86ba;font-size:15px;height:20px" @click="drawerOn = true;" text>
                 关于我们
              </el-button>
              <el-drawer
                v-model="drawerOn"
                title="关于我们"
                :direction="direction"
                :before-close="handleClose"
              >
              <div style="display:flex;justify-content:center;align-items: center;flex-direction: column;">
                 <span style="font-size: 15px;color:#5d86ba">
                  
&nbsp;&nbsp;&nbsp;&nbsp;当宠物们需要关怀和救助的时候，同济宠物救助中心始终坚守宠物健康和幸福。作为一个致力于为需要庇护的动物提供关爱的组织，我们汇聚了热情的志愿者和专业的兽医团队，共同致力于改善宠物们的生活质量。<br><br>

&nbsp;&nbsp;&nbsp;&nbsp;在同济宠物救助中心，每一位毛茸茸的小生命都受到最温暖的呵护。我们提供医疗护理、寄养服务和领养机会，努力确保每一只动物都能找到一个温暖的家。我们始终与宠物并肩前行。<br><br>

&nbsp;&nbsp;&nbsp;&nbsp;我们的使命不仅仅是提供救助和庇护，更是致力于教育社会大众关于宠物责任和爱护的重要性。我们希望通过宣传和教育，唤起每个人对于动物福利的关注，为宠物们创造一个更美好的未来。<br><br>

&nbsp;&nbsp;&nbsp;&nbsp;同济宠物救助中心欢迎所有有爱心的人士加入我们的行列，共同关爱每一个生命。让我们用爱心和责任，共同为宠物们的幸福努力奋斗。
                 </span><br>
                 <img src="@/photos/pettoy.png" style="display:block;height: 20vh;justify-content: center;align-items: center;">
                </div>
                </el-drawer>
    
          <el-popover
            placement="bottom"
            title="联系我们"
            :width="400"
            trigger="click"
          >
            <template #reference>
              <el-button style="color:#5d86ba;font-size: 15px;"  text>联系我们</el-button>
            </template>
               <div class="contactus">
              <div>
                <p>电话：<a href="tel:18981729781">18981729781</a></p>
                <p>邮箱：<a href="mailto:charlotteyanggg@qq.com">charlotteyanggg@qq.com</a></p>
                <p>地址：<a href="https://maps.google.com/?q=上海市嘉定区曹安公路4800号">上海市嘉定区曹安公路4800号</a></p>
              </div>
              <div>
                <img src="@/photos/contactus.png" style="height: 120px;">
              </div>

            </div>
          </el-popover>        
          <ul>
                         
            <template v-if="userStore.userInfo.User_ID">
              <li>
                <el-popover placement="bottom"  :width="50" trigger="hover" style="width:30px">
                  <template #reference>
                    <img src="@/photos/我的.png" alt="我的" style="height: 30px; width: 30px;">
                  </template>
                  
                  <router-link to="/mypage">
                    &nbsp;&nbsp; &nbsp;&nbsp;我的主页
                  </router-link>
                  <br><br>

                  <el-popconfirm @confirm="confirm" title="确认退出吗?" confirm-button-text="确认" cancel-button-text="取消">
                    <template #reference><a href="javascript:;"> &nbsp;&nbsp; &nbsp;&nbsp;退出登录</a></template>
                  </el-popconfirm>
                  
                </el-popover>
              </li>

              <li v-if="userStore.userInfo.Role === 'Admin'">
                <router-link to="/manager">进入管理员模式</router-link>
              </li>
            </template>
            <template v-else>
              <li><router-link to="/login">登录</router-link></li>
          
            </template>
          </ul>
        </div>
      </div>
    </nav>    
  </template>
  
 
  
  <style scoped lang="scss">
    .app-topnav {
    background: #ccdff5;
  }
  
  .titlecontainer {
     display: flex;
    // justify-content: flex-end;
    align-items: center;
    height: 60px; 
    float:right;
    margin-right:40px;
    flex-direction: row;
  }
  
  ul {
    display: flex;
    align-items: center;
    margin: 0;
    padding: 0;
    color: #5d86ba;
  }
  
  li {
    font-size: 15px;
    color: #5d86ba;
  }
  
  a {
    color: #5d86ba;
    text-decoration: none;
  }
  .titlehead {
    display: flex;
    align-items: center; /* 垂直居中图片和文字 */
    margin-left: 40px;
    float:left;
  }
  .titlehead-text{
    font-size: 23px;
    font-weight: bold;
    color:#5d86ba;
    justify-content: center; 
    margin-left: 10px;
    letter-spacing: 0.8px;
    cursor: pointer; /* 将鼠标指针设置为手指指向键 */
}
.titleall{
  overflow: hidden;
  display: flex;
  justify-content: space-between;
  align-items: center;
  height:9vh;
}

.titlemenu{
  justify-content: center;
  align-items: center;
}

.contactus{
  display:flex;
  align-items: center;
}
.el-menu-item{
  border-radius: 10px;
}
  </style>
