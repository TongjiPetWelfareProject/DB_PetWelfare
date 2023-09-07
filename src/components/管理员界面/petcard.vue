<template>
  <div style="display: flex;align-items: center;margin-bottom: 20px;">
    <span style="font-size:14px;font-weight:bold;color: rgb(123, 123, 123);">姓名 &nbsp;&nbsp;</span><el-input v-model="petNameFilter" @input="filterHandler" placeholder="搜索宠物姓名" style="display: flex;align-items: center;text-align: center;width:180px;box-shadow: 0 0px 1px rgba(66, 66, 66, 0.2);;"></el-input>
    <span style="font-size:14px;margin-left:25px;font-weight:bold;color: rgb(123, 123, 123);">品种 &nbsp;&nbsp;</span>
    <el-select @change="filterHandler" style="width:180px;display: flex;align-items: center;text-align: center;" v-model="petKindFilter" clearable placeholder="选择宠物种类">
      <el-option label="猫" value="猫"></el-option>
      <el-option label="狗" value="狗"></el-option>  
  </el-select>
  <span style="font-size:14px;margin-left:25px;font-weight:bold;color: rgb(123, 123, 123);">健康状况 &nbsp;&nbsp;</span>
    <el-select @change="filterHandler" style="width:180px;display: flex;align-items: center;text-align: center;" v-model="petHealthFilter" clearable placeholder="选择健康状况">
      <el-option label="充满活力" value="充满活力"></el-option>  
      <el-option label="健康" value="健康"></el-option>
      <el-option label="不健康" value="不健康"></el-option>  
  </el-select>
  <span style="font-size:15px;margin-left:25px;font-weight:bold;color: rgb(123, 123, 123);">接种情况 &nbsp;&nbsp;</span>
    <el-select @change="filterHandler" style="width:180px;display: flex;align-items: center;text-align: center;" v-model="petVaccineFilter" clearable placeholder="选择接种情况">
      <el-option label="已接种" value="已接种"></el-option>  
      <el-option label="未接种" value="未接种"></el-option>
  </el-select>
</div>
    <el-table :data="tableData" style="width: 100%;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);border-radius: 10px;" max-height="530">
      <el-table-column label="宠物ID" prop="id"  align="center"></el-table-column>
      <el-table-column label="宠物名" prop="petname" align="center"></el-table-column>
      <el-table-column label="种类" prop="breed"  align="center"></el-table-column>
      <el-table-column label="年龄" prop="age"  align="center"></el-table-column>
      <el-table-column label="性别" prop="sex"  align="center"></el-table-column>
      <el-table-column label="体型" prop="size"  align="center"></el-table-column>
      <el-table-column label="人气" prop="popularity"  align="center"></el-table-column>
      <el-table-column label="健康状况" prop="health"  align="center"></el-table-column>
      <el-table-column label="疫苗状况" prop="vaccine"  align="center"></el-table-column>
      <el-table-column label="来源" prop="from"  align="center"></el-table-column>
      <el-table-column label="操作" width="200" align="center">
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
    <br/>
    <el-button type="primary" @click="addRow">添加宠物</el-button>

    <!-- Edit Doctor Dialog -->
    <el-dialog v-model="editDialogVisible" title="编辑宠物信息" @close="editDialogInvisible">
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
            <el-option label="已接种" value="已接种"></el-option>
            <el-option label="未接种" value="未接种"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="原图片">
          <img :src = "editedPet.avatar" style="max-width: 148px; max-height: 148px; border-radius: 5%;" alt="原图片">
        </el-form-item>
        <el-form-item label="新图片(若新图片为空，保留原图)">
          <el-upload
            :http-request="httpRequest"
            multiple
            :limit="1"
            :show-file-list="true"
            list-type="picture-card"
            :class="{hide:hideUpload}"
            :on-change="handleEditChange"
            :on-remove="handleRemove"
        ><el-icon><Plus /></el-icon>
        </el-upload>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="editDialogInvisible">取消</el-button>
        <el-button type="primary" @click="submitEditedPet">保存</el-button>
      </div>
    </el-dialog>

    <!-- Add Doctor Dialog -->
    <el-dialog v-model="addDialogVisible" title="添加宠物信息" @close="addDialogInvisible">
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
            <el-option label="已接种" value="已接种"></el-option>
            <el-option label="未接种" value="未接种"></el-option>
          </el-select>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="addDialogInvisible">取消</el-button>
        <el-button type="primary" @click="submitNewPet">添加</el-button>
      </div>
    </el-dialog>
</template>

<style>
.hide .el-upload--picture-card {
    display: none;
}
</style>

<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import { Delete, Plus } from '@element-plus/icons-vue'
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

//以下部分和传图有关
const fileList = ref([])
const limitNum = 1;
const hideUpload = ref(false);

function httpRequest(option) {
    fileList.value.push(option);
}

function handleEditChange(file,fileList){
  hideUpload.value = fileList.length >= 1;
}

function handleRemove(file,fileList){
  hideUpload.value = fileList.length >= 1;
}

const tableData = ref([]);
const tableData2= ref([]);
const editDialogVisible = ref(false);
const addDialogVisible = ref(false);
const petNameFilter = ref('');
const petKindFilter = ref('');
const petHealthFilter = ref('');
const petVaccineFilter = ref('');
function filterHandler(value){
    tableData.value = tableData2.value.filter(item => {
    const petnameMatch = item.petname.toLowerCase().includes(petNameFilter.value.toLowerCase());
    const kindMatch = item.breed === petKindFilter.value || !petKindFilter.value;
    const healthMatch=item.health===petHealthFilter.value||!petHealthFilter.value
    const vaccineMatch=item.vaccine===petVaccineFilter.value||!petVaccineFilter.value
    return petnameMatch && kindMatch&&healthMatch&&vaccineMatch;
  });
};


const editDialogInvisible = async() => {
  editDialogVisible.value = false;//和接口的连接在dialog里
  console.log("输出图片列表1");
  console.log(fileList.value);
  location.reload(); // 这里会刷新整个页面

};

const addDialogInvisible = async() => {
  addDialogVisible.value = false;//和接口的连接在dialog里
  location.reload(); // 这里会刷新整个页面
};

const editedPet = ref<Pet>({
  id: '',
  petname: '',
  health: '',
  vaccine: '',
  image:'',
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
        const uniquePets = {};
        for (const adoptpet of response) {
            if (!uniquePets[adoptpet.PET_ID]) {
                uniquePets[adoptpet.PET_ID] = true;
                console.log(adoptpet.PET_NAME);
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
                    from: adoptpet.SOURCE,
                    avatar: adoptpet.AVATAR,
                });
            }
        }
        tableData2.value=tableData.value
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

const editRow = (index) => {
  editedPet.value = {
    id: tableData.value[index].id,
    petname: tableData.value[index].petname,
    health: tableData.value[index].health,
    vaccine: tableData.value[index].vaccine,
    avatar: tableData.value[index].avatar,
  };
  editDialogVisible.value = true;//和接口的连接在dialog里
};

const deleteRow = async (index: number) => {
  try {
      await petcard.deletePet(tableData.value[index].id);
      location.reload(); // 这里会刷新整个页面
    } catch (error) {
      console.error('删除数据失败：', error);
    }
};

const submitNewPet = async() => {
  try {
      await petcard.addPet(newPet.value);//注意：需保证id不能被修改
      location.reload(); // 这里会刷新整个页面
    } catch (error) {
      console.error('添加数据失败：', error);
    }
};

const submitEditedPet = async () => {
    if (!editedPet.value.petname){
      ElMessage({
          type: 'warning',
          message: '宠物名不能为空',
        })
    }else{
    try {
        let param = new FormData();
        param.append('id', editedPet.value.id);
        param.append('petname', editedPet.value.petname);
        param.append('health', editedPet.value.health);
        param.append('vaccine', editedPet.value.vaccine);
        fileList.value.forEach((it, index) => {
            param.append('filename', it.file);
        });
        await petcard.editPet(param);
        location.reload();
    } catch (error) {
      console.error('编辑数据失败：', error);
    }
  }
};

</script>