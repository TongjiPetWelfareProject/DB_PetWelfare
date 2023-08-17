
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
      <el-button type="primary" icon="el-icon-sort" @click="toggleSortOrder">{{ sortOrder === 'asc' ? '按照日期正序排序' : '按照日期倒序排序' }}</el-button>
    </div>
    <div class="notices">
      <ul>
        <li v-for="notice in filteredNotices" :key="notice.id" @click="goToNotice(notice)">
          <span>{{ notice.title }}</span>
          <div class="notice-date">{{ notice.date }}</div>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
  export default {
  data() {
    return {
      notices: [
        {
          id: 1,
          title: "寄养宠物的流程通知",
          date: "2023-03-01"
        },
        { 
          id: 2,
          title: "关于捐赠方式调整的通知",
          date: "2023-04-15"
        },
        {
          id: 3,
          title: "流浪猫“果果”被收养的后续追踪记录",
          date: "2023-05-01"
        },
        {
          id: 4,
          title: "流浪动物救治流程公告",
          date: "2023-05-21"
        },
        {
          id: 5,
          title: "关于节假日放假的通知",
          date: "2023-06-01"
        }
      ],
      searchText: "",
      sortOrder: "asc" // 默认按照日期正序排序
    }
  },
  computed: {
    sortedNotices() {
      return this.notices.sort((a, b) => {
        if (this.sortOrder === "asc") {
          return new Date(a.date) - new Date(b.date);
        } else {
          return new Date(b.date) - new Date(a.date);
        }
      });
    },
    filteredNotices() {
      return this.sortedNotices.filter(notice => {
        return notice.title.toLowerCase().includes(this.searchText.toLowerCase())
      })
    }
  },
  methods: {
    goToNotice(notice) {
      // 跳转到公告详情页
      console.log("跳转到公告详情页：" + notice.title)
    },
    toggleSortOrder() {
      this.sortOrder = this.sortOrder === "asc" ? "desc" : "asc";
    }
  }
}
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