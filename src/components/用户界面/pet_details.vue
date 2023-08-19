<script setup>
import { ref } from 'vue';
import { ElInput, ElButton, ElAvatar, ElDivider } from 'element-plus';
const pet = ref({
  id: "00001",
  name: "汤姆",
  breed: "猫",
  age: "1岁",
  health: "良好",
  vaccinated: true,
  popularity: 100,
  description: "这是一只可爱的宠物，喜欢玩耍和和人类互动。",
  likes: 0,
  comments: 0,
  favorites: 0,
  comment_contents: [
  { id: 1, author: '用户1（后期去掉）', text: '好文章！（后期去掉）', avatar: './src/components/photos/阿尼亚.jpg' },
  { id: 2, author: '用户2（后期去掉）', text: '感谢分享！（后期去掉）', avatar: './src/components/photos/CC.jpg' }
  ],
  images: [
    './src/components/photos/汤姆1.jpg',
    './src/components/photos/汤姆2.jpg',
    './src/components/photos/汤姆3.jpg'
  ],
  currentImageIndex: 0
});

const newComment = ref({ author: '', text: '', avatar: './src/components/photos/默认.jpg' });
const showCommentForm = ref(false);

const addComment = () => {
if (newComment.value.author && newComment.value.text) {
    pet.value.comments++;
    pet.value.comment_contents.push({
        id: pet.value.comment_contents.length + 1,
        author: newComment.value.author,
        text: newComment.value.text,
        avatar: newComment.value.avatar
    });
    newComment.value.author = '';
    newComment.value.text = '';
    showCommentForm.value = false;
}
};

const showAddComment = () => {
showCommentForm.value = true;
}

const likePet = () => {
pet.value.likes++;
};

const favoritePet = () => {
pet.value.favorites++;
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

  .interactions {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 1rem;
  margin-bottom: 1rem;
  }

  .interactions span {
  margin-left: 0.5rem;
  }

  .comment {
  display: flex;
  }

  .comment-content {
  margin-left: 10px;
  }

  .comment-form {
  margin-top: 20px;
  }

  h2, h3 {
  font-weight: bold;
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
          <el-button type="primary" @click="$router.push('/pet_adopt_form')">领养Ta</el-button>
        </div>
      </el-main>
    </el-container>
  <div class="interactions">
    <div>
      <el-button type="primary" plain @click="likePet">点赞</el-button>
      <span>{{ pet.likes }}</span>
    </div>
    <div>
      <el-button type="primary" plain @click="showAddComment">评论</el-button>
      <span>{{ pet.comments }}</span>
    </div>
    <div>
      <el-button type="primary" plain @click="favoritePet">收藏</el-button>
      <span>{{ pet.favorites }}</span>
    </div>
  </div>
  <div v-if="showCommentForm" class="comment-form">
  <el-input v-model="newComment.author" placeholder="你的名字（这个后期要去掉，直接改成用户名）"></el-input>
  <el-input v-model="newComment.text" type="textarea" placeholder="在这里评论"></el-input>
  <el-button type="primary" plain @click="addComment">提交</el-button>
  </div>
  <el-divider></el-divider>
  <h3>评论区</h3>
  <div v-for="comment in pet.comment_contents" :key="comment.id" class="comment">
  <el-avatar :src="comment.avatar" :size="40"></el-avatar>
  <div class="comment-content">
    <p>{{ comment.author }}</p>
    <p>{{ comment.text }}</p>
  </div>
  </div>
  </div>
</template>
