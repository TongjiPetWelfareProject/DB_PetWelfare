<script lang="ts" setup>
import { reactive, toRefs, ref, watch, inject } from 'vue'
import { useRoute } from 'vue-router'
import headnav from './nav.vue'
import {
  Document,Phone,School,
  Message,Coin,Location,House,Postcard,DocumentChecked,Collection,Memo,ChatLineRound
} from '@element-plus/icons-vue'
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
    <el-container >
      <div class="container" v-if="showTitle">
   <div class="left">
    </div>
</div>
  <el-header v-if="showHeader" class="elheader">
    <el-menu
    :default-active="activeIndex"
    class="el-menu-demo"
    mode="horizontal"
    text-color="#729cd4"
    active-text-color="#769FCD"
    @select="handleSelect"
    style="height: 6vh;box-shadow: 0px 4px 6px 0px rgba(0, 0, 0, 0.1);border-radius: 10px; "
    router
  >
    <el-menu-item index="/">
      <div style="display: flex;align-items: center;">
        <el-icon><house/></el-icon><span style="display: flex;align-items: center;justify-content: center;  ">首页</span>
      </div>
    </el-menu-item>
    <el-menu-item index="/pet_foster">
      <div style="display: flex;align-items: center;">
        <el-icon><school/></el-icon><span style="display: flex;align-items: center;justify-content: center;  ">寄养</span>
      </div>
    </el-menu-item>
    <el-menu-item index="/pet_adopt">
      <div style="display: flex;align-items: center;">
        <el-icon><document/></el-icon><span style="display: flex;align-items: center;justify-content: center;  ">领养</span>
      </div>
    </el-menu-item>
    <el-menu-item index="/medical">
      <div style="display: flex;align-items: center;">
        <el-icon><memo/></el-icon><span style="display: flex;align-items: center;justify-content: center;  ">医疗</span>
      </div>
    </el-menu-item>
    <el-menu-item index="/donate">
      <div style="display: flex;align-items: center;">
        <el-icon><coin/></el-icon><span style="display: flex;align-items: center;justify-content: center;  ">捐款</span>
      </div>
    </el-menu-item>
    <el-menu-item index="/forum">
      <div style="display: flex;align-items: center;">
        <el-icon><ChatLineRound/></el-icon><span style="display: flex;align-items: center;justify-content: center;  ">论坛</span>
      </div>
    </el-menu-item>
    <el-menu-item index="/notice" >
      <div style="display: flex;align-items: center;">
        <el-icon><Postcard/></el-icon><span style="display: flex;align-items: center;justify-content: center;  ">公告</span>
      </div>
    </el-menu-item>
  </el-menu>
      </el-header>
      <el-main>
        <router-view></router-view>
      </el-main>
      <el-footer v-if="showFooter" style="padding: 0px;" >
        <div class="main_content">
          <div class="contact">
            <p style="font-size: 17px;font-weight: bold;color:#729cd4;margin-bottom: 8px;margin-top: 8px;">联系我们</p>
            <div style="display: flex; align-items: center;">
    <el-icon class="textmini"><phone/></el-icon>
    <a href="tel:18981729781" class="textmini" style="display: flex; align-items: center; justify-content: center;">18981729781</a>
</div>
<div style="display: flex; align-items: center;">
    <el-icon class="textmini"><message/></el-icon>
    <a href="mailto:1040687614@qq.com" class="textmini" style="display: flex; align-items: center; justify-content: center;">1040687614@qq.com</a>
</div>
<div style="display: flex; align-items: center;">
    <el-icon class="textmini"><location/></el-icon>
    <a href="https://maps.google.com/?q=上海市嘉定区" class="textmini" style="display: flex; align-items: center; justify-content: center;">上海市嘉定区</a>
</div>
          </div>
          <div class="footerimg">
            <img src="  ../../../public/animal-shelter.png" style="width:100px;height:100px;">
          </div>
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
  display: flex;
  background-color: #ccdff5;
  align-items: center;
  justify-content: center;
  padding: 10px;
  border-top: 1px solid rgba(221, 221, 221, 0.8);
  width:100%vw;
}
.contact,.footerimg{
   padding-left:80px;
   padding-right:80px;
}

.elheader{
  padding: 0 0;
  height:6vh;
}

 .el-footer{ 
    height:100px
 }
.container {
  display: flex;
  justify-content: space-between;
}
.textmini{
  font-size: 13px;
  color: #729cd4;
  margin:4px;
}
#app {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
  }

</style>