<template>
    <el-table :data="users" style="width: 100%;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px;"
        max-height="500"  >
      <el-table-column label="用户ID" prop="id" width=100></el-table-column>
      <el-table-column label="用户名" prop="username" width=120></el-table-column>
      <el-table-column label="电话" prop="phone" width="140"></el-table-column>
      <el-table-column label="地址" prop="address" width="160"></el-table-column>
      <el-table-column label="操作" width="360">
        <template #default="scope">
          <el-button v-if="unbanned" size="mini" type="danger" @click="banUser(scope.row)">禁言</el-button>
          <el-button v-else size="mini" type="success" @click="removeBanUser(scope.row)">解禁</el-button>  
          <el-button v-if="unblocked" size="mini" type="danger" @click="blockUser(scope.row)">封号</el-button>
          <el-button v-else size="mini" type="success" @click="removeBlockUser(scope.row)">解封</el-button>
        </template>
      </el-table-column>
    </el-table>
</template>
  
<script setup>
import { ref, onMounted } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import tableUser from '@/api/cw_yh_yl_jk'
const users = ref([])

const unbanned = ref(true);
const unblocked = ref(true);
const fetchUserRecords = async () => {
try {
  const response = await tableUser.fetchUserRecords();
  for (const data of response) {
    users.value.push({
      id: data.id,
      username: data.username,
      phone: data.phone,
      address: data.address,
      //account_status: data.
    });
  }
} catch (error) {
  console.error('获取用户列表时出错：', error);
}
};

onMounted(() => {
  fetchUserRecords();
});


const banUser = (userData) => {
console.log('submit!');
unbanned.value = false;//响应式用value
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
      tableUser.banUser(userData.id)
            .then(response => {
              // 处理后端返回的响应
              ElMessage({
                type: 'success',
                message: '提交成功',
              });
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

const removeBanUser = (userData) => {
unbanned.value = true;
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
      tableUser.removeBanUser(userData.id)
            .then(response => {
              // 处理后端返回的响应
              ElMessage({
                type: 'success',
                message: '提交成功',
              });
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

const blockUser = (userData) => {
console.log('submit!');
unblocked.value = false;
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
    tableUser.blockUser(userData.id)
            .then(response => {
              // 处理后端返回的响应
              ElMessage({
                type: 'success',
                message: '提交成功',
              });
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

const removeBlockUser = (userData) => {
unblocked.value = true;
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
    tableUser.removeBlockUse(userData.id)
            .then(response => {
              // 处理后端返回的响应
              ElMessage({
                type: 'success',
                message: '提交成功',
              });
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
  