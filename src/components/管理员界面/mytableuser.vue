<template>
    <el-table :data="tableData" style="width: 100%;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px;"
        max-height="500"  >
      <el-table-column label="用户ID" prop="id" width=100></el-table-column>
      <el-table-column label="用户名" prop="username" width=100></el-table-column>
      <el-table-column label="电话" prop="phone" width="150"></el-table-column>
      <el-table-column label="地址" prop="address" width="150"></el-table-column>
      <el-table-column label="帐号状态" prop="account_status" width="100"></el-table-column>
      <el-table-column label="操作" width="360">
        <template #default="scope">
          <el-button v-if="scope.row.unmuted" size="mini" type="danger" @click="muteUser(scope.$index)">禁言</el-button>
          <el-button v-else size="mini" type="success" @click="removeMuteUser(scope.$index)">解禁</el-button>  
          <el-button v-if="scope.row.unbanned" size="mini" type="danger" @click="blockUser(scope.$index)">封号</el-button>
          <el-button v-else size="mini" type="success" @click="removeBlockUser(scope.$index)">解封</el-button>
        </template>
      </el-table-column>
    </el-table>
</template>
  
<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import tableUser from '@/api/cw_yh_yl_jk'
interface User {
  id: string;
  username: string;
  phone: string;
  address: string;
  account_status: string;
  unmuted: bool;
  unbanned: bool;
}
const tableData = ref<User[]>([]);
const fetchUserRecords = async () => {
try {
  const response = await tableUser.fetchUserRecords();
  console.log(response);
  for (const data of response) {
    let unmuted = false;
    let unbanned = false;
    if (data.account_status == "良好") {
      unmuted = true;
      unbanned = true;
    }
    else if (data.account_status == "禁言") {
      unbanned = true;
    }
    tableData.value.push({
      id: data.id,
      username: data.username,
      phone: data.phone,
      address: data.address,
      account_status: data.account_status,
      unmuted: unmuted,
      unbanned: unbanned,
    });
    console.log(tableData.value);
  }
} catch (error) {
  console.error('获取用户列表时出错：', error);
}
};

onMounted(() => {
  fetchUserRecords();
});


const muteUser = (index: number) => {
console.log('submit!');
console.log(tableData.value[index].id);
console.log(typeof tableData.value[index].id);
ElMessageBox.confirm(
    '确定要禁言该用户吗？',
    '确认',
    {
      confirmButtonText: 'OK',
      cancelButtonText: 'Cancel',
      type: 'warning',
    }
  )
  .then(() => {
      console.log("success");
      tableUser.muteUser(tableData.value[index].id)
            .then(response => {
              // 处理后端返回的响应
              ElMessage({
                type: 'success',
                message: '提交成功',
              });
              tableData.value[index].unmuted = false;
            })
            .catch(error => {
              // 处理错误
              ElMessage({
                type: 'error',
                message: '提交失败',
              });
            });
    })
    .catch(() => {
      ElMessage({
        type: 'info',
        message: '取消提交',
      })
    })
}

const removeMuteUser = (index: number) => {
console.log('submit!');
ElMessageBox.confirm(
    '确定要解禁该用户吗？',
    '确认',
    {
      confirmButtonText: 'OK',
      cancelButtonText: 'Cancel',
      type: 'warning',
    }
  )
  .then(() => {
      console.log("success");
      tableUser.removeMuteUser(tableData.value[index].id)
            .then(response => {
              // 处理后端返回的响应
              ElMessage({
                type: 'success',
                message: '提交成功',
              });
              tableData.value[index].unmuted = true;
            })
            .catch(error => {
              // 处理错误
              ElMessage({
                type: 'error',
                message: '提交失败',
              });
            });
    })
    .catch(() => {
      ElMessage({
        type: 'info',
        message: '取消提交',
      })
    })
}

const blockUser = (index: number) => {
console.log('submit!');
ElMessageBox.confirm(
    '确定要封号该用户吗？',
    '确认',
    {
      confirmButtonText: 'OK',
      cancelButtonText: 'Cancel',
      type: 'warning',
    }
  )
  .then(() => {
    tableUser.blockUser(tableData.value[index].id)
            .then(response => {
              // 处理后端返回的响应
              ElMessage({
                type: 'success',
                message: '提交成功',
              });
              tableData.value[index].unbanned = false;
            })
            .catch(error => {
              // 处理错误
              ElMessage({
                type: 'error',
                message: '提交失败',
              });
            });
    })
    .catch(() => {
      ElMessage({
        type: 'info',
        message: '取消提交',
      })
    })
}

const removeBlockUser = (index: number) => {
console.log('submit!');
ElMessageBox.confirm(
    '确定要解封该用户吗？',
    '确认',
    {
      confirmButtonText: 'OK',
      cancelButtonText: 'Cancel',
      type: 'warning',
    }
  )
  .then(() => {
    tableUser.removeBlockUser(tableData.value[index].id)
            .then(response => {
              // 处理后端返回的响应
              ElMessage({
                type: 'success',
                message: '提交成功',
              });
              tableData.value[index].unbanned = true;
              tableData.value[index].unmuted = true;//取消封号之后连禁言一起取消
            })
            .catch(error => {
              // 处理错误
              ElMessage({
                type: 'error',
                message: '提交失败',
              });
            });
    })
    .catch(() => {
      ElMessage({
        type: 'info',
        message: '取消提交',
      })
    })
}

</script>
  