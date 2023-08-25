<template>
      <el-table :data="tableData" height="600" style="width: 84vw;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px; header-cell-style:align= 'center';"  >
        <el-table-column label="用户ID" prop="id" width=100></el-table-column>
        <el-table-column label="用户名" prop="username" width=120></el-table-column>
        <el-table-column label="电话" prop="phone" width="140"></el-table-column>
        <el-table-column label="地址" prop="address" width="160"></el-table-column>
        <el-table-column label="操作" width="360">
          <template #default="scope">
            <el-button size="mini" type="danger" @click="banUser(scope.row)">禁言</el-button>
            <el-button size="mini" type="danger" @click="blockUser(scope.row)">封号</el-button>
          </template>
        </el-table-column>
      </el-table>
</template>
    
<script setup>
import { ref, onMounted } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import mytableuser from '@/api/yh_jk'
const tableData = ref([])

const fetchUserRecords = async () => {
  try {
    const response = await mytableuser.fetchUserRecords();
    for (const data of response) {
      tableData.value.push({
        id: data.id,
        name: data.username,
        phone: data.phone,
        address: data.address,
      });
    }
  } catch (error) {
    console.error('获取用户列表时出错：', error);
  }
};

onMounted(() => {
    fetchUserRecords();
});

const userData = {
    id: '1333'
}

const banUser = (userData) => {
  console.log('submit!');
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
        petadopt.banUser(userData.id)
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
        petadopt.blockUser(userData.id)
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
    
    /*const handleBan = (row) => {
      console.log('Ban user:', row);
    };
    
    const handleBlock = (row) => {
      console.log('Block user:', row);
    };*/
</script>
    