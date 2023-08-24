<template>
  <div>
    <el-table ref="tableRef" :data="tableData" style="width: 100%" @selection-change="handleSelectionChange">
      <el-table-column prop="postId" label="帖子ID" width="180"></el-table-column>
      <el-table-column prop="employeeId" label="用户ID" width="180"></el-table-column>
      <el-table-column prop="postTime" label="发帖时间" width="200" sortable :sort-method="sortTime"></el-table-column>
      <el-table-column prop="content" label="内容" width="220"></el-table-column>
      <el-table-column label="是否通过" width="180">
        <template v-slot="scope">
          <el-button size="mini" type="primary" @click="approveApplication(scope.$index)">Y</el-button>
          <el-button size="mini" type="danger" @click="rejectApplication(scope.$index)">N</el-button>
        </template>
      </el-table-column>
    </el-table>
    <!-- <el-button type="primary" @click="addEmptyRow">添加</el-button> -->
  </div>
</template>


<script>
import {  ref,onMounted} from 'vue'
import { ElTable, ElMessageBox, ElButton } from 'element-plus'
import sh_fj_jk from '@/api/sh_fj_jk'

export default {
  components: {
    ElButton,
    ElTable,
  },
  setup() {
    const tableRef = ref(null)
    const tableData = ref([
      //{ postId: "001", employeeId: '1001', postTime: '2021-10-01', content: '帖子内容1' },
      //{ postId: "002", employeeId: '1002', postTime: '2021-10-02', content: '帖子内容2' },
      //{ postId: "003", employeeId: '1003', postTime: '2021-10-03', content: '帖子内容3' },
      //{ postId: "004", employeeId: '1004', postTime: '2021-10-04', content: '帖子内容4' },
      //{ postId: "005", employeeId: '1005', postTime: '2021-10-05', content: '帖子内容5' },
    ])

    const handleSelectionChange = (selectedItems) => {
      console.log(selectedItems)
    }

    const sortTime = (a, b) => {
      return new Date(a.postTime) - new Date(b.postTime)
    }

    onMounted(async () => {
      try {
        const records = await sh_fj_jk.getCheckAPI();
        tableData.value = records;
      } catch (error) {
        console.error('获取数据时出错：', error);
      }
    });

    const approveApplication = async(index) => {
      const infoToUpdate = tableData.value[index];
      infoToUpdate.censor_status = 'aborted';

      try {
        await sh_fj_jk.updateCheckInfoAPI(infoToUpdate);
      } catch (error) {
        console.error('更新数据时出错：', error);
        infoToUpdate.censor_status = 'to be censored';
      }
    }

    const rejectApplication = async(index) => {
      const infoToUpdate = tableData.value[index];
      infoToUpdate.censor_status = 'legitimate';

      try {
        await sh_fj_jk.updateCheckInfoAPI(infoToUpdate);
      } catch (error) {
        console.error('更新数据时出错：', error);
        infoToUpdate.censor_status = 'to be censored';
      }
    }

    return {
      tableRef,
      tableData,
      handleSelectionChange,
      sortTime,
      approveApplication,
      rejectApplication,
    }
  }
}
</script>