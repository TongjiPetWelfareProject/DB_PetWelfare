<template>
  <div>
    <div class="tablecontainerdonate2">
       <el-table :data="currentPageData" stripe style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);" show-header="false">
          <el-table-column prop="time" label="捐款日期" sortable :sort-method="sortTime" />
          <el-table-column prop="name" label="用户名"   />
          <el-table-column prop="amount" label="捐款金额"  sortable :sort-method="sortAmount"/>
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
    <div class="sponsor">
      <el-text class="sponsor" style="margin-left: 0.5%;color: #85a7d5;font-size: 17px;" >爱心大使</el-text>
      <!-- <el-tag class="mx-3" size="large">爱心大使</el-tag> -->
      <!-- <el-divider content-position="left">爱心大使</el-divider> -->
      <div class="sponsorcards" style="display: flex;justify-content: space-between;">
        <el-card v-for="(sponsor, index) in sponsors" :key="index" class="sponsorcard" shadow="always">
          <template #header>
            <div class="donatecardsheader" style="display: flex;text-align: center;">
              <el-tag 
              :type="types[index]"
              class="mx-1"
              effect="light"
              size="small"
              round
              >{{index+1}}</el-tag>&nbsp;&nbsp;
              <div class="donatesponsorcards">
                <span class="donatesponsorcards" style="font-weight: bold;font-size: 15px;align-items: center;margin-top:-10px;color:#5d6166;">{{ sponsor.name }}</span>
              </div>
            </div>
          </template>
          <div class="donatesponsorcards" style="margin-top:-10px;color:#707377;font-size: 14px;font-weight: lighter;">捐款金额：{{ sponsor.amount }}</div>
          <!-- <div class="mypagefostertext">联系方式：{{ sponsor.phone }}</div> -->

        </el-card>
      </div>
    </div>
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
    const sponsors = ref([]);
    const types = ['danger', 'warning', 'success', 'primary', 'info'];

    const sortTime = (a, b) => {
      return new Date(a.donationTime) - new Date(b.donationTime)
    }

    const sortAmount = (a, b) => {
      return b.amount - a.amount;
    }

    function calculateTotalAmount(data) {
      const userAmountMap = new Map();

      for (const donation of data) {
        const username = donation.name;
        const amount = donation.amount;

        if (userAmountMap.has(username)) {
          userAmountMap.set(username, userAmountMap.get(username) + amount);
        } else {
          userAmountMap.set(username, amount);
        }
      }

      return userAmountMap;
    }

    onMounted(async () => {
      try {
        const response = await medical_donate.donationRecordsAPI();
        for (const donation of response) {
          const amount = parseFloat(donation.DONATION_AMOUNT);
                tableData.value.push({
                  
                time: donation.DONATED_DATE,
                name: donation.USER_NAME,
                amount: amount
              });
            }
        // 在获取数据后按捐款日期排序
        tableData.value.sort((a, b) => new Date(b.time) - new Date(a.time));

        const userAmountMap = calculateTotalAmount(tableData.value);
        const sortedUsers = [...userAmountMap.entries()].sort((a, b) => b[1] - a[1]);

        const maxSponsorsToShow = 5;
        const selectedSponsors = sortedUsers.slice(0, maxSponsorsToShow).map(([name, amount]) => ({
          name,
          amount
        }));

        sponsors.value = selectedSponsors;
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
      handleCurrentChange,
      sortAmount,
      sponsors,
      types
    }
  },
}
</script>

<style>
.sponsorcards{
  margin-top: 10px;
  display: flex;
  flex-wrap: nowrap; /* 防止换行 */
  align-items: flex-start; /* 控制卡片顶部对齐 */
}

.sponsorcard {
  width: calc(18%); /* 按比例分布，减去间距 */
  height: 100px;
  margin-bottom: 10px; /* 添加间距 */
}

.tablecontainerdonate2{
  display: flex;
  justify-content: space-between;
}

.sponsor{
  margin-top: 10px;
  font-size: 15px;
  font-weight: bold;
  color: rgb(131, 131, 131);
}
</style>