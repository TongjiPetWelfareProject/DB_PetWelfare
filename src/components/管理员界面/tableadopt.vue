<template>
  
  <div>
    <div style="display:flex;align-items: center;margin-bottom: 20px;">
      <span style="font-size:14px;font-weight:bold;color: rgb(123, 123, 123);">姓名 &nbsp;&nbsp;</span><el-input v-model="petNameFilter" @input="filterHandler" placeholder="搜索宠物姓名" style="margin-right:40px;width:200px;px;box-shadow: 0 0px 1px rgba(66, 66, 66, 0.2);;"></el-input>
    <el-button-group>
      <el-button @click="filterTag('全部')">全部</el-button>
      <el-button @click="filterTag('to be censored')">未审核</el-button>
      <el-button @click="filterTag('aborted')">审核失败</el-button>
      <el-button @click="filterTag('legitimate')">审核成功</el-button>
    </el-button-group>
  </div>
    <el-table :data="filteredData" :default-sort="{ prop: 'date', order: 'descending' }" style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);"
    max-height="550">
      <el-table-column prop="date" label="时间" :width="180" align="center" sortable>
      </el-table-column>
      <el-table-column prop="petName" label="宠物名" :width="100" align="center" >
      </el-table-column>
      <el-table-column prop="userName" label="用户名" :width="100" align="center">
      </el-table-column>
      <el-table-column prop="reason" label="理由" :width="400" align="center">
      </el-table-column>
      <el-table-column label="操作" align="center">
        <template #default="scope">
          <el-button  v-if="scope.row.censor_status === 'to be censored'" plain type="success" size="small"
            @click.prevent="approveApplication(scope.$index)">
            同意
          </el-button>
          <el-button  v-if="scope.row.censor_status === 'to be censored'" plain type="danger" size="small"
            @click.prevent="rejectApplication(scope.$index)">
            拒绝
          </el-button>
        </template>
      </el-table-column>
      <el-table-column prop="censor_status" label="审核状态" width="170">
        <template #default="scope">
         <!-- 使用条件语句来显示对应的标签 -->
         <span class="status-label to-be-censored" v-if="scope.row.censor_status === 'to be censored'">未审核</span>
        <span class="status-label aborted" v-else-if="scope.row.censor_status === 'aborted'">审核失败</span>
        <span class="status-label legitimate" v-else-if="scope.row.censor_status === 'legitimate'">审核成功</span>
        <span class="status-label invalid" v-else-if="scope.row.censor_status === 'invalid'">无效申请</span>
        <span class="status-label outdated" v-else-if="scope.row.censor_status === 'outdated'">过期申请</span>
       </template>
      </el-table-column>
    </el-table>
  </div>
</template>
<style>
.status-label {
    font-weight: bold;
    font-size:13px;
    padding:4px 10px; /* 调整内边距以控制背景样式大小 */
    border-radius: 20px; /* 添加圆角 */
    display: inline-block; /* 使元素成为行内块级元素，以适应文本大小 */
  }

  /* 未审核样式 */
  .status-label.to-be-censored {
    background-color: deepskyblue; /* 背景深，文字白色 */
    color: white;
  }

  /* 审核失败样式 */
  .status-label.aborted {
    background-color: tomato; /* 背景深，文字白色 */
    color: white;
  }

  /* 审核成功样式 */
  .status-label.legitimate {
    background-color: lightgreen; /* 背景浅，文字黑色 */
    color: black;
  }

  /* 无效申请样式 */
  .status-label.invalid {
    background-color: gray; /* 背景深，文字白色 */
    color: white;
  }

  /* 过期申请样式 */
  .status-label.outdated {
    background-color: #ffcc00; /* 自定义背景颜色，背景深，文字黑色 */
    color: black;
  }
</style>
<script setup lang="ts">
import { ref, computed ,onMounted} from 'vue'
import { ElButton, ElButtonGroup, ElTable, ElTableColumn, ElTag } from 'element-plus'
import axios from 'axios'
import { ElMessage } from 'element-plus'
import { fetchAdoptionRecords, updateAdoptionRecord } from '@/api/jy_ly_jk.js'; 
interface AdoptionRecord {
  date: string
  petId: string
  petName:string
  userId: string
  userName:string
  reason: string
  censor_status:string
}
const tableData = ref<AdoptionRecord[]>([])

const selectedTag = ref('全部')

const filteredData = computed(() => {
  if (selectedTag.value === '全部') {
    return tableData.value
  } else {
    return tableData.value.filter((record) => record.censor_status === selectedTag.value)
  }
})

const filterTag = (tag: string) => {
  selectedTag.value = tag
}
const tableData2= ref([]);
const petNameFilter = ref('');
function filterHandler(value){
    petNameFilter.value = value;
    tableData.value = tableData2.value.filter(item => {
        return item.petName.toLowerCase().includes(petNameFilter.value.toLowerCase());
    });
};
const fetchData = async () => {
  try {
    // const response = await axios.get('/api/manage-adopt');
    // tableData.value = response.data; // 假设API返回一个符合AdoptionRecord结构的对象数组
    tableData.value = await fetchAdoptionRecords();
    tableData2.value=tableData.value;
  } catch (error) {
    console.error('获取数据时出错：', error);
    ElMessage({ type: 'warning', message: error.response.data});
  }
};

// 组件挂载时获取数据
onMounted(fetchData);

const approveApplication = async (index: number) => {
  // 同意申请操作
  const recordToUpdate = filteredData.value[index];

  recordToUpdate.censor_status = 'legitimate';

  try {
    // const { date, petId, userId } = recordToUpdate;
    // await axios.patch(`/api/manage-adopt-update`, { date, petId, userId, censor_status: 'abored' });
    await updateAdoptionRecord(recordToUpdate);
    fetchData();
  } catch (error) {
    console.error('更新数据时出错：', error);
    ElMessage({ type: 'warning', message: error.response.data});
    recordToUpdate.censor_status = 'to be censored';
  }
}

const rejectApplication = async(index: number) => {
  // 拒绝申请操作
  const recordToUpdate = filteredData.value[index];

  recordToUpdate.censor_status = 'aborted';

  try {
    // const { date, petId, userId } = recordToUpdate;
    // await axios.patch(`/api/manage-adopt-update`, { date, petId, userId, censor_status: 'legitimate' });
    await updateAdoptionRecord(recordToUpdate);
    fetchData();
  } catch (error) {
    console.error('更新数据时出错：', error);
    ElMessage({ type: 'warning', message: error.response.data});
    recordToUpdate.censor_status = 'to be censored';
  }
}
</script>
