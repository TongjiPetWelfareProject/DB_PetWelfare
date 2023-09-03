<script setup>
import { ref, computed, onMounted } from 'vue';
import { Delete, EditPen, Search, Share, Upload, SortDown } from '@element-plus/icons-vue';
import notice_forum from '@/api/notice_forum';
import { ElMessage, ElMessageBox } from 'element-plus';

const notices = ref([]);
const searchText = ref("");
const sortOrder = ref("desc");
const noticeContent=ref('')
const noticeTitle=ref('')
const dialogVisible=ref(false)

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

const getNotices = async () => {
  try {
    const response = await notice_forum.bulletinAPI();
    for (const notice of response) {
      const formattedDate = formatBackendTime(notice.published_date);
      notices.value.push({
        id: notice.id,
        title: notice.heading,
        date: formattedDate,
        content: notice.content,
      });
    }
  } catch (error) {
    console.error('获取公告数据时出错：', error);
  }
};

onMounted(() => {
  getNotices();
});

const sortedNotices = computed(() => {
  return notices.value.slice().sort((a, b) => {
    if (sortOrder.value === "asc") {
      return new Date(a.date) - new Date(b.date);
    } else {
      return new Date(b.date) - new Date(a.date);
    }
  });
});

const filteredNotices = computed(() => {
  return sortedNotices.value.filter(notice => {
    return notice.title.toLowerCase().includes(searchText.value.toLowerCase());
  });
});

const showCustomMessageBox = ref(false);
const currentNotice = ref(null);

const goToNotice = (notice) => {
  console.log("跳转到公告详情页：" + notice.title);
 // ElMessageBox.alert(notice.content,notice.title);
   noticeContent.value=notice.content
   noticeTitle.value=notice.title
   dialogVisible.value=true
  //currentNotice.value = notice;
  //showCustomMessageBox.value = true;
};

const toggleSortOrder = () => {
  sortOrder.value = sortOrder.value === "asc" ? "desc" : "asc";
};
</script>

<template>
  <!-- <div class="main_page"> -->
    <!-- <div class="headcontainer"> -->
      <!-- <div class="title">
        <span>公告栏</span>
      </div> -->
      
      <!-- <div class="search-bar">
        <el-input v-model="searchText" placeholder="请输入搜索内容"></el-input>
        <el-button type="primary" icon="el-icon-search"></el-button>
      </div>
      <div class="sort-bar">
        <el-button type="primary" icon="el-icon-sort" @click="toggleSortOrder">{{ sortOrder === 'asc' ? '按照日期正序排序' : '按照日期倒序排序' }}</el-button>
      </div>
    </div> -->
    <el-row :gutter="20" class="head_container_notice">
    <el-col :span="8">
      <div class="ftitle">
        <div class="linetitle">
          公      告
        </div>
        <br>
        <div class="line1">
          传递关爱，共享精彩
        </div>
        <div class="line1">
          与您分享重要信息
        </div>
      </div>
    </el-col>
    <el-col :span="16">
        
    </el-col>
    </el-row>
    <br><br>
    <el-row >
        <div style="width:100px"></div>
         <div class="search-bar">
        <el-input v-model="searchText" placeholder="搜索公告标题"></el-input>
        <el-button type="primary" :icon="Search" @click="search">搜索</el-button>
        </div>
      <div >
        <div class="sort-bar">
          <el-button type="primary" :icon="SortDown" @click="toggleSortOrder">时间顺序</el-button>
        </div>
      </div>
      </el-row>
    
      <div class="content">
    <div class="notices">
      <ul>
        <li v-for="notice in filteredNotices" :key="notice.id" @click="goToNotice(notice)">
          <el-card class="notice-card" >
            <span class="notice-title">{{ notice.title }}</span>
            <div class="noticebody">
              <div class="notice-date">{{ notice.date }}</div>
            </div>
          </el-card>
          
        </li>
      </ul>
      <el-dialog
            v-model="dialogVisible"
            :title="noticeTitle"
            width="30%"
            :before-close="handleClose"
          >
          <div class="dialog-content">
            {{ noticeContent }}
          </div>
            <template #footer>
              <span class="dialog-footer">
                <el-button type="primary" @click="dialogVisible = false">
                  确认
                </el-button>
              </span>
            </template>
          </el-dialog>
    </div>
    </div>
    <teleport to="body">
      <custom-message-box
        v-if="showCustomMessageBox"
        :notice="currentNotice"
        @close="showCustomMessageBox = false"
      ></custom-message-box>
    </teleport>
  <!-- </div> -->
</template>



<style>

.dialog-content{
  max-width: 100%; /* 限制最大宽度以适应 dialog */
  white-space: pre-wrap;
}
.main_page{
  display: flex;
  align-items: center;
  flex-direction: column;
  justify-content: center;
}
.title {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
  font-size: 40px;
  color:rgb(255, 255, 255);
  font-weight: bold;
  justify-content: center;
}
.sort-bar {
  display: flex;
  margin-bottom: 25px;
  padding-left: 10px;
  justify-content: center;
}

.search-bar {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
  width:400px;
  padding-left: 10px;
  justify-content: center;
}

.search-bar input[type="text"] {
  width:200px;
  padding: 4px;
  outline: none;
  justify-content: center;
}

.search-bar button {
  background-color: #a8c0f1;
  border: none;
  padding: 8px;
  border-radius: 4px;
  cursor: pointer;
  margin-left: 10px;
  
}

.notice-title{
  color:#667ca8;
  font-size: 18px;
}

.notice-date {
  text-align: left;
  font-weight: lighter;
  font-size: 13px;
  color:#c2c3c3
}

.notice-card{
  padding-bottom:0px;
  height:12vh;
}

.noticebody{
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 4px;
  padding-bottom:0px;
}

.notices {
  max-height: 800px;
  overflow-y: auto;
  width: 80%;
  font-size: 17px;
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
  /* border-bottom: 1px solid #ccc; */
  cursor: pointer;
}

li:hover {
  background-color: #f5f5f5;
}

.headcontainer{
  background-image: url("../../../public/noticebg.jpg");
  background-size: cover;
  background-position: center;
  display: flex;
  flex-direction: column;
  text-align: center;
  justify-content: center;
  height: 300px;
  width:100%;
  align-items: center;

}

.content{
  align-items: center;
  flex-direction: column; /* 将子元素垂直排列 */
  justify-content: center;
  display: flex;
}


.head_container_notice{
  background-image: url("../../../public/noticebg.jpg");
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

.line1{
  color: rgb(255, 255, 255);
  font-size: 16px;
}

.custom-message-box {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: rgba(0, 0, 0, 0.5);
  z-index: 9999;
}

.custom-message-box-content {
  background-color: white;
  padding: 20px;
  border-radius: 5px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
  text-align: center;
}

.custom-message-box-button {
  margin-top: 10px;
}

</style>
