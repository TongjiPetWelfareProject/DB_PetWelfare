<script setup>
import { ref, computed, onMounted } from 'vue'
import { ElInput, ElButton, ElAvatar, ElDivider } from 'element-plus';

import { useUserStore } from '@/store/user';
import { useRoute, useRouter } from 'vue-router'
import petadopt from '@/api/pet_adopt'

//下面注释的这一段不要删，后面能登录了会用到！！！点赞收藏评论也要用！！！

const userStore = useUserStore();
const router = useRouter();
const route = useRoute();

/*const handleApplyForAdopt = () => {
  if (userStore.userInfo.User_ID) {
    // 用户已登录，跳转到 /pet_adopt_form
    router.push('/pet_adopt_form');
  } else {
    // 用户未登录，跳转到 /login
    router.push('/login');
  }
}*/

const pet = ref([]); // 单个宠物信息

const getPetDetails = async (petId) => {
  try {
    const response = await petadopt.getPetDetails(petId); // 假设有一个获取单个宠物信息的 API
    console.log(response);
    let gender = '';
    if (response.SEX === 'M') {
      gender = '弟弟';
    } else if (response.SEX === 'F') {
      gender = '妹妹';
    }


    pet.value = {
      id: response.PET_ID,
      name: response.PET_NAME,
      species: response.SPECIES,
      gender: gender,
      //age: response.AGE,
      popularity: response.POPULARITY,
      health_state: response.HEALTH_STATE,
      vaccine: response.VACCINE,
      image: images[0]
    };
  } catch (error) {
    console.error('获取宠物信息时出错：', error);
  }
};

// 在组件加载时调用 getPetDetails 方法，传入宠物的 ID
onMounted(() => {
  const petId = route.params.id; // 假设你从路由参数中获取宠物的 ID
  console.log(petId);
  getPetDetails(petId);
});

console.log(pet.value);

const handleApplyForAdopt = () => {
  
  router.push('/pet_adopt_form');
}

/*const pet = ref({
  id: "00001",
  name: "汤姆",
  breed: "猫",
  age: "1岁",
  gender: true,
  health: "良好",
  vaccinated: true,
  popularity: 100,
  description: "这是一只可爱的宠物，喜欢玩耍和与人类互动。",
  reads: 0,
  likes: 0,
  comments: 0,
  favorites: 0,
  comment_contents: [
  { id: 1, author: '用户1（后期去掉）', text: '好文章！（后期去掉）', avatar: '@/photos/汤姆1.jpg' },//传照片没成功
  { id: 2, author: '用户2（后期去掉）', text: '感谢分享！（后期去掉）', avatar: '@/photos/汤姆2.jpg' }//传照片没成功
  ],
  image: './photos/汤姆1.jpg',
});*/


/*pet.value.READ_NUM++;



const newComment = ref({ author: '', text: '', avatar: '@/photos/汤姆1.jpg' });//传照片没成功
newComment.value.author = '某某某';
const showCommentForm = ref(false);

const addComment = () => {
if (newComment.value.text) {
    pet.value.COMMENT_NUM++;
    pet.value.COMMENT_CONTENTS.push({
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
    pet.value.LIKE_NUM++;
    await submitLike(userStore.userInfo, pet, pet.value.LIKE_NUM);
  } else {
    pet.value.LIKE_NUM--;
    await submitLike(userStore.userInfo, pet, pet.value.LIKE_NUM);
  }
};

const favorited = ref(false);
const favoritePet = async() => {
  favorited.value = !favorited.value;
  if (favorited.value) {
    pet.value.COLLECT_NUM++;
    await submitFavorite(userStore.userInfo, pet, pet.value.COLLECT_NUM);
  } else {
    pet.value.COLLECT_NUM--;
    await submitFavorite(userStore.userInfo, pet, pet.value.COLLECT_NUM);
  }
};*/

</script>

<style scoped>

  .pet-card {
    display: flex;
    justify-content: center;
  }

  .card {
    width: 1000px;
    position: relative; /* 添加相对定位 */
  }

  .read-count {
  position: absolute;
  top: 20px; /* 调整距离顶部的位置 */
  right: 20px; /* 调整距离右侧的位置 */
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
    border: none; /* 去掉按钮边框 */
    background-color: transparent; /* 设置背景颜色为透明 */
  }

  .round-button img {
    vertical-align: middle;
  }

  .icon {
    vertical-align: middle;
    width: 50px; /* 调整图片宽度 */
    height: 50px; /* 调整图片高度 */
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


  .comment-part {
  margin-left: auto;
  margin-right: auto;
  width: 800px;
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

  .pet-label {
    font-weight: bold; /* 设置标签的字体为粗体 */
    color: #000; /* 设置标签的文字颜色 */
    font-size: 20px;
  }

  .pet-value {
    font-weight: normal; /* 设置数值的字体为普通（非粗体） */
    color: #666; /* 设置数值的文字颜色 */
  }

  .modern-button {
  font-weight: bold; /* 设置标签的字体为粗体 */
  background-color: #4b6fa5; /* 背景颜色 */
  color: white; /* 字体颜色 */
  font-size: 24px; /* 字体大小 */
  border: none; /* 去掉边框 */
  border-radius: 20px; /* 圆角 */
  padding: 15px 20px; /* 按钮内边距，根据需要调整 */
  cursor: pointer; /* 鼠标悬停时显示手型光标 */
  transition: background-color 0.3s, color 0.3s; /* 添加过渡效果 */
}

  .modern-button:hover {
    background-color: #396097; /* 鼠标悬停时的背景颜色 */
    color: white; /* 鼠标悬停时的字体颜色 */
  }
  </style>


<template>
      <div>
        <div style="display: flex;align-items: center;">
               <img src=" ../../../public/return.png" class="textreturn" style="width:24px;height: 24px;">
               &nbsp;<a href="\pet_adopt" style="text-decoration: none;color:#538adc;">返回领养主页</a>
         </div>
        
      </div>
  <div class="pet-card">
    <el-card class="card" >
      <div class="card-content">
        <div>
        <div class="pet-image">
          <img src="https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png" alt="Pet Image" />
        </div>
        <!--<el-row>
            <el-col :span="24">
              <p class="pet-value">{{ pet.description }}</p>
            </el-col>
          </el-row>-->
      </div>
        <div class="pet-info">
          <!--<p class="read-count">阅读{{ pet.READ_NUM }}</p>-->
          <span style="font-size: 30px; color:#4b6fa5;font-weight: bold;">你好呀！我的名字是</span>
          <span style="font-size: 60px; color:#edb055;font-weight: bold;">{{ pet.name }}</span>
          <p>  </p>
          <el-row>
            <el-col :span="8">
              <span class="pet-label">种类</span>
              <p class="pet-value">{{ pet.species }}</p>
            </el-col>
            <!--<el-col :span="8">
              <span class="pet-label">年龄</span>
              <p class="pet-value">{{ pet.value.age }}</p>
            </el-col>-->
            <el-col :span="8">
              <span class="pet-label">性别</span>
              <p class="pet-value">{{ pet.gender}}</p>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="12">
              <span class="pet-label">人气</span>
              <p class="pet-value">{{ pet.popularity }}</p>
            </el-col>
            <el-col :span="12">
              <span class="pet-label">宠物ID</span>
              <p class="pet-value">{{ pet.id }}</p>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="12">
              <span class="pet-label">健康状况</span>
              <p class="pet-value">{{ pet.health_state }}</p>
            </el-col>
            <el-col :span="12">
              <span class="pet-label">疫苗接种状况</span>
              <p class="pet-value">{{ pet.vaccine ? '已' : '未' }}接种疫苗</p>
            </el-col>
          </el-row>
          <p>  </p>
          <div style="text-align: center;">
            <button class="modern-button" @click="handleApplyForAdopt">领养Ta</button>
          </div>
        </div>
      </div>
    </el-card>
  </div>
  <p>  </p>
  <!--<div class="common-layout">
    <div class="interactions">
      <div>
        <button class="round-button" @click="likePet">
          <img v-if="liked" src="@/photos/like_blue.png" alt="点赞" class="icon">
          <img v-else src="@/photos/like_grey.png" alt="未点赞" class="icon">
        </button>
        <span>{{ pet.LIKE_NUM }}</span>
      </div>
      <div>
        <button class="round-button" @click="favoritePet">
          <img v-if="favorited" src="@/photos/favorite_blue.png" alt="收藏" class="icon">
          <img v-else src="@/photos/favorite_grey.png" alt="未收藏" class="icon">
        </button>
        <span>{{ pet.COLLECT_NUM }}</span>
      </div>
    </div>
      <div class="comment-part">
        <h3>评论 {{ pet.COMMENT_NUM }}</h3>
      <div class="comment-form">
        <el-input v-model="newComment.text" type="textarea" placeholder="在这里评论"></el-input>
        <p>  </p>
        <el-button type="primary" plain @click="addComment">发布</el-button>
      </div>
      <p>  </p>
      <div v-for="comment in pet.comment_contents" :key="comment.id" class="comment">
        <el-avatar :src="comment.avatar" :size="40"></el-avatar>
        <div class="comment-content">
          <p>{{ comment.author }}</p>
          <p>{{ comment.text }}</p>
        </div>
      </div>
    </div>
  </div>-->
</template>
