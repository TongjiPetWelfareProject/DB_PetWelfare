<template>
      <el-main>
        <el-affix position="top-right" :offset="20" style="float:right;top: 20px;">
          <!-- <el-button type="primary" size="medium" icon="el-icon-edit">我要发帖</el-button> -->
          <router-link to="/posting">我要发帖
        </router-link>
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
        <!-- <li v-for="post in filteredPosts" :key="post.post_id" @click="goToPost(post)"> -->
          <li v-for="post in filteredPosts" :key="post.post_id" @click="$router.push('/post_details')">
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
export default {
  name: "UserInfo",
  data() {
    return {
      forum_posts: [
        {
          post_id: 1,
          user_id: 'thesamechen',
          read_count: 5,
          like_num: 3,
          comment_num: 2,
          title: '可爱小猫咪找新家',
          post_time: '2023-03-01 00:00:00'
        },
        {
          post_id: 2,
          user_id: 'thesamechen',
          read_count: 4,
          like_num: 4,
          comment_num: 2,
          title: '日常遛狗分享',
          post_time: '2023-04-15 00:00:00'
        },
        {
          post_id: 3,
          user_id: 'thesamechen',
          read_count: 7,
          like_num: 5,
          comment_num: 2,
          title: '流浪猫“果果”被收养的后续追踪记录',
          post_time: '2023-05-01 00:00:00'
        },
        {
          post_id: 4,
          user_id: 'thesamechen',
          read_count: 15,
          like_num: 3,
          comment_num: 2,
          title: '想领养一只田园犬',
          post_time: '2023-05-21 00:00:00'
        },
        {
          post_id: 5,
          user_id: 'thesamechen',
          read_count: 25,
          like_num: 23,
          comment_num: 12,
          title: '猫咪生病在线求助！',
          post_time: '2023-06-01 00:00:00'
        }
      ],
      searchText: '',
      sortOrder: 'asc' // 默认按照发布时间正序排序
    }
  },
  computed: {
    sortedPosts() {
      if (this.sortOrder === 'asc') {
        return this.forum_posts.sort((a, b) => new Date(a.post_time) - new Date(b.post_time))
      } else if (this.sortOrder === 'desc') {
        return this.forum_posts.sort((a, b) => new Date(b.post_time) - new Date(a.post_time))
      } 
    },
    filteredPosts() {
      return this.sortedPosts.filter(post =>
        post.title.toLowerCase().includes(this.searchText.toLowerCase())
      )
    }
  },
  methods: {
    goToPost(post) {
      // 跳转到帖子详情页
      // router.push('/medical')
      console.log('跳转到帖子详情页：' + post.title)
    },
    toggleSortOrder() {
      if (this.sortOrder === 'asc') {
        this.sortOrder = 'desc'
      } else if (this.sortOrder === 'desc') {
        this.sortOrder = 'like'
      } else {
        this.sortOrder = 'asc'
      }
    },
    sortByLikeNum() {
      this.sortOrder = 'like'
    },
    search() {
      // 执行搜索操作
      console.log('搜索：' + this.searchText)
    }
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