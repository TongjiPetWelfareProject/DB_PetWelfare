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
      <div class="container">
        <ul>
          <template v-if="userStore.userInfo.User_ID">
            <li>
              <img src="我的.png" alt="我的" style="height: 22px; width: 22px;">
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
    </nav>    
  </template>
  
 
  
  <style scoped lang="scss">
  .app-topnav {
    background: #fff;
  }
  
  .container {
    display: flex;
    justify-content: flex-end;
    align-items: center;
    height: 60px; /* Adjust the height as needed */
  }
  
  ul {
    list-style: none;
    display: flex;
    align-items: center;
    margin: 0;
    padding: 0;
  }
  
  li {
    margin-left: 0px;
  }
  
  a {
    color: #333;
    text-decoration: none;
  }
  </style>
  