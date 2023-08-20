<script setup>
import { useRouter } from 'vue-router'

const router = useRouter();

const mainpage = () => {
  router.replace({path:'/'})
}

</script>

<template>
  <div class="allcontainer">
    <div class="left">
      <div class="logo-container" @click="mainpage">
          <img src="  ../../../public/animal-shelter.ico" alt="Logo" class="logo">
          <span class="logo-text">同济宠物救助中心</span>
      </div>
      <div class="info-container">
          <span class="info-text">数据库课程设计项目-宠物救助站信息管理系统</span>
      </div>


    </div>
    <div class="right">
      <router-view></router-view>
    </div>
  </div>
</template>

<style scoped>
html, body {
  height: 100%; /* 设置网页高度为100% */
  margin: 0; /* 清除默认的页面边距 */
  overflow: hidden; /* 禁止页面滚动 */
}
.logo-container {
  position: absolute;
  top: 5%; /* 调整Logo的垂直位置 */
  left: 4%; /* 调整Logo的水平位置 */
  display: flex;
  align-items: center;
  box-shadow: 0 2px 4px rgba(159, 157, 157, 0.2); /* 添加黑色阴影底板 */
  background-color: #ffffffa1; 
  padding: 8px 12px; /* 调整底板内边距 */
  border-radius: 4px; 
  cursor: pointer;
}

.logo {
  width: 30px; /* 调整Logo图片的宽度 */
  height: 30px; /* 调整Logo图片的高度 */
  margin-right: 10px; /* 调整Logo和文字之间的间距 */
}

.logo-text {
  color: #729cd4; /* 设置文字颜色为白色 */
  font-size: 20px; /* 调整文字大小 */
  font-family: "KaiTi", "楷体";/*设置字体为楷体*/
}

.info-container {
	position: absolute;
	bottom: 6%; /* 调整Logo的垂直位置 */
	left: 3%; /* 调整Logo的水平位置 */
	display: flex;
	align-items: center;
	box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2); /* 添加黑色阴影底板 */
	background-color: rgba(246, 246, 246, 0.5); /* 设置底板为半透明的 #7A7A7A */
	/* background-color: transparent; */
	padding: 8px 12px; /* 调整底板内边距 */
	border-radius: 4px; 
}

.info-text {
	font-size: 2px; /* 调整文字大小 */
	color: #000000;
	font-family: "Segoe UI", "Helvetica", sans-serif;
}

.allcontainer{
  display:flex;
}

.left{
  background-image: url("../../../public/bg5.jpg");
  width: 60vw;
  height:100vh;
}

.right{
 width:40vw;
  background-color: rgb(255, 255, 255);
}

</style>

