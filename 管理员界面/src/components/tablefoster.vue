<template>
  <div>
    <el-button-group>
      <el-button @click="filterTag('全部')">全部</el-button>
      <el-button @click="filterTag('记录')">记录</el-button>
      <el-button @click="filterTag('申请')">申请</el-button>
    </el-button-group>
    <el-table :data="filteredData" :default-sort="{ prop: 'date', order: 'descending' }" style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);">
      <el-table-column prop="date" label="时间" sortable width="120">
      </el-table-column>
      <el-table-column prop="petId" label="宠物ID" width="170">
      </el-table-column>
      <el-table-column prop="userId" label="用户ID" width="170">
      </el-table-column>
      <el-table-column prop="days" label="天数" width="170">
      </el-table-column>
      <el-table-column label="操作" width="200">
        <template #default="scope">
          <el-button v-if="scope.row.tag === '记录'" link type="primary" size="small"
            @click.prevent="editFoster(scope.$index)">
            编辑
          </el-button>
          <el-button v-if="scope.row.tag === '记录'" link type="danger" size="small"
            @click.prevent="deleteFoster(scope.$index)">
            删除
          </el-button>
          <el-button v-if="scope.row.tag === '申请'" link type="success" size="small"
            @click.prevent="approveApplication(scope.$index)">
            同意
          </el-button>
          <el-button v-if="scope.row.tag === '申请'" link type="danger" size="small"
            @click.prevent="rejectApplication(scope.$index)">
            拒绝
          </el-button>
        </template>
      </el-table-column>
      <el-table-column prop="tag" label="tag" sortable width="100">
        <template #default="scope">
          <el-tag v-if="scope.row.tag === '记录'" type="success">{{ scope.row.tag }}</el-tag>
          <el-tag v-if="scope.row.tag === '申请'" type="warning">{{ scope.row.tag }}</el-tag>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { ElButton, ElButtonGroup, ElTable, ElTableColumn, ElTag } from 'element-plus'

interface FosterRecord {
  date: string
  petId: string
  userId: string
  days: number
  tag: string
}

const tableData = ref<FosterRecord[]>([
  {
    date: '2022-12-25',
    petId: '001',
    userId: '001',
    days: 7,
    tag: '记录',
  },
  {
    date: '2023-01-06',
    petId: '002',
    userId: '002',
    days: 14,
    tag: '申请',
  },
  {
    date: '2023-03-14',
    petId: '003',
    userId: '003',
    days: 5,
    tag: '记录',
  },
  {
    date: '2023-05-21',
    petId: '004',
    userId: '004',
    days: 10,
    tag: '申请',
  },
])

const selectedTag = ref('全部')

const filteredData = computed(() => {
  if (selectedTag.value === '全部') {
    return tableData.value
  } else {
    return tableData.value.filter((record) => record.tag === selectedTag.value)
  }
})

const filterTag = (tag: string) => {
  selectedTag.value = tag
}

const deleteFoster = (index: number) => {
  tableData.value.splice(index, 1)
}

const editFoster = (index: number) => {
  // 编辑寄养操作
}

const approveApplication = (index: number) => {
  // 同意申请操作
}

const rejectApplication = (index: number) => {
  // 拒绝申请操作
}
</script>