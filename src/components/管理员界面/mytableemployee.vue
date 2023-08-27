<template>
    <div>
        <el-table :data="tableData" style="width: 100%;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px;"
            max-height="600">
            <el-table-column prop="id" label="员工ID" width="120" />
            <el-table-column prop="name" label="员工姓名" width="120" />
            <el-table-column prop="phone" label="电话" width="120" />
            <el-table-column prop="responsibility" label="职责" width="120" />
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
        <el-button type="primary" @click="addRow">添加员工</el-button>

        <!-- Edit Employee Dialog -->
        <el-dialog v-model="editDialogVisible" title="编辑员工信息" @close="resetEditDialog">
            <el-form :model="editedEmployee" label-width="80px">
                <el-form-item label="员工ID">
                    <el-input v-model="editedEmployee.id" :readonly="true"></el-input>
                </el-form-item>
                <el-form-item label="员工姓名">
                    <el-input v-model="editedEmployee.name"></el-input>
                </el-form-item>
                <el-form-item label="电话">
                    <el-input v-model="editedEmployee.phone"></el-input>
                </el-form-item>
                <el-form-item label="职责">
                    <el-input v-model="editedEmployee.responsibility"></el-input>
                </el-form-item>
                <el-form-item label="工作时间">
                    <el-input v-model="editedEmployee.workingHours"></el-input>
                </el-form-item>
                <el-form-item label="工资">
                    <el-input v-model="editedEmployee.salary"></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="editDialogVisible = false">取消</el-button>
                <el-button type="primary" @click="submitEditedEmployee">保存</el-button>
            </div>
        </el-dialog>

        <!-- Add Employee Dialog -->
        <el-dialog v-model="addDialogVisible" title="添加员工" @close="resetAddDialog">
            <el-form :model="newEmployee" label-width="80px">

                <el-form-item label="员工姓名">
                    <el-input v-model="newEmployee.name"></el-input>
                </el-form-item>
                <el-form-item label="电话">
                    <el-input v-model="newEmployee.phone"></el-input>
                </el-form-item>
                <el-form-item label="职责">
                    <el-input v-model="newEmployee.responsibility"></el-input>
                </el-form-item>
                <el-form-item label="工作时间">
                    <el-input v-model="newEmployee.workingHours"></el-input>
                </el-form-item>
                <el-form-item label="工资">
                    <el-input v-model="newEmployee.salary"></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="addDialogVisible = false">取消</el-button>
                <el-button type="primary" @click="submitNewEmployee">添加</el-button>
            </div>
        </el-dialog>
    </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { ElButton, ElTable, ElDialog, ElForm, ElInput } from 'element-plus';
import axios from 'axios';

interface Employee {
    id: string;
    name: string;
    phone: string;
    responsibility: string;
    workingHours: string;
    salary: string;
}

const tableData = ref<Employee[]>([]);
const editDialogVisible = ref(false);
const addDialogVisible = ref(false);
const editedEmployee = ref<Employee>({
    id: '',
    name: '',
    phone: '',
    responsibility: '',
    workingHours: '',
    salary: '',
});
const newEmployee = ref<Employee>({
    id: '',
    name: '',
    phone: '',
    responsibility: '',
    workingHours: '',
    salary: '',
});

onMounted(async () => {
    await fetchData();
});

const fetchData = async () => {
    try {
        const response = await axios.get('/api/employee');
        tableData.value = response.data;
    } catch (error) {
        console.error('获取数据时出错：', error);
    }
};

const deleteRow = (index: number) => {
    const employeeId = tableData.value[index].id;
    axios.delete(`/api/delete-employee/${employeeId}`)
        .then(() => {
            tableData.value.splice(index, 1);
        })
        .catch(error => {
            console.error('删除数据时出错：', error);
        });
};

const editRow = (index: number) => {
    editedEmployee.value = { ...tableData.value[index] };
    editDialogVisible.value = true;
};

const submitEditedEmployee = () => {
    axios.put(`/api/edit-employee/${editedEmployee.value.id}`, editedEmployee.value)
        .then(() => {
            const editedIndex = tableData.value.findIndex(item => item.id === editedEmployee.value.id);
            if (editedIndex !== -1) {
                // 更新表格中对应行的数据
                tableData.value[editedIndex] = { ...editedEmployee.value };
                // 关闭编辑对话框
                editDialogVisible.value = false;
            }
        })
        .catch(error => {
            console.error('编辑数据时出错:', error);
        });
};

const resetEditDialog = () => {
    editedEmployee.value = {
        id: '',
        name: '',
        phone: '',
        responsibility: '',
        workingHours: '',
        salary: '',
    };
};

const addRow = () => {
    addDialogVisible.value = true;
};

const submitNewEmployee = () => {
    axios.post('/api/add-employee', newEmployee.value)
        .then(response => {
            const newEmployeeId = response.data.id;
            newEmployee.value.id = newEmployeeId;
            tableData.value.push(newEmployee.value);
            addDialogVisible.value = false;
        })
        .catch(error => {
            console.error('添加数据时出错：', error);
        });
};

const resetAddDialog = () => {
    newEmployee.value = {
        id: '',
        name: '',
        phone: '',
        responsibility: '',
        workingHours: '',
        salary: '',
    };
};

</script>
