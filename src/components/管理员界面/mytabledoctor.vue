<template>
  <div>
    <el-table :data="tableData" style="width: 100%;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px;"
      max-height="500">
      <el-table-column prop="id" label="医生ID" width="120" />
      <el-table-column prop="name" label="医生姓名" width="120" />
      <el-table-column prop="phone" label="电话" width="120" />
      <el-table-column prop="workingHours" label="工作时间" width="120" />
      <el-table-column prop="salary" label="工资" width="170" />
      <el-table-column label="操作" width="160">
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

    <!-- Edit Doctor Dialog -->
    <el-dialog v-model="editDialogVisible" title="编辑医生信息" @close="resetEditDialog">
      <el-form :model="editedDoctor" label-width="80px">
        <!-- 表单内容 -->
        <el-form-item label="医生ID">
          <el-input v-model="editedDoctor.id"></el-input>
        </el-form-item>
        <el-form-item label="医生姓名">
          <el-input v-model="editedDoctor.name"></el-input>
        </el-form-item>
        <el-form-item label="电话">
          <el-input v-model="editedDoctor.phone"></el-input>
        </el-form-item>
        <el-form-item label="工作时间">
          <el-input v-model="editedDoctor.workingHours"></el-input>
        </el-form-item>
        <el-form-item label="工资">
          <el-input v-model="editedDoctor.salary"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="editDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="submitEditedDoctor">保存</el-button>
      </div>
    </el-dialog>

    <!-- Add Doctor Dialog -->
    <el-dialog v-model="addDialogVisible" title="添加医生" @close="resetAddDialog">
      <el-form :model="newDoctor" label-width="80px">
        <!-- 表单内容 -->
        <el-form-item label="医生ID">
          <el-input v-model="newDoctor.id"></el-input>
        </el-form-item>
        <el-form-item label="医生姓名">
          <el-input v-model="newDoctor.name"></el-input>
        </el-form-item>
        <el-form-item label="电话">
          <el-input v-model="newDoctor.phone"></el-input>
        </el-form-item>
        <el-form-item label="工作时间">
          <el-input v-model="newDoctor.workingHours"></el-input>
        </el-form-item>
        <el-form-item label="工资">
          <el-input v-model="newDoctor.salary"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="addDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="submitNewDoctor">添加</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { ElButton, ElTable, ElDialog, ElForm, ElInput } from 'element-plus';
import axios from 'axios';

interface Doctor {
  id: string;
  name: string;
  phone: string;
  workingHours: string;
  salary: string;
}

const tableData = ref<Doctor[]>([]);
const editDialogVisible = ref(false);
const addDialogVisible = ref(false);
const editedDoctor = ref<Doctor>({
  id: '',
  name: '',
  phone: '',
  workingHours: '',
  salary: '',
});
const newDoctor = ref<Doctor>({
  id: '',
  name: '',
  phone: '',
  workingHours: '',
  salary: '',
});

onMounted(async () => {
  await fetchData();
});

const fetchData = async () => {
  try {
    const response = await axios.get('/api/doctor');
    tableData.value = response.data;
  } catch (error) {
    console.error('获取数据时出错：', error);
  }
};

const deleteRow = (index: number) => {
  const doctorId = tableData.value[index].id;
  axios.delete(`/api/delete-doctor/${doctorId}`)
    .then(() => {
      tableData.value.splice(index, 1);
    })
    .catch(error => {
      console.error('删除数据时出错：', error);
    });
};

const editRow = (index: number) => {
  editedDoctor.value = { ...tableData.value[index] };
  editDialogVisible.value = true;
};

const submitEditedDoctor = () => {
  axios.put(`/api/edit-doctor/${editedDoctor.value.id}`, editedDoctor.value)
    .then(() => {
      const editedIndex = tableData.value.findIndex(item => item.id === editedDoctor.value.id);
      if (editedIndex !== -1) {
        // 更新表格中对应行的数据
        tableData.value[editedIndex] = { ...editedDoctor.value };
        // 关闭编辑对话框
        editDialogVisible.value = false;
      }
    })
    .catch(error => {
      console.error('编辑数据时出错:', error);
    });
};

const resetEditDialog = () => {
  editedDoctor.value = {
    id: '',
    name: '',
    phone: '',
    workingHours: '',
    salary: '',
  };
};

const addRow = () => {
  addDialogVisible.value = true;
};

const submitNewDoctor = () => {
  axios.post('/api/add-doctor', newDoctor.value)
    .then(response => {
      const newDoctorId = response.data.id;
      newDoctor.value.id = newDoctorId;
      tableData.value.push(newDoctor.value);
      addDialogVisible.value = false;
    })
    .catch(error => {
      console.error('添加数据时出错：', error);
    });
};

const resetAddDialog = () => {
  newDoctor.value = {
    id: '',
    name: '',
    phone: '',
    workingHours: '',
    salary: '',
  };
};

</script>
