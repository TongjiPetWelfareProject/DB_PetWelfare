<script setup>
import { ref } from 'vue';
import { ElInput, ElButton, ElAvatar, ElDivider } from 'element-plus';

import { useUserStore } from '@/store/user';
import { useRouter } from 'vue-router'

//下面注释的这一段不要删，后面能登录了会用到！！！点赞收藏评论也要用！！！

const userStore = useUserStore();
const router = useRouter();

/*const handleApplyForAdopt = () => {
  if (userStore.userInfo.User_ID) {
    // 用户已登录，跳转到 /pet_adopt_form
    router.push('/pet_adopt_form');
  } else {
    // 用户未登录，跳转到 /login
    router.push('/login');
  }
}*/

const handleApplyForAdopt = () => {
  
  router.push('/pet_adopt_form');
}

const pet = ref({
  id: "00001",
  name: "汤姆",
  breed: "猫",
  age: "1岁",
  health: "良好",
  vaccinated: true,
  popularity: 100,
  description: "这是一只可爱的宠物，喜欢玩耍和和人类互动。",
  reads: 0,
  likes: 0,
  comments: 0,
  favorites: 0,
  comment_contents: [
  { id: 1, author: '用户1（后期去掉）', text: '好文章！（后期去掉）', avatar: '@/photos/汤姆1.jpg' },//传照片没成功
  { id: 2, author: '用户2（后期去掉）', text: '感谢分享！（后期去掉）', avatar: '@/photos/汤姆2.jpg' }//传照片没成功
  ],
  image: './photos/汤姆1.jpg',
});

pet.value.reads++;



const newComment = ref({ author: '', text: '', avatar: '@/photos/汤姆1.jpg' });//传照片没成功
newComment.value.author = '某某某';
const showCommentForm = ref(false);

const addComment = () => {
if (newComment.value.text) {
    pet.value.comments++;
    pet.value.comment_contents.push({
        id: pet.value.comment_contents.length + 1,
        author: newComment.value.author,
        text: newComment.value.text,
        avatar: newComment.value.avatar
    });
    newComment.value.text = '';
}
};

const liked = ref(false);
const likePet = async() => {
  liked.value = !liked.value;
  if (liked.value) {
    pet.value.likes++;
    await submitLike(userStore.userInfo, pet, pet.value.likes);
  } else {
    pet.value.likes--;
    await submitLike(userStore.userInfo, pet, pet.value.likes);
  }
};

const favorited = ref(false);
const favoritePet = async() => {
  favorited.value = !favorited.value;
  if (favorited.value) {
    pet.value.favorites++;
    await submitFavorite(userStore.userInfo, pet, pet.value.favorites);
  } else {
    pet.value.favorites--;
    await submitFavorite(userStore.userInfo, pet, pet.value.favorites);
  }
};
</script>

<style scoped>

  .pet-card {
    display: flex;
    justify-content: center;
  }

  .card {
    width: 800px;
    background-color: rgb(173,216,230,0.5);
    position: relative; /* 添加相对定位 */
  }

  .read-count {
  position: absolute;
  top: 50px; /* 调整距离顶部的位置 */
  right: 200px; /* 调整距离右侧的位置 */
  font-size: 12px;
  color: #888;
  }

  .card-content {
    display: flex;
  }

  .pet-image {
    flex: 1;
    padding: 10px;
  }

  .pet-image img {
    width: 100%;
    height: auto;
    border-radius: 8px;
  }

  .pet-info {
    flex: 2;
    padding: 10px;
    display: flex;
    flex-direction: column;
    justify-content: center;
  }

  .pet-info h3 {
    margin: 0;
    font-size: 1.2rem;
  }

  .pet-info p {
    margin: 4px 0;
    font-size: 1rem;
    color: #888;
  }

  .round-button {
    border-radius: 20%; /* 圆形按钮 */
    border: 1px solid #ccc; /* 添加边框样式 */
  }

  .round-button img {
    vertical-align: middle;
  }

  .icon {
    vertical-align: middle;
    width: 100px; /* 调整图片宽度 */
    height: 100px; /* 调整图片高度 */
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

  .single-image {
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
  margin-left: auto;
  margin-right: auto;
  width: 800px;
  }

  h2, h3 {
  font-weight: bold;
  }
</style>


<template>
  <div class="pet-card">
    <el-card class="card" >
      <div class="card-content">
        <div class="pet-image">
          <img src="https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png" alt="Pet Image" />
        </div>
        <div class="pet-info">
          <p class="read-count">阅读{{ pet.reads }}</p>
          <h2>{{ pet.name }}</h2>
          <p>宠物ID: {{ pet.id }}</p>
          <p>品种: {{ pet.breed }}</p>
          <p>年龄: {{ pet.age }}</p>
          <p>健康状况: {{ pet.health }}</p>
          <p>是否接种疫苗: {{ pet.vaccinated ? '是' : '否' }}</p>
          <p>人气: {{ pet.popularity }}</p>
          <p>介绍: {{ pet.description }}</p>
          <p>  </p>
          <el-button type="primary" @click="handleApplyForAdopt" style="width: 100px;">领养Ta</el-button>
        </div>
      </div>
    </el-card>
  </div>
  <p>  </p>
  <div class="common-layout">
    <div class="interactions">
      <div>
        <button class="round-button" @click="likePet">
          <img v-if="liked" src="@/photos/like_blue.png" alt="点赞" class="icon">
          <img v-else src="@/photos/like_grey.png" alt="未点赞" class="icon">
        </button>
        <span>{{ pet.likes }}</span>
      </div>
      <div>
        <button class="round-button" @click="favoritePet">
          <img v-if="favorited" src="@/photos/favorite_blue.png" alt="收藏" class="icon">
          <img v-else src="@/photos/favorite_grey.png" alt="未收藏" class="icon">
        </button>
        <span>{{ pet.favorites }}</span>
      </div>
    </div>
    <div class="comment-form">
      <h3>评论 {{ pet.comments }}</h3>
      <el-input v-model="newComment.text" type="textarea" placeholder="在这里评论"></el-input>
      <p>  </p>
      <el-button type="primary" plain @click="addComment">发布</el-button>
      <p>  </p>
      <div v-for="comment in pet.comment_contents" :key="comment.id" class="comment">
        <el-avatar :src="comment.avatar" :size="40"></el-avatar>
        <div class="comment-content">
          <p>{{ comment.author }}</p>
          <p>{{ comment.text }}</p>
          <el-divider></el-divider>
        </div>
      </div>
    </div>
  </div>
</template>
