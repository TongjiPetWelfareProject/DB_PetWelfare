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
    <el-col :span="12">
      <div class="pet-image">
        <img :src=pet.image alt="Pet Image" />
      </div>
    </el-col>
    <!--<el-row>
        <el-col :span="24">
          <p class="pet-value">{{ pet.description }}</p>
        </el-col>
      </el-row>-->
    <el-col :span="12">
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
          <p class="pet-value">{{ pet.age }}</p>
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
    </el-col>
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
    <div class="comment-input" v-if="userStore.userInfo.User_ID">
      <el-input v-model="newComment" type="textarea" placeholder="在这里评论"></el-input>
    </div>
    <div class="comment-input" v-else>
      <el-input v-model="newComment" type="textarea" placeholder="请先登录后发表评论~" :readonly="true"></el-input>
    </div>
    <div class="comment-button">
      <button type="primary" class="modern-button" @click="addComment" style="font-size: 20px;" :disabled="!newComment">发布</button>
    </div>
  </div>
  <h3 style="font-size: 24px; color:#4b6fa5;font-weight: bold;">评论 {{ pet.comment_num }}</h3>
  <p></p>
  <div v-for="comment in sortedComments" :key="comment.commenter_id" class="comment">
    <el-avatar style="margin-top:20px" v-if="comment.commenter_avatar" :src="comment.commenter_avatar" :size="50"></el-avatar>
    <div style="margin-top:20px" class="comment-content">
      <p class="post-label">{{ comment.commenter }}</p>
      <p class="post-value">{{ comment.comment_content }}</p>
      <div class="comment-actions">
        <p v-if="comment.commenter_avatar" class="comment-time custom-comment-time">{{ formatBackendTime(comment.comment_time) }}</p>
        <el-button type="warning" v-if="comment.commenter_avatar && isOwnPost(comment.commenter_id)" href="#" @click="deleteComment(comment)" link>删除</el-button>
      </div>
    </div>
  </div>
</div>
</div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { ElInput, ElButton, ElAvatar, ElDivider } from 'element-plus';
import { useUserStore } from '@/store/user';
import { useRoute, useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import getpetinfo from '@/api/pet_adopt'

const userStore = useUserStore();
console.log(userStore);
const router = useRouter();
const route = useRoute();
const pet = ref([]); // 单个宠物信息
const comments = ref([]);
const petId = ref('');//应将petId定义在更广阔的作用域内，onMounted之外，访问响应式数据时需要使用.value
// 在组件加载时调用 getPetDetails 方法，传入宠物的 ID
onMounted(() => {
  petId.value = route.params.id; // 从路由参数中获取宠物的 ID
  console.log(petId.value);//成功
  getPetDetails(petId.value);
  getifinteract(petId.value);
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
      health_state = '充满活力';
    } else if (response.original_pet.Health_State === 'Well') {
      health_state = '健康';
    } else if (response.original_pet.Health_State === 'Decent') {
      health_state = '尚可';
    } else if (response.original_pet.Health_State === 'Unhealthy') {
      health_state = '不健康';
    } else if (response.original_pet.Health_State === 'Sickly') {
      health_state = '生病';
    } else if (response.original_pet.Health_State === 'Critical') {
      health_state = '危急';
    }
    console.log("宠物生日")
    console.log(response.original_pet.birthdate);
    // 获取当前日期
    const currentDate = new Date();
    // 将生日字符串转换为日期对象
    const birthDate = new Date(response.original_pet.birthdate);
    // 计算年龄（以当前日期为准）
    let age = currentDate.getFullYear() - birthDate.getFullYear();
    // 如果生日还未到，年龄减一
    if (
      currentDate.getMonth() < birthDate.getMonth() ||
      (currentDate.getMonth() === birthDate.getMonth() &&
        currentDate.getDate() < birthDate.getDate())
    ) {
      age--;
    }
    pet.value = {
      id: response.original_pet.Pet_ID,
      name: response.original_pet.Pet_Name,
      species: species,
      gender: response.original_pet.sex,
      age: age,
      popularity: response.Popularity,
      health_state: health_state,
      vaccine: response.original_pet.Vaccine,
      read_num:response.original_pet.Read_Num,
      like_num: response.original_pet.Like_Num,
      favorite_num: response.original_pet.Collect_Num,
      comment_num: response.Comment_Num,
      image: response.original_pet.Avatar,//等后端图片，后期修改
    };
    for (const comment of response.comments) {
      comments.value.push({
        commenter_id: comment.commenter_id,
        commenter: comment.commenter,
        comment_time: comment.comment_time,
        comment_content: comment.comment_contents,
        commenter_avatar: comment.commenter_avatar,
      });
    }
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

pet.read_num++;
const newComment = ref('');

const liked = ref(false);
const favorited = ref(false);

const getifinteract= async (PID) => {
  try {
      const response = await getpetinfo.ifInteract(userStore.userInfo.User_ID, PID);
      console.log(response);
      if(response.is_liked == false) {
        liked.value = false;
      }
      else {
        liked.value = true;
      }
      if(response.is_collected == false) {
        favorited.value = false;
      }
      else {
        favorited.value = true;
      }
    } catch (error) {
      console.error('获取点赞和收藏信息失败：', error);
    }
};
  
const likePet = async () => {
    console.log("点击点赞了")
    if (!userStore.userInfo.User_ID) {
      ElMessage({
          type: 'warning',
          message: '请先登录',
        })
    }
    else {
        liked.value = liked.value === true ? false : true;
      if(liked.value === false)
        pet.value.like_num--
      else
        pet.value.like_num++
      try {
        const response = await getpetinfo.submitLike(userStore.userInfo.User_ID,petId.value);
      } catch (error) {
        console.error('提交点赞信息失败：', error);
      }
    }
};


const favoritePet = async () => {
    console.log("点击收藏了")
    if (!userStore.userInfo.User_ID) {
      ElMessage({
          type: 'warning',
          message: '请先登录',
        })
    }
      else {
      favorited.value = favorited.value === true ? false : true;
      if(favorited.value === false)
        pet.value.favorite_num--
      else
        pet.value.favorite_num++
      try {
        const response = await getpetinfo.submitFavorite(userStore.userInfo.User_ID,petId.value);
      } catch (error) {
        console.error('提交收藏信息失败：', error);
      }
    }
};

const deleteComment = async (comment) => {
  try {
    console.log("删除时间为"+comment.comment_time+"的评论")
    const response = await getpetinfo.deleteComment(comment.commenter_id, petId.value, comment.comment_time);
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

const addComment = async () => {
  if (!userStore.userInfo.User_ID) {
      ElMessage({
          type: 'warning',
          message: '请先登录',
        })
    }
  else {
    try {
      let temp = newComment.value;
      newComment.value = '';
      const response = await getpetinfo.addComment(userStore.userInfo.User_ID, petId.value, temp);
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
  }
};

const isOwnPost = (UID) => {
    if( UID === userStore.userInfo.User_ID || userStore.userInfo.Role ==='Admin')
      return true
    else 
      return false
}

function formatBackendTime(backendTime) {
  const date = new Date(backendTime);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');
  const hours = String(date.getHours()).padStart(2, '0');
  const minutes = String(date.getMinutes()).padStart(2, '0');
  const seconds = String(date.getSeconds()).padStart(2, '0');

  const formattedTime = `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
  return formattedTime;
}

// 创建一个计算属性来按时间排序评论
const sortedComments = computed(() => {
  // 使用.slice()创建副本以避免修改原始数据
  const commentsCopy = [...comments.value];

  // 使用JavaScript的Array.sort()方法进行排序
  commentsCopy.sort((a, b) => {
    // 使用 formatBackendTime 返回值比较评论的时间顺序
    const timeA = formatBackendTime(a.comment_time);
    const timeB = formatBackendTime(b.comment_time);

    // 你需要根据你的日期格式来比较时间
    // 这里假设时间是可比较的字符串格式，如果不是，需要相应地调整比较逻辑
    return timeB.localeCompare(timeA);//这里是时间倒序
  });

  return commentsCopy;
});
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
    width: 40px; /* 调整图片宽度 */
    height: 40px; /* 调整图片高度 */
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
    border-bottom:1px solid #dad9d9;
    width:90%;
  
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
  background-color: #5683c7; /* 背景颜色 */
  color: white; /* 字体颜色 */
  font-size: 22px; /* 字体大小 */
  border: none; /* 去掉边框 */
  border-radius: 15px; /* 圆角 */
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
    margin-top:-10px
  }
  </style>