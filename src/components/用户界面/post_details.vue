<template>
  <div class="common-layout">
    <div class="postdetail">
        <div style="display: flex;align-items: center;">
               <img src=" ../../../public/return.png" class="textreturn" style="width:24px;height: 24px;">
               &nbsp;<a href="\forum" style="text-decoration: none;color:#538adc;">返回论坛</a>
         </div>
      <h2>{{ post.title }}</h2>
      <p>{{ post.author }}</p>
      <pre>{{ post.content }}</pre>
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
        <div class="comment-input">
          <el-input v-model="newComment.text" type="textarea" placeholder="在这里评论"></el-input>
        </div>
        <div class="comment-button">
          <button type="primary" class="modern-button" @click="addComment" style="font-size: 20px;">发布</button>
        </div>
      </div>
      <h3 style="font-size: 27px; color:#4b6fa5;font-weight: bold;">评论 {{ post.comment_num }}</h3>
      <p>  </p>
      <div v-for="comment in post.comment_contents" :key="comment.id" class="comment">
        <el-avatar :src="comment.avatar" :size="50"></el-avatar>
        <div class="comment-content">
            <p class="post-label">{{ comment.author }}</p>
            <p class="post-value">{{ comment.text }}</p>
        </div>
      </div>
    </div>
  </div>
  </template>
  
<script setup>
    import { ref, watch, onMounted } from 'vue';
    import { ElInput, ElButton, ElAvatar, ElDivider } from 'element-plus';
    import { MagicStick,Star } from '@element-plus/icons-vue'
    import { useRoute, useRouter } from 'vue-router'
    import getpostinfo from '@/api/notice_forum'

    const router = useRouter();
    const route = useRoute();

    const post = ref({
        title: '',
        author: '',
        content: '',
        like_num: 0,
        comment_num: 0,
        collect_num: 0,
        comment_contents: [
        { id: '', author: '', text: '', avatar: './src/photos/阿尼亚.jpg' }
        ]
    });
  
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

const getpost= async () => {
    try {
        const response = await getpostinfo.getpost(postId.value);
        for (const postinfo of response) {
        // const formattedDate = formatBackendTime(postinfo.published_date);
        post.value.title = postinfo.heading;
        post.value.author = postinfo.userName;
        post.value.content = postinfo.content;
        post.value.like_num = postinfo.likeNum;
        post.value.comment_num = postinfo.commentNum;
        console.log(post.title)
    }
      } catch (error) {
        console.error('获取帖子失败：', error);
      }
    };

onMounted(() => {
  // 从路由的 query 参数中获取 post_id
  postId.value = router.currentRoute.value.query.post_id;
  console.log('当前帖子ID:', postId.value);
  getpost();
});

const newComment = ref({ author: '', text: '', avatar: './src/photos/默认.jpg' });
    const showCommentForm = ref(false);
    
    const addComment = () => {
        if (newComment.value.author && newComment.value.text) {
            post.value.comments++;
            post.value.comment_contents.push({
                id: post.value.comment_contents.length + 1,
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
  
    const likePost = () => {
        post.value.likes++;
    };
  
    const favoritePost = () => {
        post.value.favorites++;
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
    }

    .round-button img {
        vertical-align: middle;
    }

    .icon {
        vertical-align: middle;
        width: 30px; /* 调整图片宽度 */
        height: 30px; /* 调整图片高度 */
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
    width: 70%;
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
        background-color: #4b6fa5; /* 背景颜色 */
        color: white; /* 字体颜色 */
        font-size: 24px; /* 字体大小 */
        border: none; /* 去掉边框 */
        border-radius: 10px; /* 圆角减小 */
        padding: 10px 20px; /* 减小按钮内边距 */
        cursor: pointer; /* 鼠标悬停时显示手型光标 */
        transition: background-color 0.3s, color 0.3s; /* 添加过渡效果 */
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
        padding: 1rem;
        border-radius: 10px;
    }
    .postdetail p1{
        font-size: 19px;
    }
    .postdetail p2{
        font-size: 16px;
    }
  </style>