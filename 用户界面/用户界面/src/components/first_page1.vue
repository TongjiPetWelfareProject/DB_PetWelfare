
<script setup>
//注意：如果直接将 src 属性的值设置为具体的字符串，例如 src="./photos/dog1.jpg"，Vue.js 不会对其进行解析，而是将该字符串直接作为 src 属性的值。而如果使用:src="item"时，Vue.js 会将 item 的值作为表达式进行解析，然后将结果作为 src 属性的值。在这种情况下，Vue.js 会将 item 视为相对路径，并相对于当前页面的 URL 进行解析。假设你的网页的 URL 是 http://localhost:5173/，而图片的路径是 ./photos/dog1.jpg，那么最终解析的 URL 就是 http://localhost:5173/photos/dog1.jpg，而http://localhost:5173/指向的目录是项目的根目录，需要把相对路径写在项目的根目录上
const images = [
  './src/components/photos/dog1.jpg',
  './src/components/photos/dog2.jpg',
  './src/components/photos/dog3.jpg',
  './src/components/photos/dog4.jpg'
];
console.log(images[1]);
</script>

<template>
  <div class="block text-center" m="t-4">
    <el-carousel trigger="click" height="400px">
      <el-carousel-item v-for="(item, index) in images" :key="index">
        <div class="image-container">
          <img class="carousel-image" :src="item" alt="Item Image">
        </div>
      </el-carousel-item>
    </el-carousel>
  </div>
</template>

<style scoped>
.demonstration {
  color: var(--el-text-color-secondary);
}

.el-carousel__item h3 {
  color: #475669;
  opacity: 0.75;
  line-height: 150px;
  margin: 0;
  text-align: center;
}

.el-carousel__item:nth-child(2n) {
  background-color: #99a9bf;
}

.el-carousel__item:nth-child(2n + 1) {
  background-color: #d3dce6;
}

.image-container {
  width: 100%;
  height: 100%;
  overflow: hidden;
}

.carousel-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}
</style>
