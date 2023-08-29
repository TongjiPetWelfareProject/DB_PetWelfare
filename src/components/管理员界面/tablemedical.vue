<template>
  <div>
    <el-button-group>
      <el-button @click="filterTag('全部')">全部</el-button>
      <el-button @click="filterTag('记录')">记录</el-button>
      <el-button @click="filterTag('申请')">申请</el-button>
    </el-button-group>
    <el-table :data="filteredData" :default-sort="{ prop: 'medicalDate', order: 'descending' }"
      style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);">
      <el-table-column prop="petId" label="宠物ID" width="100">
      </el-table-column>
      <el-table-column prop="doctorId" label="医生ID" width="100">
      </el-table-column>
      <el-table-column prop="reserveTime" label="预约时间" sortable width="120">
      </el-table-column>
      <el-table-column prop="treatTime" label="看病时间" sortable width="120">
      </el-table-column>
      <el-table-column prop="category" label="医疗内容" width="250">
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
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { ElButton, ElButtonGroup, ElTable, ElTableColumn, ElTag } from 'element-plus'
import tableData from '@/api/cw_yh_yl_jk'

interface MedicalRecord {
  petId: string
  doctorId: string
  medicalDate: string
  customDate:string
  medicalContent: string
  tag: string
}
const treatments = ref([])
const getTreatList = async () => {
  try {
    const response = await tableData.getTreatList();
    
    for (const treatment of response) {
      
      treatments.value.push({
        petId: treatment.PET_ID,
        doctorId: treatment.VET_ID,
        reserveTime: treatment.RESERVE_TIME,
        treatTime: treatment.TREAT_TIME,
        category: treatment.CATEGORY,
        tag:treatment.TAG
      });
      console.log(treatment.RESERVE_TIME.length)
    }
    console.log(treatments)
  } catch (error) {
    console.error('获取医疗列表时出错：', error);
  }
};

onMounted(() => {
  getTreatList();
});

const selectedTag = ref('全部')

const filteredData = computed(() => {
  if (selectedTag.value === '全部') {
    return treatments.value
  } else {
    return treatments.value.filter((record) => record.tag === selectedTag.value)
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

</script>