<template>
  <div class="common-layout">
    <div class="postdetail">
      <div style="display: flex;align-items: center;">
           <img src=" ../../../public/return.png" class="textreturn" style="width:24px;height: 24px;">
           &nbsp;<el-button text type="primary" style="padding-left:2px;padding-right:2px;font-size: 18px;" @click="router.go(-1)">返回</el-button> 
     </div>
         <div style="display: flex;align-items: center;">
           <h2 style="font-size: 30px;">{{ post.title }}</h2>
          
           <el-popconfirm @confirm="confirmDelete" title="确认删除吗?" confirm-button-text="确认" cancel-button-text="取消">
              <template #reference>
                <el-button type="danger" v-if="isOwnPost(post.userid)"  size="small" style="margin-left:2vw" >删除</el-button>
              </template>
          </el-popconfirm>
            <!-- <a v-if="isOwnPost(post.userid)" href="#" @click="deletePost(post.id,post.userid)" style=" text-decoration: none;color:rgb(217, 108, 108);font-size: 20px;" >删除</a> -->
           <!-- <el-button type="danger" v-if="isOwnPost(post.userid)" @click="deletePost(post.id,post.userid)" size="small" style="margin-left:2vw" >删除</el-button> -->
          </div>
    
        <!-- <el-avatar :src="comment.avatar" :size="50"></el-avatar> -->
        <div style="display: flex; align-items: center; justify-content: space-between; margin-top: -15px;">
          <div style="display: flex; align-items: center;">
           
            <p style="font-size: 18px">{{ post.author }}</p>
          </div>
          <span style="font-size: 14px; color: rgb(153, 153, 153);">{{ post.like_num }}人赞同了该帖子</span>
        </div>

      <pre>{{ post.content }}</pre>
      <div v-if="post.urls.length > 0">
        <div v-for="(image,index) in post.urls" :key="index">
          <img :src="image" :alt="'Image ' + (index + 1)" style="max-width: 100%; height: auto;">
        </div>
      </div>
      
    </div>
    <br>
    <div class="interactions">
      <div>
        <button class="round-button" @click="likePost">
          <img v-if="liked" src="@/photos/like_blue.png" alt="点赞" class="icon">
          <img v-else src="@/photos/like_grey.png" alt="未点赞" class="icon">
        </button>
        <span>{{ post.like_num }}</span>
      </div>
      <!-- <div>
        <button class="round-button" @click="favoritePost">
          <img v-if="favorited" src="@/photos/favorite_blue.png" alt="收藏" class="icon">
          <img v-else src="@/photos/favorite_grey.png" alt="未收藏" class="icon">
        </button>
        <span>{{ post.collect_num }}</span>
      </div> -->
    </div>

    <div class="comment-part">
      <div class="comment-form">
        <el-avatar v-if="userStore.userInfo.User_ID" :src="userStore.userInfo.Avatar" :size="50"></el-avatar>
        <div class="comment-input">
          <el-input v-if="userStore.userInfo.User_ID" v-model="newComment.text" type="textarea"   placeholder="在这里评论" style="font-size:16px;margin-left:15px"></el-input>
          <el-input v-else v-model="newComment.text" type="textarea" placeholder="请先登录后发表评论~"   :readonly="true" style="font-size:16px;margin-left:15px"></el-input>
        </div>
        <div class="comment-button">
          <button type="primary" class="modern-button" @click="addComment" style="font-size: 18px;" :disabled="!newComment.text">发布</button>
        </div>
      </div>
      <br>
      <el-card class="commentcard" shadow="always">
        <template #header>
          <h3 style="margin-top: -5px;margin-bottom:-5px;font-size: 21px; color:#4b6fa5;font-weight: bold;">评论 &nbsp;{{ post.comment_num }}</h3>
        </template>
        <!-- <h3 style="font-size: 25px; color:#4b6fa5;font-weight: bold;">评论 &nbsp;{{ post.comment_num }}</h3> -->
        <p></p>
        <div v-for="comment in sortedComments" :key="comment.id" class="comment">

          <el-avatar v-if="comment.avatar" :src="comment.avatar" :size="50"></el-avatar>
          <div v-if="comment.id" class="comment-content">
            <p class="post-label">{{ comment.author }}</p>
            <p class="post-value">{{ comment.text }}</p>
            <div class="comment-actions">
              <p v-if="comment.id" class="comment-time custom-comment-time">{{ formatBackendTime(comment.time) }}</p>
              <el-button type="warning" v-if="comment.id && isOwnPost(comment.user_id)" href="#" @click="deleteComment(comment)" link>删除</el-button>
            </div>
          </div>
    
        </div>
      </el-card>

    </div>
  </div>
  </template>
  
<script setup>
import { ref, onMounted,computed } from 'vue';
import { ElInput, ElAvatar } from 'element-plus';
import { useRouter } from 'vue-router'
import getpostinfo from '@/api/notice_forum'
import { ElMessage } from 'element-plus'
import { useUserStore } from '@/store/user';

const router = useRouter();
const userStore = useUserStore();
const liked = ref(false); // 初始点赞状态为未点赞
const post = ref({
  id: '',
  userid: '',
  title: '',
  author: '',
  content: '',
  like_num: 0,
  comment_num: 0,
  collect_num: 0,
  urls: [],
});

const comment_contents=ref([{  
   id: '', 
   user_id: '',
   author: '', 
   text: '', 
   avatar: '',
   time:''
}])

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

const postId = ref('')

function confirmDelete(){
  deletePost(post.value.id,post.value.user_id)
}

const getpost= async () => {
    try {
        const response = await getpostinfo.getpost(postId.value);
        for (const postinfo of response) {
        // const formattedDate = formatBackendTime(postinfo.published_date);
        post.value.id = postinfo.postId,
        post.value.userid = postinfo.userId,
        post.value.title = postinfo.heading,
        post.value.author = postinfo.userName,
        post.value.content = postinfo.content,
        post.value.like_num = postinfo.likeNum,
        post.value.comment_num = postinfo.commentNum,
        post.value.urls = postinfo.urls
    }
      } catch (error) {
        console.error('获取帖子失败：', error);
      }
    };

const getcomment= async () => {
    try {
        const response = await getpostinfo.getcomment(postId.value);
        for (const postcomment of response) {
            // const formattedDate = formatBackendTime(postinfo.published_date);
            comment_contents.value.push({
            id: postcomment.pid,
            user_id: postcomment.uid,
            avatar:postcomment.avatar,
            author: postcomment.user_Name,
            text: postcomment.content,
            time: postcomment.comment_Time,
          });
        }
      } catch (error) {
        console.error('获取帖子失败：', error);
      }
    };

const sortedComments = computed(() => {
  return comment_contents.value.slice().sort((a, b) => {
    const dateA = new Date(a.time);
    const dateB = new Date(b.time);
    return dateB - dateA;
  });
});

const getiflike= async () => {
  try {
      const response = await getpostinfo.iflike(userStore.userInfo.User_ID,postId.value);
      if(response === -1)
        liked.value = false
      else
        liked.value = true
    } catch (error) {
      console.error('获取点赞信息失败：', error);
    }
};

onMounted(() => {
  // 从路由的 query 参数中获取 post_id
  postId.value = router.currentRoute.value.query.post_id;
  console.log('当前帖子ID:', postId.value);
  getpost();
  getcomment();
  getiflike();
});

const newComment = ref({ author: userStore.userInfo.User_Name, text: '', avatar: './src/photos/阿尼亚.jpg' });
const showCommentForm = ref(false);

const addComment = async () => {
  if (userStore.userInfo.User_ID) {
    try {
    const response = await getpostinfo.addcomment(userStore.userInfo.User_ID,postId.value,newComment.value.text);
      ElMessage.success({
      message: '评论成功',
      duration: 1000 // 持续显示时间（毫秒）
    });
    // 停顿1秒后刷新
    setTimeout(() => {
      location.reload();
    }, 1000);
      } catch (error) {
        console.error('评论失败：', error);
      // 显示失败提示
      ElMessage.error({
      message: '评论失败，您已被禁言，请等待解禁',
      duration: 1000 // 持续显示时间（毫秒）
    });
      }
      } else {
        // 用户未登录，跳转到 /login
         ElMessage({
          message: '请先登录',
          type: 'warning',
        });
        router.push('/login');
      }
};

const isOwnPost = (userid) => {
    if( userid === userStore.userInfo.User_ID || userStore.userInfo.Role==='Admin')
      return true
    else 
      return false
}

const likePost = async () => {
    console.log("点击点赞了")
    liked.value = liked.value === true ? false : true;
    if(liked.value === false)
      post.value.like_num--
    else
      post.value.like_num++
    try {
      const response = await getpostinfo.likepost(userStore.userInfo.User_ID,postId.value);
    } catch (error) {
      console.error('获取点赞信息失败：', error);
    }
};

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

const deletePost = async (postid,userid) => {
  try {
    const response = await getpostinfo.deletepost(postid,userid);
      ElMessage.success({
      message: '删除成功',
      duration: 1000 // 持续显示时间（毫秒）
    });
    // 停顿1秒后跳转
    setTimeout(() => {
      router.push('/forum');
    }, 1000);
      } catch (error) {
        console.error('删除失败：', error);
      // 显示失败提示
      ElMessage.error({
      message: '删除失败，错误信息：' + error.message,
      duration: 1000 // 持续显示时间（毫秒）
    });
  }
};
</script>
  
<style scoped>
.postdetail {
    margin: 0 auto; /* 水平居中 */
    margin-bottom: 20px;
    width: 70%;
}

.round-button {
    border-radius: 20%; /* 圆形按钮 */
    border: none; /* 去掉按钮边框 */
    background-color: transparent; /* 设置背景颜色为透明 */
    cursor: pointer; /* 显示指针形式 */
}

.round-button img {
    vertical-align: middle;
}

.icon {
    vertical-align: middle;
    width: 35px; /* 调整图片宽度 */
    height: 35px; /* 调整图片高度 */
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

.commentcard{
  padding-top: 0px;
  /* margin-top:-20px */
}

.comment-part {
margin-left: auto;
margin-right: auto;
width: 70%;
}

.comment {
display: flex;
}

.comment-content {
margin-left: 10px;
border-bottom:1px solid #dad9d9;
width:100%;
padding:0;
}

.comment-form {
  display: flex;
  align-items: center; /* 垂直居中对齐 */
}

.comment-input {
  flex: 1; /* 评论输入框占据剩余空间 */
  margin-right: 10px; /* 设置评论输入框和发布按钮之间的间距 */
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

.modern-button {
    font-weight: bold; /* 设置标签的字体为粗体 */
    background-color: #73a6e4; /* 背景颜色 */
    color: white; /* 字体颜色 */
    font-size: 25px; /* 字体大小 */
    border: none; /* 去掉边框 */
    border-radius: 15px; /* 圆角减小 */
    padding: 14px 22px; /* 减小按钮内边距 */
    cursor: pointer; /* 鼠标悬停时显示手型光标 */
    transition: background-color 0.3s, color 0.3s; /* 添加过渡效果 */
    margin-left:20px
}

.postdetail h2 {
    font-weight: bold;
    color: #4b6fa5;
    font-size: 32px;
}
.postdetail h3 {
    margin-top: 5vh;
    font-weight: bold;
    color: #4b6fa5;
    font-size: 27px;
}
.postdetail p{
    font-size: 20px;
    color:#737474

}
.postdetail pre {
    font-family: monospace;
    color:#424242;
    font-size: 16px;
    line-height: 1.7;
    white-space: pre-wrap;
    word-wrap: break-word;
    background-color: #f5f5f5;
    /* box-shadow: 0 0px 4px rgba(133, 133, 133, 0.2); */
    padding: 1rem;
    border-radius: 10px;
}
.postdetail p1{
    font-size: 19px;
}
.postdetail p2{
    font-size: 16px;
}
.custom-comment-time {
  font-size: 12px; /* Adjust the font size */
  color: #999; /* Adjust the text color to a lighter gray */
}
.comment-actions {
  display: flex;
  align-items: center;
  gap: 10px; /* 调整日期和链接之间的间距 */
  margin-top:-10px;
  width:100%
}
</style>
