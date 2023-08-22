<template>
  <div>
    <div class="tablecontainerdonate2">
       <el-table :data="currentPageData" stripe style="width: 100%;" show-header="false">
          <el-table-column prop="time" label="捐款日期" sortable :sort-method="sortTime" />
          <el-table-column prop="name" label="用户名"   />
          <el-table-column prop="amount" label="捐款金额"  sortable/>
        </el-table>
      </div>
    <br>
    <el-pagination
      v-model:current-page="currentPage"
      v-model:page-size="pageSize"
      :small="small"
      :disabled="disabled"
      :background="background"
      layout="prev, pager, next, jumper"
      :total="tableData.length"
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
    />
  </div>
</template>

<script>
import { ref,onMounted,computed } from 'vue'
import { ElTable, ElMessageBox, ElButton } from 'element-plus'
import medical_donate from '@/api/medical_donate'

export default {
  components: {
    ElButton,
    ElTable,
  },
  setup() {
    const tableData = ref([]);
    const currentPage = ref(1);
    const pageSize = ref(10);

    const sortTime = (a, b) => {
      return new Date(a.donationTime) - new Date(b.donationTime)
    }

    onMounted(async () => {
      try {
        const response = await medical_donate.donationRecordsAPI();
        for (const donation of response) {
                tableData.value.push({
                time: donation.DONATED_DATE,
                name: donation.USER_NAME,
                amount: donation.DONATION_AMOUNT
              });
            }
        // 在获取数据后按捐款日期排序
        tableData.value.sort((a, b) => new Date(b.time) - new Date(a.time));
      } 
      catch (error) {
        console.error('获取捐助记录数据出错：', error);
      }
    }
    );

        
    const currentPageData = computed(() => {
      const startIndex = (currentPage.value - 1) * pageSize.value;
      const endIndex = startIndex + pageSize.value;
      return tableData.value.slice(startIndex, endIndex);
    });

    const handleSizeChange = newPageSize => {
      pageSize.value = newPageSize;
      currentPage.value = 1;
    };

    const handleCurrentChange = newCurrentPage => {
      currentPage.value = newCurrentPage;
    };

    return {
      tableData,
      currentPageData,
      sortTime,
      handleSizeChange,
      handleCurrentChange
    }
  },
}
</script>

<style>
.tablecontainerdonate2{
  display: flex;
  justify-content: space-between;
}
</style>