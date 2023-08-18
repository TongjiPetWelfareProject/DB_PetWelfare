<template>
  <div class="main_page">
    <div class="title">
      <span>公告栏</span>
    </div>
    <div class="search-bar">
      <el-input v-model="searchText" placeholder="请输入搜索内容"></el-input>
      <el-button type="primary" icon="el-icon-search"></el-button>
    </div>
    <div class="sort-bar">
      <el-button type="primary" icon="el-icon-sort" @click="toggleSortByDate">{{ sortOrder === 'asc' ? '按照日期正序排序' : '按照日期倒序排序' }}</el-button>
      <el-button type="primary" icon="el-icon-sort" @click="toggleSortByView">{{ sortOrder === 'asc' ? '按照阅读量由高到低排序' : '按照阅读量由低到高排序' }}</el-button>
    </div>
    <div class="notices">
      <ul>
        <li v-for="notice in filteredNotices" :key="notice.id" @click="showNotice(notice)">
          <span>{{ notice.title }}</span>
          <div class="notice-id">{{ notice.id }}</div>
          <div class="notice-date">{{ notice.date }}</div>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue'
import notice_forum from '@/api/notice_forum'

export default {
  setup() {
    const notices = ref([]);
    const searchText = ref("");
    const sortOrder = ref("asc");

    const getNotices = async () => {
      try {
        notices.value = await notice_forum.bulletinAPI();
      } catch (error) {
        console.error('获取公告数据时出错：', error);
      }
    };

    onMounted(() => {
      getNotices();
    });

    const toggleSortByDate = () => {
      sortOrder.value = sortOrder.value === "asc" ? "desc" : "asc";
      notices.value.sort((a, b) => {
        if (sortOrder.value === "asc") {
          return new Date(a.date) - new Date(b.date);
        } else {
          return new Date(b.date) - new Date(a.date);
        }
      });
    };

    const toggleSortByView = () => {
      sortOrder.value = sortOrder.value === "asc" ? "desc" : "asc";
      notices.value.sort((a, b) => {
        if (sortOrder.value === "asc") {
          return a.views - b.views;
        } else {
          return b.views - a.views;
        }
      });
    };

    const showNotice = (notice) => {
      console.log("公告标题：" + notice.title);
      console.log("公告内容：" + notice.content);
    };

    const filteredNotices = computed(() => {
      return notices.value.filter((notice) => {
        return notice.title.toLowerCase().includes(searchText.value.toLowerCase());
      });
    });

    return {
      notices,
      searchText,
      sortOrder,
      getNotices,
      toggleSortByDate,
      toggleSortByView,
      showNotice,
      filteredNotices,
    };
  },
};
</script>

<style >
.main_page{
  display: flex;
  align-items: center;
  flex-direction: column;
}
.title {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
  font-size: 32px;
  color:rgb(19, 138, 67);
  padding-left: 4%;
  font-weight: bold;
}
.sort-bar {
  display: flex;
  margin-bottom: 25px;
  padding-left: 4%;
}

.search-bar {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
  width:35%;
  padding-left: 4%;
}

.search-bar input[type="text"] {
  flex: 1;
  padding: 4px;
  outline: none;
}

.search-bar button {
  background-color: #abacae;
  border: none;
  padding: 8px;
  border-radius: 4px;
  cursor: pointer;
  margin-left: 10px;
}

.notice-date {
  text-align: right;
}

.notice-id {
  text-align: right;
}

.notices {
  max-height: 400px;
  overflow-y: auto;
  width: 50%;
  font-size: 18px;
  padding-left: 4%;
  font-weight: bold;
}

ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

li {
  padding: 10px;
  border-bottom: 1px solid #ccc;
  cursor: pointer;
}

li:hover {
  background-color: #f5f5f5;
}
</style>
