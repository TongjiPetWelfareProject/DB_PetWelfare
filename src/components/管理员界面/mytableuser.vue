<template>
  <div style="display: flex;align-items: center;margin-bottom: 20px;">
    <span style="font-size:14px;font-weight:bold;color: rgb(123, 123, 123);">姓名 &nbsp;&nbsp;</span><el-input v-model="userNameFilter" @input="filterHandler" placeholder="搜索用户姓名" style="display: flex;align-items: center;text-align: center;width:180px;box-shadow: 0 0px 1px rgba(66, 66, 66, 0.2);;"></el-input>
    <span style="font-size:14px;margin-left:25px;font-weight:bold;color: rgb(123, 123, 123);">状态 &nbsp;&nbsp;</span>
    <el-select @change="filterHandler" style="width:180px;display: flex;align-items: center;text-align: center;" v-model="userStatusFilter" clearable placeholder="选择用户状态">
      <el-option label="良好" value="良好"></el-option>
      <el-option label="审核中" value="审核中"></el-option>  
      <el-option label="已封禁" value="已封禁"></el-option>  
  </el-select>
</div>
 <el-table :data="tableData" style="width: 100%;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px;"
        max-height="560"  >
 <el-table-column label="用户ID" prop="id"  align="center"></el-table-column>
 <el-table-column label="用户名" prop="username" align="center"></el-table-column>
   <el-table-column label="电话" prop="phone"  align="center"></el-table-column>
 <el-table-column label="地址" prop="address"   align="center"></el-table-column>
      <el-table-column label="帐号状态" prop="account_status"  align="center"></el-table-column>
  <el-table-column label="操作" :width="200" align="center">
 <template #default="scope">
 <el-button v-if="scope.row.unmuted" size="small" plain type="danger" @click="muteUser(scope.$index)">禁言</el-button>
  <el-button v-else size="small" type="success" plain  @click="removeMuteUser(scope.$index)">解禁</el-button>  
 <el-button v-if="scope.row.unbanned" size="small"  plain type="danger" @click="blockUser(scope.$index)">封号</el-button>
<el-button v-else size="small" type="success"  plain @click="removeBlockUser(scope.$index)">解封</el-button>
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
  unmuted: boolean;
  unbanned: boolean;
}

const tableData2=ref([])
const userNameFilter = ref('');
const userStatusFilter = ref('');
function filterHandler(value){
    tableData.value = tableData2.value.filter(item => {
      const usernameMatch = item.username.toLowerCase().includes(userNameFilter.value.toLowerCase());
    const statusMatch = item.account_status === userStatusFilter.value || !userStatusFilter.value;
    return usernameMatch && statusMatch;
  });
};


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
    else if (data.account_status == "审核中") {
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
    tableData2.value=tableData.value;
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
              location.reload(); // 这里会刷新整个页面
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
              location.reload(); // 这里会刷新整个页面
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
              location.reload(); // 这里会刷新整个页面
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
              location.reload(); // 这里会刷新整个页面
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
  