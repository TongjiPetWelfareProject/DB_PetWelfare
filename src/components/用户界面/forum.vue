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
        <el-button type="primary" plain class="post_button" @click="post" :icon="EditPen">
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
        <el-button type="primary" @click="search" :icon="Search">搜索</el-button>
        </div>
      <div >
        <div class="sort-bar">
          <el-button type="primary" :icon="SortDown" @click="toggleSortOrder">时间顺序</el-button>
        </div>
      </div>
      </el-row>
   <div class="content">
      <ul class="forum-posts">
        <!-- <li v-for="post in filteredPosts" :key="post.post_id" @click="goToPost(post)"> -->
          <li v-for="post in slicedPosts" :key="post.id" @click="goToPost(post)">
            <el-card class="post-card" >
              <div class="post-title">{{ post.title }}</div>
              <div class="post-info">
                <div>发表时间：{{ post.post_time }}</div>
                <div style="display:flex;align-items: center;"><el-icon :size="18"><list/></el-icon>阅读量：{{ post.read_count }}</div>
                <div style="display:flex;align-items: center;"><el-icon :size="20"><StarFilled/></el-icon>点赞数：{{ post.like_num }}</div>
                <div style="display:flex;align-items: center;"><el-icon :size="18"><management/></el-icon>评论数：{{ post.comment_num }}</div>
                <!-- <el-button class="postbutton" type="plain" text style="text-align: center;justify-content: center;">查看详情</el-button> -->
              </div>
            </el-card>
        </li>
      </ul>

      <el-pagination
      layout="prev, pager, next,jumper"
      :total="filteredPosts.length"
      :current-page="currentPage"
      :page-size="pageSize"
      @current-change="handlePageChange"
      style="float: left;"
    />
    </div>
   

  <!-- </el-main> -->
      
</template>

<script>
import { Delete, EditPen, Search, Share, Upload,SortDown,Management,List,StarFilled} from '@element-plus/icons-vue'
import { ref, onMounted, computed } from 'vue'
import notice_forum from '@/api/notice_forum'
import { ElMessage, ElMessageBox } from 'element-plus'
import { useRouter } from 'vue-router'
import { useUserStore } from '@/store/user';


export default {
  name: "UserInfo",
  components:{
    StarFilled,
    List,
    Management
  },
  setup() {
    const forum_posts = ref([]);
    const searchText = ref('');
    const sortOrder = ref('desc');
    const router = useRouter();
    const userStore = useUserStore();

    const currentPage = ref(1); // 当前页数，默认为第1页
    const pageSize = ref(8); // 每页显示的帖子数量，默认为10条

    function handlePageChange(newPage) {
      currentPage.value = newPage;
    }

    const startIndex = computed(() => {
      return (currentPage.value - 1) * pageSize.value;
    });

    const endIndex = computed(() => {
      return startIndex.value + pageSize.value;
    });

    const slicedPosts = computed(() => {
      return filteredPosts.value.slice(startIndex.value, endIndex.value);
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

    const getPosts= async () => {
      try {
        const response = await notice_forum.getposts();
        for (const forum_post of response) {
          const formattedDate = formatBackendTime(forum_post.post_time);
          forum_posts.value.push({
          id: forum_post.postId,
          title: forum_post.heading,
          content: forum_post.content,
          post_time: formattedDate,
          read_count: forum_post.readCount,
          like_num:forum_post.likeNum,
          comment_num:forum_post.commentNum,
        });

      }
      handlePageChange(currentPage.value);
      } catch (error) {
        console.error('获取帖子数据时出错：', error);
      }
    };

    onMounted(() => {
      getPosts(); // 初始加载时获取帖子列表
    });


    const post = () => {
      if (userStore.userInfo.User_ID) {
        // 用户已登录，跳转到 /posting
        router.push('/posting');
      } else {
        // 用户未登录，跳转到 /login
         ElMessage({
          message: '请先登录',
          type: 'warning',
        });
        router.push('/login');
      }
    }

    const sortedPosts = computed(() => {
      if (sortOrder.value === 'asc') {
        return forum_posts.value.slice().sort((a, b) => new Date(a.post_time) - new Date(b.post_time));
      } else if (sortOrder.value === 'desc') {
        return forum_posts.value.slice().sort((a, b) => new Date(b.post_time) - new Date(a.post_time));
      }
    });

    const filteredPosts = computed(() => {
      return sortedPosts.value.filter(post => {
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
      router.push({ path: '/post_details', query: { post_id: post.id }});
    };

    return {
      forum_posts,
      searchText,
      filteredPosts,
      post,
      goToPost,
      toggleSortOrder,
      sortByLikeNum,
      search,
      Delete, EditPen, Search, Share, Upload,SortDown,
      handlePageChange,
      slicedPosts,
      currentPage,
      pageSize,
      StarFilled
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
  margin-left: 2vw;
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
  margin-left: 3vw;
}

.forum-posts {
  /* max-height: 800px; */
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

.el-card:hover{
  background-color: #f3f3f3;
  box-shadow: 0 0 10rgb(224, 150, 150)(0, 0, 0, 0.3);
  transform: scale(1.0);
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
  color:#4b6fa5;
  /* 设置标题只显示一行，超出部分显示省略号 */
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.post-info {
  padding-left: 2%;
  display: flex;
  justify-content: space-between;
  text-align: center;
  align-items: center;
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
