<template>
  <div>
    <el-table :data="tableData" :default-sort="{ prop: 'date', order: 'descending' }" style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);">
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
          <span v-else-if="scope.row.censor_status === 'abored'">审核成功</span>
          <span v-else-if="scope.row.censor_status === 'legitimate'">审核失败</span>
       </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script setup lang="ts">
import { ref, computed , onMounted } from 'vue'
import { ElButton, ElButtonGroup, ElTable, ElTableColumn, ElTag } from 'element-plus'
import { fetchFosterRecords, updateFosterRecord } from '../api/jy_ly_jk.js'; // Import the new api.js file

interface FosterRecord {
  date: string
  petId: string
  userId: string
  days: number
  censor_status: string
}

const tableData = ref<FosterRecord[]>([])

const fetchData = async () => {
  try {
    tableData.value = await fetchFosterRecords();
    // const response = await axios.get('/api/manage-foster');
    // tableData.value = response.data; // 假设API返回一个符合AdoptionRecord结构的对象数组
  } catch (error) {
    console.error('获取数据时出错：', error);
  }
};

// 组件挂载时获取数据
onMounted(fetchData);


const approveApplication = async(index: number) => {
  // 同意申请操作
  const recordToUpdate = tableData.value[index];

  // 将 censor_status 设置为 'abored'
  recordToUpdate.censor_status = 'abored';

  try {
    await updateFosterRecord(recordToUpdate);
    // const { date, petId, userId } = recordToUpdate;
    // // await axios.patch(`/api/manage-foster-update/${recordToUpdate.id}`, { censor_status: 'abored' });
    // await axios.patch(`/api/manage-foster-update`, { date, petId, userId, censor_status: 'abored' });
  } catch (error) {
    console.error('更新数据时出错：', error);
    recordToUpdate.censor_status = 'to be censored';
  }
}

const rejectApplication = async(index: number) => {
  // 拒绝申请操作
  const recordToUpdate = tableData.value[index];

  recordToUpdate.censor_status = 'legitimate';

  try {
    await updateFosterRecord(recordToUpdate);
    // const { date, petId, userId } = recordToUpdate;
    // await axios.patch(`/api/manage-foster-update`, { date, petId, userId, censor_status: 'legitimate' });
    // await axios.patch(`/api/manage-foster-update/${recordToUpdate.id}`, { censor_status: 'legitimate' });

  } catch (error) {
    console.error('更新数据时出错：', error);
    recordToUpdate.censor_status = 'to be censored';
}


</script>
