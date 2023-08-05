<template>
    <div class="demo-collapse">
      <br><br>
      <el-text size="large" tag="b">康复故事</el-text>
      <br>
      <el-collapse v-model="activeName" max-height="200" accordion>
        <el-collapse-item title="Tom" name="1">
          <div>
            Consistent with real life: in line with the process and logic of real
            life, and comply with languages and habits that the users are used to;
          </div>
        </el-collapse-item>
        <el-collapse-item title="Jerry" name="2">
          <div>
            Operation feedback: enable the users to clearly perceive their
            operations by style updates and interactive effects;
          </div>
          <div>
            Visual feedback: reflect current state by updating or rearranging
            elements of the page.
          </div>
        </el-collapse-item>
        <el-collapse-item title="GGBond" name="3">
          <div>
            Easy to identify: the interface should be straightforward, which helps
            the users to identify and frees them from memorizing and recalling.
          </div>
        </el-collapse-item>
        <el-collapse-item title="Peppa" name="4">
          <div>
            Controlled consequences: users should be granted the freedom to
            operate, including canceling, aborting or terminating current
            operation.
          </div>
        </el-collapse-item>
        <el-collapse-item title="George" name="5">
          <div>
            Controlled consequences: users should be granted the freedom to
            operate, including canceling, aborting or terminating current
            operation.
          </div>
        </el-collapse-item>
        <el-collapse-item title="Makabaka" name="6">
          <div>
            Controlled consequences: users should be granted the freedom to
            operate, including canceling, aborting or terminating current
            operation.
          </div>
        </el-collapse-item>
      </el-collapse>
    </div>
    <el-pagination layout="prev, pager, next" :total="50" />
  </template>
  
  <script lang="ts" setup>
  import { ref } from 'vue'
  
  const activeName = ref('1')
  </script>
  
  <style>
  .el-collapse{
    max-width:90%;
    max-height: 400px;
  }
</style>