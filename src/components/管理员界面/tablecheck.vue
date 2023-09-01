<template>
  <div>
    <el-table ref="tableRef" :data="tableData" style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);">
      <el-table-column prop="postId" label="帖子ID" width="80"></el-table-column>
      <el-table-column prop="employeeId" label="用户ID" width="120"></el-table-column>
      <el-table-column prop="postTime" label="发帖时间" width="200" sortable :sort-method="sortTime"></el-table-column>
      <el-table-column label="内容" width="500" align="center">
        <template v-slot="{ row }">
          <div class="announcement-cell">
            <el-button type="text" @click="showContent(row)">
              {{ getShortenedContent(row.content) }}
            </el-button>
          </div>
        </template>
      </el-table-column>
      <el-table-column label="是否通过" width="120">
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
import {  ref,onMounted } from 'vue'
import { ElTable, ElMessageBox, ElButton } from 'element-plus'
import sh_fj_jk from '@/api/sh_fj_jk'

export default {
  components: {
    ElButton,
    ElTable,
  },
  setup() {
    const tableRef = ref(null)
    const tableData = ref([])

    const sortTime = (a, b) => {
      return new Date(a.postTime) - new Date(b.postTime)
    }

    function showContent(row) {
      ElMessageBox.alert(row.content, '内容', {
        confirmButtonText: '关闭',
      });
    }

    function getShortenedContent(content) {
      const lines = content.split('\n');
      const maxLines = 1;
      
      if (lines.length > maxLines) {
        return lines.slice(0, maxLines).join('\n');
      }
      
      return content;
    }


    onMounted(async () => {
      try {
        const records = await sh_fj_jk.getCheckAPI();
        tableData.value = records;
      } catch (error) {
        console.error('获取数据时出错：', error);
      }
    })

    const approveApplication = async(index) => {
      const infoToUpdate = tableData.value[index];
      infoToUpdate.censor_status = 'aborted';

      try {
        await sh_fj_jk.updateCheckInfoAPI(infoToUpdate);
        refreshTableData();
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
        refreshTableData();
      } catch (error) {
        console.error('更新数据时出错：', error);
        infoToUpdate.censor_status = 'to be censored';
      }
    }

    const refreshTableData = async () => {
      // 在这里获取新的表格数据
      try {
        const records = await sh_fj_jk.getCheckAPI();
        tableData.value = records;
      } catch (error) {
        console.error('获取数据时出错：', error);
      }
    }

    return {
      tableRef,
      tableData,
      handleSelectionChange,
      sortTime,
      approveApplication,
      rejectApplication,
      refreshTableData
    }
  }
}
</script>
