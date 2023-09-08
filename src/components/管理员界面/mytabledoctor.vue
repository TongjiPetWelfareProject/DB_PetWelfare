<template>
  <div>
    <div style="display:flex;align-items: center;margin-bottom: 20px;">
        <span style="font-size:14px;font-weight:bold;color: rgb(123, 123, 123);">姓名 &nbsp;&nbsp;</span><el-input v-model="doctorNameFilter" @input="filterHandler" placeholder="搜索医生姓名" style="margin-right:40px;width:200px;px;box-shadow: 0 0px 1px rgba(66, 66, 66, 0.2);;"></el-input>
        </div>
    <el-table :data="tableData" style="width: 100%;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px;"
      max-height="550">
      <el-table-column prop="id" label="医生ID"  :width="120" align="center"/>
      <el-table-column prop="name" label="医生姓名" :width="240" align="center"/>
      <el-table-column prop="phone" label="电话" :width="240" align="center"/>
      <el-table-column prop="workingHours" label="工作时间" :width="200" align="center"/>
      <!-- <el-table-column prop="salary" label="工资"  /> -->
      <el-table-column label="操作" align="center">
        <template #default="scope">
          <el-button plain type="primary" size="small" @click.prevent="editRow(scope.$index)">
            编辑
          </el-button>
          <el-button plain type="danger" size="small" @click.prevent="deleteRow(scope.$index)">
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
          <el-input v-model="editedDoctor.id" :readonly="true"></el-input>
        </el-form-item>
        <el-form-item label="医生姓名">
          <el-input v-model="editedDoctor.name"></el-input>
        </el-form-item>
        <el-form-item label="电话">
          <el-input v-model="editedDoctor.phone" @input="handleeditPhoneInput"></el-input>
        </el-form-item>
        <el-form-item label="工作时间">
          <!-- <el-input v-model="editedDoctor.workingHours"></el-input> -->
          <div class="input-with-currency">
            <el-input-number v-model="editedDoctor.workingHours" :step="0.5" />
            <span class="currency-symbol">小时</span>
          </div>
        </el-form-item>
        <el-form-item label="工资">
          <!-- <el-input v-model="editedDoctor.salary"></el-input> -->
          <div class="input-with-currency">
            <el-input v-model="editedDoctor.salary"></el-input>
            <span class="currency-symbol">￥</span>
          </div>
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
        <el-form-item label="医生姓名">
          <el-input v-model="newDoctor.name"></el-input>
        </el-form-item>
        <el-form-item label="电话">
          <el-input v-model="newDoctor.phone" @input="handlePhoneInput"></el-input>
        </el-form-item>
        <el-form-item label="工作时间">
          <div class="input-with-currency">
            <!-- <el-input v-model="newDoctor.workingHours"></el-input> -->
            <el-input-number v-model="newDoctor.workingHours" :step="0.5" />
            <span class="currency-symbol">小时</span>
          </div>
        </el-form-item>
        <el-form-item label="工资">
          <div class="input-with-currency">
            <el-input v-model="newDoctor.salary"></el-input>
            <span class="currency-symbol">￥</span>
          </div>
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
import axiosInstance from '@/util/token-config';

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
  workingHours: '8',
  salary: '',
});


const tableData2=ref([])
const doctorNameFilter = ref('');
function filterHandler(value){
    tableData.value = tableData2.value.filter(item => {
      const doctornameMatch = item.name.toLowerCase().includes(doctorNameFilter.value.toLowerCase());
    return doctornameMatch;
  });
};

const handleeditPhoneInput = () => {
  // 获取输入框的值并移除所有非数字字符
  const digitsOnly = editedDoctor.value.phone.replace(/\D/g, "");

  // 在第4个和第9个位置插入空格
  const formattedValue = insertSpaces(digitsOnly, [3, 7]);

  editedDoctor.value.phone = formattedValue;
};

const handlePhoneInput = () => {
  // 获取输入框的值并移除所有非数字字符
  const digitsOnly = newDoctor.value.phone.replace(/\D/g, "");

  // 在第4个和第9个位置插入空格
  const formattedValue = insertSpaces(digitsOnly, [3, 7]);

  newDoctor.value.phone = formattedValue;
};

const insertSpaces = (str, positions) => {
  const result = [];
  let positionIndex = 0;

  for (let i = 0; i < str.length; i++) {
    if (positionIndex < positions.length && i === positions[positionIndex]) {
      result.push(" ");
      positionIndex++;
    }
    result.push(str[i]);
  }

  return result.join("");
};

onMounted(async () => {
  await fetchData();
});

const fetchData = async () => {
  try {
    const response = await axiosInstance.get('/api/doctor');
    tableData.value = response.data;
    tableData2.value=tableData.value;
  } catch (error) {
    console.error('获取数据时出错：', error);
  }
};

const deleteRow = (index: number) => {
  const doctorId = tableData.value[index].id;
  axiosInstance.delete(`/api/delete-doctor/${doctorId}`)
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
  axiosInstance.put(`/api/edit-doctor/${editedDoctor.value.id}`, editedDoctor.value)
    .then(() => {
      const editedIndex = tableData.value.findIndex(item => item.id === editedDoctor.value.id);
      if (editedIndex !== -1) {
        // 更新表格中对应行的数据
        tableData.value[editedIndex] = { ...editedDoctor.value };
        // 关闭编辑对话框
        editDialogVisible.value = false;
      }
      fetchData();
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
  axiosInstance.post('/api/add-doctor', newDoctor.value)
    .then(response => {
      const newDoctorId = response.data.id;
      newDoctor.value.id = newDoctorId;
      tableData.value.push(newDoctor.value);
      addDialogVisible.value = false;
      fetchData();
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
<style>
.input-with-currency {
  display: flex;
  align-items: center;
}

.currency-symbol {
  margin-left: 5px;
}
</style>