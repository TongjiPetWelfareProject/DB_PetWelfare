<script setup>
import { ref, computed, onMounted } from 'vue'
import { ElInput, ElButton, ElAvatar, ElDivider } from 'element-plus';

import { useUserStore } from '@/store/user';
import { useRoute, useRouter } from 'vue-router'
import petadopt from '@/api/pet_adopt'

const images = [//等后端图片，后期去掉
'./src/components/photos/pet1.jpg',
'./src/components/photos/pet2.jpg',
'./src/components/photos/pet3.jpg',
'./src/components/photos/pet4.jpg',
'./src/components/photos/pet5.jpg',
];

const userStore = useUserStore();
const router = useRouter();
const route = useRoute();

const pet = ref([]); // 单个宠物信息

const getPetDetails = async (petId) => {
  try {
    const response = await petadopt.getPetDetails(petId); // 假设有一个获取单个宠物信息的 API
    console.log(response);
    /*let gender = '';
    if (response.sex === 'M') {
      gender = '弟弟';
    } else if (response.SEX === 'F') {
      gender = '妹妹';
    }*/

    let species = '';
    if (response.original_pet.Species === 'cat') {
      species = '猫';
    } else if (response.original_pet.Species === 'dog') {
      species = '狗';
    }

    let health_state = '';
    if (response.original_pet.Health_State === 'Vibrant') {
      health_state = '健康';
    } else if (response.original_pet.Health_State === 'Unhealthy') {
      health_state = '不健康';
    }


    pet.value = {
      id: response.original_pet.Pet_ID,
      name: response.original_pet.Pet_Name,
      species: species,
      gender: response.original_pet.sex,
      //age: response.AGE,
      popularity: response.Popularity,
      health_state: health_state,
      vaccine: response.original_pet.Vaccine,
      read_num:response.original_pet.Read_Num,
      like_num: response.original_pet.Like_Num,
      collect_num: response.original_pet.Collect_Num,
      comment_num: response.Comment_Num,
      image: images[0]//等后端图片，后期修改
    };
  } catch (error) {
    console.error('获取宠物信息时出错：', error);
  }
};

// 在组件加载时调用 getPetDetails 方法，传入宠物的 ID
onMounted(() => {
  const petId = route.params.id; // 从路由参数中获取宠物的 ID
  console.log(petId);//成功
  getPetDetails(petId);
});


const handleApplyForAdopt = () => {
  if (userStore.userInfo.User_ID) {
    // 用户已登录，跳转到 /pet_adopt_form
    router.push({ name: 'pet_adopt_form', params: { info: pet.id }});
  } else {
    // 用户未登录，跳转到 /login
    router.push('/login');
  }
}

/*const handleApplyForAdopt = () => {
  
  router.push('/pet_adopt_form');
}*/

pet.read_num++;



const newComment = ref({ author: '', text: '', avatar: '@/photos/汤姆1.jpg' });//传照片没成功
newComment.value.author = '某某某';
const showCommentForm = ref(false);

const addComment = () => {
if (newComment.value.text) {
    pet.comment_num++;
    pet.comment.push({
        id: pet.comment.length + 1,
        author: newComment.value.author,
        text: newComment.value.text,
        avatar: newComment.value.avatar
    });
    newComment.value.text = '';
}
};

const liked = ref(false);
const likePet = async() => {
  if (!userStore.userInfo.User_ID)
    router.push('/login');

  liked.value = !liked.value;
  if (liked.value) {
    pet.like_num++;
    await petadopt.submitLike(userStore.userInfo, pet, pet.like_num);
  } else {
    pet.like_num--;
    await petadopt.submitLike(userStore.userInfo, pet, pet.like_num);
  }
};

const favorited = ref(false);
const favoritePet = async() => {
  if (!userStore.userInfo.User_ID)
    router.push('/login');
  else {  
    favorited.value = !favorited.value;
    if (favorited.value) {
      pet.collect_num++;
      await petadopt.submitFavorite(userStore.userInfo, pet, pet.collect_num);
    } else {
      pet.collect_num--;
      await petadopt.submitFavorite(userStore.userInfo, pet, pet.collect_num);
    }
  }
};

</script>

<style scoped>

  .pet-card {
    display: flex;
    justify-content: center;
  }

  .card {
    width: 85%;
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
    width: 85%;
    }

    .comment {
    display: flex;
    }

    .comment-content {
    margin-left: 10px;
    }
    
    .comment-form {
      display: flex;
      align-items: center; /* 垂直居中对齐 */
    }

    .comment-input {
      flex: 1; /* 评论输入框占据剩余空间 */
      margin-right: 10px; /* 设置评论输入框和发布按钮之间的间距 */
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
      <p></p>
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
          <p class="read-count">阅读{{ pet.read_num }}</p>
          <span style="font-size: 30px; color:#4b6fa5;font-weight: bold;">你好呀！我的名字是</span>
          <span style="font-size: 60px; color:#edb055;font-weight: bold;">{{ pet.name }}</span>
          <p>  </p>
          <el-row>
            <el-col :span="8">
              <span class="pet-label">种类</span>
              <p class="pet-value">{{ pet.species }}</p>
            </el-col>
            <el-col :span="8">
              <span class="pet-label">年龄</span>
              <p class="pet-value">先空着</p>
              <!--<p class="pet-value">{{ pet.value.age }}</p>-->
            </el-col>
            <el-col :span="8">
              <span class="pet-label">性别</span>
              <p class="pet-value">{{ pet.gender ? '弟弟' : '妹妹'}}</p>
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
  <div class="common-layout">
    <div class="interactions">
      <div>
        <button class="round-button" @click="likePet">
          <img v-if="liked" src="@/photos/like_blue.png" alt="点赞" class="icon">
          <img v-else src="@/photos/like_grey.png" alt="未点赞" class="icon">
        </button>
        <span>{{ pet.like_num }}</span>
      </div>
      <div>
        <button class="round-button" @click="favoritePet">
          <img v-if="favorited" src="@/photos/favorite_blue.png" alt="收藏" class="icon">
          <img v-else src="@/photos/favorite_grey.png" alt="未收藏" class="icon">
        </button>
        <span>{{ pet.collect_num }}</span>
      </div>
    </div>

    <div class="comment-part">
      <h3 style="font-size: 27px; color:#4b6fa5;font-weight: bold;">评论 {{ pet.comment_num }}</h3>
      <div class="comment-form">
        <div class="comment-input">
          <el-input v-model="newComment.text" type="textarea" placeholder="在这里评论"></el-input>
        </div>
        <div class="comment-button">
          <button type="primary" class="modern-button" @click="addComment" style="font-size: 20px;">发布</button>
        </div>
      </div>
      <p>  </p>
      <div v-for="comment in pet.comment_contents" :key="comment.id" class="comment">
        <el-avatar :src="comment.avatar" :size="50"></el-avatar>
        <div class="comment-content">
            <p class="comment-label">{{ comment.author }}</p>
            <p class="comment-value">{{ comment.text }}</p>
        </div>
      </div>
    </div>
  </div>
</template>
