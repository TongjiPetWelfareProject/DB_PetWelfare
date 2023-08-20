<template>
  <div>
    <el-button-group>
      <el-button @click="filterTag('全部')">全部</el-button>
      <el-button @click="filterTag('记录')">记录</el-button>
      <el-button @click="filterTag('申请')">申请</el-button>
    </el-button-group>
    <el-table :data="filteredData" :default-sort="{ prop: 'medicalDate', order: 'descending' }"
      style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);">
      <el-table-column prop="petId" label="宠物ID" width="170">
      </el-table-column>
      <el-table-column prop="doctorId" label="医生ID" width="170">
      </el-table-column>
      <el-table-column prop="medicalDate" label="医疗时间" sortable width="120">
      </el-table-column>
      <el-table-column prop="medicalContent" label="医疗内容" width="250">
      </el-table-column>
      <el-table-column label="操作" width="200">
        <template #default="scope">
          <el-button v-if="scope.row.tag === '记录'" link type="primary" size="small"
            @click.prevent="editMedicalRecord(scope.$index)">
            编辑
          </el-button>
          <el-button v-if="scope.row.tag === '记录'" link type="danger" size="small"
            @click.prevent="deleteMedicalRecord(scope.$index)">
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
    </el-table><br>
    <el-button type="primary" @click="addRow">添加记录</el-button>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { ElButton, ElButtonGroup, ElTable, ElTableColumn, ElTag } from 'element-plus'

interface MedicalRecord {
  petId: string
  doctorId: string
  medicalDate: string
  medicalContent: string
  tag: string
}

const tableData = ref<MedicalRecord[]>([
  {
    petId: '001',
    doctorId: '001',
    medicalDate: '2022-12-25',
    medicalContent: '宠物接受一次常规体检',
    tag: '记录',
  },
  {
    petId: '002',
    doctorId: '002',
    medicalDate: '2023-01-06',
    medicalContent: '宠物感冒，需要服用药物',
    tag: '申请',
  },
  {
    petId: '003',
    doctorId: '003',
    medicalDate: '2023-03-14',
    medicalContent: '宠物骨折，需要进行手术治疗',
    tag: '记录',
  },
  {
    petId: '004',
    doctorId: '004',
    medicalDate: '2023-05-21',
    medicalContent: '宠物接种疫苗',
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

const deleteMedicalRecord = (index: number) => {
  tableData.value.splice(index, 1)
}

const editMedicalRecord = (index: number) => {
  // 编辑医疗记录操作
}

const approveApplication = (index: number) => {
  // 同意申请操作
}

const rejectApplication = (index: number) => {
  // 拒绝申请操作
}
const addRow = () => {
  tableData.value.push({
    petId: '',
    doctorId: '',
    medicalDate: '',
    medicalContent: '',
    tag:'记录',
  })
}

</script>