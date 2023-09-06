<script setup>
import { reactive,toRefs,ref,onMounted } from 'vue'
import { useUserStore } from '@/store/user';
import { useRouter } from 'vue-router'
import {
  Document,Menu as IconMenu,Setting,Search,ArrowDown,Position,School,
  User,Coin,ArrowDownBold,House,Postcard,DocumentChecked,Collection,Memo
} from '@element-plus/icons-vue'

const userStore = useUserStore();
const router=useRouter();
const username=userStore.userInfo;
const formattedDateTime = ref('');

const confirm = () => {
    console.log('用户退出登录了')
    userStore.clearUserInfo();
    router.push('/');
}

onMounted(() => {
  formatCurrentDateTime();
  // router.push('/mytableemployee')
});

const formatCurrentDateTime = () => {
  const currentDate = new Date();

  const year = currentDate.getFullYear();
  const month = currentDate.getMonth() + 1; // 注意：月份是从 0 开始计数的
  const day = currentDate.getDate();
  const dayOfWeek = ['日', '一', '二', '三', '四', '五', '六'][currentDate.getDay()];
  // const hours = currentDate.getHours();
  // const minutes = currentDate.getMinutes();

  formattedDateTime.value = `今天是：${year}年 ${month}月${day}日   星期${dayOfWeek}`;
};

const handleOpen = (key, keyPath) => {
    console.log(key, keyPath)
}
const handleClose = (key, keyPath) => {
    console.log(key, keyPath)
}
</script>

<template>
  <div class="common-layout" >
    <el-container>
      <el-aside>
        <img src="@/photos/animal-shelter.png" style="height: 40px;width: 40px;margin-left: 90px;margin-top: 6px">

        <el-menu
        default-active="2"
        :collapse=false
        :default-opened=true
        class="el-menu-vertical"
        @open="handleOpen"
        @close="handleClose"
        router >
        <el-sub-menu index="1" :collapse=false :default-opened=true> 
          <template #title>
            <el-icon><Position/></el-icon>
            <span>工作人员</span>
          </template>
            <el-menu-item index="/mytableemployee">员工</el-menu-item>
            <el-menu-item index="/mytabledoctor">医生</el-menu-item>
        </el-sub-menu>
        <el-menu-item index="/petcard">
          <el-icon><icon-menu /></el-icon>
          <span>宠物信息</span>
        </el-menu-item>
        <el-menu-item index="/mytableuser" >
          <el-icon><user /></el-icon>
          <span>用户信息</span>
        </el-menu-item>
        <el-menu-item index="/tableadopt" >
          <el-icon><document /></el-icon>
          <span>领养</span>
        </el-menu-item>
        <el-menu-item index="/tablefoster">
          <el-icon><school /></el-icon>
          <span>寄养</span>
        </el-menu-item>
        <el-menu-item index="/tablemedical">
          <el-icon><memo/></el-icon>
          <span>医疗</span>
        </el-menu-item>
        <el-menu-item index="/tablenotice">
          <el-icon><postcard /></el-icon>
          <span>公告</span>
        </el-menu-item>
        <el-menu-item index="/tablehot">
          <el-icon><collection/></el-icon>
          <span>人气榜</span>
        </el-menu-item>
        <el-menu-item index="/tabledonate">
          <el-icon><coin /></el-icon>
          <span>捐赠</span>
        </el-menu-item>
        <el-menu-item index="/tablecheck">
          <el-icon><document-checked /></el-icon>
          <span>审核</span>
        </el-menu-item>
        <el-menu-item index="/tableroom">
          <el-icon><house /></el-icon>
          <span>房间</span>
        </el-menu-item>
      </el-menu>
      <!-- <img src="pet-toy.png" style="height: 80px;width: 80px;margin-left: 80px;"> -->
      </el-aside>

      <el-container class="managercontainer">
        <el-header class="manager">
          <div class="headercontent">
            <div class="left-content">
              <el-avatar :src="userStore.userInfo.Avatar" size="50"></el-avatar>
              <el-text v-if="userStore.userInfo.User_ID" class="welcome">&nbsp;Hello!&nbsp;{{ userStore.userInfo.User_Name }}</el-text>
              <el-text v-if="userStore.userInfo.User_ID" class="welcome2">{{ formattedDateTime }}</el-text>
            </div>
            <div class="right-content">
            
              <el-button class="usermodel">
                <router-link to="/" class="link-text">进入用户模式</router-link>
              </el-button>
              <img src="@/photos/退出.png" class="imgmanager">
              <el-popconfirm @confirm="confirm" title="确认退出吗?" confirm-button-text="确认" cancel-button-text="取消">
                <template #reference><a href="javascript:;" class="textmanager">退出登录</a></template>
              </el-popconfirm>
            </div>
          </div>

          
       
      </el-header>
        

        <el-main >
      
          <el-divider content-position="left" border-style="double" style="margin-top:-15px;font-size:40px">
            

            </el-divider>
            <!-- <el-tabs v-model="activeName" class="demo-tabs" style="margin-top:-30px;height:100px;font-size: 30px;">
              <el-tab-pane style="margin-top:-20px;height:1200px;font-size: 90px;" label="User" name="first">User</el-tab-pane>
            </el-tabs> -->
            <!-- <el-page-header  @back="$router.back()">
              <template #content>
                <span class="text-large font-600 mr-3"> Title </span>
              </template>
            </el-page-header> -->
            <!-- <el-tabs
              v-model="activeName"
              type="card"
              class="demo-tabs"
              style="margin-top:-20px"
            >
              <el-tab-pane label="User" name="first">User</el-tab-pane>
            </el-tabs> -->
       

          <router-view></router-view>

        </el-main>
      </el-container>


    </el-container>
  </div>
</template>




<style scoped>

body, html {
  height: 100%;
  margin: 0;
  padding: 0;
  width: 100%;
}

.common-layout{
	background-color: #ffffff;
	height: 100%;
	width: 100%;
}

.el-menu-vertical {
  
  width: 15vw;
  background-color:rgb(237, 244, 249) ;
  box-shadow: 0 2px 4px rgba(0, 0, 0, .2);
  border-radius: 10px;
}

.el-menu-item{
  box-shadow: 0 0px 2px rgba(217, 217, 217, 0.2);
  height:45px
}

.el-container {
  padding: 0px !important;
  margin: 0px !important;
}

.el-header .manager {
  background-color: rgb(255, 255, 255);
  width:84vw;
  height:48px;
}

.el-main .manager {
  width:84vw;
  box-shadow: 0 4px 0px rgba(0, 0, 0, .2);
}

.el-aside{
  background-color: rgb(225, 239, 249);
  width:15vw;
  height:100vh;
  box-shadow: 0 0px 4px rgba(0, 0, 0, .2);
  overflow-x: hidden; 
}

.search-bar{
  margin-top: 10px;
 float: right;
 width: 200px;
 border-radius: 30px;
 box-shadow: 0 0px 2px rgba(0, 0, 0, .2);
}

.search-button{
  margin-top: 10px;
 float: right;
 margin-right: 10px;
 box-shadow: 0 0px 2px rgba(0, 0, 0, .2);
}

.imgmanager{
  /* margin-top: 10px; */
  float: right;
  margin-left: 20px;
 /* margin-right: 1vw; */
 width:30px;
 height:30px;
}

.textmanager{
  /* margin-top: 14px; */
  float: right;
 /* margin-right: 0.8vw; */
 width:60px;
  font-size: 15px;
  text-decoration: none;
  color: #616161;
}

.welcome{
 /* margin-top: -10px; */
 /* float: left; */
 font-size: 20px;
 font-weight: bold;
}

.welcome2{
 /* margin-top: 10px; */
 margin-left:15vw;
 font-size: 15px;
 font-weight: light;
 /* justify-content: center;
 text-align: center; */
}

.usermodel {
  /* margin-top: 10px; */
  margin-left: 20vw;
  background-color: rgb(234, 245, 252); /* 背景颜色 */
  color: white; /* 字体颜色 */
  border: none; /* 去除边框 */
  border-radius: 5px; /* 圆角 */
  padding: 10px 20px; /* 内边距 */
  cursor: pointer; /* 鼠标样式 */
  transition: background-color 0.3s, color 0.3s; /* 过渡效果 */
}

.usermodel:hover {
  background-color: #e1eff9; /* 鼠标悬停时的背景颜色 */
}

.headercontent {
  display: flex;
  text-align:center;
  /* align-items: center; */
}

.left-content {
  display: flex;
  align-items: center;
}

.right-content {
  display: flex;
  align-items: center;
  float:right;
  margin-right:2vw;
  /* margin-left:4vw */
}

.manager{
  display: flex;
  justify-content: space-between;
  align-items: center;
  text-align: center;
}

/* 去除下划线 */
.link-text {
  text-decoration: none;
}

/* 修改字体颜色 */
.link-text {
  color: #000; /* 设置自定义字体颜色 */
}
.my-trigger{
  background-color: rgb(237, 244, 249);
  width:100px;
  height:26px;
  border-radius: 10px;
  box-shadow: 0 0px 2px rgba(0, 0, 0, .2);
  padding-left: 24px;
  padding-top: 4px;
}
.el-card{
  width: 220px;
}

.bottom {
  margin-top: 13px;
  line-height: 12px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.button {
  padding: 0;
  min-height: auto;
}


.el-row {
  margin-bottom: 20px;
}
.el-row:last-child {
  margin-bottom: 0;
}
.el-col {
  border-radius: 4px;
}

.grid-content {
  border-radius: 4px;
  min-height: 36px;
}

.managercontainer{
  background-color: #fdfdff;
}

</style>
