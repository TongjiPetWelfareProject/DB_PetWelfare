<template>
  <div>
    <el-table ref="tableRef" :data="tableData" style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);" @selection-change="handleSelectionChange">
      <el-table-column prop="id" label="宠物ID" width="200" align="center"/>
      <el-table-column prop="name" label="宠物名字" width="200" align="center"/>
      <el-table-column prop="views" label="阅读量" width="220" sortable :sort-method="sortViews" align="center"/>
      <el-table-column prop="likes" label="点赞量" width="220" sortable :sort-method="sortLikes" align="center"/>
      <el-table-column type="selection" width="155" />
    </el-table>
    <br>
    <el-button type="primary" @click="publishPopular">发布</el-button>
    <br>
    <div class="currenthot">
      <br>
      <el-text class="welcome">当前人气榜:</el-text>
      &nbsp;&nbsp;&nbsp;
      <br>
      <el-tag class="mx-1" size="large" type="warning">{{ topThreePetNames[0] }}</el-tag>
      <el-tag class="mx-2" size="large" type="success">{{ topThreePetNames[1] }}</el-tag>
      <el-tag class="mx-3" size="large">{{ topThreePetNames[2] }}</el-tag>
    </div>
  </div>
</template>

<script>
import { ref,onMounted,computed} from 'vue'
import { ElTable, ElMessageBox, ElButton } from 'element-plus'
import gg_rqb_jk from '@/api/gg_rqb_jk'


export default {
  components: {
    ElButton,
    ElTable,
  },
  setup() {
    const tableRef = ref(null)
    const tableData = ref([
    ])
    const selectedPetIds = ref([]);//存储选择的三个人气宠物
    const topThreePetNames = ref([]);//存储当前的人气榜宠物

    const handleSelectionChange = (selectedItems) => {
      selectedPetIds.value = selectedItems.map(pet => pet.id);
    };

    const sortViews = (a, b) => {
      return b.views - a.views
    }

    const sortLikes = (a, b) => {
      return b.likes - a.likes
    }

    const updateTopThreePetNames = () => {

      selectedPetIds.value.sort((a, b) => {
        const petA = tableData.value.find(pet => pet.id === a);
        const petB = tableData.value.find(pet => pet.id === b);
        return (petB.views + petB.likes) - (petA.views + petA.likes);
      });

      
      topThreePetNames.value = selectedPetIds.value.slice(0, 3).map(id => {
        const pet = tableData.value.find(pet => pet.id === id);
        return pet.name;
      });
    };

    function publishPopular() {

    if (selectedPetIds.value.length !== 3) {
      ElMessageBox.alert('请选择三只宠物作为人气榜宠物', '提示', {
        confirmButtonText: '确定',
      });
      return;
    }

    updateTopThreePetNames();
    // 调用发送宠物ID数组到后端的API函数
    gg_rqb_jk.publishPopularAPI(selectedPetIds.value)
      .then(response => {
        console.log('发布人气榜成功', response);
      })
      .catch(error => {
        console.error('发布人气榜失败', error);
      });
}

    onMounted(async () => {
      try {
        const records = await gg_rqb_jk.getTopPetsAPI();
        tableData.value = records;
        PopularPets.value = await getUserPopularAPI();//获取当前人气榜宠物
      } catch (error) {
        console.error('获取人气宠物时出错：', error);
      }
    });

    return {
      tableRef,
      tableData,
      handleSelectionChange,
      sortViews,
      sortLikes,
      publishPopular,
      topThreePetNames 
    }
  },
}
</script>


<style scoped>

.currenthot{
  display:flex;
  align-items: center;
  margin-top: 15px;
}
.welcome{
  margin-top: 10px;
  float: left;
  font-size: 15px;
  font-weight: bold;
  color: rgb(131, 131, 131);
}


.el-tag{
  margin-left:20px;
  /* justify-content: center;
  align-items: center;
  display: flex; */
}

/* .mx-1{
  background-color: rgb(249, 184, 184);
}
.mx-2{
  background-color: rgb(249, 220, 184);
}
.mx-3{
  background-color: rgb(249, 248, 184);
} */

</style>

