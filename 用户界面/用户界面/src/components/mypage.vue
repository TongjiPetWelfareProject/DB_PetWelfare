<template>
  <div class="common-layout">
    <el-container>
      <el-header>我的主页</el-header>
      <el-main><el-row :gutter="20">
          <el-col :span="5">
            <el-card class="box-card">
              <template #header>
                <div class="card-header">
                  <span>我的领养</span>
                  <!-- <el-button class="button" text>Operation button</el-button> -->
                </div>
              </template>
              <div v-for="o in 6" :key="o" class="text item">
                <span v-if="o === 1">宠物ID:00001</span>
                <span v-if="o === 2">宠物名称：乐乐</span>
                <span v-if="o === 3">领养时间:2023-5-1</span>
                <span v-if="o === 4">宠物年龄:5岁</span>
                <span v-if="o === 5">宠物性别:男</span>
                <span v-if="o === 6">宠物种类:柴犬</span>
              </div>
            </el-card>
          </el-col>
          <el-col :span="5">
            <el-card class="box-card">
              <template #header>
                <div class="card-header">
                  <span>我的寄养</span>
                  <!-- <el-button class="button" text>Operation button</el-button> -->
                </div>
              </template>
              <div v-for="o in 7" :key="o" class="text item">
                <span v-if="o === 1">宠物ID:54321</span>
                <span v-if="o === 2">宠物名称:美美</span>
                <span v-if="o === 3">寄养时间:2023-5-1</span>
                <span v-if="o === 4">寄养时长:3天</span>
                <span v-if="o === 5">宠物年龄:5岁</span>
                <span v-if="o === 6">宠物性别:女</span>
                <span v-if="o === 7">宠物种类:柴犬</span>
              </div>
            </el-card>
          </el-col>
          <el-col :span="5">
            <el-card class="box-card">
              <template #header>
                <div class="card-header">
                  <span>我的医疗</span>
                  <!-- <el-button class="button" text>Operation button</el-button> -->
                </div>
              </template>
              <div v-for="o in 7" :key="o" class="text item">
                <span v-if="o === 1">宠物ID:00001</span>
                <span v-if="o === 2">宠物名称：乐乐</span>
                <span v-if="o === 3">医生ID：001</span>
                <span v-if="o === 4">领养时间:2023-5-1</span>
                <span v-if="o === 5">宠物年龄:5岁</span>
                <span v-if="o === 6">宠物性别:男</span>
                <span v-if="o === 7">宠物种类:柴犬</span>
              </div>
            </el-card>
          </el-col>
          <el-col :span="5">
            <el-card class="box-card">
              <template #header>
                <div class="card-header">
                  <span>我的捐款</span>
                  <!-- <el-button class="button" text>Operation button</el-button> -->
                </div>
              </template>
              <div v-for="o in 3" :key="o" class="text item">
                <span v-if="o === 1">捐款ID:12581</span>
                <span v-if="o === 2">捐款金额:500</span>
                <span v-if="o === 3">捐款时间:2023-5-1</span>
              </div>
            </el-card>
          </el-col>
        </el-row></el-main>
      <el-footer>我的相关</el-footer>
      <el-main><el-row :gutter="20">
          
            
          <el-col :span="5">
            <el-text class="mx-1" size="large">我的信息</el-text>
            <el-card class="box-card">
              
              <template #header>
                <div class="card-header">
                  <span>Me</span>
                  <el-button class="button" text>编辑</el-button>
                </div>
              </template>
              <div v-for="o in 5" :key="o" class="text item">
                <span v-if="o === 1">
                  用户头像
                  <el-button type="primary">编辑</el-button>

                </span>
                <span v-if="o === 2">
                  用户名：小白
                  <el-button type="primary">编辑</el-button>
                  
                </span>
                <span v-if="o === 3">
                  ID：12345
                  <!-- <el-button type="primary">编辑</el-button> -->
                </span>
                <span v-if="o === 4">
                  电话：123-456-78911
                  <el-button type="primary">编辑</el-button>
                </span>
                <span v-if="o === 5">
                  家庭住址：上海市嘉定区安亭镇曹安公路4800号
                  <el-button type="primary">编辑</el-button>
                </span>

              </div>
            </el-card>
          </el-col>
            <el-col :span="5">
              <el-text class="mx-1" size="large">我的浏览记录</el-text>
              <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
              <li v-for="i in count" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：汤姆</span>
                <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span>
                
              </li>
              </ul>
          </el-col>

          

          <el-col :span="5">
            <el-text class="mx-1" size="large">我的收藏</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：汤姆</span>
                <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span>
            </li>
            </ul>
          </el-col>

          <el-col :span="5">
            <el-text class="mx-1" size="large">我的回复</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：汤姆</span>
                <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span>
            </li>
            </ul>
          </el-col>

          
        </el-row></el-main>
    
    

    
    </el-container>
    
    
  <div>
	      <!-- 用户头像显示 -->
		<img src="./assets/头像.jpg" class="avatar-container">
	</div>
   
    
  
  </div>
  
  
  

</template>

<script setup lang="ts">
import { UserFilled } from '@element-plus/icons-vue'
import { ref } from 'vue'
const count = ref(0)
const load = () => {
  count.value += 2
}
</script>

<style>
.el-row {
  margin-bottom: 5px;
}

.el-row:last-child {
  margin-bottom: 0;
}

.el-col {
  border-radius: 1px;
}

.grid-content {
  border-radius: 4px;
  min-height: 36px;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.text {
  font-size: 14px;
}

.item {
  margin-bottom: 18px;
}

.box-card {
  width: 300px;
}

.common-layout {
  background-image: url(assets/两只狗勾.jpg);
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
}

.demo-type {
  display: flex;
}
.demo-type > div {
  flex: 1;
  text-align: center;
}

.demo-type > div:not(:last-child) {
  border-right: 1px solid var(--el-border-color);
}

.avatar-container {
	  position: absolute;
    top:20px;
	  left: 100px;
	  width: 50px;
	  height: 50px;
	  border-radius: 999px; /* 将border-radius设置为很大的值，使容器变成圆形 */
	  background-color: #fff;
	  /* 添加其他自定义样式 */
	}


  .infinite-list {
  height: 300px;
  padding: 0;
  margin: 0;
  list-style: none;
}
.infinite-list .infinite-list-item {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 50px;
  background: var(--el-color-primary-light-9);
  margin: 10px;
  color: var(--el-color-primary);
}
.infinite-list .infinite-list-item + .list-item {
  margin-top: 10px;
}



</style>
