<template>
      <el-table :data="pets" style="width: 100%;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px;"
        max-height="500">
        <el-table-column label="宠物ID" prop="id" width=100></el-table-column>
        <el-table-column label="宠物名" prop="petname" width=100></el-table-column>
        <el-table-column label="种类" prop="breed" width="100"></el-table-column>
        <el-table-column label="年龄" prop="age" width="50"></el-table-column>
        <el-table-column label="性别" prop="sex" width="50"></el-table-column>
        <el-table-column label="人气" prop="popularity" width="50"></el-table-column>
        <el-table-column label="健康状况" prop="health" width="100"></el-table-column>
        <el-table-column label="疫苗状况" prop="vaccine" width="100"></el-table-column>
        <el-table-column label="归属" prop="from" width="100"></el-table-column>
        <el-table-column label="操作" width="250">
          <template #default="scope">
                <el-button link type="primary" size="small" @click.prevent="editRow(scope.$index)">
                    编辑
                </el-button>
                <el-button link type="danger" size="small" @click.prevent="deleteRow(scope.$index)">
                    删除
                </el-button>
            </template>
        </el-table-column>
      </el-table>
    <br />
    <el-button type="primary" @click="addRow">添加宠物</el-button>
</template>


<script setup>
import { ref, onMounted } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import petcard from '@/api/cw_yh_yl_jk'

const pets = ref([])
const getPetList = async () => {
  try {
    const response = await petcard.getPetList();
    for (const adoptpet of response) {
      console.log(adoptpet.PET_NAME)
      pets.value.push({
        id: adoptpet.PET_ID,
        petname: adoptpet.PET_NAME,
        breed: adoptpet.SPECIES,
        sex: adoptpet.SEX,
        age: adoptpet.AGE,
        popularity: adoptpet.POPULARITY,
        health: adoptpet.HEALTH_STATE,
        vaccine: adoptpet.VACCINE,
      });
    }
  } catch (error) {
    console.error('获取所有宠物数据时出错：', error);
  }
};


onMounted(() => {
  getPetList();
});

const changePetInfo = (petData) => {
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
        petcard.changePetInfo(petData.id)
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

const deletePetInfo = (petData) => {
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
        petcard.deletePetInfo(petData.id)
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