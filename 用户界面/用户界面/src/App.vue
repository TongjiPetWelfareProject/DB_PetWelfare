<template>
  <div class="common-layout" >
    <el-container>
      <div class="container" v-if="showTitle">
        <div class="left">
          <div class="tableitem" >
            <img src="main_icon.jpg" style="height: 100px;width: 100px;margin-left: 20px;margin-right: 20px">
            <p class="welcome-text">宠物救助站</p>
          </div>
        </div>
        <div class="right">
          <img src="我的.png" style="height: 50px;width: 50px;margin-left: 20px;margin-right: 20px;margin-top: 40px;">
          <router-link to="/mypage">我的页面</router-link>
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
          style="display: flex; justify-content: space-between;"
        >
          <el-menu-item index="/first_page" style="flex: 1; text-align: center;">首页</el-menu-item>
          <el-menu-item index="/pet_foster" style="flex: 1; text-align: center;">寄养</el-menu-item>
          <el-menu-item index="/pet_adopt" style="flex: 1; text-align: center;">领养</el-menu-item>
          <el-menu-item index="/donate" style="flex: 1; text-align: center;">捐款</el-menu-item>
          <el-menu-item index="/forum" style="flex: 1; text-align: center;">论坛</el-menu-item>
          <el-menu-item index="/medical" style="flex: 1; text-align: center;">医疗</el-menu-item>
          <el-menu-item index="/notice" style="flex: 1; text-align: center;">公告</el-menu-item>
        </el-menu>
      </el-header>

      <el-main>
        <router-view></router-view>
      </el-main>
      <el-footer v-if="showFooter">
        <div class="main_content">
          <p>联系我们</p>
          <p>电话：15279570357</p>
          <p>邮箱</p>
          <p>地址</p>
        </div>
      </el-footer>
    </el-container>
  </div>
</template>

<script lang="ts" setup>
import { reactive, toRefs, ref, watch } from 'vue'
const activeIndex = ref('1')
const activeIndex2 = ref('1')
const handleSelect = (key: string, keyPath: string[]) => {
  console.log(key, keyPath)
}
import { useRoute } from 'vue-router'

const showHeader = ref(true)
const showFooter = ref(true)
const showTitle = ref(true)
const route = useRoute()

watch(() => route.path, (newPath, oldPath) => {
  // 检查需要隐藏header和footer的路径
  const pathsToHide = ['/dog_foster_table','/cat_foster_table', '/reservationdoctor','/mypage','/pet_adopt_form']
  showHeader.value = !pathsToHide.includes(newPath)
  showFooter.value = !pathsToHide.includes(newPath)
  showTitle.value = !pathsToHide.includes(newPath)

})

// const state = reactive({
//   circleUrl:
//     'https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fc-ssl.duitang.com%2Fuploads%2Fblog%2F202010%2F20%2F20201020100556_1838b.thumb.1000_0.jpg&refer=http%3A%2F%2Fc-ssl.duitang.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=auto?sec=1691023509&t=0e3ffefc2c20a0461851b994233624f8',
// })

// const { circleUrl } = toRefs(state)
// const direction = ref('vertical')
// const fillRatio = ref(80)
</script>

<style >

.welcome-text{
 font-size: 40px;
 font-weight: bold;
 color:darkcyan
}

.tableitem {
  display: flex;
  /* justify-content: center; */
}

.tableitem > * {
  margin-right: 10px;
  flex: 0 0 auto;
}

.main_content{
  /* margin: 30px 50px 50px 50px; */
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