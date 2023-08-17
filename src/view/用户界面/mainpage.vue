<script lang="ts" setup>
import { reactive, toRefs, ref, watch, inject } from 'vue'
import { useRoute } from 'vue-router'
import headnav from './nav.vue'

const activeIndex = ref('1')
const activeIndex2 = ref('1')
const handleSelect = (key: string, keyPath: string[]) => {
  console.log(key, keyPath)
}

const showHeader = ref(true)
const showFooter = ref(true)
const showTitle = ref(true)
const route = useRoute()
const loggedIn = ref(false)
const username = ref()

watch(() => route.path, (newPath, oldPath) => {
  // 检查需要隐藏header和footer的路径
  const pathsToHide = ['/dog_foster_table','/cat_foster_table', '/reservationdoctor','/mypage','/pet_adopt_form','/myinfo']
  showHeader.value = !pathsToHide.includes(newPath)
  showFooter.value = !pathsToHide.includes(newPath)
  showTitle.value = !pathsToHide.includes(newPath)

})

// const onLoginSuccess = (name) => {
//   loggedIn.value = true;
//   username.value = name;
// };

</script>


<template>
  <headnav></headnav>
  <div class="common-layout" >
    <el-container>
      <div class="container" v-if="showTitle">
        <div class="left">
          <div class="tableitem" >
            <img src="./main_icon.jpg" style="height: 100px;width: 100px;margin-left: 20px;margin-right: 20px">
            <p class="welcome-text">同济宠物救助中心</p>
          </div>
        </div>
      </div>
      <el-header v-if="showHeader">
        <el-menu
        :default-active="activeIndex2"
        class="el-menu-demo"
        mode="horizontal"
        background-color="#545c64"
        text-color="#fff"
        active-text-color="#ffd04b"
        @select="handleSelect"
        router
        >
          <el-menu-item index="/">首页</el-menu-item>
          <el-menu-item index="/pet_foster">寄养</el-menu-item>
          <el-menu-item index="/pet_adopt">领养</el-menu-item>
          <el-menu-item index="/donate">捐款</el-menu-item>
          <el-menu-item index="/forum">论坛</el-menu-item>
          <el-menu-item index="/medical">医疗</el-menu-item>
          <el-menu-item index="/notice">公告</el-menu-item>
        </el-menu>
      </el-header>
      <el-main>
        <router-view></router-view>
      </el-main>
      <el-footer v-if="showFooter">
        <div class="main_content">
          <p>联系我们</p>
          <p>电话：15279570357</p>
          <p>邮箱：1664524275@qq.com</p>
          <p>地址：上海市嘉定区</p>
        </div>
      </el-footer>
    </el-container>
  </div>
</template>



<style >
.welcome-text{
 font-size: 40px;
 font-weight: bold;
 color:#1fa0cb;
}
.tableitem {
  display: flex;
}
.tableitem > * {
  margin-right: 10px;
  flex: 0 0 auto;
}

.main_content{
  background-color: rgba(226, 226, 214, 0.8);
  height: 100%;
  align-items: center;
  flex-direction: column; /* 将子元素垂直排列 */
  padding:5px
}
 .el-footer{ 
    height:160px
 }
.container {
  display: flex;
  justify-content: space-between;
}
#app {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
  }


</style>
