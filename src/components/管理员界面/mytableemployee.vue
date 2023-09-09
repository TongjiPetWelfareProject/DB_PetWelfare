<template>
    <div>
        <div style="display:flex;align-items: center;margin-bottom: 20px;">
        <span style="font-size:14px;font-weight:bold;color: rgb(123, 123, 123);">姓名 &nbsp;&nbsp;</span><el-input v-model="employeeNameFilter" @input="filterHandler" placeholder="搜索员工姓名" style="margin-right:40px;width:200px;px;box-shadow: 0 0px 1px rgba(66, 66, 66, 0.2);;"></el-input>
        </div>
        <el-table :data="tableData" style="width: 100%;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px;"
            max-height="550">
            <el-table-column prop="id" label="员工ID" align="center" :width="100" />
            <el-table-column prop="name" label="员工姓名"  align="center"/>
            <el-table-column prop="phone" label="电话"  align="center"/>
            <el-table-column prop="responsibility" label="职责"  :width="260" align="center"/>
            <el-table-column prop="workingHours" label="工作时间" :width="100" align="center"/>
            <!-- <el-table-column prop="salary" label="工资" width="120" /> -->
            <el-table-column label="操作" width="360" align="center">
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
                    <el-input v-model="editedEmployee.phone" @input="handleeditPhoneInput"></el-input>
                </el-form-item>
                <el-form-item label="职责">
                    <!-- <el-input v-model="editedEmployee.responsibility"></el-input> -->
                    <el-select v-model="editedEmployee.responsibility" class="m-2" placeholder="Select">
                        <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value" />
                    </el-select>
                </el-form-item>
                <el-form-item label="工作时间">
                    <!-- <el-input v-model="editedEmployee.workingHours"></el-input> -->
                    <div class="input-with-currency">
                        <el-input-number v-model="editedEmployee.workingHours" :step="0.5" />
                        <span class="currency-symbol">小时</span>
                    </div>
                </el-form-item>
                <el-form-item label="工资">
                    <!-- <el-input v-model="editedEmployee.salary"></el-input> -->
                    <div class="input-with-currency">
                        <el-input v-model="editedEmployee.salary"></el-input>
                        <span class="currency-symbol">￥</span>
                    </div>
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
                    <el-input v-model="newEmployee.phone" @input="handlePhoneInput"></el-input>
                </el-form-item>
                <el-form-item label="职责">
                    <!-- <el-input v-model="newEmployee.responsibility"></el-input> -->
                    <el-select v-model="newEmployee.responsibility" class="m-2" placeholder="Select">
                        <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value" />
                    </el-select>
                </el-form-item>
                <el-form-item label="工作时间">
                    <div class="input-with-currency">
                        <!-- <el-input v-model="newEmployee.workingHours"></el-input> -->
                        <el-input-number v-model="newEmployee.workingHours" :step="0.5" />
                        <span class="currency-symbol">小时</span>
                    </div>
                </el-form-item>
                <el-form-item label="工资">
                    <div class="input-with-currency">
                        <el-input v-model="newEmployee.salary"></el-input>
                        <span class="currency-symbol">￥</span>
                    </div>
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
import { ElButton, ElTable, ElDialog, ElForm, ElInput, ElMessage } from 'element-plus';
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
    workingHours: '8',
    salary: '',
});

const tableData2=ref([])
const employeeNameFilter = ref('');
function filterHandler(value){
    tableData.value = tableData2.value.filter(item => {
      const employeenameMatch = item.name.toLowerCase().includes(employeeNameFilter.value.toLowerCase());
    return employeenameMatch;
  });
};

const options = [
    {
        value: '领养顾问',
        label: '领养顾问',
    },
    {
        value: '兽医技术员',
        label: '兽医技术员',
    },
    {
        value: '动物行为学家',
        label: '动物行为学家',
    },
    {
        value: '志愿者协调员',
        label: '志愿者协调员',
    },
    {
        value: '寄养协调员',
        label: '寄养协调员',
    },
    {
        value: '设施经理',
        label: '设施经理',
    },
    {
        value: '筹款和外展协调员',
        label: '筹款和外展协调员',
    },
    {
        value: '救援运输员',
        label: '救援运输员',
    },
    {
        value: '动物护理专家',
        label: '动物护理专家',
    },
]

const handleeditPhoneInput = () => {
    // 获取输入框的值并移除所有非数字字符
    const digitsOnly = editedEmployee.value.phone.replace(/\D/g, "");
    // 在第4个和第9个位置插入空格
    const formattedValue = insertSpaces(digitsOnly, [3, 7]);
    editedEmployee.value.phone = formattedValue; // 更新 editedEmployee.value.phone
};

const handlePhoneInput = () => {
    // 获取输入框的值并移除所有非数字字符
    const digitsOnly = newEmployee.value.phone.replace(/\D/g, "");

    // 在第4个和第9个位置插入空格
    const formattedValue = insertSpaces(digitsOnly, [3, 7]);

    newEmployee.value.phone = formattedValue; // 更新 newEmployee.value.phone
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
        const response = await axios.get('/api/employee');
        tableData.value = response.data;
        tableData2.value=tableData.value;
    } catch (error) {
        console.error('获取数据时出错：', error);
    }
};

const deleteRow = (index: number) => {
    const employeeId = tableData.value[index].id;
    axios.delete(`/api/delete-employee/${employeeId}`)
        .then(() => {
            tableData.value.splice(index, 1);
			// 显示成功提示
			ElMessage.success({
			  message: '删除成功！',
			  duration: 3000 // 持续显示时间（毫秒）
			});
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
    const phoneNumber = editedEmployee.value.phone.replace(/\D/g, '');

    if (!editedEmployee.value.name || phoneNumber.length !== 11) {
        if (!editedEmployee.value.name) {
            ElMessage.error('员工信息不能为空');
        }
        if (phoneNumber.length !== 11) {
            ElMessage.error('电话号码必须是11位数字');
        }
        return;
    }
    axios.put(`/api/edit-employee/${editedEmployee.value.id}`, editedEmployee.value)
        .then(() => {
            const editedIndex = tableData.value.findIndex(item => item.id === editedEmployee.value.id);
            if (editedIndex !== -1) {
                // 更新表格中对应行的数据
                tableData.value[editedIndex] = { ...editedEmployee.value };
                // 关闭编辑对话框
                editDialogVisible.value = false;
				// 显示成功提示
				ElMessage.success({
				  message: '信息修改成功！',
				  duration: 3000 // 持续显示时间（毫秒）
				});
            }
            fetchData();
        })
        .catch(error => {
			// 显示成功提示
			ElMessage.error({
			  message: '手机号已被使用！',
			  duration: 3000 // 持续显示时间（毫秒）
			});
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
    const phoneNumber1 = newEmployee.value.phone.replace(/\D/g, '');

    if (!newEmployee.value.name || phoneNumber1.length !== 11) {
        if (!newEmployee.value.name) {
            ElMessage.error('员工信息不能为空');
        }
        if (phoneNumber1.length !== 11) {
            ElMessage.error('电话号码必须是11位数字');
        }
        return;
    }
    axios.post('/api/add-employee', newEmployee.value)
        .then(response => {
            const newEmployeeId = response.data.id;
            newEmployee.value.id = newEmployeeId;
            tableData.value.push(newEmployee.value);
            addDialogVisible.value = false;
            fetchData();
			// 显示成功提示
			ElMessage.success({
			  message: '添加成功！',
			  duration: 3000 // 持续显示时间（毫秒）
			});
        })
        .catch(error => {
			// 显示成功提示
			ElMessage.error({
			  message: '手机号已被使用！',
			  duration: 3000 // 持续显示时间（毫秒）
			});
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
<style>
.input-with-currency {
    display: flex;
    align-items: center;
}

.currency-symbol {
    margin-left: 5px;
}
</style>