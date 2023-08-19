<script setup>
import { useUserStore } from '@/store/user';
const userStore = useUserStore();
const confirm = () => {
    console.log('用户退出登录了')
    userStore.clearUserInfo()
};
</script>
<template>
    <nav class="app-topnav">
      <div class="titleall">
        <div class="titlehead">
          <img src="  ../../../public/animal-shelter.png" style="width:46px;height:46px;float:left">
          <p class="titlehead-text">同济宠物救助中心</p>
        </div>
        <div class="container">
          <a href="https://github.com/shiguangbiyi/DataBase_Program_Pet-Rescue-Station" style="margin-top:4px;margin-right:4px" title="github">
            <img src="  ../../../public/github.png" alt="描述">
          </a>
          <ul>
            <template v-if="userStore.userInfo.User_ID">
              <li>
                <img src="@/photos/我的.png" alt="我的" style="height: 22px; width: 22px;">
              </li>
              <li><a href="javascript:;"><i class="iconfont icon user"></i>{{ userStore.userInfo.User_Name }}</a></li>
              <li><router-link to="/mypage">我的页面</router-link></li>
              <li>
                <el-popconfirm @confirm="confirm" title="确认退出吗?" confirm-button-text="确认" cancel-button-text="取消">
                  <template #reference><a href="javascript:;">退出登录</a></template>
                </el-popconfirm>
              </li>
            </template>
            <template v-else>
              <li><router-link to="/login">登录</router-link></li>
              <li><a href="javascript:;">关于我们</a></li>
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
  
  .container {
    // display: flex;
    // justify-content: flex-end;
    align-items: center;
    height: 60px; /* Adjust the height as needed */
    float:right;
    margin-right:40px;
  }
  
  ul {
    display: flex;
    align-items: center;
    margin: 0;
    padding: 0;
    color: #5d86ba;
  }
  
  li {
    font-size: 14px;
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
    font-size: 20px;
    font-weight: bold;
    color:#5d86ba;
    justify-content: center; 
    margin-left: 10px;
}
.titleall{
  overflow: hidden;
}
  </style>