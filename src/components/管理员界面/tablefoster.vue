<template>
  <div>
    <el-table :data="tableData" :default-sort="{ prop: 'date', order: 'descending' }" style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);">
      <el-table-column prop="date" label="时间" sortable width="120">
      </el-table-column>
      <el-table-column prop="petName" label="宠物名" width="170">
      </el-table-column>
      <el-table-column prop="userName" label="用户名" width="170">
      </el-table-column>
       <el-table-column prop="date" label="起始日期" width="170">
      </el-table-column>
      <el-table-column prop="days" label="天数" width="170">
      </el-table-column>
      <el-table-column label="操作" width="200">
        <template #default="scope">
          <el-button  link type="success" size="small"
            @click.prevent="approveApplication(scope.$index)">
            同意
          </el-button>
          <el-button  link type="danger" size="small"
            @click.prevent="rejectApplication(scope.$index)">
            拒绝
          </el-button>
        </template>
      </el-table-column>
      <el-table-column prop="censor_status" label="审核状态" width="170">
        <template #default="scope">
         <!-- 使用条件语句来显示对应的标签 -->
          <span v-if="scope.row.censor_status === 'to be censored'">未审核</span>
          <span v-else-if="scope.row.censor_status === 'aborted'">审核成功</span>
          <span v-else-if="scope.row.censor_status === 'legitimate'">审核失败</span>
       </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script setup lang="ts">
import { ref, computed , onMounted } from 'vue'
import { ElButton, ElButtonGroup, ElTable, ElTableColumn, ElTag } from 'element-plus'
import axios from 'axios'
import { fetchFosterRecords, updateFosterRecord } from '@/api/jy_ly_jk.js'

interface FosterRecord {
  date: string
  petName:string
  userName:string
  petId: string
  userId: string
  days: number
  censor_status: string
}

const tableData = ref<FosterRecord[]>([])

const fetchData = async () => {
  try {
    tableData.value = await fetchFosterRecords();
  } catch (error) {
    console.error('获取数据时出错：', error);
  }
};

// 组件挂载时获取数据
onMounted(fetchData);


const approveApplication = async(index: number) => {
  // 同意申请操作
  const recordToUpdate = tableData.value[index];

  recordToUpdate.censor_status = 'legitimate';
  fetchData();
  try {
    await updateFosterRecord(recordToUpdate);
  } catch (error) {
    console.error('更新数据时出错：', error);
    recordToUpdate.censor_status = 'to be censored';
  }
}

const rejectApplication = async(index: number) => {
  // 拒绝申请操作
  const recordToUpdate = tableData.value[index];

  recordToUpdate.censor_status = 'aborted';
  fetchData();
  try {
    await updateFosterRecord(recordToUpdate);
  } catch (error) {
    console.error('更新数据时出错：', error);
    recordToUpdate.censor_status = 'to be censored';
}
}

</script>
