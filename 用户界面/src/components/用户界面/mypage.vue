<template>
  <div class="common-layout">
    <el-container>
      <el-header style="height: 200px;">
        <el-descriptions
    class="margin-top"
    title="我的信息"
    :column="3"
    :size="size"
    border
  >
    <template #extra>
      <el-button type="primary">编辑</el-button>
    </template>
    <el-descriptions-item>
      <template #label>
        <div class="cell-item">
          <el-icon :style="iconStyle">
            <user />
          </el-icon>
         用户名
        </div>
      </template>
     柴犬的奴
    </el-descriptions-item>
    <el-descriptions-item>
      <template #label>
        <div class="cell-item">
          <el-icon :style="iconStyle">
            <iphone />
          </el-icon>
          Telephone
        </div>
      </template>
      18100000000
    </el-descriptions-item>
    <el-descriptions-item>
      <template #label>
        <div class="cell-item">
          <el-icon :style="iconStyle">
            <star />
          </el-icon>
          点赞量
        </div>
      </template>
      78
    </el-descriptions-item>
    <el-descriptions-item>
      <template #label>
        <div class="cell-item">
          <el-icon :style="iconStyle">
            <tickets />
          </el-icon>
          阅读量
        </div>
      </template>
      89
    </el-descriptions-item>
    <el-descriptions-item>
      <template #label>
        <div class="cell-item">
          <el-icon :style="iconStyle">
            <office-building />
          </el-icon>
         地址
        </div>
      </template>
      上海市嘉定区曹安公路4800号
    </el-descriptions-item>
  </el-descriptions>

      </el-header>


      <el-main>
        <el-tabs v-model="activeName" class="demo-tabs" @tab-click="handleClick">
        <el-tab-pane label="我的宠物" name="first">
          <el-row :gutter="20">
          
          <el-col :span="6">
            <el-text class="mx-1" size="large">我的点赞</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：汤姆</span>
                <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span>
            </li>
            </ul>
          </el-col>

          <el-col :span="6">
            <el-text class="mx-1" size="large">我的收藏</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：汤姆</span>
                <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span>
            </li>
            </ul>
          </el-col>

         <el-col :span="6">
            <el-text class="mx-1" size="large">我的评论</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：汤姆</span>
                <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span>
            </li>
            </ul>
          </el-col>

          <el-col :span="6">
            <el-text class="mx-1" size="large">我的浏览</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：汤姆</span>
                <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span>
            </li>
            </ul>
            <br>
          </el-col>

          <div class="empty-column">
          <!-- 需要空一列的代码 -->
          </div>

          <el-col :span="6">
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
          <el-col :span="6">
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
        </el-row>

        <el-row :gutter="20">
          
          
        </el-row>
        </el-tab-pane>
        <el-tab-pane label="我的论坛" name="second">
          <el-row :gutter="20">
          <el-col :span="6">
            <el-text class="mx-1" size="large">我的点赞</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：汤姆</span>
                <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span>
            </li>
            </ul>
          </el-col>

          <el-col :span="6">
            <el-text class="mx-1" size="large">我的收藏</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：汤姆</span>
                <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span>
            </li>
            </ul>
          </el-col>

         <el-col :span="6">
            <el-text class="mx-1" size="large">我的评论</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：汤姆</span>
                <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span>
            </li>
            </ul>
          </el-col>

          <el-col :span="6">
            <el-text class="mx-1" size="large">我的浏览</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：汤姆</span>
                <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span>
            </li>
            </ul>
          </el-col>
        </el-row>
        </el-tab-pane>
        <el-tab-pane label="其他" name="third">
          <el-row :gutter="20">
          
            <el-col :span="6">
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
          <el-col :span="6">
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
        </el-row>
        </el-tab-pane>
        
      </el-tabs>

       
       
         

        
      </el-main>
      
      
    

    
    </el-container>
    
    
  <div>
	      <!-- 用户头像显示 -->
		<img src="./assets/头像.jpg" class="avatar-container">
	</div>
   
    
  
  </div>
  
  
  

</template>

<script setup lang="ts">
import { ref } from 'vue'
import type { TabsPaneContext } from 'element-plus'

const activeName = ref('first')

const handleClick = (tab: TabsPaneContext, event: Event) => {
  console.log(tab, event)
}
import { computed} from 'vue'
import {
  Iphone,
  Location,
  OfficeBuilding,
  Tickets,
  User,
  UserFilled,
  Star
} from '@element-plus/icons-vue'
const tabPosition = ref('left')
const count = ref(0)
const load = () => {
  count.value += 2
}
const size = ref('')
const iconStyle = computed(() => {
  const marginMap = {
    large: '8px',
    default: '6px',
    small: '4px',
  }
  return {
    marginRight: marginMap[size.value] || marginMap.default,
  }
})
const blockMargin = computed(() => {
  const marginMap = {
    large: '32px',
    default: '28px',
    small: '24px',
  }
  return {
    marginTop: marginMap[size.value] || marginMap.default,
  }
})
</script>

<style>
.el-descriptions {
  margin-top: 20px;
}
.cell-item {
  display: flex;
  align-items: center;
}
.margin-top {
  margin-top: 20px;
}
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
  width: 390px;
  height:350px;
}

/* .common-layout {
  background-image: url(assets/两只狗勾.jpg);
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
} */

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




.el-tabs--right .el-tabs__content,
.el-tabs--left .el-tabs__content {
  height: 100%;
}
/* 下面这个用来控制“我的宠物”下面一行card与上面一行对其用的*/
.empty-column {
  margin-right: 10px; /* 设置空白列宽度为20像素 */
}

</style>
