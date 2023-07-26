<template>
    <div class="tableitem" >
          <img src="./photos/猫猫头像.jpg" style="height: 100px;width: 100px;margin-left: 20px;margin-right: 20px">
          <p class="welcome-text">猫猫寄养申请表</p>
    </div>
    <el-form :model="form" label-width="120px">
      <el-form-item label="宠物昵称">
        <el-input v-model="form.name"  style="width: 50%"/>
      </el-form-item>
      <el-form-item label="寄养开始时间">
          <el-date-picker
            v-model="form.date"
            type="date"
            placeholder="选择日期"
            style="width: 50%"
          />
      </el-form-item>
      <el-form-item label="寄养天数">
        <el-input-number v-model="form.num" :min="1" :max="1000"  />
      </el-form-item>
      <el-form-item label="寄养费用">
        <el-text class="mx-1">{{ calculatedFee }}</el-text>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="onSubmit">提交</el-button>
        <!-- <el-button>Cancel</el-button> -->
      </el-form-item>
    </el-form>
  </template>
  
  <script lang="ts" setup>
  import { reactive,computed } from 'vue'
  
  // do not use same name with ref
  const form = reactive({
    name: '',
    region: '',
    date: '',
    num:'',
  })
  
  const onSubmit = () => {
    console.log('submit!')
  }
  const calculatedFee = computed(() => {
    const duration = Number(form.num) || 0;
    let fee = 0;
    fee=duration*20
    return fee;
  });
  </script>
  
  <style>
  .tableitem {
    display: flex;
    /* justify-content: center; */
  }
  .tableitem > * {
    margin-right: 10px;
    flex: 0 0 auto;
  }
  
  </style>