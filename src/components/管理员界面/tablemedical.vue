<template>
  <div>
    <div style="display:flex;align-items: center;margin-bottom: 20px;">
      <span style="font-size:14px;font-weight:bold;color: rgb(123, 123, 123);">姓名 &nbsp;&nbsp;</span><el-input v-model="petNameFilter" @input="filterHandler" placeholder="搜索宠物姓名" style="margin-right:40px;width:200px;px;box-shadow: 0 0px 1px rgba(66, 66, 66, 0.2);;"></el-input>
    <el-button-group>
      <el-button @click="filterTag('全部')">全部</el-button>
      <el-button @click="filterTag('申请')">申请</el-button>
      <el-button @click="filterTag('记录')">记录</el-button>
    </el-button-group>
    </div>
    <el-table :data="filteredData" :default-sort="{ prop: 'medicalDate', order: 'descending' }"
      style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);" max-height="550">
      <el-table-column prop="petId" label="宠物ID" :width="80" align="center">
      </el-table-column>
      <el-table-column prop="petName" label="宠物名" :width="100" align="center">
      </el-table-column>
      <el-table-column prop="vetId" label="医生ID" :width="80" align="center">
      </el-table-column>
      <el-table-column prop="vetName" label="医生姓名" :width="140" align="center">
      </el-table-column>
      <el-table-column prop="reserveTime" label="预约时间" sortable :width="160" align="center">
      </el-table-column>
      <el-table-column prop="treatTime" label="看病时间" sortable :width="160" align="center">
      </el-table-column>
      <el-table-column prop="category" label="医疗内容" :width="240" align="center">
      </el-table-column>
      <el-table-column label="操作"  align="center">
        <template #default="scope">
          <!--<el-button v-if="scope.row.tag === '记录'" link type="primary" size="small"
            @click.prevent="editRow(scope.$index)">
            编辑
          </el-button>
          <el-button v-if="scope.row.tag === '记录'" link type="danger" size="small"
            @click.prevent="deleteRow(scope.$index)">
            删除
          </el-button>-->
          <el-button v-if="scope.row.tag === '申请'" plain type="success" size="small"
            @click.prevent="approveRow(scope.$index)">
            完成
          </el-button>
          <el-button v-if="scope.row.tag === '申请'" plain type="danger" size="small"
            @click.prevent="postponeRow(scope.$index)">
            延期
          </el-button>
        </template>
      </el-table-column>
    </el-table><br>
  </div>
   

        <!-- Postpone Medical Dialog -->
    <el-dialog v-model="postponeDialogVisible" title="延期医疗时间" >
      <el-form :model="postponedMedicalApplication" label-width="80px">
        <!-- 表单内容 -->
        <el-form-item label="医疗时间">
          <el-date-picker v-model="newReserveTime" type="datetime" placeholder="选择医疗时间"></el-date-picker>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="postponeDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="postponeMedicalApplication(newReserveTime)">保存</el-button>
      </div>
    </el-dialog>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { ElButton, ElButtonGroup, ElTable, ElTableColumn, ElTag,ElMessage } from 'element-plus'
import medical from '@/api/cw_yh_yl_jk'

interface MedicalRecord {
  petId: string;
  petName: string;
  vetId: string;
  vetName: string;
  reserveTime: string;
  treatTime:string;
  category: string;
  tag: string;
}

const tableData = ref<MedicalRecord[]>([]);
const editDialogVisible = ref(false);
const postponeDialogVisible = ref(false);
const newReserveTime = ref('');
const editedMedicalRecord = ref<Pet>({
  petId: '',
  vetId: '',
  reserveTime: '',
  treatTime: '',
  category: '',
});

const tableData2= ref([]);
const petNameFilter = ref('');
const petKindFilter=ref('')
function filterHandler(value){
    petNameFilter.value = value;
    tableData.value = tableData2.value.filter(item => {
        return item.petName.toLowerCase().includes(petNameFilter.value.toLowerCase());
    });
};


const postponedMedicalApplication = ref<Pet>({
  petId: '',
  vetId: '',
  reserveTime: '',
});

const getTreatList = async () => {
  try {
    const response = await medical.getTreatList();
    console.log('打印宠物列表')
    console.log(response);
    for (const data of response) {
      
      tableData.value.push({
        petId: data.PET_ID,
        petName: data.PET_NAME,
        vetId: data.VET_ID,
        vetName: data.VET_NAME,
        reserveTime: data.RESERVE_TIME,
        treatTime: data.TREAT_TIME,
        category: data.CATEGORY,
        tag: data.TAG
      });
      console.log(data.RESERVE_TIME.length)
    }
    console.log(tableData)
    tableData2.value=tableData.value;
  } catch (error) {
    console.error('获取医疗列表时出错：', error);
  }
};

onMounted(() => {
  getTreatList();
});

const selectedTag = ref('全部')

const filteredData = computed(() => {
  if (selectedTag.value === '全部') {
    return tableData.value
  } else {
    return tableData.value.filter((record) => record.tag === selectedTag.value)
  }
})

const filterTag = (tag: string) => {
  selectedTag.value = tag
}

/*const editRow = (index: number) => {
  //editedPet.value = { ...tableData.value[index] };
  editedMedicalRecord.value = {
  petId: tableData.value[index].petId,
  vetId: tableData.value[index].vetId,
  reserveTime: tableData.value[index].reserveTime,
  treatTime: tableData.value[index].treatTime,
  category: tableData.value[index].category,
};
  editDialogVisible.value = true;//和接口的连接在dialog里
};

const editMedicalRecord = async() => {
  // 编辑医疗记录操作
  try {
      const response = await medical.editMedicalRecord(editedMedicalRecord.value);//注意：需保证id不能被修改
      const editedIndex = tableData.value.findIndex(item => item.id === editedMedicalRecord.value.id);
      if (editedIndex !== -1) {
        // 关闭编辑对话框
        editDialogVisible.value = false;
        location.reload(); // 这里会刷新整个页面
      }
    } catch (error) {
      console.error('编辑数据失败：', error);
    }
}


const deleteRow = async (index: number) => {
  try {
      const response = await medical.deleteMedicalRecord(tableData.value[index].petId, tableData.value[index].vetId, tableData.value[index].reserveTime);
      location.reload(); // 这里会刷新整个页面
    } catch (error) {
      console.error('删除数据失败：', error);
    }
};*/


const approveRow = async (index: number) => {
  try {
      const response = await medical.approveMedicalApplication(tableData.value[index].petId, tableData.value[index].vetId, tableData.value[index].reserveTime);
	  ElMessage.success({
	    message: '修改成功！',
	    duration: 1000 // 持续显示时间（毫秒）
	  });
	  setTimeout(() => {
	    location.reload(); // 这里会刷新整个页面
	  }, 1000);
      
    } catch (error) {
		ElMessage.error({
		  message: '必须在预约日期后完成！',
		  duration: 1000 // 持续显示时间（毫秒）
		});
      console.error('同意（完成）数据失败：', error);
    }
};

const postponeRow = async (index: number) => {
  postponeMedicalApplication.value = {
  petId: tableData.value[index].petId,
  vetId: tableData.value[index].vetId,
  reserveTime: tableData.value[index].reserveTime,
};
  postponeDialogVisible.value = true;//和接口的连接在dialog里
};

const postponeMedicalApplication = async(newReserveTime) => {
	if (newReserveTime.getDay() == 0||newReserveTime.getDay() == 6) {
	  ElMessage.warning('预约时间必须在工作日内');
	  return; // 不继续执行
	}
  try {
      const response = await medical.postponeMedicalApplication(postponeMedicalApplication.value.petId, postponeMedicalApplication.value.vetId, postponeMedicalApplication.value.reserveTime, newReserveTime);
      location.reload(); // 这里会刷新整个页面
    } catch (error) {
		ElMessage.warning('预约时间必须在两周内');
      console.error('延期数据失败：', error);
    }
}

</script>