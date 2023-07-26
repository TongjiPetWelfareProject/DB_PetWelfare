<template>
   <el-row>
    <el-col :span="12">
      <el-card class="box-card">
    <template #header>
      <div class="card-header">
        <span>狗寄养价格</span>
        <router-link to="/dog_foster_table" target="_blank">申请表
        <!-- <el-button class="button" text>申请表</el-button> -->
        </router-link>
      </div>
    </template>
    <div class="text item">大型犬 30元/天</div>
    <div class="text item">中型犬 25元/天</div>
    <div class="text item">小型犬 20元/天</div>
  </el-card>
    </el-col>
    <el-col :span="12">
      <el-card class="box-card">
    <template #header>
      <div class="card-header">
        <span>猫寄养价格</span>
        <router-link to="/cat_foster_table">申请表
        </router-link>
        <!-- <el-button class="button" text>申请表</el-button> -->
      </div>
    </template>
    <br>
    <div class="text item">20元/天</div>
    <br>
  </el-card>
    </el-col>
  </el-row>
</template>

<style>
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
  width: 90%;
}
.el-row {
  margin-bottom: 20px;
}
.el-row:last-child {
  margin-bottom: 0;
}
.el-col {
  border-radius: 4px;
  display: flex;
  justify-content: center
}
</style>
