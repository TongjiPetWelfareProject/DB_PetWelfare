<template>

    <el-affix :offset="200">
      <el-button type="primary" :icon="Coin" circle @click="open" style="border-radius: 10px;float:right;box-shadow: 1px 1px 1px 1px rgba(116, 114, 114, 0.2))">点击捐款</el-button>
   </el-affix>

    <div class="demo-image__lazy">
    <el-image v-for="url in urls" :key="url" :src="url" lazy />
  </div>
  <br><br>

  <el-tabs :tab-position="tabPosition" type="border-card" style="width:90% ;border-radius: 10px;box-shadow:1px 1px 1px 1px rgba(126, 126, 126, 0.2);">
    <el-tab-pane label="捐助需知">
    <div class="describe">
      <span style="font-weight: bold;font-size: 16px;">捐助范围:</span><br>
      我们非常乐意接受爱心朋友们经常性的捐助，这将帮助我们持续提供给小猫小狗们急需的食物、医疗和庇护。<br>
      我们也非常希望爱心朋友们能够为小猫小狗们捐出您的宝贵时间，与他们互动、照顾和关爱，给予他们温暖和陪伴。<br>
      您的慷慨捐助将被用于改善小猫小狗们的生活条件和提供更好的医疗护理。<br>
      我们会认真记录每一位捐助者的善举，并定期在我们的网站上公布捐助情况。<br>
      <b>为了更好地支持我们的工作，我们目前只接受现金的捐助。</b><br>

    </div>
  <br><br>
    </el-tab-pane>
    <el-tab-pane label="捐助记录">
      <el-table :data="tableData" stripe style="width: 100%;margin-top:-80" show-header:false>
    <el-table-column prop="time"  width="280" />
    <el-table-column prop="name"  width="280" />
    <el-table-column prop="amount" width="280"/>
  </el-table>
  <el-pagination layout="prev, pager, next" :total="1000" />
    </el-tab-pane>
  </el-tabs>
  <br>
  <div style="display:flex;align-items: center;justify-content: center">
    <div style="display:block">
      <div>
      <p style="font-size: 20px;font-weight: bold;color:#729cd4;">感谢您的支持和信任，您的善举将为小猫小狗们带来希望和幸福。</p>
    </div>
    
    <div style="display:block;text-align: center;justify-content: center;">
      <p style="margin-bottom:1px;color: #656565;">盼您与我们携手，为每一只小猫小狗带来美好的未来。</p><p style="margin-top:2px;color: #656565;">您的捐款不仅是资金的支持，更是对生命的关怀，让我们一同见证奇迹的发生。</p>
    </div>
    </div>
    <img src="@/photos/donateimg.png" style="width:250px;height:250px">
  </div>



</template>

<style scoped>
.text {
  font-size: 14px;
}

.item {
  padding: 18px 0;
}

.box-card {
  width: 800px;
}

.describe {
  font-size: 14px;
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

</style>

<script setup>
import { ElMessage, ElMessageBox } from 'element-plus'
import { ref,onMounted } from 'vue'
import {Coin} from '@element-plus/icons-vue'
import medical_donate from '@/api/medical_donate'

const tabPosition = ref('left')
const urls = [
  '@/photos/home8.jpg',
  '@/photos/home2.png',
  '@/photos/home7.jpg', 
]
const userId = 123; // 当前用户ID
const donationTime = new Date().toISOString(); // 当前时间转换为字符串格式
// const tableData = [
//   {
//     time: '2023-05-03 10:23',
//     name: '白云揉碎',
//     amount: '100',
//   },
//   {
//     time: '2023-05-8 12:09',
//     name: '**',
//     amount: '300',
//   },
//   {
//     time: '2023-05-11 11:12',
//     name: 'Aimee_',
//     amount: '200',
//   },
//   {
//     time: '2023-05-11 11:12',
//     name: '湫月',
//     amount: '50',
//   },
//   {
//     time: '2023-05-03 10:23',
//     name: '白云揉碎',
//     amount: '100',
//   },
//   {
//     time: '2023-05-8 12:09',
//     name: '**',
//     amount: '300',
//   },
//   {
//     time: '2023-05-11 11:12',
//     name: 'Aimee_',
//     amount: '200',
//   },
//   {
//     time: '2023-05-11 11:12',
//     name: '湫月',
//     amount: '50',
//   },
// ]

const open = async () => {
  try {
    const { value } = await ElMessageBox.prompt('请输入捐款金额', '捐款', {
      confirmButtonText: '确定',
      cancelButtonText: '取消',
      inputPattern: /^\d+$/,
      inputErrorMessage: '请输入数字',
    });
    // 用户点击确定按钮后调用 donateAPI 函数发送捐款金额到后端
    const response = await medical_donate.donateAPI(userID,
    value,donationTime);

    ElMessage({
      type: 'success',
      message: `捐款成功，金额为：${response.amount}`,
    });
  } catch (error) {
    if (error === 'cancel') {
      ElMessage({
        type: 'info',
        message: '取消捐款',
      });
    } else {
      ElMessage({
        type: 'error',
        message: `捐款失败：${error.message}`,
      });
    }
  }
};

const tableData = ref([]);

onMounted(async () => {
  try {
    const records = await medical_donate.donationRecordsAPI();
    tableData.value = records;
  } catch (error) {
    console.error('获取捐助记录数据时出错：', error);
  }
});

</script>