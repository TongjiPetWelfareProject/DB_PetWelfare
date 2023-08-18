<template>
  <el-main>
    <el-affix position="top-right" :offset="20" style="float:right;top: 20px;">
      <router-link to="/posting">我要发帖</router-link>
    </el-affix>
    <div class="content">
      <div class="sort-bar">
        <div>排序：</div>
        <div>
          <el-button @click="toggleSortOrder">
            {{ sortOrder === 'asc' ? '日期正序' : '日期倒序' }}
          </el-button>
        </div>
      </div>
      <div class="search-bar">
        <el-input v-model="searchText" placeholder="搜索帖子标题"></el-input>
        <el-button type="primary" @click="search">搜索</el-button>
      </div>
      <ul class="forum-posts">
        <li v-for="post in filteredPosts" :key="post.post_id" @click="goToPost(post)">
          <div class="post-title">
            <div>{{ post.title }}</div>
          </div>
          <div class="post-info">
            <div>发表时间：{{ post.post_time }}</div>
            <div>阅读量：{{ post.read_count }}</div>
            <div>喜爱数量：{{ post.like_num }}</div>
            <div>评论数量：{{ post.comment_num }}</div>
          </div>
        </li>
      </ul>
    </div>
  </el-main>
</template>

<script>
import { ref, onMounted, computed } from 'vue'
import notice_forum from '@/api/notice_forum'

export default {
  name: "UserInfo",
  setup() {
    const forum_posts = ref([]);
    const searchText = ref('');
    const sortOrder = ref('asc');

    const toggleSortOrder = () => {
      sortOrder.value = sortOrder.value === 'asc' ? 'desc' : 'asc';
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

    const getPosts = () => {
      // 调用API接口获取所有帖子
      notice_forum.getPosts()
        .then(response => {
          forum_posts.value = response.data;
        })
        .catch(error => {
          console.log(error);
        });
    };

    onMounted(() => {
      getPosts(); // 初始加载时获取帖子列表
    });

    const goToPost = (post) => {
      // 跳转到帖子详情页
      console.log('跳转到帖子详情页：' + post.title);
      this.$router.push({ path: '/post_details', query: { post_id: post.post_id }});
    };

    const filteredPosts = computed(() => {
      return forum_posts.value.filter(post => {
        return post.title.toLowerCase().includes(searchText.value.toLowerCase());
      });
    });

    return {
      searchText,
      sortOrder,
      toggleSortOrder,
      search,
      goToPost,
      filteredPosts
    };
  }
}
</script>

<style>

.content{
  /* margin: 30px 50px 50px 50px; */
  background-color: rgba(138, 195, 247, 0.5);
  height: 100%;
  align-items: center;
  flex-direction: column; /* 将子元素垂直排列 */
  padding:5px
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
  
  padding-left: 4%;
  font-weight: bold;
  list-style: none;
  padding: 0;
  margin: 0;
  cursor: pointer;
}

li {
  padding: 10px;
  border-bottom: 1px solid #ccc;
  display: flex;
  flex-direction: column;
}

li:hover {
  background-color: #f5f5f5;
}

.post-title {
  padding-left: 4%;
  font-weight: bold;
  font-size: 22px;
  margin-bottom: 10px;

}

.post-info {
  padding-left: 4%;
  display: flex;
  justify-content: space-between;
  font-size: 15px;
  color:#abacae
}


</style>
