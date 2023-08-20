<template>
  <div class="common-layout" >
    <el-container>
      <el-aside>
        <img src="animal-shelter.png" style="height: 40px;width: 40px;margin-left: 90px;margin-top: 6px">

        <el-menu
        default-active="2"
        class="el-menu-vertical"
        @open="handleOpen"
        @close="handleClose"
        router >
        <el-sub-menu index="1">
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

      <el-container>
        <el-header class="manager">
          <el-text class="welcome">Hello!</el-text>
          <el-avatar :size="40" :src="circleUrl" style="margin-top: 1vh;margin-left: 1vw;" />
          <el-input class="search-bar" v-model="input" placeholder="Please input" />
          <el-button class="search-button" :icon="Search" circle />
        </el-header>

        <el-main class="manager">

          <router-view></router-view>

        </el-main>
      </el-container>


    </el-container>
  </div>
</template>

<script lang="ts" setup>
import { reactive,toRefs,ref } from 'vue'
import {
  Document,Menu as IconMenu,Setting,Search,ArrowDown,Position,School,
  User,Coin,ArrowDownBold,House,Postcard,DocumentChecked,Collection,Memo
} from '@element-plus/icons-vue'

const input = ref('')
const handleOpen = (key: string, keyPath: string[]) => {
  console.log(key, keyPath)
}
const handleClose = (key: string, keyPath: string[]) => {
  console.log(key, keyPath)
}

const state = reactive({
  circleUrl:
    'https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fc-ssl.duitang.com%2Fuploads%2Fblog%2F202010%2F20%2F20201020100556_1838b.thumb.1000_0.jpg&refer=http%3A%2F%2Fc-ssl.duitang.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=auto?sec=1691023509&t=0e3ffefc2c20a0461851b994233624f8',
})

const { circleUrl } = toRefs(state)

</script>


<style scoped>

body, html {
  height: 100%;
  margin: 0;
  padding: 0;
  width: 100%;
  /* overflow-x: hidden; /* 防止左右滚动条出现  */
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

.welcome{
 margin-top: 10px;
 float: left;
 font-size: 20px;
 font-weight: bold;
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
</style>
