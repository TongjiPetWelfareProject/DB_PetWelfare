<template>
    <div>
        <el-affix :offset="300" >
        <div style="display:flex;flex-direction: column;float: right;margin-right:8vw">
            <el-button type="primary"  circle size="large" @click="likePost" >
                <el-icon><magic-stick/></el-icon>
            </el-button>
            <p class="affixposttext" style="white-space: nowrap;">点赞&nbsp;{{ post.likes }}</p>
        </div>    
        </el-affix>
        <el-affix :offset="400" >
            <div style="display:flex;flex-direction: column;float: right;margin-right:8vw">
                <el-button type="primary" circle size="large" @click="likePost">
                    <el-icon><star/></el-icon>
                </el-button>
                <p class="affixposttext">收藏&nbsp; {{ post.favorites }}</p>
            
            </div>
        </el-affix>
    </div>
    <div class="main_container">
        
    <div class="postdetail">
      <div>
        <div style="display: flex;align-items: center;">
               <img src=" ../../../public/return.png" class="textreturn" style="width:24px;height: 24px;">
               &nbsp;<a href="\forum" style="text-decoration: none;color:#538adc;">返回论坛</a>
         </div>
        
      </div>
      <h2>{{ post.title }}</h2>
      <p>{{ post.author }}</p>
      <pre>{{ post.content }}</pre>
      <br>
      <div class="interactions">
          <div>
            <el-button type="primary" plain @click="likePost">点赞</el-button>
            <span>{{ post.likes }}</span>
          </div>
    
          <div>
            <el-button type="primary" plain @click="showAddComment">评论</el-button>
            <span>{{ post.comments }}</span>
          </div>
          <div>
            <el-button type="primary" plain @click="favoritePost">收藏</el-button>
            <span>{{ post.favorites }}</span>
          </div>
        </div>
        <br>
    <div v-if="showCommentForm" class="comment-form">
    <el-input v-model="newComment.author" placeholder="你的名字（这个后期要去掉，直接改成用户名）"></el-input>
    <el-input v-model="newComment.text" type="textarea" placeholder="在这里评论"></el-input>
    <el-button type="primary" plain @click="addComment">提交</el-button>
    </div>
    <el-divider></el-divider>
    <h3>评论区</h3>
    <div v-for="comment in post.comment_contents" :key="comment.id" class="comment">
    <el-row style=" display: flex;
        align-items: center; /* 垂直居中 */">
        <el-avatar :src="comment.avatar" :size="40"></el-avatar>
        <p>&nbsp;&nbsp;</p>
        <p1>{{ comment.author }}</p1>
    </el-row>
    <div class="comment-content">
        <p2>{{ comment.text }}</p2>
        <br>
    </div>
    </div>
</div>
</div>
</template>
  
<script setup>
import { ref,watch,onMounted } from 'vue';
import { ElInput, ElButton, ElAvatar, ElDivider } from 'element-plus';
import { MagicStick,Star } from '@element-plus/icons-vue'
import { useRouter } from 'vue-router'
import getpostinfo from '@/api/notice_forum'

const router = useRouter();
const post = ref({
    title: '',
    author: '',
    content: '',
    likes: 0,
    comments: 0,
    favorites: 0,
    comment_contents: [
    { id: '', author: '', text: '', avatar: './src/components/photos/阿尼亚.jpg' }
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

const newComment = ref({ author: '', text: '', avatar: './src/components/photos/默认.jpg' });
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

<style>
.main_container{ 
    display: flex;
    justify-content: center; /* 水平居中 */
    align-items: center; /* 垂直居中 */
    
}

.affixposttext{
    color: #8f8f8f;
    font-size: 14px;
}
.postdetail {
    
    margin-bottom: 20px;
    width:70%;
    align-items: center;
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
    margin-left: 10px;
    display: flex;
    flex-direction: column; /* 垂直排列 */
}

.comment-content {
    margin-left: 10px;
    display: flex;
    flex-direction: column; /* 垂直排列 */
    border-bottom: 0.5px solid #d9d9d9;
}

.comment-form {
    margin-top: 20px;
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
