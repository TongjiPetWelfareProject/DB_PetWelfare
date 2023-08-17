<template>
  <el-table :data="tableData" style="width: 100%;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px;"
    max-height="500">
    <el-table-column prop="id" label="医生ID" width="120" />
    <el-table-column prop="name" label="医生姓名" width="120" />
    <el-table-column prop="phone" label="电话" width="120" />
    <el-table-column prop="workingHours" label="工作时间" width="120" />
    <el-table-column prop="salary" label="工资" width="170" />
    <el-table-column label="操作" width="360">
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
  <el-button type="primary" @click="addRow">添加医生</el-button>
</template>
    
<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { ElButton, ElTable, ElTableColumn } from 'element-plus';
import axios from 'axios';

interface Doctor {
  id: string;
  name: string;
  phone: string;
  workingHours: string;
  salary: string;
}

const tableData = ref<Doctor[]>([]);

const fetchData = async () => {
  try {
    const response = await axios.get('/api/doctor');
    tableData.value = response.data;
  } catch (error) {
    console.error('获取数据时出错：', error);
  }
};

onMounted(fetchData);

const deleteRow = (index: number) => {
  const doctorId = tableData.value[index].id;
  axios.delete(`/api/doctor/${doctorId}`) // 使用DELETE请求删除指定医生数据
    .then(() => {
      tableData.value.splice(index, 1); // 从tableData中移除已删除的医生数据
    })
    .catch(error => {
      console.error('删除数据时出错：', error);
    });
};

const editRow = (index: number) => {
  const doctor = tableData.value[index];
  const doctorId = doctor.id;
  axios.put(`/api/doctor/${doctorId}`, doctor) // 使用PUT请求更新指定医生数据
    .then(() => {
      // 更新成功的处理逻辑
    })
    .catch(error => {
      console.error('编辑数据时出错:', error);
    });
};

const addRow = () => {
  const newDoctor = {
    id: '',
    name: '',
    phone: '',
    workingHours: '',
    salary: '',
  };

  axios.post('/api/doctor', newDoctor) // 使用POST请求添加医生数据
    .then(response => {
      const newDoctorId = response.data.id;
      newDoctor.id = newDoctorId; // 将新的医生ID赋值给newDoctor对象
      tableData.value.push(newDoctor); // 将新的医生数据添加到tableData中
    })
    .catch(error => {
      console.error('添加数据时出错：', error);
    });
};

</script>