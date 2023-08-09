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
    <el-button type="primary" @click="addEmptyRow">发布</el-button>
  </div>
</template>

<script>
import { ref } from 'vue'
import { ElTable, ElMessageBox, ElButton } from 'element-plus'

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

    const addEmptyRow = () => {
      const emptyRow = {
        id: '',
        name: '',
        views: '',
        likes: '',
      }
      tableData.value.push(emptyRow)
    }

    return {
      tableRef,
      tableData,
      handleSelectionChange,
      sortViews,
      sortLikes,
      deleteRow,
      addEmptyRow,
    }
  },
}
</script>
