<template>
  <div class="tableitem" >
        <img src="./photos/狗狗头像.jpeg" style="height: 100px;width: 100px;margin-left: 20px;margin-right: 20px">
        <p class="welcome-text">狗狗寄养申请表</p>
  </div>
  <el-form :model="form" label-width="120px">
    <el-form-item label="宠物昵称">
      <el-input v-model="form.name"  style="width: 50%"/>
    </el-form-item>
    <el-form-item label="宠物类型">
      <el-select v-model="form.region" placeholder="请选择宠物类型">
        <el-option label="大型犬" value="大型犬" />
        <el-option label="中型犬" value="中型犬" />
        <el-option label="小型犬" value="小型犬" />
      </el-select>
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

    if (form.region === '大型犬') {
      fee = 30 * duration;
    } else if (form.region === '中型犬') {
      fee = 25 * duration;
    } else if (form.region === '小型犬') {
      fee = 20 * duration;
    }

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