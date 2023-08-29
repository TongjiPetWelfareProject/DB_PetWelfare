<template>

      <el-button class="fixedbuttondonate" type="primary" :icon="Coin" circle @click="open" style="border-radius: 10px;float:right;box-shadow: 1px 1px 1px 1px rgba(116, 114, 114, 0.2))">点击捐款</el-button>
    

    <div class="demo-image__lazy">
    <el-image v-for="url in urls" :key="url" :src="url" lazy />
  </div>
  <br><br>

  <el-tabs :tab-position="tabPosition" type="border-card" style="width:90% ;border-radius: 10px;box-shadow:1px 1px 1px 1px rgba(126, 126, 126, 0.2);">
    <el-tab-pane label="捐助需知">
    <div class="describe">
      <span style="font-weight: bold;font-size: 17px;">捐助范围:</span><br>
      我们非常乐意接受爱心朋友们经常性的捐助，这将帮助我们持续提供给小猫小狗们急需的食物、医疗和庇护。<br>
      我们也非常希望爱心朋友们能够为小猫小狗们捐出您的宝贵时间，与他们互动、照顾和关爱，给予他们温暖和陪伴。<br>
      您的慷慨捐助将被用于改善小猫小狗们的生活条件和提供更好的医疗护理。<br>
      我们会认真记录每一位捐助者的善举，并定期在我们的网站上公布捐助情况。<br>
      <b>为了更好地支持我们的工作，我们目前只接受现金的捐助。</b><br>

    </div>
  <br><br>
    </el-tab-pane>
    <el-tab-pane label="捐助记录">
      <div class="tablecontainerdonate">
       <el-table :data="currentPageData" stripe style="width: 100%;" show-header="false">
          <el-table-column prop="time" label="捐款日期"  />
          <el-table-column prop="name" label="用户名"  />
          <el-table-column prop="amount" label="捐款金额"  />
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
  
    </el-tab-pane>
  </el-tabs>
  <br>
  <el-divider />
  <div class="sponsor">
    <!-- <el-text class="sponsor" style="margin-left: 0.5%;color: #85a7d5;font-size: 17px;" >爱心大使</el-text> -->
    <!-- <el-tag class="mx-3" size="large">爱心大使</el-tag> -->
    <!-- <el-divider content-position="left">爱心大使</el-divider> -->
    <div class="section-title-big">
      <img src="  ../../../public/爱心.png" style="margin-right:15px;height:40px">
      <span>爱心大使</span>
      <!-- <img src="  ../../../public/crown.png" style="margin-left:20px"> -->
    </div>
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
  <br>
  <el-divider />
  <div style="display:flex;align-items: center;justify-content: center">
    <div style="display:block">
      <div>
      <p style="font-size: 20px;font-weight: bold;color:#729cd4;">感谢您的支持和信任，您的善举将为小猫小狗们带来希望和幸福。</p>
    </div>
    
    <div style="display:block;text-align: center;justify-content: center;">
      <p style="margin-bottom:1px;color: #656565;">盼您与我们携手，为每一只小猫小狗带来美好的未来。</p><p style="margin-top:2px;color: #656565;">您的捐款不仅是资金的支持，更是对生命的关怀，让我们一同见证奇迹的发生。</p>
    </div>
    </div>
    <img src="../../../public/donateimg.png" style="width:250px;height:250px">
  </div>



</template>

<style scoped>


.fixedbuttondonate{
    position: fixed;
    top: 30%; 
    right: 2vw; 
    border-radius: 10px;
    box-shadow:  0px 4px 4px rgba(116, 114, 114, 0.2);
  }

.text {
  font-size: 14px;
}

.item {
  padding: 18px 0;
}

.box-card {
  width: 800px;
}

.tablecontainerdonate{
  display: flex;
  justify-content: space-between;
}

.describe {
  font-size: 15px;
  color: #656565;
  line-height: 26px;
}

.demo-image__lazy {
  height: 350px;
  overflow-y: auto;
  width:90%;
  box-shadow: 0 4px 0px rgba(0, 0, 0, .2);
  border-radius:10px
}
.demo-image__lazy .el-image {
  display: block;
  min-height: 200px;
  margin-bottom: 10px;
}
.demo-image__lazy .el-image:last-child {
  margin-bottom: 0;
}

.section-title-big {
  font-size: 22px;
  line-height: 32px;
  font-weight: 700;
  padding-bottom: 16px;
  color: #9a89bb;
  text-align: center;
  display: flex;
  align-items: center;
  justify-content: center;
}

</style>

<script setup>
import { ElMessage, ElMessageBox } from 'element-plus'
import { ref,onMounted,computed } from 'vue'
import { useUserStore } from '@/store/user'
import {Coin} from '@element-plus/icons-vue'
import medical_donate from '@/api/medical_donate'
import { useRouter } from 'vue-router'
const userStore = useUserStore()
const router = useRouter()
const tabPosition = ref('left')
const urls = [
  '../../../public/home8.jpg',
  '../../../public/home2.png',
  '../../../public/home7.jpg', 
]

const donationTime = new Date().toISOString(); // 当前时间转换为字符串格式

const tableData = ref([]);
const currentPage = ref(1);
const pageSize = ref(10);
const sponsors = ref([]);
const types = ['danger', 'warning', 'success', 'primary', 'info'];

const open = async () => {
      if (userStore.userInfo.User_ID) {
        try {
          const { value } = await ElMessageBox.prompt(
            '请输入捐款金额',
            '捐款', {
              confirmButtonText: '确定',
              cancelButtonText: '取消',
              inputPattern: /^\d+$/,
              inputErrorMessage: '请输入数字',
            }
          );

          const transferMethod = await ElMessageBox.confirm(
            '请添加微信号: TongjiPetRescue 后转账并保存转账证明截图即可',
            '付款方式确认', {
              confirmButtonText: '已转账',
              cancelButtonText: '取消',
            }
          );

          const paymentStatus = await ElMessageBox.confirm(
            '您付款成功了吗？',
            '付款确认', {
              confirmButtonText: '成功付款',
              cancelButtonText: '取消支付',
            }
          );

          if (paymentStatus === 'confirm') {
            const response = await medical_donate.donateAPI(
              userStore.userInfo.User_ID,
              value,
              donationTime
            );

            ElMessage({
              type: 'success',
              message: `捐款成功，金额为：${value}。我们会努力为毛孩子们提供一个更加温暖舒适的家园，感谢您的爱心捐赠！`,
            });
           setTimeout(() => {
              location.reload();
            }, 3000);
          } else {
            ElMessage({
              type: 'info',
              message: '取消支付',
            });
          }
        } catch (error) {
          if (error === 'cancel') {
            ElMessage({
              type: 'info',
              message: '取消捐款',
            });
          } else {
            ElMessage({
              type: 'error',
              message: `${error.message}`,
            });
          }
        }
      } else {
        ElMessage({
          message: '请先登录',
          type: 'warning',
        })
        router.push('/login');
      }
    };


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


</script>
