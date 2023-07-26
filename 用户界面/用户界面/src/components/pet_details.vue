<script setup>
import { ref } from 'vue';
const pet = {
  id: "00001",
  name: "汤姆",
  breed: "猫",
  age: "1岁",
  health: "良好",
  vaccinated: true,
  popularity: 100,
  description: "这是一只可爱的宠物，喜欢玩耍和和人类互动。",
  likes: ref(0),//使用ref创建的响应式引用需要通过.value来访问其值
  comments: ref(0),
  favorites: ref(0),
  images: [
    './src/components/photos/汤姆1.jpg',
    './src/components/photos/汤姆2.jpg',
    './src/components/photos/汤姆3.jpg'
  ],
  currentImageIndex: 0
};

const likePet = () => {
  pet.likes.value++;
};

const commentPet = () => {
  pet.comments.value++;
};

const favoritePet = () => {
  pet.favorites.value++;
};
</script>

<style scoped>
  .all-container {
    background-color: rgba(166, 219, 225, 0.8);
    width: 1000px; /* 设置矩形的宽度 */
    height: 1000px; /* 设置矩形的高度 */
    /* 添加其他样式属性以适应你的需求 */
  }

  .all-container > * {
    margin-top: 20px;
    margin-bottom: 20px;
  }

  .gallery-and-info-container {
    display: flex;
    flex-direction: row; /* 水平排列子元素 */
  }

  .gallery-and-info-container > * {
    margin-left: 20px;
    margin-right: 20px;
  }

  .interactions {
    display: flex;
    justify-content: center;
    align-items: center;
  }

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


<template>
  <div class="common-layout">
    <el-container>
      <el-aside width="400px">
        <el-carousel trigger="click" height="300px" width="300px">
          <el-carousel-item v-for="(item, index) in pet.images" :key="index">
            <div class="image-container">
              <img class="carousel-image" :src="item" alt="Item Image">
            </div>
          </el-carousel-item>
        </el-carousel>
      </el-aside>
      <el-main>
        <div class="pet-details">
          <h2>{{ pet.name }}</h2>
          <p>宠物ID: {{ pet.id }}</p>
          <p>品种: {{ pet.breed }}</p>
          <p>年龄: {{ pet.age }}</p>
          <p>健康状况: {{ pet.health }}</p>
          <p>是否接种疫苗: {{ pet.vaccinated ? '是' : '否' }}</p>
          <p>人气: {{ pet.popularity }}</p>
          <p>介绍: {{ pet.description }}</p>
          <!-- <el-button type="primary" plain>领养Ta</el-button> -->
          <router-link to="/pet_adopt_form">领养Ta
        </router-link>
        </div>
        <div class="interactions">
          <div class="like">
            <el-button type="primary" plain @click="likePet">点赞</el-button>
            <span>{{ pet.likes }}</span>
          </div>
          <div class="comment">
            <el-button type="primary" plain @click="commentPet">评论</el-button>
            <span>{{ pet.comments }}</span>
          </div>
          <div class="favorite">
            <el-button type="primary" plain @click="favoritePet">收藏</el-button>
            <span>{{ pet.favorites }}</span>
          </div>
        </div>
      </el-main>
    </el-container>
  </div>
</template>
