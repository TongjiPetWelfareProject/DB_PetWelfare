<template>
      <el-table :data="tableData" style="width: 100%;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px;"
        max-height="500">
        <el-table-column label="宠物ID" prop="id" width="100"></el-table-column>
        <el-table-column label="宠物名" prop="petname" width="100"></el-table-column>
        <el-table-column label="种类" prop="breed" width="50"></el-table-column>
        <el-table-column label="年龄" prop="age" width="50"></el-table-column>
        <el-table-column label="性别" prop="sex" width="50"></el-table-column>
        <el-table-column label="体型" prop="size" width="50"></el-table-column>
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
    <el-dialog v-model="editDialogVisible" title="编辑宠物信息" >
      <el-form :model="editedPet" label-width="80px">
        <!-- 表单内容 -->
        <el-form-item label="宠物名">
          <el-input v-model="editedPet.petname"></el-input>
        </el-form-item>
        <el-form-item label="健康状况">
          <el-select v-model="editedPet.health">
            <el-option label="充满活力" value="充满活力"></el-option>
            <el-option label="健康" value="健康"></el-option>
            <el-option label="尚可" value="尚可"></el-option>
            <el-option label="不健康" value="不健康"></el-option>
            <el-option label="生病" value="生病"></el-option>
            <el-option label="危急" value="危急"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="疫苗状况">
          <el-select v-model="editedPet.vaccine">
            <el-option label="已经接种" value="已经接种"></el-option>
            <el-option label="未接种" value="未接种"></el-option>
          </el-select>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="editDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="submitEditedPet">保存</el-button>
      </div>
    </el-dialog>

    <!-- Add Doctor Dialog -->
    <el-dialog v-model="addDialogVisible" title="添加宠物信息" @close="resetAddDialog">
      <el-form :model="newPet" label-width="80px">
        <!-- 表单内容 -->
        <el-form-item label="宠物名">
          <el-input v-model="newPet.petname"></el-input>
        </el-form-item>
        <el-form-item label="种类">
          <el-select v-model="newPet.breed">
            <el-option label="猫" value="猫"></el-option>
            <el-option label="狗" value="狗"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="年龄">
          <el-input-number v-model="newPet.age" :min="0" :max="100"></el-input-number>
        </el-form-item>
        <el-form-item label="性别">
          <el-select v-model="newPet.sex">
            <el-option label="公" value="公"></el-option>
            <el-option label="母" value="母"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="体型">
          <el-select v-model="newPet.size">
            <el-option label="小型" value="小型"></el-option>
            <el-option label="中型" value="中型"></el-option>
            <el-option label="大型" value="大型"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="健康状况">
          <el-select v-model="newPet.health">
            <el-option label="充满活力" value="充满活力"></el-option>
            <el-option label="健康" value="健康"></el-option>
            <el-option label="尚可" value="尚可"></el-option>
            <el-option label="不健康" value="不健康"></el-option>
            <el-option label="生病" value="生病"></el-option>
            <el-option label="危急" value="危急"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="疫苗状况">
          <el-select v-model="newPet.vaccine">
            <el-option label="已经接种" value="已经接种"></el-option>
            <el-option label="未接种" value="未接种"></el-option>
          </el-select>
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
  size: string;
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
  health: '',
  vaccine: '',
});

const newPet = ref<Pet>({
  petname: '',
  breed: '猫',
  age: 0,
  sex: '公',
  size: '小型',
  health: '充满活力',
  vaccine: '未接种',
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
        size: adoptpet.PSIZE,
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
  //editedPet.value = { ...tableData.value[index] };
  editedPet.value = {
  id: tableData.value[index].id,
  petname: tableData.value[index].petname,
  health: tableData.value[index].health,
  vaccine: tableData.value[index].vaccine,
};
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
      const response = await petcard.editPet(editedPet.value);//注意：需保证id不能被修改
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
    petname: '',
    breed: '猫',
    age: 0,
    sex: '公',
    size: '小型',
    health: '充满活力',
    vaccine: '未接种',
  };
};

</script>