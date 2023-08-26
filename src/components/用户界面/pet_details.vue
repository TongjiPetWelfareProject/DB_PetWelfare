<script setup>
import { ref, computed, onMounted } from 'vue'
import { ElInput, ElButton, ElAvatar, ElDivider } from 'element-plus';

import { useUserStore } from '@/store/user';
import { useRoute, useRouter } from 'vue-router'
import getpetinfo from '@/api/pet_adopt'

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
const petId = ref('');//应将petId定义在更广阔的作用域内，onMounted之外，访问响应式数据时需要使用.value

// 在组件加载时调用 getPetDetails 方法，传入宠物的 ID
onMounted(() => {
  petId.value = route.params.id; // 从路由参数中获取宠物的 ID
  console.log(petId.value);//成功
  getPetDetails(petId.value);
  getcomment();
  getiflike();
  getiffavorite();
});

const getPetDetails = async (PID) => {
  try {
    const response = await getpetinfo.getPetDetails(PID); // 假设有一个获取单个宠物信息的 API
    console.log(response);

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
      favorite_num: response.original_pet.Collect_Num,
      comment_num: response.Comment_Num,
      image: images[0]//等后端图片，后期修改
    };
  } catch (error) {
    console.error('获取宠物信息时出错：', error);
  }
};


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

const comment_contents=ref([{  
   id: '', 
   user_id: '',
   author: '', 
   text: '', 
   avatar: '',
   time:''
}])

/*const addComment = () => {
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
};*/

const addComment = async () => {
  try {
    const response = await getpetinfo.addcomment(userStore.userInfo.User_ID,petId,newComment.value.text);
      ElMessage.success({
      message: '评论成功',
      duration: 1000 // 持续显示时间（毫秒）
    });
    // 停顿3秒后刷新
    setTimeout(() => {
      location.reload();
    }, 1000);
      } catch (error) {
        console.error('评论失败：', error);
      // 显示失败提示
      ElMessage.error({
      message: '评论失败，错误信息：' + error.message,
      duration: 1000 // 持续显示时间（毫秒）
    });
      }
};

const isOwnPost = (UID) => {
    if( UID === userStore.userInfo.User_ID || userStore.userInfo.Role==='Admin')
      return true
    else 
      return false
}

const showAddComment = () => {
    showCommentForm.value = true;
}

const liked = ref(false);
const likePet = async () => {
    console.log("点击点赞了")
    liked.value = liked.value === true ? false : true;
    if(liked.value === false)
      pet.value.like_num--
    else
      pet.value.like_num++
    try {
      const response = await getpetinfo.likepet(userStore.userInfo.User_ID,petId);
    } catch (error) {
      console.error('获取点赞信息失败：', error);
    }
};

const favorited = ref(false);
const favoritePet = async () => {
    console.log("点击点赞了")
    favorited.value = favorited.value === true ? false : true;
    if(favorited.value === false)
      pet.value.favorite_num--
    else
      pet.value.favorite_num++
    try {
      const response = await getpetinfo.likepet(userStore.userInfo.User_ID,petId);
    } catch (error) {
      console.error('获取点赞信息失败：', error);
    }
};

const getcomment= async () => {
    try {
        const response = await getpetinfo.getComment(petId);
        for (const postcomment of response) {
            // const formattedDate = formatBackendTime(postinfo.published_date);
            comment_contents.value.push({
            id: postcomment.pid,
            user_id: postcomment.uid,
            author: postcomment.user_Name,
            text: postcomment.content,
            time: postcomment.comment_Time,
            avatar:'./src/photos/阿尼亚.jpg'
          });
        }
      } catch (error) {
        console.error('获取评论失败：', error);
      }
    };

const sortedComments = computed(() => {
  return comment_contents.value.slice().sort((a, b) => {
    const dateA = new Date(a.time);
    const dateB = new Date(b.time);
    return dateB - dateA;
  });
});

const deleteComment = async (comment) => {
  try {
    console.log("删除时间为"+comment.time+"的帖子")
    const response = await getpostinfo.deletecomment(comment.user_id,comment.id,comment.time);
      ElMessage.success({
      message: '删除成功',
      duration: 1000 // 持续显示时间（毫秒）
    });
    // 停顿3秒后刷新
    setTimeout(() => {
      location.reload();
    }, 1000);
      } catch (error) {
        console.error('删除失败：', error);
      // 显示失败提示
      ElMessage.error({
      message: '删除失败，错误信息：' + error.message,
      duration: 1000 // 持续显示时间（毫秒）
    });
  }
}

const getiflike= async () => {
  try {
      const response = await getpetinfo.ifLike(userStore.userInfo.User_ID,petId);
      if(response === -1)
        liked.value = false
      else
        liked.value = true
    } catch (error) {
      console.error('获取点赞信息失败：', error);
    }
};

const getiffavorite= async () => {
  try {
      const response = await getpetinfo.ifFavorite(userStore.userInfo.User_ID,petId);
      if(response === -1)
        favorited.value = false
      else
        favorited.value = true
    } catch (error) {
      console.error('获取收藏信息失败：', error);
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

  .post-label {
    font-weight: bold; /* 设置标签的字体为粗体 */
    color: #000; /* 设置标签的文字颜色 */
    font-size: 15px;
  }

  .post-value {
      font-weight: normal; /* 设置数值的字体为普通（非粗体） */
      color: #666; /* 设置数值的文字颜色 */
  }

  .custom-comment-time {
    font-size: 12px; /* Adjust the font size */
    color: #999; /* Adjust the text color to a lighter gray */
  }
  .comment-actions {
    display: flex;
    align-items: center;
    gap: 10px; /* 调整日期和链接之间的间距 */
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
        <span>{{ pet.favorite_num }}</span>
      </div>
    </div>

    <div class="comment-part">
      <div class="comment-form">
        <div class="comment-input">
          <el-input v-model="newComment.text" type="textarea" placeholder="在这里评论"></el-input>
        </div>
        <div class="comment-button">
          <button type="primary" class="modern-button" @click="addComment" style="font-size: 20px;">发布</button>
        </div>
      </div>
      <h3 style="font-size: 27px; color:#4b6fa5;font-weight: bold;">评论 {{ pet.comment_num }}</h3>
      <p></p>
      <div v-for="comment in sortedComments" :key="comment.id" class="comment">
        <el-avatar v-if="comment.avatar" :src="comment.avatar" :size="50"></el-avatar>
        <div class="comment-content">
          <p class="post-label">{{ comment.author }}</p>
          <p class="post-value">{{ comment.text }}</p>
          <div class="comment-actions">
            <p v-if="comment.avatar" class="comment-time custom-comment-time">{{ formatBackendTime(comment.time) }}</p>
            <a v-if="comment.avatar && isOwnPost(comment.user_id)" href="#" @click="deleteComment(comment)">删除</a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
