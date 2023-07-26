<template>
     <el-text size="large" tag="b">预约问诊</el-text>
     <br><br>
    <el-form :model="form" label-width="120px" style="max-width: 90%;">
      <el-form-item label="宠物姓名">
        <el-input v-model="form.name" />
      </el-form-item>
      <el-form-item label="种类">
        <el-select v-model="form.kind" placeholder="please select a type">
          <el-option label="肠胃" value="肠胃" />
          <el-option label="外伤" value="外伤" />
        </el-select>
      </el-form-item>
      <el-form-item label="预约时间">
        <el-col :span="11">
          <el-date-picker
            v-model="form.date1"
            type="date"
            placeholder="Pick a date"
            style="width: 100%"
          />
        </el-col>
        <el-col :span="2" class="text-center">
          <span class="text-gray-500">-</span>
        </el-col>
        <el-col :span="11">
          <el-time-picker
            v-model="form.date2"
            placeholder="Pick a time"
            style="width: 100%"
          />
        </el-col>
      </el-form-item>
      <!-- <el-form-item label="Instant delivery">
        <el-switch v-model="form.delivery" />
      </el-form-item> -->
      <el-form-item label="预约项目">
        <el-checkbox-group v-model="form.type">
          <el-checkbox label="检查" name="type" />
          <el-checkbox label="治疗" name="type" />
          <el-checkbox label="手术" name="type" />
        </el-checkbox-group>
      </el-form-item>
      <el-form-item label="问诊情况">
        <el-radio-group v-model="form.resource">
          <el-radio label="首次" />
          <el-radio label="复诊" />
        </el-radio-group>
      </el-form-item>
      <el-form-item label="病情描述">
        <el-input v-model="form.desc" type="textarea" />
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="onSubmit">提交</el-button>
        <el-button>取消</el-button>
      </el-form-item>
    </el-form>
  </template>
  
  <script lang="ts" setup>
  import { reactive } from 'vue'
  

  const form = reactive({
    name: '',
    kind: '',
    date1: '',
    date2: '',
    delivery: false,
    type: [],
    resource: '',
    desc: '',
  })
  
  const onSubmit = () => {
    console.log('submit!')
  }
  </script>