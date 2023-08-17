<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router'
import backgroundImage1 from '@/photos/猫互动.jpg';
import backgroundImage2 from '@/photos/狗互动.jpg';
import backgroundImage3 from '@/photos/主人怀抱小狗.jpg';
const router = useRouter();
const backgroundImages = [backgroundImage1, backgroundImage2,backgroundImage3];
const selectedBackgroundImage = ref(backgroundImages[Math.floor(Math.random() * backgroundImages.length)]);

const mainpage = () => {
  router.replace({path:'/'})
}

</script>

<template>
  <div class="background-container" :style="{ backgroundImage: `url(${selectedBackgroundImage})` }">
    <div class="logo-container" @click="mainpage">
          <img src="@/photos/logo.ico" alt="Logo" class="logo">
          <span class="logo-text">同济宠物救助中心</span>
    </div>
  	<div class="info-container">
  	    <span class="info-text">数据库课程设计项目-宠物救助站信息管理系统</span>
  	</div>
    <!-- 根据状态变量显示对应的组件 -->
    <router-view></router-view>
  </div>
</template>

<style scoped>
html, body {
  height: 100%; /* 设置网页高度为100% */
  margin: 0; /* 清除默认的页面边距 */
  overflow: hidden; /* 禁止页面滚动 */
}
.background-container {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
}

.logo-container {
  position: absolute;
  top: 6%; /* 调整Logo的垂直位置 */
  left: 3%; /* 调整Logo的水平位置 */
  display: flex;
  align-items: center;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2); /* 添加黑色阴影底板 */
  background-color: #7A7A7A; 
  padding: 8px 12px; /* 调整底板内边距 */
  border-radius: 4px; 
  cursor: pointer;
}

.logo {
  width: 50px; /* 调整Logo图片的宽度 */
  height: 50px; /* 调整Logo图片的高度 */
  margin-right: 10px; /* 调整Logo和文字之间的间距 */
}

.logo-text {
  color: #fff; /* 设置文字颜色为白色 */
  font-size: 32px; /* 调整文字大小 */
  font-family: "KaiTi", "楷体";/*设置字体为楷体*/
}

.info-container {
	position: absolute;
	bottom: 6%; /* 调整Logo的垂直位置 */
	left: 3%; /* 调整Logo的水平位置 */
	display: flex;
	align-items: center;
	box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2); /* 添加黑色阴影底板 */
	background-color: rgba(122, 122, 122, 0.5); /* 设置底板为半透明的 #7A7A7A */
	/* background-color: transparent; */
	padding: 8px 12px; /* 调整底板内边距 */
	border-radius: 4px; 
}

.info-text {
	font-size: 2px; /* 调整文字大小 */
	color: #000000;
	font-family: "Segoe UI", "Helvetica", sans-serif;
}

</style>

