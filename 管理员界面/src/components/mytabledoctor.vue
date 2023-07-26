<template>
  <el-table :data="tableData" style="width: 100%;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px;" max-height="600">
    <el-table-column prop="id" label="医生ID" width="120" />
    <el-table-column prop="name" label="医生姓名" width="120" />
    <el-table-column prop="phone" label="电话" width="120" />
    <el-table-column prop="workingHours" label="工作时间" width="120" />
    <el-table-column prop="salary" label="工资" width="170" />
    <el-table-column label="操作" width="360">
      <template #default="scope">
        <el-button link type="primary" size="small" @click.prevent="editRow(scope.$index)">
          编辑
        </el-button>
        <el-button link type="danger" size="small" @click.prevent="deleteRow(scope.$index)">
          删除
        </el-button>
      </template>
    </el-table-column>
  </el-table>
  <br />
  <el-button type="primary" @click="addRow">添加医生</el-button>
</template>
    
<script lang="ts" setup>
import { ref } from 'vue';
import { ElButton, ElTable, ElTableColumn } from 'element-plus';
import axios from "axios";

// const tableData = ref([
//   {
//     id: '001',
//     name: '张三',
//     phone: '1234567890',
//     workingHours: '10:00-18:00',
//     salary: '5000',
//   },
//   {
//     id: '002',
//     name: '李四',
//     phone: '0987654321',
//     workingHours: '9:00-22:00',
//     salary: '6000',
//   },
//   {
//     id: '003',
//     name: '王五',
//     phone: '1234567890',
//     workingHours: '10:00-22:00',
//     salary: '7000',
//   },
//   {
//     id: '004',
//     name: '李四',
//     phone: '0987654321',
//     workingHours: '9:00-22:00',
//     salary: '6000',
//   },
//   {
//     id: '005',
//     name: '张三',
//     phone: '1234567890',
//     workingHours: '10:00-18:00',
//     salary: '5000',
//   },
//   {
//     id: '006',
//     name: '李四',
//     phone: '0987654321',
//     workingHours: '9:00-22:00',
//     salary: '6000',
//   },
// ])

const tableData = ref([])

const fetchData = () => {
  axios.get('/api/doctor')
    .then(response => {
      tableData.value = response.data
    })
    .catch(error => {
      console.error('Error:', error)
    })
}

const deleteRow = (index: number) => {
  tableData.value.splice(index, 1)
}

const editRow = (index: number) => {
  // 编辑操作
}

const addRow = () => {
  tableData.value.push({
    id: '',
    name: '',
    phone: '',
    workingHours: '',
    salary: '',
  })
}

</script>