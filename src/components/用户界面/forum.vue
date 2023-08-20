<template>
  <!-- <div class="head_container"> -->
  <el-row :gutter="20" class="head_container">
    <el-col :span="8">
      <div class="ftitle">
        <div class="linetitle">
          宠  物  论  坛
        </div>
        <br>
        <div class="line1">
         一个共享关心、资源和知识的平台
        </div>
         <div class="line1">
         相互交流，相互帮助
        </div>
        <el-button type="primary" plain class="post_button"  @click="$router.push('/posting')">
        发&nbsp;帖
      </el-button>
      </div>
    </el-col>
    <el-col :span="16">
        
    </el-col>

    </el-row>
        <br>
        <br>
    
      <el-row >
        <div style="width:100px"></div>
         <div class="search-bar">
        <el-input v-model="searchText" placeholder="搜索帖子标题"></el-input>
        <el-button type="primary" @click="search">搜索</el-button>
        </div>
      <div >
        <div class="sort-bar">
          
          <el-button type="primary" icon="el-icon-sort" @click="toggleSortOrder">{{ sortOrder === 'asc' ? '按照日期正序排序' : '按照日期倒序排序' }}</el-button>
        </div>
      </div>
      </el-row>
   <div class="content">
      <ul class="forum-posts">
        <!-- <li v-for="post in filteredPosts" :key="post.post_id" @click="goToPost(post)"> -->
          <li v-for="post in filteredPosts" :key="post.post_id" @click="$router.push('/post_details')">
            <el-card class="post-card" >
              <div class="post-title">{{ post.title }}</div>
              <div class="post-info">
                <div>发表时间：{{ post.post_time }}</div>
                <div>阅读量：{{ post.read_count }}</div>
                <div>喜爱数量：{{ post.like_num }}</div>
                <div>评论数量：{{ post.comment_num }}</div>
                <!-- <el-button class="postbutton" type="plain" text style="text-align: center;justify-content: center;">查看详情</el-button> -->
              </div>
            </el-card>
        </li>
      </ul>
    </div>
  <!-- </el-main> -->
      
</template>

<script>
// import { Delete, EditPen, Search, Share, Upload,Coin } from '@element-plus/icons-vue'
import { ref, onMounted, computed } from 'vue'
import notice_forum from '@/api/notice_forum'


// {
//           post_id: 1,
//           user_id: 'thesamechen',
//           read_count: 5,
//           like_num: 3,
//           comment_num: 2,
//           title: '可爱小猫咪找新家',
//           post_time: '2023-03-01 00:00:00'
// },



export default {
  name: "UserInfo",
  setup() {
    const forum_posts = ref([]);
    const searchText = ref('');
    const sortOrder = ref('asc');

    const getPosts= async () => {
      try {
        const response = await notice_forum.getposts();
        for (const forum_post of response) {
          forum_posts.value.push({
          id: forum_post.postId,
          // title: forum_post.heading,
          title: forum_post.heading,
          content: forum_post.content,
          post_time: forum_post.post_time,
          read_count: forum_post.readCount,
          like_num:forum_post.likeNum,
          comment_num:forum_post.commentNum,
        });
      }
      } catch (error) {
        console.error('获取公告数据时出错：', error);
      }
    };

    onMounted(() => {
      getPosts(); // 初始加载时获取帖子列表
    });

    const sortedPosts = computed(() => {
      if (sortOrder.value === 'asc') {
        return forum_posts.value.slice().sort((a, b) => new Date(a.post_time) - new Date(b.post_time));
      } else if (sortOrder.value === 'desc') {
        return forum_posts.value.slice().sort((a, b) => new Date(b.post_time) - new Date(a.post_time));
      }
    });

    const filteredPosts = computed(() => {
      return forum_posts.value.filter(post => {
        return post.title.toLowerCase().includes(searchText.value.toLowerCase());
      });
    });

    const toggleSortOrder = () => {
      sortOrder.value = sortOrder.value === 'asc' ? 'desc' : 'asc';
    };

    const sortByLikeNum = () => {
      sortOrder.value = 'like';
    };

    const search = () => {
      // 执行搜索操作
      console.log('搜索：' + searchText.value);

      // 调用API接口搜索帖子
      notice_forum.searchPosts(searchText.value)
        .then(response => {
          forum_posts.value = response.data;
        })
        .catch(error => {
          console.log(error);
        });
    };

    const goToPost = (post) => {
      // 跳转到帖子详情页
      console.log('跳转到帖子详情页：' + post.title);
      this.$router.push({ path: '/post_details', query: { post_id: post.post_id }});
    };

    return {
      forum_posts,
      searchText,
      filteredPosts,
      goToPost,
      toggleSortOrder,
      sortByLikeNum,
      search
    };
  }
}
</script>

<style>

.content{
  /* margin: 30px 50px 50px 50px; */
  /* background-color: rgba(138, 195, 247, 0.5); */
  height: 100%;
  align-items: center;
  flex-direction: column; /* 将子元素垂直排列 */
  padding:5px;
  justify-content: center;
  display: flex;
}
.sort-bar {
  display: flex;
  margin-bottom: 20px;
  padding-left: 4%;
}

.sort-bar button {
  background-color: #9bb9f6;
  border: none;
  padding: 8px;
  border-radius: 4px;
  cursor: pointer;
  margin-right: 10px;
}

.search-bar {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
  width: 38%;
  padding-left: 4%;
}

.search-bar input[type='text'] {
  flex: 1;
  padding: 4px;
  outline: none;
}

.search-bar button {
  background-color: #4b6fa5;
  border: none;
  padding: 8px;
  border-radius: 4px;
  cursor: pointer;
  margin-left: 10px;
}

.forum-posts {
  max-height: 800px;
  overflow-y: auto;
  width: 80%;
  align-items: center;
  padding-left: 4%;
  font-weight: bold;
  list-style: none;
  padding: 0;
  margin: 0;
  cursor: pointer;
}

.forum-posts li {
  padding: 10px;
  /* border-bottom: 1px solid #ccc; */
  display: flex;
  flex-direction: column;
}

.forum-posts li:hover {
  background-color: #f5f5f5;
}

.post-card{
  height:12vh;
}

.post-title {
  padding-left: 2%;
  font-weight: bold;
  font-size: 19px;
  margin-bottom: 10px;
  color:#4b6fa5
}

.post-info {
  padding-left: 2%;
  display: flex;
  justify-content: space-between;
  text-align: center;
  font-size: 14px;
  color:#abacae;
  font-weight: lighter;
}

.head_container{
  background-image: url("../../../public/forumbg.jpg");
  background-size: cover;
  background-position: center;
  height: 300px;
  width:100%;
  /* display: flex; /* 将容器设置为弹性布局 */
 align-items: center; /*在主轴上居中对齐 */
  /* justify-content: center; 在交叉轴上居中对齐 */
}

.ftitle{
  display: flex;
  flex-direction: column; /* 将子元素垂直排列 */
  align-items: center;
  justify-content: center;
}

.linetitle{
  font-weight: bold;
  font-size: 50px;
  color:#ccdff5;
}

.post_button{
  
  /* background-color: #eebe77; */
  /* color: rgb(255, 255, 255); */
  padding: 15px 15px;
  font-size: 16px;
  border-radius: 10px;
  margin-top: 3vh;
}
.line1{
  color: #ffffff;
  font-size: 16px;
}

</style>
