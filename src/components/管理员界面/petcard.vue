<template>
      <el-table :data="tableData" style="width: 100%;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px;"
        max-height="500">
        <el-table-column label="宠物ID" prop="id" width="100"></el-table-column>
        <el-table-column label="宠物名" prop="petname" width="100"></el-table-column>
        <el-table-column label="种类" prop="breed" width="50"></el-table-column>
        <el-table-column label="年龄" prop="age" width="50"></el-table-column>
        <el-table-column label="性别" prop="sex" width="50"></el-table-column>
        <!--<el-table-column label="体型" prop="size" width="50"></el-table-column>-->
        <el-table-column label="人气" prop="popularity" width="50"></el-table-column>
        <el-table-column label="健康状况" prop="health" width="100"></el-table-column>
        <el-table-column label="疫苗状况" prop="vaccine" width="100"></el-table-column>
        <el-table-column label="来源" prop="from" width="100"></el-table-column>
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

    <!-- Edit Doctor Dialog -->
    <el-dialog v-model="editDialogVisible" title="编辑宠物信息" @close="resetEditDialog">
      <el-form :model="editedPet" label-width="80px">
        <!-- 表单内容 -->
        <el-form-item label="宠物ID">
          <el-input v-model="editedPet.id"></el-input>
        </el-form-item>
        <el-form-item label="宠物名">
          <el-input v-model="editedPet.petname"></el-input>
        </el-form-item>
        <el-form-item label="种类">
          <el-input v-model="editedPet.breed"></el-input>
        </el-form-item>
        <el-form-item label="年龄">
          <el-input v-model="editedPet.age"></el-input>
        </el-form-item>
        <el-form-item label="性别">
          <el-input v-model="editedPet.sex"></el-input>
        </el-form-item>
        <!--<el-form-item label="体型">
          <el-input v-model="editedPet.size"></el-input>
        </el-form-item>-->
        <el-form-item label="人气">
          <el-input v-model="editedPet.popularity"></el-input>
        </el-form-item>
        <el-form-item label="健康状况">
          <el-input v-model="editedPet.health"></el-input>
        </el-form-item>
        <el-form-item label="疫苗状况">
          <el-input v-model="editedPet.vaccine"></el-input>
        </el-form-item>
        <el-form-item label="归属">
          <el-input v-model="editedPet.from"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="editDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="submitEditedPet">保存</el-button>
      </div>
    </el-dialog>

    <!-- Add Doctor Dialog -->
    <el-dialog v-model="addDialogVisible" title="添加宠物" @close="resetAddDialog">
      <el-form :model="newPet" label-width="80px">
        <!-- 表单内容 -->
        <el-form-item label="宠物ID">
          <el-input v-model="newPet.id"></el-input>
        </el-form-item>
        <el-form-item label="宠物名">
          <el-input v-model="newPet.petname"></el-input>
        </el-form-item>
        <el-form-item label="种类">
          <el-input v-model="newPet.breed"></el-input>
        </el-form-item>
        <el-form-item label="年龄">
          <el-input v-model="newPet.age"></el-input>
        </el-form-item>
        <el-form-item label="性别">
          <el-input v-model="newPet.sex"></el-input>
        </el-form-item>
        <!--<el-form-item label="体型">
          <el-input v-model="newPet.size"></el-input>
        </el-form-item>-->
        <el-form-item label="人气">
          <el-input v-model="newPet.popularity"></el-input>
        </el-form-item>
        <el-form-item label="健康状况">
          <el-input v-model="newPet.health"></el-input>
        </el-form-item>
        <el-form-item label="疫苗状况">
          <el-input v-model="newPet.vaccine"></el-input>
        </el-form-item>
        <el-form-item label="归属">
          <el-input v-model="newPet.from"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="addDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="submitNewPet">添加</el-button>
      </div>
    </el-dialog>
</template>


<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import petcard from '@/api/cw_yh_yl_jk'


interface Pet {
  id: string;
  petname: string;
  breed: string;
  age: int;
  sex: string;
  //size: string;
  popularity: string;
  health: string;
  vaccine: string;
  from: string;
}

const tableData = ref<Pet[]>([]);
const editDialogVisible = ref(false);
const addDialogVisible = ref(false);
const editedPet = ref<Pet>({
  id: '',
  petname: '',
  breed: '',
  age: 0,
  sex: '',
  //size: '',
  popularity: '',
  health: '',
  vaccine: '',
  from: '',
});

const newPet = ref<Pet>({
  id: '',
  petname: '',
  breed: '',
  age: 0,
  sex: '',
  //size: '',
  popularity: '',
  health: '',
  vaccine: '',
  from: '',
});

const getPetList = async () => {
  try {
    const response = await petcard.getPetList();
    console.log(response);
    for (const adoptpet of response) {
      console.log(adoptpet.PET_NAME)
      tableData.value.push({
        id: adoptpet.PET_ID,
        petname: adoptpet.PET_NAME,
        breed: adoptpet.SPECIES,
        age: adoptpet.AGE,
        sex: adoptpet.SEX,
        //size: adoptpet.PSIZE,
        popularity: adoptpet.POPULARITY,
        health: adoptpet.HEALTH_STATE,
        vaccine: adoptpet.VACCINE,
        from:adoptpet.SOURCE,
      });
    }
  } catch (error) {
    console.error('获取所有宠物数据时出错：', error);
  }
};

onMounted(() => {
  getPetList();
});

const addRow = () => {
  addDialogVisible.value = true;//和接口的连接在dialog里
};

const editRow = (index: number) => {
  editedPet.value = { ...tableData.value[index] };
  editDialogVisible.value = true;//和接口的连接在dialog里
};

const deleteRow = async (index: number) => {
  try {
      const response = await petcard.deletePet(tableData.value[index].id);
      tableData.value.splice(index, 1);
    } catch (error) {
      console.error('删除数据失败：', error);
    }
};

const submitNewPet = async() => {
  try {
      const response = await petcard.addPet(newPet.value);//注意：需保证id不能被修改
      const newPetId = response.data.id;
      newPet.value.id = newPetId;
      tableData.value.push(newPet.value);
      addDialogVisible.value = false;
    } catch (error) {
      console.error('添加数据失败：', error);
    }
};

const submitEditedPet = async() => {
  try {
      const response = await petcard.editPet(editedPet.value.id, editedPet.value);//注意：需保证id不能被修改
      const editedIndex = tableData.value.findIndex(item => item.id === editedPet.value.id);
      if (editedIndex !== -1) {
        // 更新表格中对应行的数据
        tableData.value[editedIndex] = { ...editedPet.value };
        // 关闭编辑对话框
        editDialogVisible.value = false;
      }
    } catch (error) {
      console.error('编辑数据失败：', error);
    }
};

const resetAddDialog = () => {//关闭对话框时重新赋值
  newPet.value = {
    id: '',
    petname: '',
    breed: '',
    age: 0,
    sex: '',
    //size: '',
    popularity: '',
    health: '',
    vaccine: '',
    from: '',
  };
};

const resetEditDialog = () => {//关闭对话框时重新赋值
  editedPet.value = {
    id: '',
    petname: '',
    breed: '',
    age: 0,
    sex: '',
    //size: '',
    popularity: '',
    health: '',
    vaccine: '',
    from: '',
  };
};

//未封装
/*const deleteRow = (index: number) => {
  const petId = tableData.value[index].id;
  axios.delete(`/api/deletePet/${petId}`)
    .then(() => {
      tableData.value.splice(index, 1);
    })
    .catch(error => {
      console.error('删除数据时出错：', error);
    });
};

const editRow = (index: number) => {
  editedPet.value = { ...tableData.value[index] };
  editDialogVisible.value = true;
};

const submitEditedPet = () => {
  axios.put(`/api/editPet/${editedPet.value.id}`, editedPet.value)
    .then(() => {
      const editedIndex = tableData.value.findIndex(item => item.id === editedPet.value.id);
      if (editedIndex !== -1) {
        // 更新表格中对应行的数据
        tableData.value[editedIndex] = { ...editedPet.value };
        // 关闭编辑对话框
        editDialogVisible.value = false;
      }
    })
    .catch(error => {
      console.error('编辑数据时出错:', error);
    });
};

const addRow = () => {
  addDialogVisible.value = true;
};

const submitNewPet = () => {
  axios.post('/api/addPet', newPet.value)
    .then(response => {
      const newPetId = response.data.id;
      newPet.value.id = newPetId;
      tableData.value.push(newPet.value);
      addDialogVisible.value = false;
    })
    .catch(error => {
      console.error('添加数据时出错：', error);
    });
};*/


//以下是自己的

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