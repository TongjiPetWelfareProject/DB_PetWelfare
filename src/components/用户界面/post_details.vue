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
    import { ref } from 'vue';
    import { ElInput, ElButton, ElAvatar, ElDivider } from 'element-plus';
    import {MagicStick,Star} from '@element-plus/icons-vue'

    const post = ref({
        title: '柴犬生病在线求助',
        author: '柴犬の奴',
        content: '我是柴犬花花的主人，我非常担心地向大家求助。我亲爱的花花最近出现了健康问题，我希望能够获得您宝贵的建议和支持。\n花花是一只可爱活泼的柴犬，它一直是我们家庭的快乐源泉。然而，最近它开始出现食欲不振、呕吐和乏力的症状。我非常担心它的健康状况，但由于种种原因，我目前无法立即前往兽医诊所。\n因此，我希望能够在这里寻求您的帮助。如果您是兽医专家或有相关经验的宠物爱好者，请您花一些时间阅读以下信息并给予我一些建议。\n花花的症状：食欲不振、呕吐和乏力。它显得消沉，不再像往常那样精力充沛。\n花花的背景：花花是一只三岁的柴犬，之前一直健康活泼，食欲良好。\n饮食和生活习惯：花花平时饮食规律，没有变化。我们有一个良好的日常护理和饮食计划。\n我明白远程诊断可能存在限制，但我希望您能就花花的症状和可能的原因给予我一些初步建议。您的专业意见对我来说至关重要，它将帮助我更好地了解花花的健康问题并做出相应的应对。\n如果您需要更多的信息，请随时与我联系。我非常感激您的帮助和支持，您的专业知识将有助于花花尽快恢复健康，我们将感激不尽。',
        likes: 0,
        comments: 0,
        favorites: 0,
        comment_contents: [
        { id: 1, author: '用户1（后期去掉）', text: '好文章！（后期去掉）', avatar: './src/components/photos/阿尼亚.jpg' },
        { id: 2, author: '用户2（后期去掉）', text: '感谢分享！（后期去掉）', avatar: './src/components/photos/CC.jpg' }
        ]
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