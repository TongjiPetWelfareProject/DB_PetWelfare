<template>
  <div>
    <el-table ref="tableRef" :data="tableData" style="width: 100%;border-radius:10px;box-shadow: 0 0px 4px rgba(66, 66, 66, 0.2);" @selection-change="handleSelectionChange">
      <el-table-column prop="id" label="宠物ID" width="200" />
      <el-table-column prop="name" label="宠物名字" width="200" />
      <el-table-column prop="views" label="阅读量" width="220" sortable :sort-method="sortViews" />
      <el-table-column prop="likes" label="点赞量" width="220" sortable :sort-method="sortLikes" />
      <el-table-column type="selection" width="155" />
      <!-- <el-table-column label="操作" width="180">
        <template v-slot="scope">
          <el-button size="mini" type="danger" @click="deleteRow(scope.row)">删除</el-button>
        </template>
      </el-table-column> -->
    </el-table>
    <br>
    <el-button type="primary" @click="publishPopular">发布</el-button>
  </div>
</template>

<script>
import { ref,onMounted} from 'vue'
import { ElTable, ElMessageBox, ElButton } from 'element-plus'
import gg_rqb_jk from '../api/gg_rqb_jk'


export default {
  components: {
    ElButton,
    ElTable,
  },
  setup() {
    const tableRef = ref(null)
    const tableData = ref([
      // { id: "001", name: '点点', views: 100, likes: 50 },
      // { id: "002", name: '米多', views: 200, likes: 80 },
      // { id: "003", name: '胡儿', views: 150, likes: 60 },
      // { id: "004", name: 'ponki', views: 120, likes: 70 },
      // { id: "005", name: 'rudy', views: 180, likes: 90 },
      // { id: "006", name: '点点', views: 100, likes: 50 },
      // { id: "007", name: '米多', views: 200, likes: 80 },
      // { id: "008", name: '胡儿', views: 150, likes: 60 },
      // { id: "009", name: 'ponki', views: 120, likes: 70 },
      // { id: "010", name: 'rudy', views: 180, likes: 90 },
    ])
    const selectedPetIds = ref([]);//存储选择的三个人气宠物
    const PopularPets = ref([]);//存储当前的人气榜宠物

    const handleSelectionChange = (selectedItems) => {
    //  console.log(selectedItems)
    }

    const sortViews = (a, b) => {
      return a.views - b.views
    }

    const sortLikes = (a, b) => {
      return a.likes - b.likes
    }

    const deleteRow = (row) => {
      ElMessageBox.confirm('确定删除该宠物吗？', '提示', {
        type: 'warning',
        confirmButtonText: '确定',
        cancelButtonText: '取消',
      }).then(() => {
        // 删除操作
        const index = tableData.value.indexOf(row)
        if (index !== -1) {
          tableData.value.splice(index, 1)
        }
      }).catch(() => {
        // 取消删除
      })
    }

    function publishPopular() {
      // 调用发送宠物ID数组到后端的API函数
      gg_rqb_jk.publishPopularAPI(selectedPetIds.value)
        .then(response => {
          console.log('发布人气榜成功', response);
          // 在这里可以根据后端返回的数据进行相应的操作
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
      deleteRow,
      publishPopular,
    }
  },
}
</script>