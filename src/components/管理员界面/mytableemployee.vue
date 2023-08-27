<template>
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
</template>
    
<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { ElButton, ElTable, ElTableColumn } from 'element-plus';
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

const fetchData = async () => {
    try {
        const response = await axios.get('/api/employee');
        tableData.value = response.data;
    } catch (error) {
        console.error('获取数据时出错：', error);
    }
};

onMounted(fetchData);

const deleteRow = (index: number) => {
    const employeeId = tableData.value[index].id;
    axios.delete(`/api/employee/${employeeId}`)
        .then(() => {
            tableData.value.splice(index, 1);
        })
        .catch(error => {
            console.error('删除数据时出错：', error);
        });
};

const editRow = (index: number) => {
    const employee = tableData.value[index];
    const employeeId = employee.id;
    axios.put(`/api/employee/${employeeId}`, employee)
        .then(() => {
            // Handle successful update
        })
        .catch(error => {
            console.error('编辑数据时出错：', error);
        });
};

const addRow = () => {
    const newEmployee: Employee = {
        id: '',
        name: '',
        phone: '',
        responsibility: '',
        workingHours: '',
        salary: '',
    };

    axios.post('/api/addemployee', newEmployee)
        .then(response => {
            const newEmployeeId = response.data.id;
            newEmployee.id = newEmployeeId;
            tableData.value.push(newEmployee);
        })
        .catch(error => {
            console.error('添加数据时出错：', error);
        });
};

</script>